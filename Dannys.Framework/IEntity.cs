using System;

namespace Dannys.Framework
{
    public interface IEntity<TKey, TUserKey> 
        where TKey : struct, IEquatable<TKey>
        where TUserKey : struct, IEquatable<TUserKey>
    {
        TKey ID { get; set; }

        DateTimeOffset CreateDate { get; set; }

        DateTimeOffset LastUpdateDate { get; set; }

        TUserKey CreatedBy { get; set; }

        TUserKey LastUpdatedBy { get; set; }

        byte[] RowVersion { get; set; }
    }
}
