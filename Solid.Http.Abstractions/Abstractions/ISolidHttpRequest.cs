using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Solid.Http.Abstractions
{
    /// <summary>
    /// The extendable request
    /// </summary>
    public interface ISolidHttpRequest
    {
        /// <summary>
        /// The SolidHttpClient that was used to generate the request
        /// </summary>
        ISolidHttpClient Client { get; }

        /// <summary>
        /// The request message that will be sent to an HttpClient
        /// </summary>
        HttpRequestMessage BaseRequest { get; }

        /// <summary>
        /// The response message that will come from an HttpClient
        /// </summary>
        HttpResponseMessage BaseResponse { get; }

        /// <summary>
        /// The cancellation token
        /// </summary>
        CancellationToken CancellationToken { get; }

        /// <summary>
        /// Add an event handler to be run just before the request is sent
        /// </summary>
        /// <param name="handler">The handler to be run</param>
        /// <returns>The Solid.Http request object</returns>
        ISolidHttpRequest OnRequest(Func<IServiceProvider, HttpRequestMessage, Task> handler);

        /// <summary>
        /// Add an event to be run the moment the response is received
        /// </summary>
        /// <param name="handler">The handler to be run</param>
        /// <returns>The Solid.Http request object</returns>
        ISolidHttpRequest OnResponse(Func<IServiceProvider, HttpResponseMessage, Task> handler);

        /// <summary>
        /// Get the task awaiter to make this request directly awaitable
        /// </summary>
        /// <returns>A task awaiter</returns>
        TaskAwaiter<HttpResponseMessage> GetAwaiter();
    }
}
