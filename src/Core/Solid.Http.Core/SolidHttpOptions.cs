using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http
{
    public class SolidHttpOptions
    {
        internal Func<IServiceProvider, SolidHttpClient, ValueTask> OnClientCreatedAsync { get; set; }
        internal Func<IServiceProvider, SolidHttpRequest, ValueTask> OnRequestCreatedAsync { get; set; }
        internal Func<IServiceProvider, HttpRequestMessage, ValueTask> OnHttpRequestAsync { get; set; }
        internal Func<IServiceProvider, HttpResponseMessage, ValueTask> OnHttpResponseAsync { get; set; }
    }
}
