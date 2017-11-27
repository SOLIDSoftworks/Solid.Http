using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Solid.Http.Abstractions;
using Solid.Http.Json.Abstraction;
using Solid.Http.Json.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solid.Http.Json
{
    /// <summary>
    /// Extension class to add Json support
    /// </summary>
    public static class SolidHttpBuilderExtensions
    {
        /// <summary>
        /// Adds json support using supplied settings
        /// <para>Can create a deserializer for application/json, text/json, and text/javascript</para>
        /// </summary>
        /// <param name="builder">The setup</param>
        /// <param name="settings">Supplied JsonSerializerSettings</param>
        /// <returns>ISolidHttpSetup</returns>
        public static TBuilder AddJson<TBuilder>(this TBuilder builder, JsonSerializerSettings settings)
            where TBuilder : class, ISolidHttpCoreBuilder
        {
            var provider = new JsonSerializerSettingsProvider(settings);
            builder.Services.AddSingleton<IJsonSerializerSettingsProvider>(provider);
            builder.Services.AddSolidHttpDeserializer<JsonResponseDeserializerFactory>("application/json", "text/json", "text/javascript");

            return builder
                .AddSolidHttpCoreOptions(options =>
                {
                    options.Events.OnRequestCreated += (sender, args) =>
                    {
                        var p = args.Services.GetRequiredService<IJsonSerializerSettingsProvider>();
                        args.Request.BaseRequest.Properties.Add("JsonSerializerSettings", p.GetJsonSerializerSettings());
                    };
                }) as TBuilder;
        }

        /// <summary>
        /// Adds json support using default settings
        /// </summary>
        /// <param name="builder">The builder</param>
        /// <returns>ISolidHttpSetup</returns>
        public static TBuilder AddJson<TBuilder>(this TBuilder builder)
            where TBuilder : class, ISolidHttpCoreBuilder
        {
            var settings = new JsonSerializerSettings();
            return builder.AddJson(settings);
        }
    }
}
