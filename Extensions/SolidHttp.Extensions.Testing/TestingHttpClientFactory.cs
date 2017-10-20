using System;
using System.Net.Http;

namespace SolidHttp.Extensions.Testing
{
    public class TestingHttpClientFactory<TStartup> : IHttpClientFactory
        where TStartup : class
    {
        public HttpClient Create()
        {
            return new InMemoryLocalHttpServerClient<TStartup>();
        }
    }
}
