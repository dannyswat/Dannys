using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Linq.Expressions;

namespace Dannys.Framework.EFCore.Converters
{
	public class BirthdayConverter : ValueConverter<Birthday, int>
	{
		static Lazy<BirthdayConverter> lazy = new Lazy<BirthdayConverter>(() => new BirthdayConverter());
		public BirthdayConverter(ConverterMappingHints mappingHints = default) : base(ToIntRepresentation, FromIntRepresentation, mappingHints) { }

		public static BirthdayConverter Default => lazy.Value;

		static Expression<Func<Birthday, int>> ToIntRepresentation = birthday => birthday.GetIntegerRepresentation();

		static Expression<Func<int, Birthday>> FromIntRepresentation = repr => Birthday.FromIntegerRepresentation(repr);
	}
}
