using Microsoft.Extensions.DependencyInjection;
using Solid.Http.Abstractions;
using Solid.Http.Events;
using Solid.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http.Events
{
    internal class SolidHttpEvents : ISolidHttpEvents
    {
        private List<Action<IServiceProvider, ISolidHttpClient>> _clientCreatedHandlers = new List<Action<IServiceProvider, ISolidHttpClient>>();

        public IEnumerable<Action<IServiceProvider, ISolidHttpClient>> ClientCreatedHandlers => _clientCreatedHandlers.AsEnumerable();

        public void OnClientCreated(Action<IServiceProvider, ISolidHttpClient> handler)
        {
            _clientCreatedHandlers.Add(handler);
        }

        public void OnRequest(Action<IServiceProvider, HttpRequestMessage> handler)
        {
            OnRequest(handler.ToAsyncFunc());
        }

        public void OnRequest(Func<IServiceProvider, HttpRequestMessage, Task> handler)
        {
            OnRequestCreated((p, r) => r.OnRequest(handler));
        }

        public void OnRequestCreated(Action<IServiceProvider, ISolidHttpRequest> handler)
        {
            OnClientCreated((p, c) => c.OnRequestCreated(handler));
        }

        public void OnResponse(Action<IServiceProvider, HttpResponseMessage> handler)
        {
            OnResponse(handler.ToAsyncFunc());
        }

        public void OnResponse(Func<IServiceProvider, HttpResponseMessage, Task> handler)
        {
            OnRequestCreated((p, r) => r.OnResponse(handler));
        }
    }
}
