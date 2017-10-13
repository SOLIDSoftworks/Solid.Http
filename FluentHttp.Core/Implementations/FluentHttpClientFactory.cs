using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace FluentHttp
{
    /// <summary>
    /// The FluentHttpClientFactory
    /// </summary>
    public class FluentHttpClientFactory : IFluentHttpClientFactory
    {
        private static HttpClient _client;
        private static int _initialized = 0;

        private IFluentHttpEventInvoker _events;
        private IDeserializerProvider _deserializers;
        private IConfiguration _configuration;
        
        /// <summary>
        /// The application configuration which can be used in extension methods
        /// </summary>
        public IConfiguration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    var message =
                        "FluentHttpClientFactory was created with a null configuration." + Environment.NewLine +
                        "If you are initializing a FluentHttpClientFactory manually, please provide an IConfiguration in the constructor of FluentHttpClientFactory." + Environment.NewLine +
                        "If you are initializing using services.AddFluentHttp(), then make sure that your IConfiguration resides in the service container.";
                    throw new InvalidOperationException(message);
                }
                return _configuration;
            }
        }

        /// <summary>
        /// Creates a FluentHttpClientFactory
        /// </summary>
        /// <param name="events">The events to be triggered when a FluentHttpClient is created</param>
        /// <param name="deserializers">The deserializer provider for FluentHttp</param>
        /// <param name="configuration">The application configuration</param>
        public FluentHttpClientFactory(IFluentHttpEventInvoker events, IDeserializerProvider deserializers, IConfiguration configuration = null)
        {
            _events = events;
            _deserializers = deserializers;
            _configuration = configuration;
        }
        
        /// <summary>
        /// Creates a FluentHttpClient
        /// </summary>
        /// <returns>FluentHttpClient</returns>
        public FluentHttpClient Create()
        {
            return CreateFluentHttpClient(GetHttpClient());
        }
        
        /// <summary>
        /// Creates the inner HttpClient
        /// </summary>
        /// <returns></returns>
        protected virtual HttpClient CreateHttpClient()
        {
            return new HttpClient();
        }

        private HttpClient GetHttpClient()
        {
            if (_client == null)
            {
                if (Interlocked.Exchange(ref _initialized, 1) == 0)
                {
                    _client = CreateHttpClient();
                }

                SpinWait.SpinUntil(() => _client != null);
            }

            return _client;
        }

        private FluentHttpClient CreateFluentHttpClient(HttpClient inner)
        {
            var client = new FluentHttpClient(inner, _deserializers, _events);
            _events.InvokeOnClientCreated(this, client);
            return client;
        }
    }
}
