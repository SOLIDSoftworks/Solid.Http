using Microsoft.Extensions.DependencyInjection.Extensions;
using Solid.Http;
using Solid.Http.Xml;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for configuring <see cref="SolidHttpXmlOptions" /> on an <seealso cref="IServiceCollection" />.
    /// </summary>
    public static class Solid_Http_Xml_ServiceCollectionExtensions
    {
        /// <summary>
        /// Configures <see cref="SolidHttpXmlOptions" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> that is being extended.</param>
        /// <param name="configureOptions">A delegate that configures <see cref="SolidHttpXmlOptions" />.</param>
        /// <returns>The <see cref="IServiceCollection" /> so that additional calls can be chained.</returns>
        public static IServiceCollection ConfigureSolidHttpXml(this IServiceCollection services, Action<SolidHttpXmlOptions> configureOptions)
        {
            return services.Configure(configureOptions);
        }
    }
}
