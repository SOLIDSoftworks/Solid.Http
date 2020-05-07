using Solid.Http;
using Solid.Http.Json;
using Solid.Http.Json.Core.Abstractions;
using System;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Solid_Http_NewtonsoftJson_SolidHttpBuilderExtensions
    {
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
