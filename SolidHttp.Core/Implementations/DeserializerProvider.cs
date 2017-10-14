using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Threading.Tasks;

namespace SolidHttp
{
    /// <summary>
    /// The DeserializerProvider
    /// </summary>
    public class DeserializerProvider : IDeserializerProvider
    {
        private static ConcurrentDictionary<string, IResponseDeserializerFactory> _factories = new ConcurrentDictionary<string, IResponseDeserializerFactory>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// The singleton instance of the DeserializerProvider
        /// </summary>
        public static DeserializerProvider Instance => new DeserializerProvider();

        /// <summary>
        /// Gets a deserializer
        /// </summary>
        /// <typeparam name="T">The type to deserialize to</typeparam>
        /// <param name="mimeType">The mimetype of the serialized content</param>
        /// <returns>The deserializer</returns>
        public void AddDeserializerFactory(IResponseDeserializerFactory factory, string mimeType, params string[] more)
        {
            AddDeserializerFactory(mimeType, factory);
            foreach(var type in more)
            {
                AddDeserializerFactory(type, factory);
            }
        }

        /// <summary>
        /// Adds a deserializer
        /// </summary>
        /// <param name="factory">The deserializer factory to add</param>
        /// <param name="mimeType">The mime type that this deserializer knows how to deserialize</param>
        /// <param name="more">More mime types that this deserializer knows how to deserialize</param>
        public Func<HttpContent, Task<T>> GetDeserializer<T>(string mimeType)
        {
            var factory = null as IResponseDeserializerFactory;
            if (!_factories.TryGetValue(mimeType, out factory))
                return null;
            return factory.CreateDeserializer<T>();
        }

        private void AddDeserializerFactory(string mimeType, IResponseDeserializerFactory factory)
        {
            _factories.AddOrUpdate(mimeType, factory, (k, v) => factory);
        }
    }
}
