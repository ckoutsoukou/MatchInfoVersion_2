using MatchInfo.WebApi.DbContexts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq.Expressions;
using MatchInfo.WebApi.Entities;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using MatchInfo.WebApi.RepositoriesAbstractions;

namespace MatchInfo.WebApi.Repositories
{
    /// <summary>
    /// A class for BaseRepository
    /// </summary>
    /// <typeparam name="T">The T type</typeparam>
    public class BaseRepository<T>:IBaseRepository<T> where T : class, IEntityBase
    {
        private readonly MatchInfoDbContext _dbContext;

        protected const string cErrorNotFound = "Record with id {0} has been deleted by another user or an incorrect id has been specified";

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="context">EF Context</param>
        /// <param name="mapper">The automapper to use for db-dto conversions</param>
        public BaseRepository(MatchInfoDbContext context)
        {
            if (context is null) throw new ArgumentNullException(nameof(context));
            this._dbContext = context;
        }



        public IQueryable<T> GetQueryable(Expression<Func<T, bool>>? expression, bool asNoTracking = false, string? includeProperties =  null)
        {
            var query = (IQueryable<T>)_dbContext.Set<T>();
            if (asNoTracking)
                query = query.AsNoTracking();

            if (expression != null)
                query = query.Where(expression);

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProperty);
            }

            return query;
        }

        public List<T> GetList(Expression<Func<T, bool>>? expression, bool asNoTracking = false, string? includeProperties = null)
        {
            return GetQueryable(expression: expression, asNoTracking: asNoTracking, includeProperties:includeProperties).ToList();
        }

        public virtual List<T> GetAll(bool asNoTracking = false, string? includeProperties = null)
        {
            return GetList(expression:null, asNoTracking: asNoTracking, includeProperties: includeProperties).ToList();
        }


        public virtual T? GetById(int id, bool asNoTracking = false, string? includeProperties = null)
        {
            Expression<Func<T, bool>> expression = x => x.Id == id;
            return  GetQueryable(expression: expression, asNoTracking: asNoTracking, includeProperties: includeProperties).FirstOrDefault();
        }

        /// <summary>
        /// Insert a new Member Airport in a Route Group
        /// </summary>
        /// <param name="dbItem">The member airport</param>
        /// <param name="applyChanges">Apply save changes or not.</param>
        /// <returns>The number of affect records</returns>
        /// <exception cref="ArgumentNullException">dbItem was null</exception>
        /// <exception cref="DbUpdateException">Database threw an exception</exception>
        public virtual int Insert(T dbItem, bool applyChanges = true)
        {
            if (dbItem is null)
                throw new ArgumentNullException(nameof(dbItem));

            this._dbContext.Set<T>().Add(dbItem);
            int affected = 0;

            if (applyChanges)
            {
                affected = _dbContext.SaveChanges();
                if (affected > 0)
                {
                    var entry = _dbContext.Entry(dbItem);
                                    }
            }
            return affected;
        }

        public virtual void InsertRange(IEnumerable<T> dbItems, bool applyChanges = true)
            {
                if (dbItems is null)
                    throw new ArgumentNullException(nameof(dbItems));


                this._dbContext.Set<T>().AddRange(dbItems);
                int affected = 0;

                if (applyChanges)
                {
                    affected = _dbContext.SaveChanges();
                }
            }


        /// <summary>
        /// Update a member airport of a Route Group
        /// </summary>
        /// <param name="dbItem">The instance to delete</param>
        /// <param name="applyChanges">Apply save changes or not.</param>
        /// <returns>The number of affect records</returns>
        /// <exception cref="DbUpdateConcurrencyException">The record has been modified by another user</exception>
        /// <exception cref="DbUpdateException">Database threw an exception</exception>
        public virtual int Update(T dbItem, bool applyChanges = true)
        {
            if (dbItem is null) return 0;

            var entry = _dbContext.Entry(dbItem);
            int affected = 0;

            //proceed with update, only if any change has been detected on the actual data
            if (entry.State != EntityState.Unchanged)
            {
                if (applyChanges)
                    affected = _dbContext.SaveChanges();
            }

            return affected;
        }


        /// <summary>
        /// Delete a member airport of a Route Group
        /// </summary>
        /// <param name="dbItem">The instance to delete</param>
        /// <param name="applyChanges">Apply save changes or not.</param>
        /// <returns>The number of affect records</returns>
        public virtual int Delete(T dbItem, bool applyChanges = true)
        {
            if (dbItem is null) throw new ArgumentNullException(nameof(dbItem));

            int affected = 0;

            this._dbContext.Set<T>().Remove(dbItem);

            if (applyChanges)
                affected = _dbContext.SaveChanges();

            return affected;
        }

        public virtual int DeleteRange(IEnumerable<T> dbItems, bool applyChanges = true)
        {
            if (dbItems is null) throw new ArgumentNullException(nameof(dbItems));

            int affected = 0;

            this._dbContext.Set<T>().RemoveRange(dbItems);

            if (applyChanges)
                affected = _dbContext.SaveChanges();

            return affected;
        }
    }
}
