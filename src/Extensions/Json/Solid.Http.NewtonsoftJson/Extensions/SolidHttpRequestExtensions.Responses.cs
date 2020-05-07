using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http
{
    public static class Solid_Http_NewtonsoftJson_SolidHttpRequestExtensions_Responses
    {
        /// <summary>
        /// Deserializes the response content as the specified type using the specified settings
        /// </summary>
        /// <typeparam name="T">The type of response body</typeparam>
        /// <param name="request">The SolidHttpRequest</param>
        /// <param name="settings">The specified JsonSerializerSettings</param>
        /// <returns>Task of type T</returns>
        public static async ValueTask<T> As<T>(this ISolidHttpRequest request, JsonSerializerSettings settings)
        {
            return await request.As<T>(async content =>
            {
                var json = await content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json, settings);
            });
        }

        /// <summary>
        /// Deserializes the response content as the specified anonymous type using the specified settings
        /// </summary>
        /// <typeparam name="T">The type of resonse body</typeparam>
        /// <param name="request">The SolidHttpRequest</param>
        /// <param name="anonymous">The anonymous type</param>
        /// <param name="settings">The specified JsonSerializerSettings</param>
        /// <returns>Task of type T</returns>
        public static ValueTask<T> As<T>(this ISolidHttpRequest request, T anonymous, JsonSerializerSettings settings) 
            => request.As<T>(settings);

        /// <summary>
        /// Deserializes the response content as the specified type using the specified settings
        /// </summary>
        /// <typeparam name="T">The type of resonse body</typeparam>
        /// <param name="request">The SolidHttpRequest</param>
        /// <param name="settings">The specified JsonSerializerSettings</param>
        /// <returns>Task of type IEnumerable&lt;T&gt;</returns>
        public static ValueTask<IEnumerable<T>> AsMany<T>(this ISolidHttpRequest request, JsonSerializerSettings settings) 
            => request.As<IEnumerable<T>>(settings);

        /// <summary>
        /// Deserializes the response content as the specified anonymous type using the specified settings
        /// </summary>
        /// <typeparam name="T">The type of resonse body</typeparam>
        /// <param name="request">The SolidHttpRequest</param>
        /// <param name="anonymous">The anonymous type</param>
        /// <param name="settings">The specified JsonSerializerSettings</param>
        /// <returns>Task of type IEnumerable&lt;T&gt;</returns>
        public static ValueTask<IEnumerable<T>> AsMany<T>(this ISolidHttpRequest request, T anonymous, JsonSerializerSettings settings) 
            => request.As<IEnumerable<T>>(settings);
    }
}
