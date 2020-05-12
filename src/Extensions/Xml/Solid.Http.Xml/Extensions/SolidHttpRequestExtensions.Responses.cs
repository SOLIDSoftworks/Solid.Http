using Solid.Http.Xml;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace Solid.Http
{
    /// <summary>
    /// Extension methods for XML deserialization.
    /// </summary>
    public static class Solid_Http_Xml_SolidHttpRequestExtensions_Responses
    {
        /// <summary>
        /// Deserializes an XML <see cref="HttpContent" /> as <typeparamref name="T" /> using the 
        /// specified serializer settings.
        /// </summary>
        /// <typeparam name="T">The type of response body.</typeparam>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="settings">The specified <see cref="DataContractSerializerSettings" />.</param>
        /// <returns><see cref="ValueTask{T}" /> of type <typeparamref name="T" /></returns>
        public static ValueTask<T> As<T>(this ISolidHttpRequest request, DataContractSerializerSettings settings)
        {
            return request.As<T>((services, content) =>
            {
                var deserializer = services.GetService<DataContractXmlDeserializer>();
                return deserializer.DeserializeAsync<T>(content, settings); 
            });
        }

        /// <summary>
        /// Deserializes an XML <see cref="HttpContent" /> as an <seealso cref="IEnumerable{T}" /> 
        /// of type <typeparamref name="T" /> using the specified serializer settings.
        /// </summary>
        /// <typeparam name="T">The type of response body.</typeparam>
        /// <param name="request">The <see cref="ISolidHttpRequest" /> that is being extended.</param>
        /// <param name="settings">The specified <see cref="DataContractSerializerSettings" />.</param>
        /// <returns><see cref="ValueTask{T}" /> of type <seealso cref="IEnumerable{T}" /> of type <typeparamref name="T" />.</returns>
        public static ValueTask<IEnumerable<T>> AsMany<T>(this ISolidHttpRequest request, DataContractSerializerSettings settings)
            => request.As<IEnumerable<T>>(settings);
    }
}
