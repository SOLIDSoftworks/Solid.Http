using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Solid.Http.Abstractions;
using Solid.Http.Json;
using Solid.Http.Json.Abstraction;
using Solid.Http.Json.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension class to add Json support
    /// </summary>
    public static class SolidHttpJsonBuilderExtensions
    {
        /// <summary>
        /// Adds json support using supplied settings
        /// <para>Can create a deserializer for application/json, text/json, and text/javascript</para>
        /// </summary>
        /// <param name="builder">The builder</param>
        /// <param name="settings">Supplied JsonSerializerSettings</param>
        /// <returns>ISolidHttpCoreBuilder</returns>
        public static ISolidHttpCoreBuilder AddJson(this ISolidHttpCoreBuilder builder, JsonSerializerSettings settings)
        {
            var provider = new JsonSerializerSettingsProvider(settings);
            builder.Services.AddSingleton<IJsonSerializerSettingsProvider>(provider);
            builder.AddDeserializer<JsonResponseDeserializerFactory>("application/json", "text/json", "text/javascript");

            return builder
                .OnRequestCreated((services, request) =>
                {
                    var p = services.GetRequiredService<IJsonSerializerSettingsProvider>();
                    request.BaseRequest.Properties.Add("JsonSerializerSettings", p.GetJsonSerializerSettings());
                });
        }

        /// <summary>
        /// Adds json support using default settings
        /// </summary>
        /// <param name="builder">The builder</param>
        /// <returns>ISolidHttpCoreBuilder</returns>
        public static ISolidHttpCoreBuilder AddJson(this ISolidHttpCoreBuilder builder)
        {
            var settings = new JsonSerializerSettings();
            return builder.AddJson(settings);
        }
    }
}
