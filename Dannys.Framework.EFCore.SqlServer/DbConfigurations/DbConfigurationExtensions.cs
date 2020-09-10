using Microsoft.EntityFrameworkCore;
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

        public static void ConfigureName<TEntity>(this EntityTypeBuilder<TEntity> builder, int maxLength = 100) where TEntity : class, IName
        {
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(maxLength);
            builder.HasIndex(e => e.Name);
        }

        public static PropertyBuilder ConfigureBirthday(this PropertyBuilder builder)
        {
            return builder.HasColumnType("int")
                .HasConversion(Converters.BirthdayConverter.Default);
        }
    }
}
