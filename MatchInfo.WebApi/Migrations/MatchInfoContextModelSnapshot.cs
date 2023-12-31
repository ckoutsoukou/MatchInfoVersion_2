﻿// <auto-generated />
using System;
using MatchInfo.WebApi.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MatchInfo.WebApi.Migrations
{
    [DbContext(typeof(MatchInfoDbContext))]
    partial class MatchInfoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.9");

            modelBuilder.Entity("MatchInfo.API.Entities.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("MatchDateTime")
                        .HasColumnType("TEXT");

                    b.Property<byte>("Sport")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TeamA")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<string>("TeamB")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MatchDateTime", "TeamA", "TeamB", "Sport")
                        .IsUnique();

                    b.ToTable("Matches", t =>
                        {
                            t.HasCheckConstraint("CK_Matches_Sport", "[Sport] = 1 OR [Sport] = 2");
                        });

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "OSFP-PAO",
                            MatchDateTime = new DateTime(2023, 7, 30, 13, 0, 0, 0, DateTimeKind.Unspecified),
                            Sport = (byte)1,
                            TeamA = "OSFP",
                            TeamB = "PAO"
                        },
                        new
                        {
                            Id = 2,
                            Description = "AEK-PAO",
                            MatchDateTime = new DateTime(2023, 6, 29, 13, 0, 0, 0, DateTimeKind.Unspecified),
                            Sport = (byte)2,
                            TeamA = "AEK",
                            TeamB = "PAO"
                        });
                });

            modelBuilder.Entity("MatchInfo.API.Entities.MatchOdd", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("MatchId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Odd")
                        .HasColumnType("REAL");

                    b.Property<string>("Specifier")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MatchId", "Specifier")
                        .IsUnique();

                    b.ToTable("MatchOdds");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            MatchId = 1,
                            Odd = 1.5,
                            Specifier = "X"
                        },
                        new
                        {
                            Id = 2,
                            MatchId = 2,
                            Odd = 2.2999999999999998,
                            Specifier = "1"
                        },
                        new
                        {
                            Id = 3,
                            MatchId = 2,
                            Odd = 3.1000000000000001,
                            Specifier = "2"
                        });
                });

            modelBuilder.Entity("MatchInfo.API.Entities.MatchOdd", b =>
                {
                    b.HasOne("MatchInfo.API.Entities.Match", "Match")
                        .WithMany("MatchOdds")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Match");
                });

            modelBuilder.Entity("MatchInfo.API.Entities.Match", b =>
                {
                    b.Navigation("MatchOdds");
                });
#pragma warning restore 612, 618
        }
    }
}
