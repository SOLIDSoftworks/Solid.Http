﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Solid.Http
{
    /// <summary>
    /// Extension methods for altering the <see cref="Uri" /> of the <seealso cref="ISolidHttpRequest" />.
    /// </summary>
    public static class Solid_Http_SolidHttpRequestExtensions_Urls
    {
        /// <summary>
        /// Replaces a templated parameter in the url
        /// </summary>
        /// <param name="request">The SolidHttpRequest</param>
        /// <param name="name">The name of the templated parameter</param>
        /// <param name="value">The value to inject</param>
        /// <returns>SolidHttpRequest</returns>
        public static ISolidHttpRequest WithNamedParameter(this ISolidHttpRequest request, string name, object value)
            => request.WithNamedParameter(name, value, o => o.ConvertToString());

        /// <summary>
        /// Replaces a templated parameter in the url.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest"/> b eing extended.</param>
        /// <param name="name">The name of the templated parameter.</param>
        /// <param name="value">The value of the templated parameter.</param>
        /// <param name="convert">A delegate used to convert the <paramref name="value"/> to a <see cref="string"/>.</param>
        /// <returns>The <see cref="ISolidHttpRequest"/> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest WithNamedParameter(this ISolidHttpRequest request, string name, object value, Func<object, string> convert)
        {
            var url = request.BaseRequest.RequestUri.OriginalString;
            var regex = new Regex($@"{{\s*{name}\s*}}");
            url = regex.Replace(url, convert(value));
            request.BaseRequest.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);
            return request;
        }

        /// <summary>
        /// Adds a query parameter to the url of the <see cref="ISolidHttpRequest"/>.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest"/> that is being extended.</param>
        /// <param name="parameters">The query parameters to be added.</param>
        /// <returns>The <see cref="ISolidHttpRequest"/> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest WithQueryParameters(this ISolidHttpRequest request, IDictionary<string, object> parameters)
        {
            foreach (var parameter in parameters)
                request.WithQueryParameter(parameter.Key, parameter.Value, o => o.ConvertToStrings());
            return request;
        }

        /// <summary>
        /// Adds a query parameter to the url of the <see cref="ISolidHttpRequest"/>.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest"/> that is being extended.</param>
        /// <param name="parameters">The query parameters to be added.</param>
        /// <returns>The <see cref="ISolidHttpRequest"/> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest WithQueryParameters(this ISolidHttpRequest request, IDictionary<string, string> parameters)
        {
            foreach (var parameter in parameters)
                request.WithQueryParameter(parameter.Key, parameter.Value, o => o.ConvertToStrings());
            return request;
        }

        /// <summary>
        /// Adds a query parameter to the url of the <see cref="ISolidHttpRequest"/>.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest"/> that is being extended.</param>
        /// <param name="name">The name of the query parameter.</param>
        /// <param name="value">The value of the query parameter.</param>
        /// <returns>The <see cref="ISolidHttpRequest"/> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest WithQueryParameter(this ISolidHttpRequest request, string name, object value)
            => request.WithQueryParameter(name, value, o => o.ConvertToStrings());

        /// <summary>
        /// Adds a query parameter to the url.
        /// </summary>
        /// <param name="request">The <see cref="ISolidHttpRequest"/> that is being extended.</param>
        /// <param name="name">The name of the query parameter</param>
        /// <param name="value">The value of the query parameter.</param>
        /// <param name="convert">A converter used to convert the <paramref name="value"/> to an <see cref="IEnumerable{T}"/> of <seealso cref="string"/>.</param>
        /// <returns>The <see cref="ISolidHttpRequest"/> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest WithQueryParameter(this ISolidHttpRequest request, string name, object value, Func<object, IEnumerable<string>> convert)
        {
            var url = request.BaseRequest.RequestUri.OriginalString;
            var values = convert(value);
            foreach (var v in values)
            {
                if (url.Contains("?"))
                    url += $"&{name}={v}";
                else
                    url += $"?{name}={v}";
            }
            request.BaseRequest.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);
            return request;
        }
    }
}
