using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Solid.Http.Xml
{
    internal static class SolidHttpRequestExtensions
    {
        public static DataContractSerializerSettings GetXmlSerializerSettings(this SolidHttpRequest request)
        {
            return request.BaseRequest.Properties["XmlSerializerSettings"] as DataContractSerializerSettings;
        }
    }
}
