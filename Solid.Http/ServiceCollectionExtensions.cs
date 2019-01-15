using Microsoft.Extensions.DependencyInjection;
using Solid.Http;
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
        public static IServiceCollection AddSolidHttp(this IServiceCollection services) => services.AddSolidHttp(_ => { });

        /// <summary>
        /// Add SolidHttp to the service collection using the default implementation of IHttpClientFactory
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <returns>ISolidHttpBuilder</returns>
        public static IServiceCollection AddSolidHttp(this IServiceCollection services, Action<ISolidHttpBuilder> action)
        {
            var builder = null as ISolidHttpCoreBuilder;
            services.AddSolidHttpCore(b => builder = b);
            builder.AddJson();
            action(builder);
            return services;
        }
    }
}
