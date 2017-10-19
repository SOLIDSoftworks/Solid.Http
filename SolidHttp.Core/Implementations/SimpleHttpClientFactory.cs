using System;
using System.Net.Http;

namespace SolidHttp
{
    /// <summary>
    /// Simple http client factory.
    /// </summary>
    public class SimpleHttpClientFactory : IHttpClientFactory
    {
        /// <summary>
        /// Creates a new HttpClient
        /// </summary>
        /// <returns>HttpClient</returns>
        public HttpClient Create()
        {
            return new HttpClient();
        }
    }
}
