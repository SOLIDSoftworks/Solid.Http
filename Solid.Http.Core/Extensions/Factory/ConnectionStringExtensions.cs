using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http.Abstractions
{
    /// <summary>
    /// Extensions to create a SolidHttpClient using a connection string
    /// </summary>
    public static class ConnectionStringExtensions
    {
        /// <summary>
        /// Creates a SolidHttpClient using a connection string
        /// </summary>
        /// <param name="factory">The ISolidHttpClientFactory</param>
        /// <param name="connectionStringName">The name of the connection string in the configuration file</param>
        /// <returns>SolidHttpClient</returns>
        public static ISolidHttpClient CreateUsingConnectionString(this ISolidHttpClientFactory factory, string connectionStringName)
        {
            var baseAddress = factory.Configuration.GetConnectionString(connectionStringName);
            return factory.CreateWithBaseAddress(baseAddress);
        }
    }
}
