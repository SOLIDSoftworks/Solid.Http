using Solid.Http.Events;
using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Solid.Http
{
    /// <summary>
    /// A SolidHttpRequest that is used to perform http requests. This class is designed be extended using extension methods.
    /// </summary>
    public class SolidHttpRequest
    {
        internal SolidHttpRequest(SolidHttpClient client, HttpMethod method, Uri url, CancellationToken cancellationToken)
        {
            Client = client;
            BaseRequest = new HttpRequestMessage(method, url);
            CancellationToken = cancellationToken;
        }

        internal SolidHttpClient Client { get; private set; }

        /// <summary>
        /// The base request that is sent
        /// </summary>
        public HttpRequestMessage BaseRequest { get; private set; }

        /// <summary>
        /// The response
        /// </summary>
        public HttpResponseMessage Response { get; private set; }

        /// <summary>
        /// The cancellation token for the request
        /// </summary>
        public CancellationToken CancellationToken { get; private set; }


        /// <summary>
        /// The event triggered before an http request is sent
        /// </summary>
        public event EventHandler<RequestEventArgs> OnRequest;

        private event EventHandler<ResponseEventArgs> _onResponse;
        /// <summary>
        /// The event triggered after an http response is received
        /// </summary>
        public event EventHandler<ResponseEventArgs> OnResponse
        {
            add
            {                
                _onResponse += value;
                if (Response != null)
                    value(this, Client.Events.CreateArgs(Response));
            }
            remove
            {
                _onResponse -= value;
            }
        }

        /// <summary>
        /// The awaiter that enables a SolidHttpRequest to be awaited
        /// </summary>
        /// <returns>A TaskAwaiter for an HttpResponseMessage</returns>
        public TaskAwaiter<HttpResponseMessage> GetAwaiter()
        {
            Func<SolidHttpRequest, Task<HttpResponseMessage>> waiter = (async r =>
            {
                if (Response == null)
                {
                    Client.Events.InvokeOnRequest(this, BaseRequest);
                    if (OnRequest != null)
                        OnRequest(this, Client.Events.CreateArgs(BaseRequest));

                    Response = await Client.InnerClient.SendAsync(BaseRequest, CancellationToken);

                    Client.Events.InvokeOnResponse(this, Response);
                    if (_onResponse != null)
                        _onResponse(this, Client.Events.CreateArgs(Response));
                }
                return Response;
            });
            return waiter(this).GetAwaiter();
        }
    }
}