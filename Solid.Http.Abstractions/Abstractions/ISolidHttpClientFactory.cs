using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solid.Http.Abstractions
{
    /// <summary>
    /// The ISolidHttpClientFactory interface
    /// </summary>
    public interface ISolidHttpClientFactory : IDisposable
    {
        /// <summary>
        /// The application configuration which can be used in extension methods
        /// </summary>
        IConfiguration Configuration { get; }

        /// <summary>
        /// Creates a SolidHttpClient
        /// </summary>
        /// <returns>SolidHttpClient</returns>
        ISolidHttpClient Create();
    }
}
