using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace SolidHttp
{
    /// <summary>
    /// The SolidHttpClientFactory
    /// </summary>
    public class SolidHttpClientFactory : ISolidHttpClientFactory
    {
        private static HttpClient _client;
        private static int _initialized = 0;

        private ISolidHttpEventInvoker _events;
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
                        "SolidHttpClientFactory was created with a null configuration." + Environment.NewLine +
                        "If you are initializing a SolidHttpClientFactory manually, please provide an IConfiguration in the constructor of SolidHttpClientFactory." + Environment.NewLine +
                        "If you are initializing using services.AddSolidHttp(), then make sure that your IConfiguration resides in the service container.";
                    throw new InvalidOperationException(message);
                }
                return _configuration;
            }
        }

        /// <summary>
        /// Creates a SolidHttpClientFactory
        /// </summary>
        /// <param name="events">The events to be triggered when a SolidHttpClient is created</param>
        /// <param name="deserializers">The deserializer provider for SolidHttp</param>
        /// <param name="configuration">The application configuration</param>
        public SolidHttpClientFactory(ISolidHttpEventInvoker events, IDeserializerProvider deserializers, IConfiguration configuration = null)
        {
            _events = events;
            _deserializers = deserializers;
            _configuration = configuration;
        }
        
        /// <summary>
        /// Creates a SolidHttpClient
        /// </summary>
        /// <returns>SolidHttpClient</returns>
        public SolidHttpClient Create()
        {
            return CreateSolidHttpClient(GetHttpClient());
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

        private SolidHttpClient CreateSolidHttpClient(HttpClient inner)
        {
            var client = new SolidHttpClient(inner, _deserializers, _events);
            _events.InvokeOnClientCreated(this, client);
            return client;
        }
    }
}
