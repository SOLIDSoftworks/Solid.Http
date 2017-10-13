using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace FluentHttp
{
    internal class FluentHttpEvents : IFluentHttpEventInvoker
    {
        public event EventHandler<FluentHttpClientCreatedEventArgs> OnClientCreated;
        public event EventHandler<FluentHttpRequestCreatedEventArgs> OnRequestCreated;
        public event EventHandler<RequestEventArgs> OnRequest;
        public event EventHandler<ResponseEventArgs> OnResponse;        

        public void InvokeOnClientCreated(object invoker, FluentHttpClient client)
        {
            if (OnClientCreated != null)
                OnClientCreated(invoker, new FluentHttpClientCreatedEventArgs { Client = client });
        }

        public void InvokeOnRequest(object invoker, HttpRequestMessage request)
        {
            if (OnRequest != null)
                OnRequest(invoker, new RequestEventArgs { Request = request });
        }

        public void InvokeOnRequestCreated(object invoker, FluentHttpRequest request)
        {
            if (OnRequestCreated != null)
                OnRequestCreated(invoker, new FluentHttpRequestCreatedEventArgs { Request = request });
        }

        public void InvokeOnResponse(object invoker, HttpResponseMessage response)
        {
            if (OnResponse != null)
                OnResponse(invoker, new ResponseEventArgs { Response = response });
        }
    }
}
