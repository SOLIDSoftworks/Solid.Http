using Solid.Http.Abstractions;
using Solid.Http.Factories;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Solid.Http.Providers
{
    /// <summary>
    /// Abstract HttpClientProvider which tries to use IHttpClientFactory to create HttpClient instances
    /// </summary>
    public abstract class HttpClientProvider : IHttpClientProvider
    {
        /// <summary>
        /// The HttpClientFactory used by the provider
        /// </summary>
        public IHttpClientFactory Factory { get; }

        /// <summary>
        /// Create an HttpClientProvider
        /// </summary>
        /// <param name="factory">The optional injected IHttpClientFactory. If nothing is injected, a simple implementation is used.</param>
        public HttpClientProvider(IHttpClientFactory factory)
        {
            Factory = factory ?? new FauxHttpClientFactory();
        }
        /// <summary>
        /// Gets an HttpClient from an IHttpClientFactory using the request url
        /// </summary>
        /// <param name="url">The request url</param>
        /// <returns>An HttpClient</returns>
        public HttpClient Get(Uri url)
        {
            var name = GenerateHttpClientName(url);
            return Factory.CreateClient(name);
        }
        /// <summary>
        /// Generates a name for the HttpClient being requested by using the request url
        /// </summary>
        /// <param name="url">The request url</param>
        /// <returns>A generated name</returns>
        protected abstract string GenerateHttpClientName(Uri url);
    }
}
