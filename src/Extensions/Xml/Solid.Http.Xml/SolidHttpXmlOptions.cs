using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;

namespace Solid.Http.Xml
{
    public class SolidHttpXmlOptions
    {
        public List<MediaTypeHeaderValue> SupportedMediaTypes = SolidHttpXmlOptionsDefaults.SupportedMediaTypes;
        public DataContractSerializerSettings Settings { get; set; } = SolidHttpXmlOptionsDefaults.Settings;
    }
}
