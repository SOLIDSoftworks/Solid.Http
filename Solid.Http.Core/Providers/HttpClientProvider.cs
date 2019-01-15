using Solid.Http.Abstractions;
using Solid.Http.Factories;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Solid.Http.Providers
{
    public abstract class HttpClientProvider : IHttpClientProvider
    {
        public IHttpClientFactory Factory { get; }

        public HttpClientProvider(IHttpClientFactory factory)
        {
            Factory = factory ?? new FauxHttpClientFactory();
        }

        public HttpClient Get(Uri url)
        {
            var name = GenerateHttpClientName(url);
            //if (_options.Strategy == HttpClientStrategy.SingleInstance)
            name = "Solid.Http";
            //else if (_options.Strategy == HttpClientStrategy.InstancePerHost)
            //    name = url.Host.ToLower();

            return Factory.CreateClient(name);
        }

        protected abstract string GenerateHttpClientName(Uri url);
    }
}
