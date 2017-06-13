using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FluentHttp
{
    public static class UrlExtensions
    {
        public static FluentHttpRequest WithNamedParameter(this FluentHttpRequest request, string name, string value)
        {
            var url = request.BaseRequest.RequestUri.OriginalString;
            var regex = new Regex($@"{{\s*{name}\s*}}");
            url = regex.Replace(url, value);
            request.BaseRequest.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);
            return request;
        }

        public static FluentHttpRequest WithQueryParameter(this FluentHttpRequest request, string name, string value)
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
