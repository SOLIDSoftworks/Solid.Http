using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace FluentHttp
{
    public static class MethodExtensions
    {
        #region GET
        public static FluentHttpRequest GetAsync(this FluentHttpClient client, string url)
        {
            return client.GetAsync(new Uri(url));
        }

        public static FluentHttpRequest GetAsync(this FluentHttpClient client, Uri url)
        {
            return client.GetAsync(url, CancellationToken.None);
        }

        public static FluentHttpRequest GetAsync(this FluentHttpClient client, string url, CancellationToken cancellationToken)
        {
            return client.GetAsync(new Uri(url), cancellationToken);
        }

        public static FluentHttpRequest GetAsync(this FluentHttpClient client, Uri url, CancellationToken cancellationToken)
        {
            return client.PerformRequestAsync(HttpMethod.Get, url, cancellationToken);
        }
        #endregion GET

        #region POST
        public static FluentHttpRequest PostAsync(this FluentHttpClient client, string url)
        {
            return client.PostAsync(new Uri(url));
        }

        public static FluentHttpRequest PostAsync(this FluentHttpClient client, Uri url)
        {
            return client.PostAsync(url, CancellationToken.None);
        }

        public static FluentHttpRequest PostAsync(this FluentHttpClient client, string url, CancellationToken cancellationToken)
        {
            return client.PostAsync(new Uri(url), cancellationToken);
        }

        public static FluentHttpRequest PostAsync(this FluentHttpClient client, Uri url, CancellationToken cancellationToken)
        {
            return client.PerformRequestAsync(HttpMethod.Post, url, cancellationToken);
        }
        #endregion POST

        #region PUT
        public static FluentHttpRequest PutAsync(this FluentHttpClient client, string url)
        {
            return client.PutAsync(new Uri(url));
        }

        public static FluentHttpRequest PutAsync(this FluentHttpClient client, Uri url)
        {
            return client.PutAsync(url, CancellationToken.None);
        }

        public static FluentHttpRequest PutAsync(this FluentHttpClient client, string url, CancellationToken cancellationToken)
        {
            return client.PutAsync(new Uri(url), cancellationToken);
        }

        public static FluentHttpRequest PutAsync(this FluentHttpClient client, Uri url, CancellationToken cancellationToken)
        {
            return client.PerformRequestAsync(HttpMethod.Put, url, cancellationToken);
        }
        #endregion PUT

        #region PATCH
        public static FluentHttpRequest PatchAsync(this FluentHttpClient client, string url)
        {
            return client.PatchAsync(new Uri(url));
        }

        public static FluentHttpRequest PatchAsync(this FluentHttpClient client, Uri url)
        {
            return client.PatchAsync(url, CancellationToken.None);
        }

        public static FluentHttpRequest PatchAsync(this FluentHttpClient client, string url, CancellationToken cancellationToken)
        {
            return client.PatchAsync(new Uri(url), cancellationToken);
        }

        public static FluentHttpRequest PatchAsync(this FluentHttpClient client, Uri url, CancellationToken cancellationToken)
        {
            var patch = new HttpMethod("PATCH");
            return client.PerformRequestAsync(patch, url, cancellationToken);
        }
        #endregion PATCH

        #region DELETE
        public static FluentHttpRequest DeleteAsync(this FluentHttpClient client, string url)
        {
            return client.DeleteAsync(new Uri(url));
        }

        public static FluentHttpRequest DeleteAsync(this FluentHttpClient client, Uri url)
        {
            return client.DeleteAsync(url, CancellationToken.None);
        }

        public static FluentHttpRequest DeleteAsync(this FluentHttpClient client, string url, CancellationToken cancellationToken)
        {
            return client.DeleteAsync(new Uri(url), cancellationToken);
        }

        public static FluentHttpRequest DeleteAsync(this FluentHttpClient client, Uri url, CancellationToken cancellationToken)
        {
            return client.PerformRequestAsync(HttpMethod.Delete, url, cancellationToken);
        }
        #endregion DELETE
    }
}
