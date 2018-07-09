using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http.Serialization
{
    internal class Deserializer : Deserializer<IResponseDeserializerFactory>
    {
        public Deserializer(string mimeType, IResponseDeserializerFactory factory) 
            : base(mimeType, factory)
        {

        }
    }

    internal class Deserializer<TFactory> : IDeserializer
        where TFactory : IResponseDeserializerFactory
    {
        private string _mimeType;
        private TFactory _factory;

        public Deserializer(string mimeType, TFactory factory)
        {
            _mimeType = mimeType;
            _factory = factory;
        }

        public bool CanDeserialize(string mimeType)
        {
            if (string.IsNullOrEmpty(mimeType)) return false;
            return mimeType.StartsWith(_mimeType, StringComparison.OrdinalIgnoreCase);
        }

        public Task<T> DeserializeAsync<T>(HttpContent content)
        {
            var deserializer = _factory.CreateDeserializer<T>();
            return deserializer(content);
        }
    }
}
