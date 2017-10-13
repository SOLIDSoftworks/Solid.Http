using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentHttp
{
    /// <summary>
    /// Extensions to create a FluentHttpClient using a connection string
    /// </summary>
    public static class ConnectionStringExtensions
    {
        /// <summary>
        /// Creates a FluentHttpClient using a connection string
        /// </summary>
        /// <param name="factory">The IFluentHttpClientFactory</param>
        /// <param name="connectionStringName">The name of the connection string in the configuration file</param>
        /// <returns>FluentHttpClient</returns>
        public static FluentHttpClient CreateUsingConnectionString(this IFluentHttpClientFactory factory, string connectionStringName)
        {
            var baseAddress = factory.Configuration.GetConnectionString(connectionStringName);
            return factory.CreateWithBaseAddress(baseAddress);
        }
    }
}
