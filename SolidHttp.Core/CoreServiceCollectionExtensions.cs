using System;
using Microsoft.Extensions.DependencyInjection;
using Solid.Http.Abstractions;
using Solid.Http.Factories;

namespace Solid.Http
{
    /// <summary>
    /// Extensions method for the service collection
    /// </summary>
    public static class CoreServiceCollectionExtensions
    {
        /// <summary>
        /// Add SolidHttp to the service collection
        /// </summary>
        /// <typeparam name="TFactory">The custom IHttpClientFactory type</typeparam>
        /// <param name="services">The service collection</param>
        /// <returns>ISolidHttpSetup</returns>
        public static ISolidHttpCoreBuilder AddSolidHttpCore<TFactory>(this IServiceCollection services)
            where TFactory : class, IHttpClientFactory
        {
            var builder = new SolidHttpCoreBuilder<TFactory>(services);
            return builder;
        }

        /// <summary>
        /// Add SolidHttp to the service collection using the default implementation of IHttpClientFactory
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <returns>ISolidHttpSetup</returns>
        public static ISolidHttpCoreBuilder AddSolidHttpCore(this IServiceCollection services)
        {
            return services.AddSolidHttpCore<SimpleHttpClientFactory>();
        }

        /// <summary>
        /// Add SolidHttp to the service collection
        /// </summary>
        /// <typeparam name="TFactory">The custom IHttpClientFactory type</typeparam>
        /// <param name="services">The service collection</param>
        /// <returns>ISolidHttpSetup</returns>
        public static ISolidHttpCoreBuilder AddSolidHttpCore<TFactory>(this IServiceCollection services, Action<ISolidHttpOptions> configure)
            where TFactory : class, IHttpClientFactory
        {
            services.AddSingleton(configure);
            var builder = new SolidHttpCoreBuilder<TFactory>(services);
            return builder;
        }

        /// <summary>
        /// Add SolidHttp to the service collection using the default implementation of IHttpClientFactory
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <returns>ISolidHttpSetup</returns>
        public static ISolidHttpCoreBuilder AddSolidHttpCore(this IServiceCollection services, Action<ISolidHttpOptions> configure)
        {
            return services.AddSolidHttpCore<SimpleHttpClientFactory>(configure);
        }
    }
}
