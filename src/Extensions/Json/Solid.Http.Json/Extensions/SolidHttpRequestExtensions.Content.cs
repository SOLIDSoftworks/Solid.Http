using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Solid.Http.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Solid.Http
{
    public static class Solid_Http_Json_SolidHttpRequestExtensions_Content
    {
        /// <summary>
        /// Adds StringContent containing a json string of the supplied body object
        /// </summary>
        /// <typeparam name="T">The type of body</typeparam>
        /// <param name="request">The SolidHttpRequest</param>
        /// <param name="body">The request body object</param>
        /// <param name="contentType">(Optional) The content type header value</param>
        /// <param name="options">(Optional) JsonSerializerOptions to use to serialize the body object</param>
        /// <returns>SolidHttpRequest</returns>
        public static ISolidHttpRequest WithJsonContent<T>(this ISolidHttpRequest request, T body, string contentType = "application/json", JsonSerializerOptions options = null)
        {
            if (options == null)
                options = request.Services.GetService<IOptions<SolidHttpJsonOptions>>().Value.SerializerOptions;

            var bytes = JsonSerializer.SerializeToUtf8Bytes<T>(body, options);
            var stream = new MemoryStream(bytes);
            var content = new StreamContent(stream);
            content.Headers.ContentType = new MediaTypeHeaderValue(contentType)
            {
                CharSet = "utf-8"
            };
            return request.WithContent(content);
        }
    }
}
