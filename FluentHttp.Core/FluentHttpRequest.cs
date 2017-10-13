using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace FluentHttp
{
    /// <summary>
    /// A FluentHttpRequest that is used to perform http requests. This class is designed be extended using extension methods.
    /// </summary>
    public class FluentHttpRequest
    {
        internal FluentHttpRequest(FluentHttpClient client, HttpMethod method, Uri url, CancellationToken cancellationToken)
        {
            Client = client;
            BaseRequest = new HttpRequestMessage(method, url);
            CancellationToken = cancellationToken;
        }

        internal FluentHttpClient Client { get; private set; }

        /// <summary>
        /// The base request that is sent
        /// </summary>
        public HttpRequestMessage BaseRequest { get; private set; }

        /// <summary>
        /// The cancellation token for the request
        /// </summary>
        public CancellationToken CancellationToken { get; private set; }

        /// <summary>
        /// The event triggered before an http request is sent
        /// </summary>
        public event EventHandler<RequestEventArgs> OnRequest;

        /// <summary>
        /// The event triggered after an http response is received
        /// </summary>
        public event EventHandler<ResponseEventArgs> OnResponse;
        
        /// <summary>
        /// The awaiter that enables a FluentHttpRequest to be awaited
        /// </summary>
        /// <returns>A TaskAwaiter for an HttpResponseMessage</returns>
        public TaskAwaiter<HttpResponseMessage> GetAwaiter()
        {
            Func<FluentHttpRequest, Task<HttpResponseMessage>> waiter = (async r =>
            {
                Client.Events.InvokeOnRequest(this, BaseRequest);
                if (OnRequest != null)
                    OnRequest(this, new RequestEventArgs { Request = BaseRequest });

                var response = await Client.InnerClient.SendAsync(BaseRequest, CancellationToken);

                Client.Events.InvokeOnResponse(this, response);
                if (OnResponse != null)
                    OnResponse(this, new ResponseEventArgs { Response = response });
                return response;
            });
            return waiter(this).GetAwaiter();
        }
    }
}