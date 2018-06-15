using Solid.Http.Abstractions;
using System;
using System.Net.Http;

namespace Solid.Http.Factories
{
    /// <summary>
    /// Simple http client factory.
    /// </summary>
    public class FauxHttpClientFactory : IHttpClientFactory
    {
        private Lazy<HttpClient> _lazyClient;

        public FauxHttpClientFactory()
        {
            _lazyClient = new Lazy<HttpClient>(InitializeClient, System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);
        }

        public HttpClient CreateClient(string name)
        {
            return _lazyClient.Value;
        }

        private HttpClient InitializeClient()
        {
            return new HttpClient();
        }
    }
}
