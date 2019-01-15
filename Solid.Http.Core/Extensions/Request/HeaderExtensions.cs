using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Primitives;

namespace Solid.Http.Abstractions
{
    /// <summary>
    /// HeaderExtensions
    /// </summary>
    public static class HeaderExtensions
    {
        /// <summary>
        /// Adds a header to the http request
        /// </summary>
        /// <param name="request">The SolidHttpRequest</param>
        /// <param name="name">The name of the header</param>
        /// <param name="values">Value(s) for the header</param>
        /// <returns>ISolidHttpRequest</returns>
        public static ISolidHttpRequest WithHeader(this ISolidHttpRequest request, string name, StringValues values)
        {
            return request.WithHeaders(headers => headers.Add(name, values.ToArray()));
        }

        /// <summary>
        /// Adds a header to the http request
        /// </summary>
        /// <param name="request">The SolidHttpRequest</param>
        /// <param name="name">The name of the header</param>
        /// <param name="firstValue">The first value of the header</param>
        /// <param name="secondValue">The second value of the header</param>
        /// <param name="moreValues">More values for the header</param>
        /// <returns>ISolidHttpRequest</returns>
        public static ISolidHttpRequest WithHeader(this ISolidHttpRequest request, string name, string firstValue, string secondValue, params string[] moreValues)
        {
            var values = new[] { firstValue, secondValue }.Concat(moreValues);
            return request.WithHeaders(headers => headers.Add(name, values));
        }

        /// <summary>
        /// Adds headers to the http request
        /// </summary>
        /// <param name="request">The SolidHttpRequest</param>
        /// <param name="addHeaders">The action to add headers</param>
        /// <returns>ISolidHttpRequest</returns>
        public static ISolidHttpRequest WithHeaders(this ISolidHttpRequest request, Action<HttpRequestHeaders> addHeaders)
        {
            addHeaders(request.BaseRequest.Headers);
            return request;
        }
    }
}
