using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Threading.Tasks;

namespace FluentHttp
{
    public class SerializerProvider : IDeserializerProvider
    {
        private static ConcurrentDictionary<string, IResponseDeserializer> _serializers = new ConcurrentDictionary<string, IResponseDeserializer>(StringComparer.OrdinalIgnoreCase);
        public static SerializerProvider Instance => new SerializerProvider();

        public void AddDeserializer(IResponseDeserializer serializer, string mimeType, params string[] more)
        {
            AddDeserializer(mimeType, serializer);
            foreach(var type in more)
            {
                AddDeserializer(type, serializer);
            }
        }

        public Func<HttpContent, Task<T>> GetDeserializer<T>(string mimeType)
        {
            var serializer = null as IResponseDeserializer;
            if (!_serializers.TryGetValue(mimeType, out serializer))
                return null;
            return serializer.CreateDeserializer<T>();
        }

        private void AddDeserializer(string mimeType, IResponseDeserializer serializer)
        {
            _serializers.AddOrUpdate(mimeType, serializer, (k, v) => serializer);
        }
    }
}
