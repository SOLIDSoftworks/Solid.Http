using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Solid.Http
{
    public static class Solid_Http_Json_SolidHttpRequestExtensions_Responses
    {
        /// <summary>
        /// Deserializes the response content as the specified type using the specified settings
        /// </summary>
        /// <typeparam name="T">The type of response body</typeparam>
        /// <param name="request">The SolidHttpRequest</param>
        /// <param name="options">The specified JsonSerializerOptions</param>
        /// <returns>Task of type T</returns>
        public static async ValueTask<T> As<T>(this ISolidHttpRequest request, JsonSerializerOptions options)
        {
            return await request.As<T>(async content =>
            {
                using (var stream = await content.ReadAsStreamAsync())
                {
                    return await JsonSerializer.DeserializeAsync<T>(stream, options);
                }
            });
        }

        /// <summary>
        /// Deserializes the response content as the specified type using the specified settings
        /// </summary>
        /// <typeparam name="T">The type of resonse body</typeparam>
        /// <param name="request">The SolidHttpRequest</param>
        /// <param name="options">The specified JsonSerializerOptions</param>
        /// <returns>Task of type IEnumerable&lt;T&gt;</returns>
        public static ValueTask<IEnumerable<T>> AsMany<T>(this ISolidHttpRequest request, JsonSerializerOptions options)
        {
            return request.As<IEnumerable<T>>(options);
        }
    }
}
