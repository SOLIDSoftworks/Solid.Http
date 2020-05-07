using Solid.Http;
using Solid.Http.Json;
using System;


namespace Microsoft.Extensions.DependencyInjection
{
    public static class Solid_Http_Json_SolidHttpBuilderExtensions
    {
        public static SolidHttpBuilder AddJson(this SolidHttpBuilder builder)
        {
            builder.AddJsonDeserializer<SystemTextJsonDeserializer>();
            return builder;
        }

        public static SolidHttpBuilder AddJson(this SolidHttpBuilder builder, Action<SolidHttpJsonOptions> configureOptions)
        {
            builder.AddJson();
            builder.Services.ConfigureSolidHttpJson(configureOptions);
            return builder;
        }
    }
}
