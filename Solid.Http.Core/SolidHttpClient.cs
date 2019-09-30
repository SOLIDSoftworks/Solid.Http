using Solid.Http.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Solid.Http.Events;

namespace Solid.Http
{
    /// <summary>
    /// A SolidHttpClient that is used to perform create SolidHttpRequests. This class is designed be extended using extension methods.
    /// </summary>
    internal class SolidHttpClient : ISolidHttpClient
    {
        private IServiceProvider _services;
        private Action<IServiceProvider, ISolidHttpRequest> _onRequestCreated;
        private IDictionary<string, object> _properties = new Dictionary<string, object>();

        public SolidHttpClient(IServiceProvider services, IEnumerable<IDeserializer> deserializers, SolidEventHandler<ISolidHttpRequest> onRequestCreated)
        {
            Deserializers = deserializers;

            _services = services;
            _onRequestCreated += onRequestCreated.Handler ?? onRequestCreated.Noop;
        }

        public IEnumerable<IDeserializer> Deserializers { get; }

        public void AddProperty<T>(string key, T value)
        {
            // TODO: Check if key exists and throw meaningful error
            _properties.Add(key, value);
        }
        
        public T GetProperty<T>(string key)
        {
            if (!_properties.ContainsKey(key)) return default(T);
            var value = _properties[key];
            if (value == null) return default(T);

            var requestedType = typeof(T);
            var actualType = value.GetType();

            if (!requestedType.IsAssignableFrom(actualType))
                throw new InvalidCastException($"Cannot get property '{key}' as type '{requestedType.FullName}' because it is of type '{actualType.FullName}");

            return (T)value;
        }

        public ISolidHttpClient OnRequestCreated(Action<IServiceProvider, ISolidHttpRequest> handler)
        {
            _onRequestCreated += handler;
            return this;
        }

        public ISolidHttpRequest PerformRequestAsync(HttpMethod method, Uri url, CancellationToken cancellationToken)
        {
            var onRequest = _services.GetService<SolidAsyncEventHandler<HttpRequestMessage>>();
            var onResponse = _services.GetService<SolidAsyncEventHandler<HttpResponseMessage>>();
            var request = new SolidHttpRequest(this, _services, method, url, onRequest, onResponse, cancellationToken);
            Invoke(_onRequestCreated, request);
            return request;
        }

        private void Invoke<T>(Action<IServiceProvider, T> handler, T t)
        {
            var list = handler.GetInvocationList().Cast<Action<IServiceProvider, T>>();
            foreach (var action in list)
                action(_services, t);
        }
    }
}
