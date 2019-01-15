using Solid.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http.Abstractions
{
    /// <summary>
    /// OnResponse extension methods
    /// </summary>
    public static class OnResponseExtensions
    {
        /// <summary>
        /// Add an event to be run the moment the response is received
        /// </summary>
        /// <param name="request">The Solid.Http request</param>
        /// <param name="action">The handler to be run</param>
        /// <returns>The Solid.Http request object</returns>
        public static ISolidHttpRequest OnResponse(this ISolidHttpRequest request, Action<IServiceProvider, HttpResponseMessage> action) =>
            request.OnResponse(action.ToAsyncFunc());

        /// <summary>
        /// Add an event to be run the moment the response is received
        /// </summary>
        /// <param name="request">The Solid.Http request</param>
        /// <param name="action">The handler to be run</param>
        /// <returns>The Solid.Http request object</returns>
        public static ISolidHttpRequest OnResponse(this ISolidHttpRequest request, Action<HttpResponseMessage> action) =>
            request.OnResponse((_, r) => action(r));

        /// <summary>
        /// Add an event to be run the moment the response is received
        /// </summary>
        /// <param name="request">The Solid.Http request</param>
        /// <param name="func">The handler to be run</param>
        /// <returns>The Solid.Http request object</returns>
        public static ISolidHttpRequest OnResponse(this ISolidHttpRequest request, Func<HttpResponseMessage, Task> func) =>
            request.OnResponse((_, r) => func(r));
    }
}
