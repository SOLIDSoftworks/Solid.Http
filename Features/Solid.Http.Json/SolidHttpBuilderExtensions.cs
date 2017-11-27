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
        public static ISolidHttpCoreBuilder AddJson(this ISolidHttpCoreBuilder builder, JsonSerializerSettings settings)
        {
            var provider = new JsonSerializerSettingsProvider(settings);
            builder.Services.AddSingleton<IJsonSerializerSettingsProvider>(provider);

            return builder
                .AddDeserializer<JsonResponseDeserializerFactory>("application/json", "text/json", "text/javascript")
                .AddSolidHttpCoreOptions(options =>
                {
                    options.Events.OnRequestCreated += (sender, args) =>
                    {
                        var p = args.Services.GetRequiredService<IJsonSerializerSettingsProvider>();
                        args.Request.BaseRequest.Properties.Add("JsonSerializerSettings", p.GetJsonSerializerSettings());
                    };
                });
        }

        /// <summary>
        /// Adds json support using default settings
        /// </summary>
        /// <param name="setup">The setup</param>
        /// <returns>ISolidHttpSetup</returns>
        public static ISolidHttpCoreBuilder AddJson(this ISolidHttpCoreBuilder setup)
        {
            var settings = new JsonSerializerSettings();
            return setup.AddJson(settings);
        }
    }
}
