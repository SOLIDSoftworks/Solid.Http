using Microsoft.Extensions.DependencyInjection;
using Solid.Http.Abstractions;
using Solid.Http.Xml.Abstraction;
using Solid.Http.Xml.Providers;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Solid.Http.Xml
{
    public static class SolidHttpBuilderExtensions
    {
        /// <summary>
        /// Adds xml support using supplied settings
        /// <para>Can create a deserializer for application/xml and text/xml</para>
        /// </summary>
        /// <param name="builder">The setup</param>
        /// <param name="settings">Supplied XmlSerializerSettings</param>
        /// <returns>ISolidHttpSetup</returns>
        public static TBuilder AddXml<TBuilder>(this TBuilder builder, DataContractSerializerSettings settings)
            where TBuilder : class, ISolidHttpBuilder
        {
            var provider = new XmlSerializerSettingsProvider(settings);
            builder.Services.AddSingleton<IXmlSerializerSettingsProvider>(provider);
            builder.Services.AddSolidHttpDeserializer<XmlResponseDeserializerFactory>("application/xml", "text/xml");

            return builder
                .AddSolidHttpOptions(options =>
                {
                    options.Events.OnRequestCreated += (sender, args) =>
                    {
                        var p = args.Services.GetRequiredService<IXmlSerializerSettingsProvider>();
                        args.Request.BaseRequest.Properties.Add("XmlSerializerSettings", p.GetXmlSerializerSettings());
                    };
                }) as TBuilder;
        }

        /// <summary>
        /// Adds xml support using default settings
        /// </summary>
        /// <param name="builder">The builder</param>
        /// <returns>ISolidHttpSetup</returns>
        public static TBuilder AddXml<TBuilder>(this TBuilder builder)
            where TBuilder : class, ISolidHttpBuilder
        {
            var settings = new DataContractSerializerSettings();
            return builder.AddXml(settings);
        }
    }
}
