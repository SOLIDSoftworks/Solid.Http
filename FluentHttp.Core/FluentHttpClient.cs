using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace FluentHttp
{
    public class FluentHttpClient
    {
        public FluentHttpClient(HttpClient client, ISerializerProvider serializers)
        {
            InnerClient = client;
            Serializers = serializers;
        }

		internal HttpClient InnerClient { get; private set; }
        internal ISerializerProvider Serializers { get; private set; }

        public Uri BaseAddress { get; internal set; }

        public event EventHandler<FluentHttpRequestCreatedEventArgs> OnRequestCreated;

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
