using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace System
{
    internal static class ObjectExtensions
    {
        public static string ConvertToString(this object value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value), "Value cannot be null.");

            if (value is string str) return str;
            //if (value is IConvertible convertable) return convertable.ToString(CultureInfo.InvariantCulture);

            return value.ToString();

        }

        public static IEnumerable<string> ConvertToStrings(this object value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value), "Value cannot be null.");

            if (value is StringValues values) return values;
            return new[] { ConvertToString(value) };
        }
    }
}
