using System;
using System.Collections.Concurrent;
using System.Net.Http;

namespace Solid.Http.Factories
{
    internal class FauxHttpClientFactory : IHttpClientFactory
    {
        private ConcurrentDictionary<string, HttpClient> _clients;

        public FauxHttpClientFactory()
        { 
            _clients = new ConcurrentDictionary<string, HttpClient>();
        }

        public HttpClient CreateClient(string name)
        {
            var client = _clients.GetOrAdd(name, key => InitializeClient(key));            
            return client;
        }

        private HttpClient InitializeClient(string name)
        {
            return new CachedHttpClient(() => _clients.TryRemove(name, out var _));
        }

        class CachedHttpClient : HttpClient
        {
            private Action _disposing;

            public CachedHttpClient(Action disposing) : base()
            {
                _disposing = disposing;
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                    _disposing();
                base.Dispose(disposing);
            }
        }
    }
}
