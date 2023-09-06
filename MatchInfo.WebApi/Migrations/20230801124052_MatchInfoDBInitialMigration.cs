using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MatchInfo.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class MatchInfoDBInitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MatchDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TeamA = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    TeamB = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Sport = table.Column<byte>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.CheckConstraint("CK_Matches_Sport", "[Sport] = 1 OR [Sport] = 2");
                });

            migrationBuilder.CreateTable(
                name: "MatchOdds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MatchId = table.Column<int>(type: "INTEGER", nullable: false),
                    Specifier = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Odd = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchOdds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchOdds_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Matches",
                columns: new[] { "Id", "Description", "MatchDateTime", "Sport", "TeamA", "TeamB" },
                values: new object[,]
                {
                    { 1, "OSFP-PAO", new DateTime(2023, 7, 30, 13, 0, 0, 0, DateTimeKind.Unspecified), (byte)1, "OSFP", "PAO" },
                    { 2, "AEK-PAO", new DateTime(2023, 6, 29, 13, 0, 0, 0, DateTimeKind.Unspecified), (byte)2, "AEK", "PAO" }
                });

            migrationBuilder.InsertData(
                table: "MatchOdds",
                columns: new[] { "Id", "MatchId", "Odd", "Specifier" },
                values: new object[,]
                {
                    { 1, 1, 1.5, "X" },
                    { 2, 2, 2.2999999999999998, "1" },
                    { 3, 2, 3.1000000000000001, "2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_MatchDateTime_TeamA_TeamB_Sport",
                table: "Matches",
                columns: new[] { "MatchDateTime", "TeamA", "TeamB", "Sport" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MatchOdds_MatchId_Specifier",
                table: "MatchOdds",
                columns: new[] { "MatchId", "Specifier" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchOdds");

            migrationBuilder.DropTable(
                name: "Matches");
        }
    }
}
