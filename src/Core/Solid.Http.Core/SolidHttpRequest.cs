using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Solid.Http
{
    internal class SolidHttpRequest : ISolidHttpRequest
    {
        private HttpClientProvider _httpClientProvider;

        public SolidHttpRequest(IServiceProvider services, HttpClientProvider httpClientProvider, IOptions<SolidHttpOptions> options)
        {
            Services = services;
            _httpClientProvider = httpClientProvider;
            //Context = new SolidHttpRequestContext();
            OnHttpRequestAsync = options.Value.OnHttpRequestAsync;
            OnHttpResponseAsync = options.Value.OnHttpResponseAsync;
        }

        //public ISolidHttpRequestContext Context { get; }
        public CancellationToken CancellationToken { get; internal set; }
        public IServiceProvider Services { get; }
        /// <summary>
        /// The base request that is sent
        /// </summary>
        public HttpRequestMessage BaseRequest { get; internal set; }

        /// <summary>
        /// The response
        /// </summary>
        public HttpResponseMessage BaseResponse { get; private set; }
        public IDictionary<string, object> Context => BaseRequest?.Properties;
        internal Func<IServiceProvider, SolidHttpRequest, ValueTask> DeferredRequestCreatedAsync { get; set; }
        internal Func<IServiceProvider, HttpRequestMessage, ValueTask> OnHttpRequestAsync { get; set; }
        internal Func<IServiceProvider, HttpResponseMessage, ValueTask> OnHttpResponseAsync { get; set; }


        public ISolidHttpRequest OnHttpRequest(Func<IServiceProvider, HttpRequestMessage, ValueTask> handler)
        {
            OnHttpRequestAsync = OnHttpRequestAsync.Add(handler);
            return this;
        }
        public ISolidHttpRequest OnHttpResponse(Func<IServiceProvider, HttpResponseMessage, ValueTask> handler)
        {
            OnHttpResponseAsync = OnHttpResponseAsync.Add(handler);
            return this;
        }

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
                    await DeferredRequestCreatedAsync.InvokeAllAsync(Services, this);

                    await OnHttpRequestAsync.InvokeAllAsync(Services, BaseRequest);
                    var http = _httpClientProvider.Get(BaseRequest.RequestUri);
                    BaseResponse = await http.SendAsync(BaseRequest, CancellationToken);
                    await OnHttpResponseAsync.InvokeAllAsync(Services, BaseResponse);
                }
                return BaseResponse;
            });
            return waiter(this).GetAwaiter();
        }


        public async ValueTask<T> As<T>(Func<IServiceProvider, HttpContent, ValueTask<T>> deserialize)
        {
            var response = await this;
            if (response.Content == null)
                return default;

            try
            {
                return await deserialize(Services, response.Content);
            }
            catch(Exception ex)
            {
                if (!Context.TryGetValue<bool>(Constants.IgnoreSerializationErrorKey, out var ignore) || !ignore) throw;
                
                // TODO: log error?
            }
            return default;
        }
    }
}
