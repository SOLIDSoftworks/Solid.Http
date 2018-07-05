﻿using Solid.Http.Abstractions;
using Solid.Http.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace Solid.Http
{
    /// <summary>
    /// A SolidHttpClient that is used to perform create SolidHttpRequests. This class is designed be extended using extension methods.
    /// </summary>
    public class SolidHttpClient : ISolidHttpClient
    {
        private IServiceProvider _services;
        private List<Action<IServiceProvider, ISolidHttpRequest>> _requestCreatedHandlers;
        private IDictionary<string, object> _properties = new Dictionary<string, object>();

        /// <summary>
        /// Create a SolidHttpClient
        /// </summary>
        /// <param name="client">The inner HttpClient to be used</param>
        /// <param name="serializers">The deserializers supported by this SolidHttpClient</param>
        public SolidHttpClient(IServiceProvider services, IEnumerable<IDeserializer> deserializers)
        {
            _services = services;
            _requestCreatedHandlers = new List<Action<IServiceProvider, ISolidHttpRequest>>();
        }

        //internal ISolidHttpEvents Events { get; private set; }
        //internal HttpClient InnerClient { get; private set; }
        public IEnumerable<IDeserializer> Deserializers { get; }

        /// <summary>
        /// Adds a property to the client that can be used in extensions methods
        /// </summary>
        /// <typeparam name="T">The type of parameter</typeparam>
        /// <param name="key">The parameter key</param>
        /// <param name="value">The parameter value</param>
        public void AddProperty<T>(string key, T value)
        {
            // TODO: Check if key exists and throw meaningful error
            _properties.Add(key, value);
        }
        
        /// <summary>
        /// Gets a property from the client
        /// </summary>
        /// <typeparam name="T">The type of parameter</typeparam>
        /// <param name="key">The parameter key</param>
        /// <returns>The parameter</returns>
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

        /// <summary>
        /// The event triggered when a SolidHttpRequest is created
        /// </summary>
        //public event EventHandler<SolidHttpRequestCreatedEventArgs> OnRequestCreated;

        /// <summary>
        /// Perform an http request
        /// </summary>
        /// <param name="method">The http method for the request</param>
        /// <param name="url">The url to be requested</param>
        /// <param name="cancellationToken">The cancellation token for the request</param>
        /// <returns></returns>
        //public SolidHttpRequest PerformRequestAsync(HttpMethod method, Uri url, CancellationToken cancellationToken)
        //{
        //    var request = new SolidHttpRequest(method, url, cancellationToken);
        //    Events.InvokeOnRequestCreated(this, request);
        //    if (OnRequestCreated != null)
        //        OnRequestCreated(this, Events.CreateArgs(request));
        //    return request;
        //}

        ISolidHttpRequest ISolidHttpClient.PerformRequestAsync(HttpMethod method, Uri url, CancellationToken cancellationToken)
        {
            var request = new SolidHttpRequest(this, _services, method, url, cancellationToken);
            foreach(var handler in _requestCreatedHandlers)
                handler(_services, request);
            return request;
        }


        void ISolidHttpClient.OnRequestCreated(Action<IServiceProvider, ISolidHttpRequest> handler)
        {
            _requestCreatedHandlers.Add(handler);
        }
    }
}
