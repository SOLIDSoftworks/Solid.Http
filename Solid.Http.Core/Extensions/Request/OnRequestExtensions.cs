using Solid.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http.Abstractions
{
    /// <summary>
    /// OnRequest extension methods
    /// </summary>
    public static class OnRequestExtensions
    {
        /// <summary>
        /// Add an event handler to be run just before the request is sent
        /// </summary>
        /// <param name="request">The Solid.Http request</param>
        /// <param name="action">The handler to be run</param>
        /// <returns>The Solid.Http request object</returns>
        public static ISolidHttpRequest OnRequest(this ISolidHttpRequest request, Action<IServiceProvider, HttpRequestMessage> action) =>
            request.OnRequest(action.ToAsyncFunc());

        /// <summary>
        /// Add an event handler to be run just before the request is sent
        /// </summary>
        /// <param name="request">The Solid.Http request</param>
        /// <param name="action">The handler to be run</param>
        /// <returns>The Solid.Http request object</returns>
        public static ISolidHttpRequest OnRequest(this ISolidHttpRequest request, Action<HttpRequestMessage> action) =>
            request.OnRequest((_, r) => action(r));

        /// <summary>
        /// Add an event handler to be run just before the request is sent
        /// </summary>
        /// <param name="request">The Solid.Http request</param>
        /// <param name="func">The handler to be run</param>
        /// <returns>The Solid.Http request object</returns>
        public static ISolidHttpRequest OnRequest(this ISolidHttpRequest request, Func<HttpRequestMessage, Task> func) =>
            request.OnRequest((_, r) => func(r));
    }
}
