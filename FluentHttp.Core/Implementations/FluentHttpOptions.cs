using System;
namespace FluentHttp
{
    internal class FluentHttpOptions : IFluentHttpOptions
    {
        private IDeserializerProvider _deserializers;
        private IFluentHttpClientFactoryEventInvoker _events;
        public FluentHttpOptions(IDeserializerProvider deserializers, IFluentHttpClientFactoryEventInvoker events)
        {
            _deserializers = deserializers;
            _events = events;
        }

        public IDeserializerProvider Deserializers => _deserializers;

        public IFluentHttpClientFactoryEvents FactoryEvents => _events;
    }
}
