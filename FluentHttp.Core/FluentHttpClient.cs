using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace FluentHttp
{
    /// <summary>
    /// A FluentHttpClient that is used to perform create FluentHttpRequests. This class is designed be extended using extension methods.
    /// </summary>
    public class FluentHttpClient
    {
        private IDictionary<string, object> _properties = new Dictionary<string, object>();

        /// <summary>
        /// Create a FluentHttpClient
        /// </summary>
        /// <param name="client">The inner HttpClient to be used</param>
        /// <param name="serializers">The deserializers supported by this FluentHttpClient</param>
        public FluentHttpClient(HttpClient client, IDeserializerProvider deserializers)
        {
            InnerClient = client;
            Deserializers = deserializers;
        }

        internal HttpClient InnerClient { get; private set; }
        internal IDeserializerProvider Deserializers { get; private set; }

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
        /// The event triggered when a FluentHttpRequest is created
        /// </summary>
        public event EventHandler<FluentHttpRequestCreatedEventArgs> OnRequestCreated;

        /// <summary>
        /// Perform an http request
        /// </summary>
        /// <param name="method">The http method for the request</param>
        /// <param name="url">The url to be requested</param>
        /// <param name="cancellationToken">The cancellation token for the request</param>
        /// <returns></returns>
        public FluentHttpRequest PerformRequestAsync(HttpMethod method, Uri url, CancellationToken cancellationToken)
        {
            var request = new FluentHttpRequest(this, method, url, cancellationToken);
            if (OnRequestCreated != null)
                OnRequestCreated(this, new FluentHttpRequestCreatedEventArgs { Request = request });
            return request;
        }
    }
}
