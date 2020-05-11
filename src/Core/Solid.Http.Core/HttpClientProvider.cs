using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Solid.Http
{
    internal class HttpClientProvider : IHttpClientProvider
    {
        private IHttpClientFactory _factory;

        public HttpClientProvider(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public HttpClient Get(Uri url)
        {
            var name = url?.Host.ToLower() ?? "solid.http";
            return _factory.CreateClient(name);
        }
    }
}
