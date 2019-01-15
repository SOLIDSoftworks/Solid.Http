using Solid.Http.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;

namespace Solid.Http.Abstractions
{
    public static class RequestExtensions
    {
        /// <summary>
        /// Adds StringContent containing a json string of the supplied body object
        /// </summary>
        /// <typeparam name="T">The type of body</typeparam>
        /// <param name="request">The SolidHttpRequest</param>
        /// <param name="body">The request body object</param>
        /// <param name="settings">(Optional) DataContractSerializerSettings to use to serialize the body object</param>
        /// <returns>SolidHttpRequest</returns>
        public static ISolidHttpRequest WithXmlContent<T>(this ISolidHttpRequest request, T body, DataContractSerializerSettings settings = null)
        {
            using (var ms = new MemoryStream())
            {
                var ser = new DataContractSerializer(typeof(T), settings ?? request.GetXmlSerializerSettings());
                ser.WriteObject(ms, body);
                ms.Position = 0;
                
                var content = new StringContent(new StreamReader(ms).ReadToEnd(), Encoding.UTF8, "application/xml");
                return request.WithContent(content);
            }
        }
    }
}
