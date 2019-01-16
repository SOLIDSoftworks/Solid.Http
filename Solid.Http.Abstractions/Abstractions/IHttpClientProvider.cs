using System;
using System.Net.Http;

namespace Solid.Http.Abstractions
{
    /// <summary>
    /// A provider that providers HttpClients
    /// </summary>
    public interface IHttpClientProvider
    {
        /// <summary>
        /// Gets an HttpClient using the request url
        /// </summary>
        /// <param name="url">The request url</param>
        /// <returns>An HttpClient</returns>
        HttpClient Get(Uri url);
    }
}
