﻿using System;
using System.Collections;
using System.Linq;

namespace TsRanodmizer.Extensions
{
	public static class EnumerableExtensions
	{
		static readonly Type EnumerableType = typeof(Enumerable);

		public static IEnumerable CastAsType(this IEnumerable source, Type targetType)
		{
			var castMethod = EnumerableType.GetMethod("Cast")?.MakeGenericMethod(targetType);

			return (IEnumerable)castMethod?.Invoke(null, new object[] { source });
		}

		public static IList ToListOfType(this IEnumerable source, Type targetType)
		{
			var enumerable = CastAsType(source, targetType);

			var listMethod = EnumerableType.GetMethod("ToList")?.MakeGenericMethod(targetType);

			return (IList)listMethod?.Invoke(null, new object[] { enumerable });
		}
	}
}
