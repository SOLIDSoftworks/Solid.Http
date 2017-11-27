using System;
using System.Net.Http;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Solid.Http.Abstractions;

namespace Solid.Http.Cache
{
    internal class HttpClientCache : IHttpClientCache
    {
        private IServiceProvider _serviceProvider;
        private Lazy<HttpClient> _lazyClient;

        public HttpClientCache(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _lazyClient = new Lazy<HttpClient>(Create, LazyThreadSafetyMode.ExecutionAndPublication);
        }

        public HttpClient Get()
        {
            return _lazyClient.Value;
        }

        private HttpClient Create()
        {
            var factory = _serviceProvider.GetService<IHttpClientFactory>();
            var client = factory.Create();
            return client;
        }

    }
}
