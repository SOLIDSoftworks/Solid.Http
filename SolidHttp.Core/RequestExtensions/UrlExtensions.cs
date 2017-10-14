using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SolidHttp
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
        /// <param name="value">The value of the query parameter</param>
        /// <returns></returns>
        public static SolidHttpRequest WithQueryParameter(this SolidHttpRequest request, string name, string value)
        {
            var url = request.BaseRequest.RequestUri.OriginalString;
            if (url.Contains("?"))
                url += $"&{name}={value}";
            else
                url += $"?{name}={value}";
            request.BaseRequest.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);
            return request;
        }
    }
}
