using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    internal static class UriExtensions
    {
        public static Uri WithTrailingSlash(this Uri baseAddress)
        {
            if (baseAddress.OriginalString.EndsWith("/", StringComparison.OrdinalIgnoreCase)) return baseAddress;
            return new Uri(baseAddress.OriginalString + "/");
        }
    }
}
