using Newtonsoft.Json;
using Solid.Http.Json.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solid.Http.Json.Providers
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
