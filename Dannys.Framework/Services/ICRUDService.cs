using Dannys.Framework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dannys.Framework.Services
{
    public interface ICRUDService<TEntity, TKey, TUserKey>
        where TEntity : class, IEntity<TKey, TUserKey>
        where TKey : struct, IEquatable<TKey>
        where TUserKey : struct, IEquatable<TUserKey>
    {
        OperationResult<TEntity> Create(TEntity entity);

        OperationResult Update(TEntity entity);

        OperationResult Delete(TKey id);

        TEntity Get(TKey id);

        IEnumerable<TEntity> Search(SearchOptions<TEntity> options);

        int SearchCount(SearchCountOptions<TEntity> options);
    }
}
