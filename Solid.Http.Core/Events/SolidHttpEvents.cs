using Microsoft.Extensions.DependencyInjection;
using Solid.Http.Abstractions;
using Solid.Http.Events;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Solid.Http.Events
{
    internal class SolidHttpEvents : ISolidHttpEvents, ISolidHttpEventHandlerProvider
    {
        private Dictionary<string, Action<object, SolidHttpClientCreatedEventArgs>> _clientCreatedEventHandlers = new Dictionary<string, Action<object, SolidHttpClientCreatedEventArgs>>();
        private Dictionary<string, Action<object, SolidHttpRequestCreatedEventArgs>> _requestCreatedEventHandlers = new Dictionary<string, Action<object, SolidHttpRequestCreatedEventArgs>>();
        private Dictionary<string, Action<object, RequestEventArgs>> _requestEventHandlers = new Dictionary<string, Action<object, RequestEventArgs>>();
        private Dictionary<string, Action<object, ResponseEventArgs>> _responseEventHandlers = new Dictionary<string, Action<object, ResponseEventArgs>>();

        public event EventHandler<SolidHttpClientCreatedEventArgs> OnClientCreated
        {
            add
            {
                var key = value.Method.Name;
                if (_clientCreatedEventHandlers.ContainsKey(key)) return;
                _clientCreatedEventHandlers.Add(key, (sender, args) => value(sender, args));
            }
            remove
            {
                var key = value.Method.Name;
                if (_clientCreatedEventHandlers.ContainsKey(key)) // this is experimental
                    _clientCreatedEventHandlers.Remove(key);
            }
        }

        public event EventHandler<SolidHttpRequestCreatedEventArgs> OnRequestCreated
        {
            add
            {
                var key = value.Method.Name;
                if (_requestCreatedEventHandlers.ContainsKey(key)) return;
                _requestCreatedEventHandlers.Add(key, (sender, args) => value(sender, args));
            }
            remove
            {
                var key = value.Method.Name;
                if (_requestCreatedEventHandlers.ContainsKey(key)) // this is experimental
                    _requestCreatedEventHandlers.Remove(key);
            }
        }
        public event EventHandler<RequestEventArgs> OnRequest
        {
            add
            {
                var key = value.Method.Name;
                if (_requestEventHandlers.ContainsKey(key)) return;
                _requestEventHandlers.Add(key, (sender, args) => value(sender, args));
            }
            remove
            {
                var key = value.Method.Name;
                if (_requestEventHandlers.ContainsKey(key)) // this is experimental
                    _requestEventHandlers.Remove(key);
            }
        }
        public event EventHandler<ResponseEventArgs> OnResponse
        {
            add
            {
                var key = value.Method.Name;
                if (_responseEventHandlers.ContainsKey(key)) return;
                _responseEventHandlers.Add(key, (sender, args) => value(sender, args));
            }
            remove
            {
                var key = value.Method.Name;
                if (_responseEventHandlers.ContainsKey(key)) // this is experimental
                    _responseEventHandlers.Remove(key);
            }
        }

        public IEnumerable<Action<object, SolidHttpClientCreatedEventArgs>> GetOnClientCreatedEventHandlers()
        {
            return _clientCreatedEventHandlers.Values;
        }

        public IEnumerable<Action<object, SolidHttpRequestCreatedEventArgs>> GetOnRequestCreatedEventHandlers()
        {
            return _requestCreatedEventHandlers.Values;
        }

        public IEnumerable<Action<object, RequestEventArgs>> GetOnRequestEventHandlers()
        {
            return _requestEventHandlers.Values;
        }

        public IEnumerable<Action<object, ResponseEventArgs>> GetOnResponseEventHandlers()
        {
            return _responseEventHandlers.Values;
        }
    }
}
