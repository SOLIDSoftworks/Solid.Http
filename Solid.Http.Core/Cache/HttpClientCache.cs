//using System;
//using System.Net.Http;
//using System.Threading;
//using Microsoft.Extensions.DependencyInjection;
//using Solid.Http.Abstractions;

//namespace Solid.Http.Cache
//{
//    internal class HttpClientCache : IHttpClientCache, IDisposable
//    {
//        private IServiceProvider _serviceProvider;
//        private Lazy<HttpClient> _lazyClient;

//        public HttpClientCache(IServiceProvider serviceProvider)
//        {
//            _serviceProvider = serviceProvider;
//            InitializeLazyClient();
//        }

//        public HttpClient Get()
//        {
//            return _lazyClient.Value;
//        }

//        public void Clear()
//        {
//            DisposeLazyClient();
//            InitializeLazyClient();
//        }

//        public void Dispose()
//        {
//            DisposeLazyClient();
//        }

//        private HttpClient Create()
//        {
//            var factory = _serviceProvider.GetService<IHttpClientFactory>();
//            var client = factory.Create();
//            return client;
//        }

//        private void InitializeLazyClient()
//        {
//            _lazyClient = new Lazy<HttpClient>(Create, LazyThreadSafetyMode.ExecutionAndPublication);
//        }

//        private void DisposeLazyClient()
//        {
//            if (_lazyClient.IsValueCreated)
//                _lazyClient.Value.Dispose();
//        }
//    }
//}
