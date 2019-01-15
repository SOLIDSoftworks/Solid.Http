using Solid.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Solid.Http.Abstractions;

namespace Solid.Http
{
    /// <summary>
    /// A SolidHttpRequest that is used to perform http requests. This class is designed be extended using extension methods.
    /// </summary>
    internal class SolidHttpRequest : ISolidHttpRequest
    {
        private Func<IServiceProvider, HttpRequestMessage, Task> _onRequest;
        private Func<IServiceProvider, HttpResponseMessage, Task> _onResponse;

        private IServiceProvider _services;

        internal SolidHttpRequest(
            ISolidHttpClient client, 
            IServiceProvider services, 
            HttpMethod method, 
            Uri url,
            Func<IServiceProvider, HttpRequestMessage, Task> onRequest,
            Func<IServiceProvider, HttpResponseMessage, Task> onResponse, 
            CancellationToken cancellationToken)
        {
            _services = services;
            Client = client;
            BaseRequest = new HttpRequestMessage(method, url);
            CancellationToken = cancellationToken;

            _onRequest += onRequest ?? ((_, __) => Task.CompletedTask);
            _onResponse += onResponse ?? ((_, __) => Task.CompletedTask);
        }

        public ISolidHttpClient Client { get; }

        /// <summary>
        /// The base request that is sent
        /// </summary>
        public HttpRequestMessage BaseRequest { get; }

        /// <summary>
        /// The response
        /// </summary>
        public HttpResponseMessage BaseResponse { get; private set; }

        /// <summary>
        /// The cancellation token for the request
        /// </summary>
        public CancellationToken CancellationToken { get; }


        /// <summary>
        /// The awaiter that enables a SolidHttpRequest to be awaited
        /// </summary>
        /// <returns>A TaskAwaiter for an HttpResponseMessage</returns>
        public TaskAwaiter<HttpResponseMessage> GetAwaiter()
        {
            Func<SolidHttpRequest, Task<HttpResponseMessage>> waiter = (async r =>
            {
                if (BaseResponse == null)
                {
                    await _onRequest(_services, BaseRequest);
                    var provider = _services.GetService<IHttpClientProvider>();
                    var http = provider.Get(BaseRequest.RequestUri);
                    BaseResponse = await http.SendAsync(BaseRequest, CancellationToken);
                    await _onResponse(_services, BaseResponse);
                }
                return BaseResponse;
            });
            return waiter(this).GetAwaiter();
        }

        public ISolidHttpRequest OnRequest(Func<IServiceProvider, HttpRequestMessage, Task> handler)
        {
            _onRequest += handler;
            return this;
        }

        public ISolidHttpRequest OnResponse(Func<IServiceProvider, HttpResponseMessage, Task> handler)
        {
            _onResponse += handler;
            return this;
        }
    }
}