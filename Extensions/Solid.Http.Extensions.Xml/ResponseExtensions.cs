using Solid.Http.Xml;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http.Abstractions
{
    /// <summary>
    /// Solid.Http request extenions methods
    /// </summary>
    public static class ResponseExtensions
    {
        /// <summary>
        /// Deserializes the response content as the specified type using the specified settings
        /// </summary>
        /// <typeparam name="T">The type of response body</typeparam>
        /// <param name="request">The SolidHttpRequest</param>
        /// <param name="settings">The specified DataContractSerializerSettings</param>
        /// <returns>Task of type T</returns>
        public static async Task<T> As<T>(this ISolidHttpRequest request, DataContractSerializerSettings settings)
        {
            var factory = new XmlResponseDeserializerFactory(settings);
            var deserialize = factory.CreateDeserializer<T>();
            return await request.As<T>(deserialize);
        }

        /// <summary>
        /// Deserializes the response content as the specified anonymous type using the specified settings
        /// </summary>
        /// <typeparam name="T">The type of resonse body</typeparam>
        /// <param name="request">The SolidHttpRequest</param>
        /// <param name="anonymous">The anonymous type</param>
        /// <param name="settings">The specified DataContractSerializerSettings</param>
        /// <returns>Task of type T</returns>
        public static Task<T> As<T>(this ISolidHttpRequest request, T anonymous, DataContractSerializerSettings settings)
        {
            return request.As<T>(settings);
        }

        /// <summary>
        /// Deserializes the response content as the specified type using the specified settings
        /// </summary>
        /// <typeparam name="T">The type of resonse body</typeparam>
        /// <param name="request">The SolidHttpRequest</param>
        /// <param name="settings">The specified JsonSerializerSettings</param>
        /// <returns>Task of type IEnumerable&lt;T&gt;</returns>
        public static Task<IEnumerable<T>> AsMany<T>(this ISolidHttpRequest request, DataContractSerializerSettings settings)
        {
            return request.As<IEnumerable<T>>(settings);
        }

        /// <summary>
        /// Deserializes the response content as the specified anonymous type using the specified settings
        /// </summary>
        /// <typeparam name="T">The type of resonse body</typeparam>
        /// <param name="request">The SolidHttpRequest</param>
        /// <param name="anonymous">The anonymous type</param>
        /// <param name="settings">The specified DataContractSerializerSettings</param>
        /// <returns>Task of type IEnumerable&lt;T&gt;</returns>
        public static Task<IEnumerable<T>> AsMany<T>(this ISolidHttpRequest request, T anonymous, DataContractSerializerSettings settings)
        {
            return request.As<IEnumerable<T>>(settings);
        }
    }
}
