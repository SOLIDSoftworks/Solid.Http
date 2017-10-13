using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace FluentHttp
{
    public class FluentHttpClientFactory : IFluentHttpClientFactory
    {
        private static HttpClient _client;
        private static int _initialized = 0;

        private IFluentHttpClientFactoryEventInvoker _events;
        private IDeserializerProvider _deserializers;
        private IConfiguration _configuration;

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

        public FluentHttpClientFactory(IFluentHttpClientFactoryEventInvoker events, IDeserializerProvider deserializers, IConfiguration configuration = null)
        {
            _events = events;
            _deserializers = deserializers;
            _configuration = configuration;
        }

        public FluentHttpClient Create()
        {
            return CreateFluentHttpClient(GetHttpClient());
        }
        
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
            var client = new FluentHttpClient(inner, _deserializers);
            _events.InvokeClientCreated(this, client);
            return client;
        }
    }
}
