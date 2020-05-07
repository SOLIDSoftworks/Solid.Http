using Microsoft.Extensions.DependencyInjection.Extensions;
using Solid.Http;
using Solid.Http.Zip;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Solid_Http_Json_SolidHttpBuilderExtensions
    {
        public static SolidHttpBuilder AddZip(this SolidHttpBuilder builder)
        {
            builder.Services.TryAddSingleton<ZipArchiveDeserializer>();
            builder.AddDeserializer(p => p.GetService<ZipArchiveDeserializer>());
            return builder;
        }

        public static SolidHttpBuilder AddZip(this SolidHttpBuilder builder, Action<SolidHttpZipOptions> configureOptions)
        {
            builder.AddZip();
            builder.Services.ConfigureSolidHttpZip(configureOptions);
            return builder;
        }
    }
}
