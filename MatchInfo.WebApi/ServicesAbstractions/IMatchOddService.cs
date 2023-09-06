using MatchInfo.WebApi.DbContexts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq.Expressions;
using System.Security.Principal;
using MatchInfo.WebApi.Models;

namespace MatchInfo.WebApi.ServicesAbstractions
{
    /// <summary>
    /// An interface for BaseRepository
    /// </summary>
    /// <typeparam name="TSource">The T type</typeparam>
    public interface IMatchOddService
    {
        List<MatchOddDto> GetList(Expression<Func<MatchOddDto, bool>>? expression, bool asNoTracking = false, string? includeProperties = null);

        List<MatchOddDto> GetAll(bool asNoTracking = false, string? includeProperties = null);

        MatchOddDto? GetById(int id, bool asNoTracking = false, string? includeProperties = null);

        MatchOddDto Insert(MatchOddDto model);
        MatchOddDto Update(MatchOddDto model, bool applyChanges = true);
        void Delete(int id);

    }
}
