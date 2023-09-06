using MatchInfo.WebApi.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Xml;

namespace MatchInfo.WebApi.DbContexts
{       
    /// <summary>
    /// A class for dbContext
    /// </summary>
    public class MatchInfoDbContext : DbContext
    {
        /// <summary>
        /// Ctor for MatchInfoContext.
        /// </summary>
        /// <param name="options">The db context options.</param>
        public MatchInfoDbContext(DbContextOptions<MatchInfoDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        /// <summary>
        /// Override OnModelCreating method.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Match>().ToTable("Matches");
            modelBuilder.Entity<MatchOdd>().ToTable("MatchOdds");

            modelBuilder.Entity<Match>().ToTable(t => t.HasCheckConstraint("CK_Matches_Sport", "[Sport] = 1 OR [Sport] = 2"));

            modelBuilder.Entity<Match>().HasData(
                   new Match()
                   {
                       Id = 1,
                       Description = "OSFP-PAO",
                       MatchDateTime = new DateTime(2023,7,30,13,0,0),
                       TeamA = "OSFP",
                       TeamB = "PAO",
                       Sport = 1
                   },
                   new Match()
                   {
                       Id = 2,
                       Description = "AEK-PAO",
                       MatchDateTime = new DateTime(2023, 6, 29, 13, 0, 0),
                       TeamA = "AEK",
                       TeamB = "PAO",
                       Sport = 2
                   });

            modelBuilder.Entity<MatchOdd>().HasData(
                new MatchOdd()
                {
                    Id = 1,
                    MatchId = 1,
                    Specifier = "X",
                    Odd = 1.5
                },
                new MatchOdd()
                {
                    Id = 2,
                    MatchId = 2,
                    Specifier = "1",
                    Odd = 2.3
                },
                new MatchOdd()
                    {
                        Id = 3,
                        MatchId = 2,
                        Specifier = "2",
                        Odd = 3.1
                    });
        }

    }
}
