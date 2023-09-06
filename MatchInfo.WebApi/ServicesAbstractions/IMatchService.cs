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
    public interface IMatchService
    {
        List<MatchDto> GetList(Expression<Func<MatchDto, bool>>? expression, bool asNoTracking = false, string? includeProperties = null);

        List<MatchDto> GetAll(bool asNoTracking = false, string? includeProperties = null);

        MatchDto? GetById(int id, bool asNoTracking = false, string? includeProperties = null);

        MatchDto Insert(MatchDto model);

        MatchDto Update(MatchDto model);

        void Delete(int id);

    }
}
