using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentHttp
{
    public static class ConnectionStringExtensions
    {
        public static FluentHttpClient CreateUsingConnectionString(this IFluentHttpClientFactory factory, string connectionStringName)
        {
            var baseAddress = factory.Configuration.GetConnectionString(connectionStringName);
            return factory.CreateWithBaseAddress(baseAddress);
        }
    }
}
