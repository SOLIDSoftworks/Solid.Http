using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;

namespace Solid.Http.Xml
{
    public static class SolidHttpXmlOptionsDefaults
    {
        public static DataContractSerializerSettings Settings => CreateDefaultSettings();

        public static List<MediaTypeHeaderValue> SupportedMediaTypes => CreateDefaultSupportedMediaTypes();

        private static DataContractSerializerSettings CreateDefaultSettings()
        {
            var settings = new DataContractSerializerSettings();
            return settings;
        }

        private static List<MediaTypeHeaderValue> CreateDefaultSupportedMediaTypes()
        {
            return new List<MediaTypeHeaderValue>
            {
                MediaTypeHeaderValue.Parse("application/xml"),
                MediaTypeHeaderValue.Parse("text/xml")
            };
        }
    }
}
