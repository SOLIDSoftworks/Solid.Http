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
    /// <summary>
    /// Extension methods for adding XML content to <see cref="ISolidHttpRequest" />.
    /// </summary>
    public static class Solid_Http_Xml_SolidHttpRequestExtensions_Content
    {
        /// <summary>
        /// Adds <see cref="StreamContent" /> containing UTF-8 serialized XML of type <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">The type of body</typeparam>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="body">The request body object of type <typeparamref name="T" />.</param>
        /// <param name="contentType">(Optional) The content type header value.</param>
        /// <param name="settings">(Optional) <see cref="DataContractSerializerSettings" /> to use to serialize the <paramref name="body" />.</param>
        /// <returns>The <see cref="ISolidHttpRequest" /> so that additional calls can be chained.</returns>
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
