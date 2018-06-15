using System;
using System.Net.Http;
using Solid.Http.Abstractions;
using Solid.Http.Factories;

namespace Solid.Http.Providers
{
    public class HttpClientProvider : IHttpClientProvider
    {
        public IHttpClientFactory Factory { get; }

        public HttpClientProvider(IHttpClientFactory factory = null)
        {
            Factory = factory ?? new FauxHttpClientFactory();
        }

        public HttpClient Get()
        {
            return Factory.CreateClient("Solid.Http");
        }
    }
}
