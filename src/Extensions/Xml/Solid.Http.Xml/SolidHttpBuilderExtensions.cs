using Microsoft.Extensions.DependencyInjection.Extensions;
using Solid.Http;
using Solid.Http.Xml;
using System;
using System.Runtime.Serialization;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for adding XML deserialization to <see cref="SolidHttpBuilder" />.
    /// </summary>
    public static class Solid_Http_Json_SolidHttpBuilderExtensions
    {
        /// <summary>
        /// Adds XML deserialization to <see cref="SolidHttpBuilder" />.
        /// <para>This XML deserialization uses <see cref="DataContractSerializer" />.</para>
        /// </summary>
        /// <param name="builder">The <see cref="SolidHttpBuilder" /> that is being extended.</param>
        /// <returns>The <see cref="SolidHttpBuilder" /> so that additional calls can be chained.</returns>
        public static SolidHttpBuilder AddXml(this SolidHttpBuilder builder)
        {
            builder.Services.TryAddSingleton<DataContractXmlDeserializer>();
            builder.AddDeserializer(p => p.GetService<DataContractXmlDeserializer>());
            return builder;
        }

        /// <summary>
        /// Adds XML deserialization to <see cref="SolidHttpBuilder" />.
        /// <para>This XML deserialization uses <see cref="DataContractSerializer" />.</para>
        /// </summary>
        /// <param name="builder">The <see cref="SolidHttpBuilder" /> that is being extended.</param>
        /// <param name="configureOptions">A delegate used to configure <see cref="SolidHttpXmlOptions" />.</param>
        /// <returns>The <see cref="SolidHttpBuilder" /> so that additional calls can be chained.</returns>
        public static SolidHttpBuilder AddXml(this SolidHttpBuilder builder, Action<SolidHttpXmlOptions> configureOptions)
        {
            builder.AddXml();
            builder.Services.ConfigureSolidHttpXml(configureOptions);
            return builder;
        }
    }
}
