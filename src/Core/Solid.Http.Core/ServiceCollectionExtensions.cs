using Microsoft.Extensions.DependencyInjection.Extensions;
using Solid.Http;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for adding Solid.Http.Core to an <see cref="IServiceCollection" />.
    /// </summary>
    public static class Solid_Http_Core_ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds Solid.Http.Core to an <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add Solid.Http.Core to.</param>
        /// <returns>The <see cref="IServiceCollection" /> so that additional calls can be chained.</returns>
        public static IServiceCollection AddSolidHttpCore(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddLogging();
            services.TryAddSingleton<DeserializerProvider>();
            services.TryAddSingleton<ISolidHttpClientFactory, SolidHttpClientFactory>();
            services.TryAddTransient<IHttpClientProvider, HttpClientProvider>();
            services.TryAddTransient<SolidHttpClient>();
            services.TryAddTransient<SolidHttpRequest>();
            return services;
        }

        /// <summary>
        /// Adds Solid.Http.Core to an <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add Solid.Http.Core to.</param>
        /// <param name="buildAction">An action to run on <see cref="SolidHttpBuilder" />.</param>
        /// <returns>The <see cref="IServiceCollection" /> so that additional calls can be chained.</returns>
        public static IServiceCollection AddSolidHttpCore(this IServiceCollection services, Action<SolidHttpBuilder> buildAction)
        {
            services.ConfigureSolidHttp(buildAction);
            return services.AddSolidHttpCore();
        }

        /// <summary>
        /// Runs an action on <see cref="SolidHttpBuilder" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> that Solid.Http.Core has been, or will be, added to.</param>
        /// <param name="buildAction">An action to run on <see cref="SolidHttpBuilder" />.</param>
        /// <returns>The <see cref="IServiceCollection" /> so that additional calls can be chained.</returns>
        public static IServiceCollection ConfigureSolidHttp(this IServiceCollection services, Action<SolidHttpBuilder> buildAction)
        {
            var builder = new SolidHttpBuilder(services);
            buildAction(builder);
            return services;
        }
    }
}
