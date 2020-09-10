using System;
using System.Collections.Generic;
using System.Text;

namespace Dannys.Framework.Repositories
{
    public interface ICRUDRepository<TEntity, TKey, TUserKey> : IRepository
        where TKey : struct, IEquatable<TKey>
        where TUserKey : struct, IEquatable<TUserKey>
        where TEntity : class
    {
        TEntity Create(TEntity entity);

        void Update(TEntity entity);

        void Delete(TKey id);

        TEntity Get(TKey id);

        IEnumerable<TEntity> Search(SearchOptions<TEntity> options);

        int SearchCount(SearchCountOptions<TEntity> options);
    }
}