using MatchInfo.WebApi.DbContexts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using MatchInfo.WebApi.RepositoriesAbstractions;

namespace MatchInfo.WebApi.Repositories
{
    /// <summary>
    /// A class for MatchOddsRepository
    /// </summary>.
    public class MatchOddsRepository : BaseRepository<Entities.MatchOdd>, IMatchOddsRepository
    {
        /// <summary>
        /// Ctor for MatchOddsRepository.
        /// </summary>
        /// <param name="context">EF Context.</param>
        /// <param name="mapper">The automapper to use for db-dto conversions.</param>
        public MatchOddsRepository(MatchInfoDbContext context) : base(context: context)
        {
        }
    }
}
