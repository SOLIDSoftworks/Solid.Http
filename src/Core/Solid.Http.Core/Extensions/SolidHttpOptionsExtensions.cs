using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http
{
    public static class Solid_Http_SolidHttpOptionsExtensions
    {
        public static SolidHttpOptions OnClientCreated(this SolidHttpOptions options, Action<ISolidHttpClient> handler)
        {
            options.OnClientCreated = options.OnClientCreated.Add(handler);
            return options;
        }

        public static SolidHttpOptions OnClientCreated(this SolidHttpOptions options, Action<IServiceProvider, ISolidHttpClient> handler)
        {
            options.OnClientCreated = options.OnClientCreated.Add(handler);
            return options;
        }

        public static SolidHttpOptions OnRequestCreated(this SolidHttpOptions options, Action<ISolidHttpRequest> handler)
        {
            options.OnRequestCreated = options.OnRequestCreated.Add(handler);
            return options;
        }

        public static SolidHttpOptions OnRequestCreated(this SolidHttpOptions options, Action<IServiceProvider, ISolidHttpRequest> handler)
        {
            options.OnRequestCreated = options.OnRequestCreated.Add(handler);
            return options;
        }

        public static SolidHttpOptions OnHttpRequest(this SolidHttpOptions options, Func<IServiceProvider, HttpRequestMessage, ValueTask> handler)
        {
            options.OnHttpRequestAsync = options.OnHttpRequestAsync.Add(handler);
            return options;
        }

        public static SolidHttpOptions OnHttpRequest(this SolidHttpOptions options, Action<HttpRequestMessage> handler)
        {
            options.OnHttpRequestAsync = options.OnHttpRequestAsync.Add(handler);
            return options;
        }

        public static SolidHttpOptions OnHttpRequest(this SolidHttpOptions options, Action<IServiceProvider, HttpRequestMessage> handler)
        {
            options.OnHttpRequestAsync = options.OnHttpRequestAsync.Add(handler);
            return options;
        }

        public static SolidHttpOptions OnHttpRequest(this SolidHttpOptions options, Func<HttpRequestMessage, ValueTask> handler)
        {
            options.OnHttpRequestAsync = options.OnHttpRequestAsync.Add(handler);
            return options;
        }

        public static SolidHttpOptions OnHttpResponse(this SolidHttpOptions options, Func<IServiceProvider, HttpResponseMessage, ValueTask> handler)
        {
            options.OnHttpResponseAsync = options.OnHttpResponseAsync.Add(handler);
            return options;
        }

        public static SolidHttpOptions OnHttpResponse(this SolidHttpOptions options, Action<HttpResponseMessage> handler)
        {
            options.OnHttpResponseAsync = options.OnHttpResponseAsync.Add(handler);
            return options;
        }

        public static SolidHttpOptions OnHttpResponse(this SolidHttpOptions options, Action<IServiceProvider, HttpResponseMessage> handler)
        {
            options.OnHttpResponseAsync = options.OnHttpResponseAsync.Add(handler);
            return options;
        }

        public static SolidHttpOptions OnHttpResponse(this SolidHttpOptions options, Func<HttpResponseMessage, ValueTask> handler)
        {
            options.OnHttpResponseAsync = options.OnHttpResponseAsync.Add(handler);
            return options;
        }
    }
}
