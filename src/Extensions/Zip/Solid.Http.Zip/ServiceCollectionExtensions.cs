using Microsoft.Extensions.DependencyInjection.Extensions;
using Solid.Http;
using Solid.Http.Zip;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for configuring <see cref="SolidHttpZipOptions" /> on an <seealso cref="IServiceCollection" />.
    /// </summary>
    public static class Solid_Http_Zip_ServiceCollectionExtensions
    {
        /// <summary>
        /// Configures <see cref="SolidHttpZipOptions" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> that is being extended.</param>
        /// <param name="configureOptions">A delegate that configures <see cref="SolidHttpZipOptions" />.</param>
        /// <returns>The <see cref="IServiceCollection" /> so that additional calls can be chained.</returns>
        public static IServiceCollection ConfigureSolidHttpZip(this IServiceCollection services, Action<SolidHttpZipOptions> configureOptions)
            => services.Configure(configureOptions);
    }
}
