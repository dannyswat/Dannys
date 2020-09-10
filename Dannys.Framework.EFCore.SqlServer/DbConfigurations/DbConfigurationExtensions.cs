using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dannys.Framework.EFCore
{
    public static class DbConfigurationExtensions
    {
        public static void ConfigureKey<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class, IKey
        {
            builder.Property(e => e.Key)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(30);
            builder.HasIndex(e => e.Key).IsUnique();
        }

        public static void ConfigureName<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class, IName
        {
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.HasIndex(e => e.Name);
        }
    }
}
