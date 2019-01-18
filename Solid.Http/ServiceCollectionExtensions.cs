using Microsoft.Extensions.DependencyInjection;
using Solid.Http;
using Solid.Http.Abstractions;
using Solid.Http.Json;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Service collection extension methods
    /// </summary>
    public static class ServiceCollectionExtensions
    {

        /// <summary>
        /// Add SolidHttp to the service collection using the default features (json)
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddSolidHttp(this IServiceCollection services) => services.AddSolidHttp(_ => { });

        /// <summary>
        /// Add SolidHttp to the service collection using the default features (json)
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="action">A configuration action</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddSolidHttp(this IServiceCollection services, Action<ISolidHttpBuilder> action)
        {
            var core = null as ISolidHttpCoreBuilder;
            services.AddSolidHttpCore(b => core = b);
            core.AddJson();
            var builder = new SolidHttpBuilder(core);
            action(builder);
            return services;
        }
    }
}
