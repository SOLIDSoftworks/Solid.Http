using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Threading.Tasks;

namespace FluentHttp
{
    public class SerializerProvider : ISerializerProvider
    {
        private static ConcurrentDictionary<string, IFluentHttpSerializer> _serializers = new ConcurrentDictionary<string, IFluentHttpSerializer>(StringComparer.OrdinalIgnoreCase);
        public static SerializerProvider Instance => new SerializerProvider();

        public void AddSerializer(IFluentHttpSerializer serializer, string mimeType, params string[] more)
        {
            AddSerializer(mimeType, serializer);
            foreach(var type in more)
            {
                AddSerializer(type, serializer);
            }
        }

        public Func<HttpContent, Task<T>> GetSerializer<T>(string mimeType)
        {
            var serializer = null as IFluentHttpSerializer;
            if (!_serializers.TryGetValue(mimeType, out serializer))
                return null;
            return serializer.CreateDeserializer<T>();
        }

        private void AddSerializer(string mimeType, IFluentHttpSerializer serializer)
        {
            _serializers.AddOrUpdate(mimeType, serializer, (k, v) => serializer);
        }
    }
}
