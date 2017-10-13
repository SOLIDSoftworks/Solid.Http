using System;
namespace FluentHttp
{
    internal class FluentHttpOptions : IFluentHttpOptions
    {
        private IDeserializerProvider _deserializers;
        private IFluentHttpEventInvoker _events;
        public FluentHttpOptions(IDeserializerProvider deserializers, IFluentHttpEventInvoker events)
        {
            _deserializers = deserializers;
            _events = events;
        }

        public IDeserializerProvider Deserializers => _deserializers;

        public IFluentHttpEvents Events => _events;
    }
}
