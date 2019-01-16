using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Solid.Http.Abstractions
{
    internal static class SolidHttpRequestExtensions
    {
        public static DataContractSerializerSettings GetXmlSerializerSettings(this ISolidHttpRequest request)
        {
            return request.BaseRequest.Properties["XmlSerializerSettings"] as DataContractSerializerSettings;
        }
    }
}
