using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Solid.Http.NewtonsoftJson
{
    public static class SolidHttpNewtonsoftJsonOptionsDefaults
    {
        public static readonly JsonSerializerSettings SerializerOptions = CreateDefaultJsonSerializerSettings();

        public static readonly List<MediaTypeHeaderValue> SupportedMediaTypes = CreateDefaultSupportedMediaTypes();

        private static List<MediaTypeHeaderValue> CreateDefaultSupportedMediaTypes()
        {
            return new List<MediaTypeHeaderValue>
            {
                MediaTypeHeaderValue.Parse("application/json"),
                MediaTypeHeaderValue.Parse("text/json")
            };
        }

        private static JsonSerializerSettings CreateDefaultJsonSerializerSettings()
        {
            var settings = new JsonSerializerSettings
            {
            };

            return settings;
        }
    }
}
