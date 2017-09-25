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
        private ISerializerProvider _serializers;
        private IConfiguration _configuration;

        public FluentHttpClientFactory(IFluentHttpClientFactoryEventInvoker events, ISerializerProvider serializers, IConfiguration configuration = null)
        {
            _events = events;
            _serializers = serializers;
            _configuration = configuration;
        }

        public FluentHttpClient Create()
        {
            return CreateFluentHttpClient(GetHttpClient());
        }

        public FluentHttpClient CreateUsingConnectionString(string connectionStringName)
        {
            if (_configuration == null)
            {
                var message =
                    "FluentHttpClientFactory was created with a null configuration." + Environment.NewLine +
                    "If you are creating FluentHttpClientFactory manually, please provide an IConfigurationRoot in the constructor of FluentHttpClientFactory." + Environment.NewLine +
                    // TODO: fix this line in the error message
                    "If you are calling services.UseFluentHttp(), then change that call to services.UseFluentHttp(configuration) where configuration is the application configuration.";
                throw new InvalidOperationException(message);
            }

            var baseAddress = _configuration.GetConnectionString(connectionStringName);
            return CreateWithBaseAddress(baseAddress);
        }

        public FluentHttpClient CreateWithBaseAddress(string baseAddress)
        {
            return CreateWithBaseAddress(new Uri(baseAddress));
        }

        public FluentHttpClient CreateWithBaseAddress(Uri baseAddress)
        {
            var client = Create();
            client.BaseAddress = baseAddress;
            return client;
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
            var client = new FluentHttpClient(inner, _serializers);
            _events.InvokeClientCreated(this, client);
            return client;
        }
    }
}
