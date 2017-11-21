using Microsoft.Extensions.DependencyInjection;
using SolidHttp.Abstractions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SolidHttp.Events
{
    internal class SolidHttpEventInvoker : ISolidHttpEventInvoker
    {
        private ISolidHttpEventHandlerProvider _events;
        private IServiceProvider _scope;

        public SolidHttpEventInvoker(ISolidHttpEventHandlerProvider events, IServiceProvider scope)
        {
            _events = events;
            _scope = scope;
        }

        public void InvokeOnClientCreated(object invoker, SolidHttpClient client)
        {
            var args = CreateArgs(client);
            foreach (var handler in _events.GetOnClientCreatedEventHandlers())
                handler(invoker, args);
        }

        public void InvokeOnRequest(object invoker, HttpRequestMessage request)
        {
            var args = CreateArgs(request);
            foreach (var handler in _events.GetOnRequestEventHandlers())
                handler(invoker, args);
        }

        public void InvokeOnRequestCreated(object invoker, SolidHttpRequest request)
        {
            var args = CreateArgs(request);
            foreach (var handler in _events.GetOnRequestCreatedEventHandlers())
                handler(invoker, args);
        }

        public void InvokeOnResponse(object invoker, HttpResponseMessage response)
        {
            var args = CreateArgs(response);
            foreach (var handler in _events.GetOnResponseEventHandlers())
                handler(invoker, args);
        }

        public RequestEventArgs CreateArgs(HttpRequestMessage request)
        {
            return new RequestEventArgs { Request = request, Services = _scope };
        }

        public ResponseEventArgs CreateArgs(HttpResponseMessage response)
        {
            return new ResponseEventArgs { Response = response, Services = _scope };
        }

        public SolidHttpClientCreatedEventArgs CreateArgs(SolidHttpClient client)
        {
            return new SolidHttpClientCreatedEventArgs { Client = client, Services = _scope };
        }

        public SolidHttpRequestCreatedEventArgs CreateArgs(SolidHttpRequest request)
        {
            return new SolidHttpRequestCreatedEventArgs { Request = request, Services = _scope };
        }
    }
}
