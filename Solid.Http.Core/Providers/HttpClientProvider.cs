using System;
using System.Net.Http;
using Solid.Http.Factories;
using Solid.Http.Models;

namespace Solid.Http.Providers
{
    public class HttpClientProvider : IHttpClientProvider
    {
        private ISolidHttpOptions _options;

        public IHttpClientFactory Factory { get; }

        public HttpClientProvider(ISolidHttpOptions options, IHttpClientFactory factory = null)
        {
            _options = options;
            Factory = factory ?? new FauxHttpClientFactory();
        }

        public HttpClient Get(Uri url)
        {
            var name = string.Empty;
            if (_options.Strategy == HttpClientStrategy.SingleInstance)
                name = "Solid.Http";
            else if (_options.Strategy == HttpClientStrategy.InstancePerHost)
                name = url.Host.ToLower();

            return Factory.CreateClient(name);
        }
    }
}
