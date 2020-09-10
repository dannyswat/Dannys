using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dannys.Framework.EFCore
{
    /// <summary>
    /// Need further test for usefulness
    /// </summary>
	public interface IDbContext
	{
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        EntityEntry Entry(object entity);
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        DatabaseFacade Database { get; }
        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();
    }
}
