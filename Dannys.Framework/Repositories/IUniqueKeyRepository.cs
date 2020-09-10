using System;
using System.Collections.Generic;
using System.Text;

namespace Dannys.Framework.Repositories
{
    public interface IUniqueKeyRepository<TEntity, TKey, TUserKey> 
        where TEntity : class, IEntity<TKey, TUserKey>
        where TKey : struct, IEquatable<TKey>
        where TUserKey : struct, IEquatable<TUserKey>
    {
        TKey GetID(string key);
    }
}
