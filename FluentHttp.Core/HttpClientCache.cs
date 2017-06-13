using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace FluentHttp
{
    public class HttpClientCache : IHttpClientCache
    {
        private ConcurrentDictionary<string, HttpClient> _clientCache = new ConcurrentDictionary<string, HttpClient>();
        public HttpClient Cache(string key, HttpClient client)
        {
            return _clientCache.AddOrUpdate(key, client, (k, v) => client);
        }

        public bool Exists(string key)
        {
            return _clientCache.ContainsKey(key);
        }

        public HttpClient Get(string key)
        {
            var client = null as HttpClient;
            _clientCache.TryGetValue(key, out client);
            return client;
        }
    }
}
