using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;

namespace Solid.Http.Xml
{
    /// <summary>
    /// Options for configuring Solid.Http.Xml.
    /// </summary>
    public class SolidHttpXmlOptions
    {
        /// <summary>
        /// The supported XML media types.
        /// </summary>
        public List<MediaTypeHeaderValue> SupportedMediaTypes = SolidHttpXmlOptionsDefaults.SupportedMediaTypes;

        /// <summary>
        /// The <see cref="DataContractSerializerSettings" /> used for deserializing XML.
        /// </summary>
        public DataContractSerializerSettings SerializerSettings { get; set; } = SolidHttpXmlOptionsDefaults.SerializerSettings;
    }
}
