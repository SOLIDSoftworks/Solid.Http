using Solid.Http;
using Solid.Http.Json;
using Solid.Http.NewtonsoftJson;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Solid_Http_NewtonsoftJson_SolidHttpBuilderExtensions
    {
        public static SolidHttpBuilder AddNewtonsoftJson(this SolidHttpBuilder builder)
        {
            builder.AddDeserializer<NewtonsoftJsonDeserializer>();
            return builder;
        }

        public static SolidHttpBuilder AddNewtonsoftJson(this SolidHttpBuilder builder, Action<SolidHttpNewtonsoftJsonOptions> configureOptions)
        {
            builder.AddNewtonsoftJson();
            builder.Services.ConfigureSolidHttpNewtonsoftJson(configureOptions);
            return builder;
        }
    }
}
