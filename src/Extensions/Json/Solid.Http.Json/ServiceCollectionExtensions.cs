using Microsoft.Extensions.DependencyInjection.Extensions;
using Solid.Http;
using Solid.Http.Json;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for configuring <see cref="SolidHttpJsonOptions" /> on an <seealso cref="IServiceCollection" />.
    /// </summary>
    public static class Solid_Http_Json_ServiceCollectionExtensions
    {
        /// <summary>
        /// Configures <see cref="SolidHttpJsonOptions" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> that is being extended.</param>
        /// <param name="configureOptions">A delegate that configures <see cref="SolidHttpJsonOptions" />.</param>
        /// <returns>The <see cref="IServiceCollection" /> so that additional calls can be chained.</returns>
        public static IServiceCollection ConfigureSolidHttpJson(this IServiceCollection services, Action<SolidHttpJsonOptions> configureOptions)
            => services.Configure(configureOptions);
    }
}
