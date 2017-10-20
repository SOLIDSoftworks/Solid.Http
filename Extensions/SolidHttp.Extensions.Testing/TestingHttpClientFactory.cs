using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;

namespace SolidHttp.Extensions.Testing
{
    public class TestingHttpClientFactory<TStartup> : IHttpClientFactory
        where TStartup : class
    {
        private IConfiguration _configuration;

        public TestingHttpClientFactory(IConfiguration configuration = null)
        {
            _configuration = configuration;
        }
        public HttpClient Create()
        {
            return new InMemoryLocalHttpServerClient<TStartup>(_configuration);
        }
    }
}
