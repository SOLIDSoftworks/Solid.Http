using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http
{
    /// <summary>
    /// Extension methods for registering global handlers on <see cref="SolidHttpOptions" />.
    /// </summary>
    public static class Solid_Http_SolidHttpOptionsExtensions
    {
        /// <summary>
        /// Registers a handler that is run when an <see cref="ISolidHttpClient" /> is created.
        /// </summary>
        /// <param name="options">The <see cref="SolidHttpOptions" /> that is being extended.</param>
        /// <param name="handler">The event handler that runs when a new <see cref="ISolidHttpClient" /> is created.</param>
        /// <returns>The <see cref="SolidHttpOptions" /> so that additional calls can be chained.</returns>
        public static SolidHttpOptions OnClientCreated(this SolidHttpOptions options, Action<ISolidHttpClient> handler)
        {
            options.OnClientCreated = options.OnClientCreated.Add(handler);
            return options;
        }

        /// <summary>
        /// Registers a handler that is run when an <see cref="ISolidHttpClient" /> is created.
        /// </summary>
        /// <param name="options">The <see cref="SolidHttpOptions" /> that is being extended.</param>
        /// <param name="handler">The event handler that runs when a new <see cref="ISolidHttpClient" /> is created.</param>
        /// <returns>The <see cref="SolidHttpOptions" /> so that additional calls can be chained.</returns>
        public static SolidHttpOptions OnClientCreated(this SolidHttpOptions options, Action<IServiceProvider, ISolidHttpClient> handler)
        {
            options.OnClientCreated = options.OnClientCreated.Add(handler);
            return options;
        }

        /// <summary>
        /// Registers a handler that is run when an <see cref="ISolidHttpRequest" /> is created.
        /// </summary>
        /// <param name="options">The <see cref="SolidHttpOptions" /> that is being extended.</param>
        /// <param name="handler">The event handler that runs when a new <see cref="ISolidHttpRequest" /> is created.</param>
        /// <returns>The <see cref="SolidHttpOptions" /> so that additional calls can be chained.</returns>
        public static SolidHttpOptions OnRequestCreated(this SolidHttpOptions options, Action<ISolidHttpRequest> handler)
        {
            options.OnRequestCreated = options.OnRequestCreated.Add(handler);
            return options;
        }

        /// <summary>
        /// Registers a handler that is run when an <see cref="ISolidHttpRequest" /> is created.
        /// </summary>
        /// <param name="options">The <see cref="SolidHttpOptions" /> that is being extended.</param>
        /// <param name="handler">The event handler that runs when a new <see cref="ISolidHttpRequest" /> is created.</param>
        /// <returns>The <see cref="SolidHttpOptions" /> so that additional calls can be chained.</returns>
        public static SolidHttpOptions OnRequestCreated(this SolidHttpOptions options, Action<IServiceProvider, ISolidHttpRequest> handler)
        {
            options.OnRequestCreated = options.OnRequestCreated.Add(handler);
            return options;
        }

        /// <summary>
        /// Registers an event handler that is run just before the <see cref="HttpRequestMessage" /> is sent to the <seealso cref="HttpClient" />.
        /// </summary>
        /// <param name="options">The <see cref="SolidHttpOptions" /> that is being extended.</param>
        /// <param name="handler">An event handler that is run just before <see cref="HttpRequestMessage" /> is sent to the <seealso cref="HttpClient" />.</param>
        /// <returns>The <see cref="SolidHttpOptions" /> so that additional calls can be chained.</returns>
        public static SolidHttpOptions OnHttpRequest(this SolidHttpOptions options, Func<IServiceProvider, HttpRequestMessage, ValueTask> handler)
        {
            options.OnHttpRequestAsync = options.OnHttpRequestAsync.Add(handler);
            return options;
        }

        /// <summary>
        /// Registers an event handler that is run just before the <see cref="HttpRequestMessage" /> is sent to the <seealso cref="HttpClient" />.
        /// </summary>
        /// <param name="options">The <see cref="SolidHttpOptions" /> that is being extended.</param>
        /// <param name="handler">An event handler that is run just before <see cref="HttpRequestMessage" /> is sent to the <seealso cref="HttpClient" />.</param>
        /// <returns>The <see cref="SolidHttpOptions" /> so that additional calls can be chained.</returns>
        public static SolidHttpOptions OnHttpRequest(this SolidHttpOptions options, Action<HttpRequestMessage> handler)
        {
            options.OnHttpRequestAsync = options.OnHttpRequestAsync.Add(handler);
            return options;
        }

        /// <summary>
        /// Registers an event handler that is run just before the <see cref="HttpRequestMessage" /> is sent to the <seealso cref="HttpClient" />.
        /// </summary>
        /// <param name="options">The <see cref="SolidHttpOptions" /> that is being extended.</param>
        /// <param name="handler">An event handler that is run just before <see cref="HttpRequestMessage" /> is sent to the <seealso cref="HttpClient" />.</param>
        /// <returns>The <see cref="SolidHttpOptions" /> so that additional calls can be chained.</returns>
        public static SolidHttpOptions OnHttpRequest(this SolidHttpOptions options, Action<IServiceProvider, HttpRequestMessage> handler)
        {
            options.OnHttpRequestAsync = options.OnHttpRequestAsync.Add(handler);
            return options;
        }

        /// <summary>
        /// Registers an event handler that is run just before the <see cref="HttpRequestMessage" /> is sent to the <seealso cref="HttpClient" />.
        /// </summary>
        /// <param name="options">The <see cref="SolidHttpOptions" /> that is being extended.</param>
        /// <param name="handler">An event handler that is run just before <see cref="HttpRequestMessage" /> is sent to the <seealso cref="HttpClient" />.</param>
        /// <returns>The <see cref="SolidHttpOptions" /> so that additional calls can be chained.</returns>
        public static SolidHttpOptions OnHttpRequest(this SolidHttpOptions options, Func<HttpRequestMessage, ValueTask> handler)
        {
            options.OnHttpRequestAsync = options.OnHttpRequestAsync.Add(handler);
            return options;
        }

        /// <summary>
        /// Registers an event handler that is run just after the <see cref="HttpResponseMessage" /> is receied by the <seealso cref="HttpClient" />.
        /// </summary>
        /// <param name="options">The <see cref="SolidHttpOptions" /> that is being extended.</param>
        /// <param name="handler">An event handler that is run just after the <see cref="HttpResponseMessage" /> is received by the <seealso cref="HttpClient" />.</param>
        /// <returns>The <see cref="SolidHttpOptions" /> so that additional calls can be chained.</returns>
        public static SolidHttpOptions OnHttpResponse(this SolidHttpOptions options, Func<IServiceProvider, HttpResponseMessage, ValueTask> handler)
        {
            options.OnHttpResponseAsync = options.OnHttpResponseAsync.Add(handler);
            return options;
        }

        /// <summary>
        /// Registers an event handler that is run just after the <see cref="HttpResponseMessage" /> is receied by the <seealso cref="HttpClient" />.
        /// </summary>
        /// <param name="options">The <see cref="SolidHttpOptions" /> that is being extended.</param>
        /// <param name="handler">An event handler that is run just after the <see cref="HttpResponseMessage" /> is received by the <seealso cref="HttpClient" />.</param>
        /// <returns>The <see cref="SolidHttpOptions" /> so that additional calls can be chained.</returns>
        public static SolidHttpOptions OnHttpResponse(this SolidHttpOptions options, Action<HttpResponseMessage> handler)
        {
            options.OnHttpResponseAsync = options.OnHttpResponseAsync.Add(handler);
            return options;
        }

        /// <summary>
        /// Registers an event handler that is run just after the <see cref="HttpResponseMessage" /> is receied by the <seealso cref="HttpClient" />.
        /// </summary>
        /// <param name="options">The <see cref="SolidHttpOptions" /> that is being extended.</param>
        /// <param name="handler">An event handler that is run just after the <see cref="HttpResponseMessage" /> is received by the <seealso cref="HttpClient" />.</param>
        /// <returns>The <see cref="SolidHttpOptions" /> so that additional calls can be chained.</returns>
        public static SolidHttpOptions OnHttpResponse(this SolidHttpOptions options, Action<IServiceProvider, HttpResponseMessage> handler)
        {
            options.OnHttpResponseAsync = options.OnHttpResponseAsync.Add(handler);
            return options;
        }

        /// <summary>
        /// Registers an event handler that is run just after the <see cref="HttpResponseMessage" /> is receied by the <seealso cref="HttpClient" />.
        /// </summary>
        /// <param name="options">The <see cref="SolidHttpOptions" /> that is being extended.</param>
        /// <param name="handler">An event handler that is run just after the <see cref="HttpResponseMessage" /> is received by the <seealso cref="HttpClient" />.</param>
        /// <returns>The <see cref="SolidHttpOptions" /> so that additional calls can be chained.</returns>
        public static SolidHttpOptions OnHttpResponse(this SolidHttpOptions options, Func<HttpResponseMessage, ValueTask> handler)
        {
            options.OnHttpResponseAsync = options.OnHttpResponseAsync.Add(handler);
            return options;
        }
    }
}
