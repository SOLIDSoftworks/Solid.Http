using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Solid.Http.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;

namespace Solid.Http
{
    public static class Solid_Http_Xml_SolidHttpRequestExtensions_Content
    {
        /// <summary>
        /// Adds StringContent containing a xml string of the supplied body object
        /// </summary>
        /// <typeparam name="T">The type of body</typeparam>
        /// <param name="request">The SolidHttpRequest</param>
        /// <param name="body">The request body object</param>
        /// <param name="settings">(Optional) DataContractSerializerSettings to use to serialize the body object</param>
        /// <returns>SolidHttpRequest</returns>
        public static ISolidHttpRequest WithXmlContent<T>(this ISolidHttpRequest request, T body, string contentType = "application/xml", DataContractSerializerSettings settings = null)
        {
            if (settings == null)
                settings = request.Services.GetService<IOptions<SolidHttpXmlOptions>>().Value.SerializerSettings;

            var stream = new MemoryStream();
            var serializer = new DataContractSerializer(typeof(T), settings);
            serializer.WriteObject(stream, body);
            stream.Position = 0;

            var content = new StreamContent(stream);
            content.Headers.ContentType = new MediaTypeHeaderValue(contentType)
            {
                CharSet = "utf-8"
            };
            return request.WithContent(content);
        }
    }
}
