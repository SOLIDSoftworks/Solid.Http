using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;

namespace Solid.Http.Xml
{
    /// <summary>
    /// Default values for <see cref="SolidHttpXmlOptions" />.
    /// </summary>
    public static class SolidHttpXmlOptionsDefaults
    {
        /// <summary>
        /// Default <see cref="DataContractSerializerSettings" />.
        /// </summary>
        public static DataContractSerializerSettings SerializerSettings => CreateDefaultSettings();

        /// <summary>
        /// Default supported XML media types.
        /// </summary>
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
