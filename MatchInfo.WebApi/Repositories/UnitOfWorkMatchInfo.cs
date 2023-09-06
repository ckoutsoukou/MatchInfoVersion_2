using AutoMapper;
using MatchInfo.WebApi.DbContexts;
using MatchInfo.WebApi.Entities;
using MatchInfo.WebApi.RepositoriesAbstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Text.RegularExpressions;
using Match = MatchInfo.WebApi.Entities.Match;

namespace MatchInfo.WebApi.Repositories
{
    /// <summary>
    /// A unitofwork class for match info database.
    /// </summary>
    public class UnitOfWorkMatchInfo : IUnitOfWorkMatchInfo
    {
        /// <summary>
        /// The dbContext for MatchInfo database.
        /// </summary>
        private readonly MatchInfoDbContext _matchInfoContext;


        /// <summary>
        /// Indicates if matchInfoContext is disposed.
        /// </summary>
        //private bool disposedValue = false; // To detect redundant calls


        /// <summary>
        /// Auto Mapper.
        /// </summary>

       // public ILogger Logger { get { return _logger; } }

        /// <summary>
        /// Ctor for UnitOfWorkMatchInfo.
        /// </summary>
        /// <param name="context">the context.</param>
        /// <param name="mapper">The automapper to use for db-dto conversions.</param>
        public UnitOfWorkMatchInfo(MatchInfoDbContext context) => _matchInfoContext = context;

        /// <summary>
        /// Saves all changes of a context.
        /// </summary>
        public void SaveChanges()
        {
            _matchInfoContext.SaveChanges();
        }

        ///// <summary>
        ///// Disposes the matchInfoContext.
        ///// </summary>
        ///// <param name="disposing">The disposing value.</param>
        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!disposedValue)
        //    {
        //        if (disposing)
        //        {
        //            _matchInfoContext.Dispose();
        //        }

        //        disposedValue = true;
        //    }
        //}

        ///// <summary>
        ///// Disposes the matchInfoContext.
        ///// </summary>
        //public void Dispose()
        //{
        //    Dispose(true);
        //}

    }
}
