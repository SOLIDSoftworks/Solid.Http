using Solid.Http.Abstractions;
using Solid.Http.Events;
using Solid.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Solid.Http
{
    /// <summary>
    /// A SolidHttpRequest that is used to perform http requests. This class is designed be extended using extension methods.
    /// </summary>
    public class SolidHttpRequest : ISolidHttpRequest
    {
        private List<Func<IServiceProvider, HttpRequestMessage, Task>> _requestHandlers = new List<Func<IServiceProvider, HttpRequestMessage, Task>>();
        private List<Func<IServiceProvider, HttpResponseMessage, Task>> _responseHandlers = new List<Func<IServiceProvider, HttpResponseMessage, Task>>();

        private IServiceProvider _services;

        internal SolidHttpRequest(ISolidHttpClient client, IServiceProvider services, HttpMethod method, Uri url, CancellationToken cancellationToken)
        {
            _services = services;
            Client = client;
            BaseRequest = new HttpRequestMessage(method, url);
            CancellationToken = cancellationToken;
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


        ///// <summary>
        ///// The event triggered before an http request is sent
        ///// </summary>
        //public event EventHandler<RequestEventArgs> OnRequest;

        //private event EventHandler<ResponseEventArgs> _onResponse;
        ///// <summary>
        ///// The event triggered after an http response is received
        ///// </summary>
        //public event EventHandler<ResponseEventArgs> OnResponse
        //{
        //    add
        //    {                
        //        _onResponse += value;
        //        if (Response != null)
        //            value(this, Client.Events.CreateArgs(Response));
        //    }
        //    remove
        //    {
        //        _onResponse -= value;
        //    }
        //}

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
                    foreach (var handler in _requestHandlers)
                        await handler(_services, BaseRequest);
                    var provider = _services.GetService<IHttpClientProvider>();
                    var http = provider.Get(BaseRequest.RequestUri);
                    BaseResponse = await http.SendAsync(BaseRequest, CancellationToken);

                    foreach (var handler in _responseHandlers)
                        await handler(_services, BaseResponse);
                }
                return BaseResponse;
            });
            return waiter(this).GetAwaiter();
        }

        public void OnRequest(Action<IServiceProvider, HttpRequestMessage> handler)
        {
            OnRequest(handler.ToAsyncFunc());
        }

        public void OnRequest(Func<IServiceProvider, HttpRequestMessage, Task> handler)
        {
            _requestHandlers.Add(handler);
        }

        public void OnResponse(Action<IServiceProvider, HttpResponseMessage> handler)
        {
            OnResponse(handler.ToAsyncFunc());
        }

        public void OnResponse(Func<IServiceProvider, HttpResponseMessage, Task> handler)
        {
            _responseHandlers.Add(handler);
        }
    }
}