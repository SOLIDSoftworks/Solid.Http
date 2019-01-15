using Solid.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http.Abstractions
{
    public static class OnRequestExtensions
    {
        public static ISolidHttpRequest OnRequest(this ISolidHttpRequest request, Action<IServiceProvider, HttpRequestMessage> action) =>
            request.OnRequest(action.ToAsyncFunc());

        public static ISolidHttpRequest OnRequest(this ISolidHttpRequest request, Action<HttpRequestMessage> action) =>
            request.OnRequest((_, r) => action(r));

        public static ISolidHttpRequest OnRequest(this ISolidHttpRequest request, Func<HttpRequestMessage, Task> func) =>
            request.OnRequest((_, r) => func(r));
    }
}
