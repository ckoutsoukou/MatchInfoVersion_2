using MatchInfo.WebApi.DbContexts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq.Expressions;
using System.Security.Principal;

namespace MatchInfo.WebApi.RepositoriesAbstractions
{
    /// <summary>
    /// An interface for BaseRepository
    /// </summary>
    /// <typeparam name="TSource">The T type</typeparam>
    public interface IBaseRepository<TSource> where TSource : class
    {
        IQueryable<TSource> GetQueryable(Expression<Func<TSource, bool>> expression, bool asNoTracking = false, string? includeProperties = null);
        List<TSource> GetList(Expression<Func<TSource, bool>>? expression, bool asNoTracking = false, string? includeProperties = null);

        /// <summary>
        /// Get all items
        /// </summary>
        /// <param name="asNoTracking">Optionally request that records are not tracked.</param>
        /// <returns>A list of items</returns>
        List<TSource> GetAll(bool asNoTracking = false, string? includeProperties = null);

        TSource? GetById(int id, bool asNoTracking = false, string? includeProperties = null);

        /// <summary>
        /// Insert a new item
        /// </summary>
        /// <param name="dbItem">The member airport</param>
        /// <param name="applyChanges">Apply save changes or not.</param>
        /// <returns>The number of affect records</returns>
        int Insert(TSource dbItem, bool applyChanges = true);

        void InsertRange(IEnumerable<TSource> dbItems, bool applyChanges = true);

        /// <summary>
        /// Updates an existing item
        /// </summary>
        /// <param name="dbItem">The instance to delete</param>
        /// <param name="applyChanges">Apply save changes or not.</param>
        /// <returns>The number of affect records</returns>
        int Update(TSource dbItem, bool applyChanges = true);

        /// <summary>
        /// Delete an existing item
        /// </summary>
        /// <param name="dbItem">The instance to delete</param>
        /// <param name="applyChanges">Apply save changes or not.</param>
        /// <returns>The number of affect records</returns>
        int Delete(TSource dbItem, bool applyChanges = true);

        int DeleteRange(IEnumerable<TSource> dbItems, bool applyChanges = true);

    }
}
