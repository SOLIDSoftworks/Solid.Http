using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Solid.Http.Abstractions
{
    public interface ISolidHttpRequest
    {
        ISolidHttpClient Client { get; }
        HttpRequestMessage BaseRequest { get; }
        HttpResponseMessage BaseResponse { get; }
        CancellationToken CancellationToken { get; }

        ISolidHttpRequest OnRequest(Func<IServiceProvider, HttpRequestMessage, Task> handler);
        ISolidHttpRequest OnResponse(Func<IServiceProvider, HttpResponseMessage, Task> handler);
        TaskAwaiter<HttpResponseMessage> GetAwaiter();
    }
}
