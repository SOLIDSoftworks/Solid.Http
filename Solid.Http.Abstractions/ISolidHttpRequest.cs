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
        ISolidHttpClient Client { get; }
        HttpRequestMessage BaseRequest { get; }
        HttpResponseMessage BaseResponse { get; }
        CancellationToken CancellationToken { get; }
        void OnRequest(Action<IServiceProvider, HttpRequestMessage> handler);
        void OnRequest(Func<IServiceProvider, HttpRequestMessage, Task> handler);
        void OnResponse(Action<IServiceProvider, HttpResponseMessage> handler);
        void OnResponse(Func<IServiceProvider, HttpResponseMessage, Task> handler);
        TaskAwaiter<HttpResponseMessage> GetAwaiter();
    }
}
