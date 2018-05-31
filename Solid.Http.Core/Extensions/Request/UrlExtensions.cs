using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Solid.Http
{
    /// <summary>
    /// UrlExtensions
    /// </summary>
    public static class UrlExtensions
    {
        /// <summary>
        /// Replaces a templated parameter in the url
        /// </summary>
        /// <param name="request">The SolidHttpRequest</param>
        /// <param name="name">The name of the templated parameter</param>
        /// <param name="value">The value to inject</param>
        /// <returns>SolidHttpRequest</returns>
        public static SolidHttpRequest WithNamedParameter(this SolidHttpRequest request, string name, string value)
        {
            var url = request.BaseRequest.RequestUri.OriginalString;
            var regex = new Regex($@"{{\s*{name}\s*}}");
            url = regex.Replace(url, value);
            request.BaseRequest.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);
            return request;
        }

        /// <summary>
        /// Adds a query parameter to the url
        /// </summary>
        /// <param name="request">The SolidHttpRequest</param>
        /// <param name="name">The name of the query parameter</param>
        /// <param name="values">Value or values for the query parameter</param>
        /// <returns></returns>
        public static SolidHttpRequest WithQueryParameter(this SolidHttpRequest request, string name, StringValues values)
        {
            if (values == StringValues.Empty) return request;
            var queryVals = string.Join("&", values.Select(v => $"{name}={v}"));

            var url = request.BaseRequest.RequestUri.OriginalString;
            if (url.Contains("?"))
                url += $"&{queryVals}";
            else
                url += $"?{queryVals}";
            request.BaseRequest.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);
            return request;
        }
    }
}
