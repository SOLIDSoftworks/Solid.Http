using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FluentHttp
{
    /// <summary>
    /// The ISerializedProvider interface
    /// </summary>
    public interface ISerializerProvider
    {
        /// <summary>
        /// Gets a deserializer
        /// </summary>
        /// <typeparam name="T">The type to deserialize to</typeparam>
        /// <param name="mimeType">The mimetype of the serialized content</param>
        /// <returns>The deserializer</returns>
        Func<HttpContent, Task<T>> GetSerializer<T>(string mimeType);

        /// <summary>
        /// Adds a serializer
        /// </summary>
        /// <param name="serializer">The serializer to add</param>
        /// <param name="mimeType">The mime type that this serializer knows how to deserialize</param>
        /// <param name="more">More mime types that this serializer knows how to deserialize</param>
        void AddSerializer(IFluentHttpSerializer serializer, string mimeType, params string[] more);
    }
}
