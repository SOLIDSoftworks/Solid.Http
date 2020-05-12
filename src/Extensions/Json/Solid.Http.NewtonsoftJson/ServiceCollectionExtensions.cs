using Microsoft.Extensions.DependencyInjection.Extensions;
using Solid.Http;
using Solid.Http.NewtonsoftJson;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for configuring <see cref="SolidHttpNewtonsoftJsonOptions" /> on an <seealso cref="IServiceCollection" />.
    /// </summary>
    public static class Solid_Http_Json_ServiceCollectionExtensions
    {
        /// <summary>
        /// Configures <see cref="SolidHttpNewtonsoftJsonOptions" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> that is being extended.</param>
        /// <param name="configureOptions">A delegate that configures <see cref="SolidHttpNewtonsoftJsonOptions" />.</param>
        /// <returns>The <see cref="IServiceCollection" /> so that additional calls can be chained.</returns>
        public static IServiceCollection ConfigureSolidHttpNewtonsoftJson(this IServiceCollection services, Action<SolidHttpNewtonsoftJsonOptions> configureOptions)
            => services.Configure(configureOptions);
    }
}
