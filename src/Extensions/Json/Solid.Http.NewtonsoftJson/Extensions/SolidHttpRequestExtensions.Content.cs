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
    /// <summary>
    /// Extension methods for adding JSON content to <see cref="ISolidHttpRequest" />.
    /// </summary>
    public static class Solid_Http_NewtonsoftJson_SolidHttpRequestExtensions_Content
    {
        /// <summary>
        /// Adds <see cref="StringContent" /> containing a json string of type <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">The type of body</typeparam>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="body">The request body object of type <typeparamref name="T" />.</param>
        /// <param name="contentType">(Optional) The content type header value.</param>
        /// <param name="settings">(Optional) <see cref="JsonSerializerSettings" /> to use to serialize the <paramref name="body" />..</param>
        /// <returns>The <see cref="ISolidHttpRequest" /> so that additional calls can be chained.</returns>
        public static ISolidHttpRequest WithNewtonsoftJsonContent<T>(this ISolidHttpRequest request, T body, string contentType = "application/json", JsonSerializerSettings settings = null)
        {
            if (settings == null)
                settings = request.Services.GetService<IOptions<SolidHttpNewtonsoftJsonOptions>>().Value.SerializerSettings;

            var json = JsonConvert.SerializeObject(body, settings);
            var content = new StringContent(json, Encoding.UTF8, contentType);
            return request.WithContent(content);
        }
    }
}
