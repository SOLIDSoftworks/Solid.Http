using Newtonsoft.Json;
using SolidHttp.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolidHttp.Json
{
    /// <summary>
    /// Extension class to add Json support
    /// </summary>
    public static class SolidHttpSetupExtensions
    {
        /// <summary>
        /// Adds json support using supplied settings
        /// <para>Can create a deserializer for application/json, text/json, and text/javascript</para>
        /// </summary>
        /// <param name="setup">The setup</param>
        /// <param name="settings">Supplied JsonSerializerSettings</param>
        /// <returns>ISolidHttpSetup</returns>
        public static ISolidHttpCoreBuilder AddJson(this ISolidHttpCoreBuilder setup, JsonSerializerSettings settings)
        {
            DefaultSerializerSettingsProvider.SetDefaultSerializerSettings(settings);
            return setup.AddSolidHttpCoreOptions(options =>
            {
                var deserializer = new JsonResponseDeserializerFactory(settings);
                options.Deserializers.AddDeserializerFactory(deserializer, "application/json", "text/json", "text/javascript");
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
