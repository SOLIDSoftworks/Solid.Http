using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace FluentHttp
{
    public class FluentHttpRequest
    {
        internal FluentHttpRequest(FluentHttpClient client, HttpMethod method, Uri url, CancellationToken cancellationToken)
        {
            Client = client;
            BaseRequest = new HttpRequestMessage(method, url);
            CancellationToken = cancellationToken;
        }

        internal FluentHttpClient Client { get; private set; }

        public HttpRequestMessage BaseRequest { get; private set; }
        public CancellationToken CancellationToken { get; private set; }

        public event EventHandler<RequestEventArgs> OnRequest;
        public event EventHandler<ResponseEventArgs> OnResponse;
        
        public TaskAwaiter<HttpResponseMessage> GetAwaiter()
        {
            Func<FluentHttpRequest, Task<HttpResponseMessage>> waiter = (async r =>
            {
                if (OnRequest != null)
                    OnRequest(this, new RequestEventArgs { Request = BaseRequest });
                var response = await Client.InnerClient.SendAsync(BaseRequest, CancellationToken);
                if (OnResponse != null)
                    OnResponse(this, new ResponseEventArgs { Response = response });
                return response;
            });
            return waiter(this).GetAwaiter();
        }
    }
}