using Solid.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http.Abstractions
{
    public static class OnResponseExtensions
    {

        public static ISolidHttpRequest OnResponse(this ISolidHttpRequest request, Action<IServiceProvider, HttpResponseMessage> action) =>
            request.OnResponse(action.ToAsyncFunc());

        public static ISolidHttpRequest OnResponse(this ISolidHttpRequest request, Action<HttpResponseMessage> action) =>
            request.OnResponse((_, r) => action(r));

        public static ISolidHttpRequest OnResponse(this ISolidHttpRequest request, Func<HttpResponseMessage, Task> func) =>
            request.OnResponse((_, r) => func(r));
    }
}
