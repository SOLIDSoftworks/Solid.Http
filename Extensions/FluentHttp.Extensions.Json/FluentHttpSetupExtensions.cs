using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentHttp
{
    public static class FluentHttpSetupExtensions
    {
        public static IFluentHttpSetup AddJson(this IFluentHttpSetup setup, JsonSerializerSettings settings)
        {
            DefaultSerializerSettingsProvider.SetDefaultSerializerSettings(settings);
            return setup.Configure(options =>
            {
                var deserializer = new JsonResponseDeserializerFactory(settings);
                options.Deserializers.AddDeserializerFactory(deserializer, "application/json", "text/json", "text/javascript");
            });
        }
        public static IFluentHttpSetup AddJson(this IFluentHttpSetup setup)
        {
            var settings = new JsonSerializerSettings();
            return setup.AddJson(settings);
        }
    }
}
