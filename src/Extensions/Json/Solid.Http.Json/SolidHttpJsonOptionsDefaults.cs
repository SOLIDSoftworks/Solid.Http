using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Solid.Http.Json
{
    public static class SolidHttpJsonOptionsDefaults
    {
        public static JsonSerializerOptions SerializerOptions => CreateDefaultJsonSerializerOptions();

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
