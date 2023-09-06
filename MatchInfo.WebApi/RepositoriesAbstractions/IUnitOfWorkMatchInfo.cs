using AutoMapper;
using MatchInfo.WebApi.DbContexts;
using MatchInfo.WebApi.Entities;
using MatchInfo.WebApi.Repositories;
using Microsoft.Extensions.Logging;
using System;

namespace MatchInfo.WebApi.RepositoriesAbstractions
{
    /// <summary>
    /// A unitofwork interface for match info database.
    /// </summary>
    public interface IUnitOfWorkMatchInfo //: IDisposable
    {

        /// <summary>
        /// Saves all changes of a context.
        /// </summary>
        public void SaveChanges();

    }
}
