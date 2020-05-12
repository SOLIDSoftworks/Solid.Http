using Solid.Http;
using Solid.Http.Json;
using System;


namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for adding JSON deserialization to <see cref="SolidHttpBuilder" />.
    /// </summary>
    public static class Solid_Http_Json_SolidHttpBuilderExtensions
    {
        /// <summary>
        /// Adds JSON deserialization to <see cref="SolidHttpBuilder" />.
        /// <para>This JSON deserialization uses System.Text.Json.</para>
        /// </summary>
        /// <param name="builder">The <see cref="SolidHttpBuilder" /> that is being extended.</param>
        /// <returns>The <see cref="SolidHttpBuilder" /> so that additional calls can be chained.</returns>
        public static SolidHttpBuilder AddJson(this SolidHttpBuilder builder)
        {
            builder.AddJsonDeserializer<SystemTextJsonDeserializer>();
            return builder;
        }

        /// <summary>
        /// Adds JSON deserialization to <see cref="SolidHttpBuilder" />.
        /// <para>This JSON deserialization uses System.Text.Json.</para>
        /// </summary>
        /// <param name="builder">The <see cref="SolidHttpBuilder" /> that is being extended.</param>
        /// <param name="configureOptions">A delegate used to configure <see cref="SolidHttpJsonOptions" />.</param>
        /// <returns>The <see cref="SolidHttpBuilder" /> so that additional calls can be chained.</returns>
        public static SolidHttpBuilder AddJson(this SolidHttpBuilder builder, Action<SolidHttpJsonOptions> configureOptions)
        {
            builder.AddJson();
            builder.Services.ConfigureSolidHttpJson(configureOptions);
            return builder;
        }
    }
}
