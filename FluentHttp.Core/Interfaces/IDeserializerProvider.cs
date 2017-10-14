using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SolidHttp
{
    /// <summary>
    /// The IDeserializerProvider interface
    /// </summary>
    public interface IDeserializerProvider
    {
        /// <summary>
        /// Gets a deserializer
        /// </summary>
        /// <typeparam name="T">The type to deserialize to</typeparam>
        /// <param name="mimeType">The mimetype of the serialized content</param>
        /// <returns>The deserializer</returns>
        Func<HttpContent, Task<T>> GetDeserializer<T>(string mimeType);

        /// <summary>
        /// Adds a deserializer
        /// </summary>
        /// <param name="factory">The deserializer factory to add</param>
        /// <param name="mimeType">The mime type that this deserializer knows how to deserialize</param>
        /// <param name="more">More mime types that this deserializer knows how to deserialize</param>
        void AddDeserializerFactory(IResponseDeserializerFactory factory, string mimeType, params string[] more);
    }
}
