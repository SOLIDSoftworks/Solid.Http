using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Primitives;

namespace Solid.Http
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
        /// <param name="values">More values for the header</param>
        /// <returns></returns>
        public static SolidHttpRequest WithHeader(this SolidHttpRequest request, string name, StringValues values)
        {
            return request.WithHeaders(headers => headers.Add(name, values.ToArray()));
        }

        /// <summary>
        /// Adds headers to the http request
        /// </summary>
        /// <param name="request">The SolidHttpRequest</param>
        /// <param name="addHeaders">The action to add headers</param>
        /// <returns>SolidHttpRequest</returns>
        public static SolidHttpRequest WithHeaders(this SolidHttpRequest request, Action<HttpRequestHeaders> addHeaders)
        {
            addHeaders(request.BaseRequest.Headers);
            return request;
        }
    }
}
