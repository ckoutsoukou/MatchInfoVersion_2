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
    /// <summary>
    /// A class for BaseRepository
    /// </summary>
    /// <typeparam name="Match">The T type</typeparam>
    public class MatchService: BaseService, IMatchService
    {
        internal IMatchOddService matchOddService;


        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="uoW">EF Context</param>
        /// <param name="mapper">The automapper to use for db-dto conversions</param>
        public MatchService( IRepositoryManager repositoryManager, IMapper mapper, IMatchOddService matchOddService) : base(repositoryManager: repositoryManager, mapper: mapper)
        {
            //if (repository is null) throw new ArgumentNullException(nameof(repository));
            this.matchOddService = matchOddService;

        }

        public List<MatchDto> GetList(Expression<Func<MatchDto, bool>>? expression, bool asNoTracking = false, string? includeProperties = null)
        {
            Expression<Func<Match, bool>>? predicate = null;
            if (expression is not null)
                predicate = mapper.Map<Expression<Func<Match, bool>>>(expression);
            var dbItems = this.repositoryManager.MatchesRepository.GetList(expression: predicate, asNoTracking: asNoTracking, includeProperties: includeProperties);    
            var dtoItems = mapper.Map<List<Match>, List<MatchDto>>(dbItems);

            return dtoItems;
        }
        public List<MatchDto> GetAll(bool asNoTracking = false, string? includeProperties = null)
        {
            return GetList(expression:null, asNoTracking: asNoTracking, includeProperties: includeProperties).ToList();
        }


        public MatchDto? GetById(int id, bool asNoTracking = false, string? includeProperties = null)
        {
            var dbItem = this.repositoryManager.MatchesRepository.GetById(id: id, asNoTracking:asNoTracking, includeProperties: includeProperties);
            if (dbItem == null)
                throw new KeyNotFoundException($"Match with id {id} does not exist");
            return mapper.Map<Match, MatchDto>(dbItem);
        }

        /// <summary>
        /// Insert a new Member Airport in a Route Group
        /// </summary>
        /// <param name="model">The member airport</param>
        /// <param name="applyChanges">Apply save changes or not.</param>
        /// <returns>The number of affect records</returns>
        /// <exception cref="ArgumentNullException">dbItem was null</exception>
        /// <exception cref="DbUpdateException">Database threw an exception</exception>
        public MatchDto Insert(MatchDto model)
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model));

            if (model.TeamA == model.TeamB)
                throw new ArgumentException("Team A should have different value from Team B.");

            /* Map the dto on a new db istance to copy all essential data */
            Entities.Match dbItem = mapper.Map<Models.MatchDto, Entities.Match>(model);

            /* Insert match with match odds */
            dbItem.MatchOdds = mapper.Map<List<Models.MatchOddDto>, List<Entities.MatchOdd>>(model.MatchOddDtos);
            this.repositoryManager.MatchesRepository.Insert(dbItem);

            return mapper.Map<Entities.Match, Models.MatchDto>(dbItem);
        }

        /// <summary>
        /// Updates a match
        /// </summary>
        /// <param name="model">The instance to delete</param>
        /// <param name="applyChanges">Apply save changes or not.</param>
        /// <returns>The number of affect records</returns>
        /// <exception cref="DbUpdateConcurrencyException">The record has been modified by another user</exception>
        /// <exception cref="DbUpdateException">Database threw an exception</exception>
        public MatchDto Update(MatchDto model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (model.TeamA == model.TeamB)
                throw new ArgumentException("Team A should have different value from Team B.");

            List<Models.MatchOddDto> insertedMatchOddDtos;
            List<Models.MatchOddDto> updatedMatchOddDtos;
            List<Entities.MatchOdd> dbRemovedMatchOddItems;

            Entities.Match dbItem = this.repositoryManager.MatchesRepository.GetById(id: model.Id, includeProperties: "MatchOdds");

            if (dbItem == null)
                throw new KeyNotFoundException(string.Format(cErrorNotFound, model.Id.ToString()));

            /* 1.Get inserted and removed shifts */
            insertedMatchOddDtos = model.MatchOddDtos.Where(x => x.Id == 0).ToList();
            dbRemovedMatchOddItems = dbItem.MatchOdds.Where(p => !model.MatchOddDtos.Any(p2 => p2.Id == p.Id)).ToList();
            updatedMatchOddDtos = model.MatchOddDtos.Where(p => dbItem.MatchOdds.Any(p2 => p2.Id == p.Id)).ToList();

            /* Delete match odds */
            if (dbRemovedMatchOddItems.Any())
                this.repositoryManager.MatchOddsRepository.DeleteRange(dbRemovedMatchOddItems);

            /* In case inserted match odds exist */
            if (insertedMatchOddDtos.Any())
            {
                List<WebApi.Entities.MatchOdd> dbInsertedMatchOddItems = mapper.Map<List<Models.MatchOddDto>, List<Entities.MatchOdd>>(insertedMatchOddDtos);
                this.repositoryManager.MatchOddsRepository.InsertRange(dbInsertedMatchOddItems);
            }

            /* Update match odds */
            if (updatedMatchOddDtos.Any())
            {
                foreach (var updatedMatchOddDto in updatedMatchOddDtos)
                {
                    this.matchOddService.Update(model: updatedMatchOddDto, applyChanges: false);
                }
            }
            /* Update match */
            mapper.Map(model, dbItem);
            this.repositoryManager.MatchesRepository.Update(dbItem: dbItem);

            return this.GetById(model.Id, asNoTracking: true, includeProperties:"MatchOdds");
        }


        /// <summary>
        /// Delete a member airport of a Route Group
        /// </summary>
        /// <param name="dbItem">The instance to delete</param>
        /// <param name="applyChanges">Apply save changes or not.</param>
        /// <returns>The number of affect records</returns>
        public void Delete(int id)
        {
            
        }

    }
}
