using Microsoft.Extensions.DependencyInjection.Extensions;
using Solid.Http;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for adding Solid.Http.Core with Solid.Http.Json to an <see cref="IServiceCollection" />.
    /// </summary>
    public static class Solid_Http_Core_ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds Solid.Http to an <see cref="IServiceCollection" />.
        /// <para>Solid.Http includes Solid.Http.Core and Solid.Http.Json.</para>
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add Solid.Http to.</param>
        /// <returns>The <see cref="IServiceCollection" /> so that additional calls can be chained.</returns>
        public static IServiceCollection AddSolidHttp(this IServiceCollection services) =>
            services.AddSolidHttpCore(builder => builder.AddJson());

        /// <summary>
        /// Adds Solid.Http to an <see cref="IServiceCollection" />.
        /// <para>Solid.Http includes Solid.Http.Core and Solid.Http.Json.</para>
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add Solid.Http to.</param>
        /// <param name="buildAction">An action to run on <see cref="SolidHttpBuilder" />.</param>
        /// <returns>The <see cref="IServiceCollection" /> so that additional calls can be chained.</returns>
        public static IServiceCollection AddSolidHttp(this IServiceCollection services, Action<SolidHttpBuilder> buildAction)
            => services.AddSolidHttp().ConfigureSolidHttp(buildAction);
    }
}
