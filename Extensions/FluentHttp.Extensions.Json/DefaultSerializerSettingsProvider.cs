using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolidHttp
{
    internal static class DefaultSerializerSettingsProvider
    {
        private static JsonSerializerSettings _settings;
        public static JsonSerializerSettings DefaultSettings => _settings;
        public static void SetDefaultSerializerSettings(JsonSerializerSettings settings)
        {
            _settings = settings;
        }
    }
}
