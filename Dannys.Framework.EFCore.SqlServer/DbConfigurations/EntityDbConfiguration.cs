using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dannys.Framework.EFCore
{
    public abstract class EntityDbConfiguration<TEntity, TKey, TUserKey> : IEntityTypeConfiguration<TEntity>
        where TKey : struct, IEquatable<TKey>
        where TUserKey : struct, IEquatable<TUserKey>
        where TEntity : class, IEntity<TKey, TUserKey>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(e => e.ID).UseIdentityColumn();
            builder.Property(e => e.RowVersion).IsRowVersion();
        }
    }
}
