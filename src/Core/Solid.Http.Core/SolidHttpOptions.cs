using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http
{
    /// <summary>
    /// Options for configuring Solid.Http.Core.
    /// </summary>
    public class SolidHttpOptions
    {
        internal Action<IServiceProvider, ISolidHttpClient> OnClientCreated { get; set; }
        internal Action<IServiceProvider, ISolidHttpRequest> OnRequestCreated { get; set; }
        internal Func<IServiceProvider, HttpRequestMessage, ValueTask> OnHttpRequestAsync { get; set; }
        internal Func<IServiceProvider, HttpResponseMessage, ValueTask> OnHttpResponseAsync { get; set; }
    }
}
