using Newtonsoft.Json;
using SolidHttp.Json.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolidHttp.Json.Providers
{
    internal class JsonSerializerSettingsProvider : IJsonSerializerSettingsProvider
    {
        private static JsonSerializerSettings _settings;

        public JsonSerializerSettingsProvider(JsonSerializerSettings settings)
        {
            _settings = settings;
        }

        public JsonSerializerSettings GetJsonSerializerSettings()
        {
            return _settings;
        }
    }
}
