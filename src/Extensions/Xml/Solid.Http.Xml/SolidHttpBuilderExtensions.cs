using Microsoft.Extensions.DependencyInjection.Extensions;
using Solid.Http;
using Solid.Http.Xml;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Solid_Http_Json_SolidHttpBuilderExtensions
    {
        public static SolidHttpBuilder AddXml(this SolidHttpBuilder builder)
        {
            builder.Services.TryAddSingleton<DataContractXmlDeserializer>();
            builder.AddDeserializer(p => p.GetService<DataContractXmlDeserializer>());
            return builder;
        }

        public static SolidHttpBuilder AddXml(this SolidHttpBuilder builder, Action<SolidHttpXmlOptions> configureOptions)
        {
            builder.AddXml();
            builder.Services.ConfigureSolidHttpXml(configureOptions);
            return builder;
        }
    }
}
