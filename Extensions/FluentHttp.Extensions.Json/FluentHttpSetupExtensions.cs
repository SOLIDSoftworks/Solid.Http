using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentHttp
{
    /// <summary>
    /// Extension class to add Json support
    /// </summary>
    public static class FluentHttpSetupExtensions
    {
        /// <summary>
        /// Adds json support using supplied settings
        /// <para>Can create a deserializer for application/json, text/json, and text/javascript</para>
        /// </summary>
        /// <param name="setup">The setup</param>
        /// <param name="settings">Supplied JsonSerializerSettings</param>
        /// <returns>IFluentHttpSetup</returns>
        public static IFluentHttpSetup AddJson(this IFluentHttpSetup setup, JsonSerializerSettings settings)
        {
            DefaultSerializerSettingsProvider.SetDefaultSerializerSettings(settings);
            return setup.Configure(options =>
            {
                var deserializer = new JsonResponseDeserializerFactory(settings);
                options.Deserializers.AddDeserializerFactory(deserializer, "application/json", "text/json", "text/javascript");
            });
        }

        /// <summary>
        /// Adds json support using default settings
        /// </summary>
        /// <param name="setup">The setup</param>
        /// <returns>IFluentHttpSetup</returns>
        public static IFluentHttpSetup AddJson(this IFluentHttpSetup setup)
        {
            var settings = new JsonSerializerSettings();
            return setup.AddJson(settings);
        }
    }
}
