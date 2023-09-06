using MatchInfo.WebApi.DbContexts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq.Expressions;
using MatchInfo.WebApi.Entities;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using MatchInfo.WebApi.ServicesAbstractions;
using MatchInfo.WebApi.RepositoriesAbstractions;
using MatchInfo.WebApi.Models;
using MatchInfo.WebApi.Repositories;

namespace MatchInfo.WebApi.Services
{
    
    public class BaseService
    {
        protected const string cErrorNotFound = "Record with id {0} has been deleted by another user or an incorrect id has been specified";

        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
      
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="uoW">EF Context</param>
        /// <param name="mapper">The automapper to use for db-dto conversions</param>
        public BaseService( IRepositoryManager repositoryManager, IMapper mapper)
        {
            //if (repository is null) throw new ArgumentNullException(nameof(repository));
            this._repositoryManager = repositoryManager;
            this._mapper = mapper;
        }

        /// <summary>
        /// The automapper to use for db-dto conversions
        /// </summary>
        protected IMapper mapper => _mapper;

        protected IRepositoryManager repositoryManager => _repositoryManager;
    }
}
