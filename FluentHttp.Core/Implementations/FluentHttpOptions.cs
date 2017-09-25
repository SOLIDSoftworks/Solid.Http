using System;
namespace FluentHttp
{
    internal class FluentHttpOptions : IFluentHttpOptions
    {
        private ISerializerProvider _serializers;
        private IFluentHttpClientFactory _factory;
        public FluentHttpOptions(ISerializerProvider serializers, IFluentHttpClientFactory factory)
        {
            _serializers = serializers;
            _factory = factory;
        }

        public ISerializerProvider Serializers => _serializers;

        public IFluentHttpClientFactory Factory => _factory;
    }
}
