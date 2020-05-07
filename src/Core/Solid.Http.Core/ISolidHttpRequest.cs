using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Solid.Http
{
    public interface ISolidHttpRequest
    {
        IServiceProvider Services { get; }
        IDictionary<string, object> Context { get; }

        CancellationToken CancellationToken { get; }

        /// <summary>
        /// The base request that is sent
        /// </summary>
        HttpRequestMessage BaseRequest { get; }
        /// <summary>
        /// The response
        /// </summary>
        HttpResponseMessage BaseResponse { get; }
        ISolidHttpRequest OnHttpRequest(Func<IServiceProvider, HttpRequestMessage, ValueTask> handler);
        ISolidHttpRequest OnHttpResponse(Func<IServiceProvider, HttpResponseMessage, ValueTask> handler);
        ValueTask<T> As<T>(Func<IServiceProvider, HttpContent, ValueTask<T>> deserialize);
        TaskAwaiter<HttpResponseMessage> GetAwaiter();
    }
}
