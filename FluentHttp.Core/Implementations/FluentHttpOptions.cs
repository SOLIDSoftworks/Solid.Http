using System;
namespace FluentHttp
{
    internal class FluentHttpOptions : IFluentHttpOptions
    {
        private ISerializerProvider _serializers;
        private IFluentHttpClientFactoryEventInvoker _events;
        public FluentHttpOptions(ISerializerProvider serializers, IFluentHttpClientFactoryEventInvoker events)
        {
            _serializers = serializers;
            _events = events;
        }

        public ISerializerProvider Serializers => _serializers;

        public IFluentHttpClientFactoryEvents FactoryEvents => _events;
    }
}
