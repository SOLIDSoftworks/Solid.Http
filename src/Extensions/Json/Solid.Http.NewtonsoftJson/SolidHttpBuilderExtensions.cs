using Solid.Http;
using Solid.Http.Json;
using Solid.Http.NewtonsoftJson;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for adding JSON deserialization to <see cref="SolidHttpBuilder" />.
    /// </summary>
    public static class Solid_Http_NewtonsoftJson_SolidHttpBuilderExtensions
    {
        /// <summary>
        /// Adds JSON deserialization to <see cref="SolidHttpBuilder" />.
        /// <para>This JSON deserialization uses Newtonsoft.Json.</para>
        /// </summary>
        /// <param name="builder">The <see cref="SolidHttpBuilder" /> that is being extended.</param>
        /// <returns>The <see cref="SolidHttpBuilder" /> so that additional calls can be chained.</returns>
        public static SolidHttpBuilder AddNewtonsoftJson(this SolidHttpBuilder builder)
        {
            builder.AddJsonDeserializer<NewtonsoftJsonDeserializer>();
            return builder;
        }

        /// <summary>
        /// Adds JSON deserialization to <see cref="SolidHttpBuilder" />.
        /// <para>This JSON deserialization uses Newtonsoft.Json.</para>
        /// </summary>
        /// <param name="builder">The <see cref="SolidHttpBuilder" /> that is being extended.</param>
        /// <param name="configureOptions">A delegate used to configure <see cref="SolidHttpNewtonsoftJsonOptions" />.</param>
        /// <returns>The <see cref="SolidHttpBuilder" /> so that additional calls can be chained.</returns>
        public static SolidHttpBuilder AddNewtonsoftJson(this SolidHttpBuilder builder, Action<SolidHttpNewtonsoftJsonOptions> configureOptions)
        {
            builder.AddNewtonsoftJson();
            builder.Services.ConfigureSolidHttpNewtonsoftJson(configureOptions);
            return builder;
        }
    }
}
