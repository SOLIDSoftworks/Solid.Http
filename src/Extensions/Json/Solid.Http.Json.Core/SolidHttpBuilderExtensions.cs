using Solid.Http;
using Solid.Http.Json;
using Solid.Http.Json.Core.Abstractions;
using System;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions methods for adding JSOn deserializers to the <see cref="SolidHttpBuilder" />.
    /// </summary>
    public static class Solid_Http_NewtonsoftJson_SolidHttpBuilderExtensions
    {
        /// <summary>
        /// Adds a JSON deserializer to the <see cref="SolidHttpBuilder" />.
        /// </summary>
        /// <param name="builder">The <see cref="SolidHttpBuilder" /> being extended.</param>
        /// <typeparam name="TDeserializer">The type of JSON deserializer.</typeparam>
        /// <returns>The <see cref="SolidHttpBuilder" /> so that additional calls can be chained.</returns>
        public static SolidHttpBuilder AddJsonDeserializer<TDeserializer>(this SolidHttpBuilder builder)
            where TDeserializer : class, IJsonDeserializer
        {
            builder.Services.RemoveAll<IJsonDeserializer>();
            builder.Services.AddSingleton<IJsonDeserializer, TDeserializer>();
            builder.AddDeserializer(p => p.GetService<IJsonDeserializer>());
            return builder;
        }
    }
}
