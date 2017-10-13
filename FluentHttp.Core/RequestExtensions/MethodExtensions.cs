using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace FluentHttp
{
    /// <summary>
    /// MethodExtensions
    /// </summary>
    public static class MethodExtensions
    {
        #region GET
        /// <summary>
        /// Performs a GET request
        /// </summary>
        /// <param name="client">The FluentHttpClient</param>
        /// <param name="url">The url to be requested</param>
        /// <returns>FluentHttpRequest</returns>
        public static FluentHttpRequest GetAsync(this FluentHttpClient client, string url)
        {
            return client.GetAsync(new Uri(url, UriKind.RelativeOrAbsolute));
        }

        /// <summary>
        /// Performs a GET request
        /// </summary>
        /// <param name="client">The FluentHttpClient</param>
        /// <param name="url">The url to be requested</param>
        /// <returns>FluentHttpRequest</returns>
        public static FluentHttpRequest GetAsync(this FluentHttpClient client, Uri url)
        {
            return client.GetAsync(url, CancellationToken.None);
        }

        /// <summary>
        /// Performs a GET request
        /// </summary>
        /// <param name="client">The FluentHttpClient</param>
        /// <param name="url">The url to be requested</param>
        /// <param name="cancellationToken">The cancellation token for the request</param>
        /// <returns>FluentHttpRequest</returns>
        public static FluentHttpRequest GetAsync(this FluentHttpClient client, string url, CancellationToken cancellationToken)
        {
            return client.GetAsync(new Uri(url, UriKind.RelativeOrAbsolute), cancellationToken);
        }

        /// <summary>
        /// Performs a GET request
        /// </summary>
        /// <param name="client">The FluentHttpClient</param>
        /// <param name="url">The url to be requested</param>
        /// <param name="cancellationToken">The cancellation token for the request</param>
        /// <returns>FluentHttpRequest</returns>
        public static FluentHttpRequest GetAsync(this FluentHttpClient client, Uri url, CancellationToken cancellationToken)
        {
            return client.PerformRequestAsync(HttpMethod.Get, url, cancellationToken);
        }
        #endregion GET

        #region POST
        /// <summary>
        /// Performs a POST request
        /// </summary>
        /// <param name="client">The FluentHttpClient</param>
        /// <param name="url">The url to be requested</param>
        /// <returns>FluentHttpRequest</returns>
        public static FluentHttpRequest PostAsync(this FluentHttpClient client, string url)
        {
            return client.PostAsync(new Uri(url, UriKind.RelativeOrAbsolute));
        }

        /// <summary>
        /// Performs a POST request
        /// </summary>
        /// <param name="client">The FluentHttpClient</param>
        /// <param name="url">The url to be requested</param>
        /// <returns>FluentHttpRequest</returns>
        public static FluentHttpRequest PostAsync(this FluentHttpClient client, Uri url)
        {
            return client.PostAsync(url, CancellationToken.None);
        }

        /// <summary>
        /// Performs a POST request
        /// </summary>
        /// <param name="client">The FluentHttpClient</param>
        /// <param name="url">The url to be requested</param>
        /// <param name="cancellationToken">The cancellation token for the request</param>
        /// <returns>FluentHttpRequest</returns>
        public static FluentHttpRequest PostAsync(this FluentHttpClient client, string url, CancellationToken cancellationToken)
        {
            return client.PostAsync(new Uri(url, UriKind.RelativeOrAbsolute), cancellationToken);
        }

        /// <summary>
        /// Performs a POST request
        /// </summary>
        /// <param name="client">The FluentHttpClient</param>
        /// <param name="url">The url to be requested</param>
        /// <param name="cancellationToken">The cancellation token for the request</param>
        /// <returns>FluentHttpRequest</returns>
        public static FluentHttpRequest PostAsync(this FluentHttpClient client, Uri url, CancellationToken cancellationToken)
        {
            return client.PerformRequestAsync(HttpMethod.Post, url, cancellationToken);
        }
        #endregion POST

        #region PUT
        /// <summary>
        /// Performs a PUT request
        /// </summary>
        /// <param name="client">The FluentHttpClient</param>
        /// <param name="url">The url to be requested</param>
        /// <returns>FluentHttpRequest</returns>
        public static FluentHttpRequest PutAsync(this FluentHttpClient client, string url)
        {
            return client.PutAsync(new Uri(url, UriKind.RelativeOrAbsolute));
        }

        /// <summary>
        /// Performs a PUT request
        /// </summary>
        /// <param name="client">The FluentHttpClient</param>
        /// <param name="url">The url to be requested</param>
        /// <returns>FluentHttpRequest</returns>
        public static FluentHttpRequest PutAsync(this FluentHttpClient client, Uri url)
        {
            return client.PutAsync(url, CancellationToken.None);
        }

        /// <summary>
        /// Performs a PUT request
        /// </summary>
        /// <param name="client">The FluentHttpClient</param>
        /// <param name="url">The url to be requested</param>
        /// <param name="cancellationToken">The cancellation token for the request</param>
        /// <returns>FluentHttpRequest</returns>
        public static FluentHttpRequest PutAsync(this FluentHttpClient client, string url, CancellationToken cancellationToken)
        {
            return client.PutAsync(new Uri(url, UriKind.RelativeOrAbsolute), cancellationToken);
        }

        /// <summary>
        /// Performs a PUT request
        /// </summary>
        /// <param name="client">The FluentHttpClient</param>
        /// <param name="url">The url to be requested</param>
        /// <param name="cancellationToken">The cancellation token for the request</param>
        /// <returns>FluentHttpRequest</returns>
        public static FluentHttpRequest PutAsync(this FluentHttpClient client, Uri url, CancellationToken cancellationToken)
        {
            return client.PerformRequestAsync(HttpMethod.Put, url, cancellationToken);
        }
        #endregion PUT

        #region PATCH
        /// <summary>
        /// Performs a PATCH request
        /// </summary>
        /// <param name="client">The FluentHttpClient</param>
        /// <param name="url">The url to be requested</param>
        /// <returns>FluentHttpRequest</returns>
        public static FluentHttpRequest PatchAsync(this FluentHttpClient client, string url)
        {
            return client.PatchAsync(new Uri(url, UriKind.RelativeOrAbsolute));
        }

        /// <summary>
        /// Performs a PATCH request
        /// </summary>
        /// <param name="client">The FluentHttpClient</param>
        /// <param name="url">The url to be requested</param>
        /// <returns>FluentHttpRequest</returns>
        public static FluentHttpRequest PatchAsync(this FluentHttpClient client, Uri url)
        {
            return client.PatchAsync(url, CancellationToken.None);
        }

        /// <summary>
        /// Performs a PATCH request
        /// </summary>
        /// <param name="client">The FluentHttpClient</param>
        /// <param name="url">The url to be requested</param>
        /// <param name="cancellationToken">The cancellation token for the request</param>
        /// <returns>FluentHttpRequest</returns>
        public static FluentHttpRequest PatchAsync(this FluentHttpClient client, string url, CancellationToken cancellationToken)
        {
            return client.PatchAsync(new Uri(url, UriKind.RelativeOrAbsolute), cancellationToken);
        }

        /// <summary>
        /// Performs a PATCH request
        /// </summary>
        /// <param name="client">The FluentHttpClient</param>
        /// <param name="url">The url to be requested</param>
        /// <param name="cancellationToken">The cancellation token for the request</param>
        /// <returns>FluentHttpRequest</returns>
        public static FluentHttpRequest PatchAsync(this FluentHttpClient client, Uri url, CancellationToken cancellationToken)
        {
            var patch = new HttpMethod("PATCH");
            return client.PerformRequestAsync(patch, url, cancellationToken);
        }
        #endregion PATCH

        #region DELETE
        /// <summary>
        /// Performs a DELETE request
        /// </summary>
        /// <param name="client">The FluentHttpClient</param>
        /// <param name="url">The url to be requested</param>
        /// <returns>FluentHttpRequest</returns>
        public static FluentHttpRequest DeleteAsync(this FluentHttpClient client, string url)
        {
            return client.DeleteAsync(new Uri(url, UriKind.RelativeOrAbsolute));
        }

        /// <summary>
        /// Performs a DELETE request
        /// </summary>
        /// <param name="client">The FluentHttpClient</param>
        /// <param name="url">The url to be requested</param>
        /// <returns>FluentHttpRequest</returns>
        public static FluentHttpRequest DeleteAsync(this FluentHttpClient client, Uri url)
        {
            return client.DeleteAsync(url, CancellationToken.None);
        }

        /// <summary>
        /// Performs a DELETE request
        /// </summary>
        /// <param name="client">The FluentHttpClient</param>
        /// <param name="url">The url to be requested</param>
        /// <param name="cancellationToken">The cancellation token for the request</param>
        /// <returns>FluentHttpRequest</returns>
        public static FluentHttpRequest DeleteAsync(this FluentHttpClient client, string url, CancellationToken cancellationToken)
        {
            return client.DeleteAsync(new Uri(url, UriKind.RelativeOrAbsolute), cancellationToken);
        }

        /// <summary>
        /// Performs a DELETE request
        /// </summary>
        /// <param name="client">The FluentHttpClient</param>
        /// <param name="url">The url to be requested</param>
        /// <param name="cancellationToken">The cancellation token for the request</param>
        /// <returns>FluentHttpRequest</returns>
        public static FluentHttpRequest DeleteAsync(this FluentHttpClient client, Uri url, CancellationToken cancellationToken)
        {
            return client.PerformRequestAsync(HttpMethod.Delete, url, cancellationToken);
        }
        #endregion DELETE
    }
}
