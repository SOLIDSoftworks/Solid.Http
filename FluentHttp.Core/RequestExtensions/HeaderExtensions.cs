using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Linq;

namespace FluentHttp
{
    /// <summary>
    /// HeaderExtensions
    /// </summary>
    public static class HeaderExtensions
    {
        /// <summary>
        /// Adds a header to the http request
        /// </summary>
        /// <param name="request">The FluentHttpRequest</param>
        /// <param name="name">The name of the header</param>
        /// <param name="value">The value of the header</param>
        /// <returns>FluentHttpRequest</returns>
        public static FluentHttpRequest WithHeader(this FluentHttpRequest request, string name, IEnumerable<string> values)
        {
            return request.WithHeaders(headers => headers.Add(name, values));
        }

        /// <summary>
        /// Adds a header to the http request
        /// </summary>
        /// <param name="request">The FluentHttpRequest</param>
        /// <param name="name">The name of the header</param>
        /// <param name="value">The value of the header</param>
        /// <param name="more">More values for the header</param>
        /// <returns>FluentHttpRequest</returns>
        public static FluentHttpRequest WithHeader(this FluentHttpRequest request, string name, string value, params string[] more)
        {
            var values = new[] { value }.Concat(more);
            return request.WithHeaders(headers => headers.Add(name, values));
        }

        /// <summary>
        /// Adds headers to the http request
        /// </summary>
        /// <param name="request">The FluentHttpRequest</param>
        /// <param name="addHeaders">The action to add headers</param>
        /// <returns>FluentHttpRequest</returns>
        public static FluentHttpRequest WithHeaders(this FluentHttpRequest request, Action<HttpRequestHeaders> addHeaders)
        {
            addHeaders(request.BaseRequest.Headers);
            return request;
        }

        //public static FluentHttpRequest WithContentHeader(this FluentHttpRequest request, string name, string value)
        //{
        //    if (request.BaseRequest.Content == null)
        //        throw new InvalidOperationException("Cannot set a content header on null content");
        //    request.BaseRequest.Content.Headers.Add(name, value);
        //    return request;
        //}
    }
}
