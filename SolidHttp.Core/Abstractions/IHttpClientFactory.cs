using System;
using System.Net.Http;

namespace SolidHttp.Abstractions
{
    /// <summary>
    /// Http client factory.
    /// </summary>
    public interface IHttpClientFactory
    {
        /// <summary>
        /// Creates an HttpClient
        /// </summary>
        /// <returns>HttpClient</returns>
        HttpClient Create();
    }
}
