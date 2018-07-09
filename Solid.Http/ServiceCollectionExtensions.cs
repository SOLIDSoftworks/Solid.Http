using Microsoft.Extensions.DependencyInjection;
using Solid.Http.Abstractions;
using Solid.Http.Json;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {

        /// <summary>
        /// Add SolidHttp to the service collection using the default implementation of IHttpClientFactory
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <returns>ISolidHttpBuilder</returns>
        public static ISolidHttpBuilder AddSolidHttp(this IServiceCollection services)
        {
            var core = services
                .AddSolidHttpCore();
            return new SolidHttpBuilder(core);
        }

        /// <summary>
        /// Add SolidHttp to the service collection using the default implementation of IHttpClientFactory
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <returns>ISolidHttpBuilder</returns>
        public static ISolidHttpBuilder AddSolidHttp(this IServiceCollection services, Action<ISolidHttpOptions> configure)
        {
            var core = services
                .AddSolidHttpCore(configure);
            return new SolidHttpBuilder(core);
        }
    }
}
