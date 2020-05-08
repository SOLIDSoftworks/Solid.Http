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
        private IHttpClientProvider _httpClientProvider;
        private Func<IServiceProvider, HttpRequestMessage, ValueTask> _onHttpRequestAsync;
        private Func<IServiceProvider, HttpResponseMessage, ValueTask> _onHttpResponseAsync;

        public SolidHttpRequest(IServiceProvider services, IHttpClientProvider httpClientProvider, IOptions<SolidHttpOptions> options)
        {
            Services = services;
            _httpClientProvider = httpClientProvider;
            //Context = new SolidHttpRequestContext();
            _onHttpRequestAsync = options.Value.OnHttpRequestAsync;
            _onHttpResponseAsync = options.Value.OnHttpResponseAsync;
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

        public ISolidHttpRequest OnHttpRequest(Func<IServiceProvider, HttpRequestMessage, ValueTask> handler)
        {
            _onHttpRequestAsync = _onHttpRequestAsync.Add(handler);
            return this;
        }
        public ISolidHttpRequest OnHttpResponse(Func<IServiceProvider, HttpResponseMessage, ValueTask> handler)
        {
            _onHttpResponseAsync = _onHttpResponseAsync.Add(handler);
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
                    await _onHttpRequestAsync.InvokeAllAsync(Services, BaseRequest);
                    var http = _httpClientProvider.Get(BaseRequest.RequestUri);
                    BaseResponse = await http.SendAsync(BaseRequest, CancellationToken);
                    await _onHttpResponseAsync.InvokeAllAsync(Services, BaseResponse);
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
