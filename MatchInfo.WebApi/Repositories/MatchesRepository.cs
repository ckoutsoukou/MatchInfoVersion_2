using MatchInfo.WebApi.DbContexts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using MatchInfo.WebApi.Entities;
using System.Linq.Expressions;
using MatchInfo.WebApi.RepositoriesAbstractions;

namespace MatchInfo.WebApi.Repositories
{
    /// <summary>
    /// A class for MatchesRepository.
    /// </summary>
    public class MatchesRepository : BaseRepository<Entities.Match>, IMatchesRepository
    {
        /// <summary>
        /// Ctor for MatchesRepository.
        /// </summary>
        /// <param name="context">EF Context.</param>
        /// <param name="mapper">The automapper to use for db-dto conversions.</param>
        public MatchesRepository(MatchInfoDbContext context) : base(context: context)
        {
        }



    }
}
