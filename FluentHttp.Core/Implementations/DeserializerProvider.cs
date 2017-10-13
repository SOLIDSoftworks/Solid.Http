using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Threading.Tasks;

namespace FluentHttp
{
    public class DeserializerProvider : IDeserializerProvider
    {
        private static ConcurrentDictionary<string, IResponseDeserializerFactory> _factories = new ConcurrentDictionary<string, IResponseDeserializerFactory>(StringComparer.OrdinalIgnoreCase);
        public static DeserializerProvider Instance => new DeserializerProvider();

        public void AddDeserializerFactory(IResponseDeserializerFactory factory, string mimeType, params string[] more)
        {
            AddDeserializerFactory(mimeType, factory);
            foreach(var type in more)
            {
                AddDeserializerFactory(type, factory);
            }
        }

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
