using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentHttp
{
    /// <summary>
    /// The IFluentHttpClientFactory interface
    /// </summary>
    public interface IFluentHttpClientFactory
    {
        /// <summary>
        /// The application configuration which can be used in extension methods
        /// </summary>
        IConfiguration Configuration { get; }

        /// <summary>
        /// Creates a FluentHttpClient
        /// </summary>
        /// <returns>FluentHttpClient</returns>
        FluentHttpClient Create();
    }
}
