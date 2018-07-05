using Solid.Http.Abstractions;
using System;
using System.Collections.Concurrent;
using System.Net.Http;

namespace Solid.Http.Factories
{
    /// <summary>
    /// Simple http client factory.
    /// </summary>
    public class FauxHttpClientFactory : IHttpClientFactory
    {
        private ConcurrentDictionary<string, Lazy<HttpClient>> _lazyClients;

        public FauxHttpClientFactory()
        { 
            _lazyClients = new ConcurrentDictionary<string, Lazy<HttpClient>>();
        }

        public HttpClient CreateClient(string name)
        {
            var lazy = _lazyClients.GetOrAdd(name, key => InitializeLazyClient());
            return lazy.Value;
        }

        private Lazy<HttpClient> InitializeLazyClient()
        {
            return new Lazy<HttpClient>(InitializeClient, System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);
        }

        private HttpClient InitializeClient()
        {
            return new HttpClient();
        }
    }
}
