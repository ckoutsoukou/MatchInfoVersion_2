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

namespace MatchInfo.WebApi.RepositoriesAbstractions
{
    /// <summary>
    /// An interface for MatchesRepository.
    /// </summary>
    public interface IMatchesRepository : IBaseRepository<Match>
    {

    }
}

