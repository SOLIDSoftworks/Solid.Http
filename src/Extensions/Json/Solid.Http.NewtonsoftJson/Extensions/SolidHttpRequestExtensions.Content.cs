using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Solid.Http.NewtonsoftJson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Solid.Http
{
    public static class Solid_Http_NewtonsoftJson_SolidHttpRequestExtensions_Content
    {
        /// <summary>
        /// Adds StringContent containing a json string of the supplied body object
        /// </summary>
        /// <typeparam name="T">The type of body</typeparam>
        /// <param name="request">The SolidHttpRequest</param>
        /// <param name="body">The request body object</param>
        /// <param name="contentType">(Optional) The content type header value</param>
        /// <param name="settings">(Optional) JsonSerializerSettings to use to serialize the body object</param>
        /// <returns>SolidHttpRequest</returns>
        public static ISolidHttpRequest WithJsonContent<T>(this ISolidHttpRequest request, T body, string contentType = "application/json", JsonSerializerSettings settings = null)
        {
            if (settings == null)
                settings = request.Services.GetService<IOptions<SolidHttpNewtonsoftJsonOptions>>().Value.SerializerSettings;

            var json = JsonConvert.SerializeObject(body, settings);
            var content = new StringContent(json, Encoding.UTF8, contentType);
            return request.WithContent(content);
        }
    }
}
