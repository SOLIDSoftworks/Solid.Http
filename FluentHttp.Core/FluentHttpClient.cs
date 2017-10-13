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
        /// The base http address of this FluentHttpClient
        /// </summary>
        public Uri BaseAddress { get; internal set; }

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
            if (BaseAddress != null)
                url = new Uri(BaseAddress, url);

            var request = new FluentHttpRequest(this, method, url, cancellationToken);
            if (OnRequestCreated != null)
                OnRequestCreated(this, new FluentHttpRequestCreatedEventArgs { Request = request });
            return request;
        }
    }
}
