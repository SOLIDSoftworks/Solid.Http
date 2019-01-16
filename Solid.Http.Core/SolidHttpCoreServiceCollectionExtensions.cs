using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Solid.Http;
using Solid.Http.Abstractions;
using Solid.Http.Events;
using Solid.Http.Factories;
using Solid.Http.Providers;
using Solid.Http.Serialization;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions method for the service collection
    /// </summary>
    public static class SolidHttpCoreServiceCollectionExtensions
    {
        /// <summary>
        /// Add Solid.Http.Core to the service collection
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="action">Action to configure Solid.Http.Core</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddSolidHttpCore(this IServiceCollection services, Action<ISolidHttpCoreBuilder> action)
        {
            var builder = new SolidHttpBuilder
            {
                Services = services,
                Properties = new Dictionary<string, object>()
            };
            services.TryAddSingleton(new SolidEventHandler<ISolidHttpClient>());
            services.TryAddSingleton(new SolidEventHandler<ISolidHttpRequest>());
            services.TryAddSingleton(new SolidAsyncEventHandler<HttpRequestMessage>());
            services.TryAddSingleton(new SolidAsyncEventHandler<HttpResponseMessage>());
            services.TryAddTransient<ISolidHttpClient, SolidHttpClient>();
            services.TryAddSingleton<ISolidHttpClientFactory, SolidHttpClientFactory>();
            action(builder);
            services.TryAddSingleton<IHttpClientProvider, SingleInstanceHttpClientProvider>();
            return services;
        }

        /// <summary>
        /// Add Solid.Http.Core to the service collection
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddSolidHttpCore(this IServiceCollection services) =>
            services.AddSolidHttpCore(_ => { });
    }
}
