using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace FluentHttp
{
    public class HttpClientProvider : IHttpClientProvider
    {
        private Lazy<HttpClient> _lazyClient;

        public HttpClientProvider()
        {
            _lazyClient = new Lazy<HttpClient>(Create, LazyThreadSafetyMode.ExecutionAndPublication);
        }

        public HttpClient Get()
        {
            return _lazyClient.Value;
        }

        private HttpClient Create()
        {
            return new HttpClient();
        }
    }
}
