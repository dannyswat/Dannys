using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dannys.Framework.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Dannys.Framework.EFCore
{
    public abstract class EntityCRUDRepository<TDbContext, TEntity, TKey, TUserKey> : ICRUDRepository<TEntity, TKey, TUserKey>
        where TDbContext : DbContext
        where TEntity : class, IEntity<TKey, TUserKey>
        where TKey : struct, IEquatable<TKey>
        where TUserKey : struct, IEquatable<TUserKey>
    {
        protected TDbContext dbContext;
        protected IUser<TUserKey> user;
        protected DbSet<TEntity> dbSet;

        public EntityCRUDRepository(TDbContext dbContext, IUser<TUserKey> user)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<TEntity>();
            this.user = user;
        }

        public virtual TEntity Create(TEntity entity)
        {
            entity.CreateDate = entity.LastUpdateDate = DateTimeOffset.Now;
            entity.CreatedBy = entity.LastUpdatedBy = user.UserID;

            dbSet.Add(entity);
            return entity;
        }

        public virtual void Delete(TKey id)
        {
            var entity = Get(id);
            if (entity != null)
                dbSet.Remove(entity);
        }

        public virtual TEntity Get(TKey id)
        {
            return dbSet.Find(id);
        }

        public virtual int SaveChanges()
        {
            return dbContext.SaveChanges();
        }

        public virtual IEnumerable<TEntity> Search(SearchOptions<TEntity> options)
        {
            var qry = dbSet.Where(options.FilterExpression);
            return qry.Skip(options.StartOffset).Take(options.PageSize).ToArray();
        }

        public virtual int SearchCount(SearchCountOptions<TEntity> options)
        {
            var qry = dbSet.Where(options.FilterExpression);
            return qry.Count();
        }

        public virtual void Update(TEntity entity)
        {
            entity.LastUpdateDate = DateTimeOffset.Now;
            entity.LastUpdatedBy = user.UserID;
            dbSet.Update(entity);
        }
    }
}
