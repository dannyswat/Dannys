using Dannys.Framework.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dannys.Framework.EFCore
{
    public abstract class KeyEntityCRUDRepository<TDbContext, TEntity, TKey, TUserKey> : EntityCRUDRepository<TDbContext, TEntity, TKey, TUserKey>, IUniqueKeyRepository<TEntity, TKey, TUserKey>
       where TDbContext : DbContext
        where TEntity : class, IEntity<TKey, TUserKey>, IKey
        where TKey : struct, IEquatable<TKey>
        where TUserKey : struct, IEquatable<TUserKey>
    {
        public KeyEntityCRUDRepository(TDbContext dbContext, IUser<TUserKey> user) : base(dbContext, user) { }

        public TKey GetID(string key)
        {
            return dbSet.Where(e => e.Key == key).Select(e => e.ID).FirstOrDefault();
        }
    }
}
