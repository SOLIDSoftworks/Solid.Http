using Solid.Http.Xml;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Solid.Http
{
    public static class Solid_Http_Xml_SolidHttpRequestExtensions_Responses
    {
        /// <summary>
        /// Deserializes the response content as the specified type using the specified settings
        /// </summary>
        /// <typeparam name="T">The type of response body</typeparam>
        /// <param name="request">The SolidHttpRequest</param>
        /// <param name="settings">The specified DataContractSerializerSettings</param>
        /// <returns>Task of type T</returns>
        public static ValueTask<T> As<T>(this ISolidHttpRequest request, DataContractSerializerSettings settings)
        {
            return request.As<T>((services, content) =>
            {
                var deserializer = services.GetService<DataContractXmlDeserializer>();
                return deserializer.DeserializeAsync<T>(content, settings); 
            });
        }

        /// <summary>
        /// Deserializes the response content as the specified type using the specified settings
        /// </summary>
        /// <typeparam name="T">The type of resonse body</typeparam>
        /// <param name="request">The SolidHttpRequest</param>
        /// <param name="settings">The specified JsonSerializerSettings</param>
        /// <returns>Task of type IEnumerable&lt;T&gt;</returns>
        public static ValueTask<IEnumerable<T>> AsMany<T>(this ISolidHttpRequest request, DataContractSerializerSettings settings)
        {
            return request.As<IEnumerable<T>>(settings);
        }
    }
}
