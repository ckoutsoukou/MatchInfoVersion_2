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
    
    public class MatchOddService: BaseService,IMatchOddService
    {

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="uoW">EF Context</param>
        /// <param name="mapper">The automapper to use for db-dto conversions</param>
        public MatchOddService( IRepositoryManager repositoryManager, IMapper mapper) : base(repositoryManager, mapper)
        {
        }
     
        public List<MatchOddDto> GetList(Expression<Func<MatchOddDto, bool>>? expression, bool asNoTracking = false, string? includeProperties = null)
        {
            Expression<Func<MatchOdd, bool>>? predicate = null;
            if (expression is not null)
                predicate = mapper.Map<Expression<Func<MatchOdd, bool>>>(expression);
            var dbItems = this.repositoryManager.MatchOddsRepository.GetList(expression: predicate, asNoTracking: asNoTracking, includeProperties: includeProperties);    
            var dtoItems = mapper.Map<List<MatchOdd>, List<MatchOddDto>>(dbItems);

            return dtoItems;
        }
        public List<MatchOddDto> GetAll(bool asNoTracking = false, string? includeProperties = null)
        {
            return GetList(expression:null, asNoTracking: asNoTracking, includeProperties: includeProperties).ToList();
        }


        public MatchOddDto? GetById(int id, bool asNoTracking = false, string? includeProperties = null)
        {
            var dbItem = this.repositoryManager.MatchOddsRepository.GetById(id: id, asNoTracking:asNoTracking, includeProperties: includeProperties);
            return mapper.Map<MatchOdd, MatchOddDto>(dbItem);
        }

        
        public MatchOddDto Insert(MatchOddDto model)
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model));

            this.repositoryManager.MatchOddsRepository.Insert(dbItem: dbItem, applyChanges: applyChanges);
            return new MatchOddDto() { Specifier = "1" };

        }

        /// <summary>
        /// Updates a match
        /// </summary>
        /// <param name="model">The instance to delete</param>
        /// <param name="applyChanges">Apply save changes or not.</param>
        /// <returns>The number of affect records</returns>
        /// <exception cref="DbUpdateConcurrencyException">The record has been modified by another user</exception>
        /// <exception cref="DbUpdateException">Database threw an exception</exception>
        public MatchOddDto Update(MatchOddDto model, bool applyChanges = true)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var dbItem = this.repositoryManager.MatchOddsRepository.GetById(model.Id);

            //verify db record exists (cannot update non existing record)
            if (dbItem == null)
                throw new KeyNotFoundException(string.Format(cErrorNotFound, model.Id.ToString()));

            //write over the dbItem values from the dto
            mapper.Map(model, dbItem);

            this.repositoryManager.MatchOddsRepository.Update(dbItem: dbItem, applyChanges: applyChanges);

            return mapper.Map<Entities.MatchOdd, Models.MatchOddDto>(dbItem);
        }


        
        public void Delete(int id)
        {
            
        }

    }
}
