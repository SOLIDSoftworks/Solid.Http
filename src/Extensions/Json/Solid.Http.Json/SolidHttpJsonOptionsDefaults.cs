using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Solid.Http.Json
{
    /// <summary>
    /// Default values for <see cref="SolidHttpJsonOptions" />.
    /// </summary>
    public static class SolidHttpJsonOptionsDefaults
    {
        /// <summary>
        /// Default <see cref="JsonSerializerOptions" />.
        /// </summary>
        public static JsonSerializerOptions SerializerOptions => CreateDefaultJsonSerializerOptions();

        /// <summary>
        /// Default supported JSON media types.
        /// </summary>
        public static List<MediaTypeHeaderValue> SupportedMediaTypes => CreateDefaultSupportedMediaTypes();

        private static List<MediaTypeHeaderValue> CreateDefaultSupportedMediaTypes()
        {
            return new List<MediaTypeHeaderValue>
            {
                MediaTypeHeaderValue.Parse("application/json"),
                MediaTypeHeaderValue.Parse("text/json")
            };
        }

        private static JsonSerializerOptions CreateDefaultJsonSerializerOptions()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return options;
        }
    }
}
