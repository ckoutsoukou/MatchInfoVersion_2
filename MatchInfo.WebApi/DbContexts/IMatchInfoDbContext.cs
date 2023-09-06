using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Xml;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MatchInfo.WebApi.DbContexts
{
    /// <summary>
    /// An interface for dbContext
    /// </summary>
    public interface IMatchInfoDbContext : IDisposable
    {
        EntityEntry Entry(object entity);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
    }
}
