using System;
namespace SolidHttp
{
    internal class SolidHttpOptions : ISolidHttpOptions
    {
        private IDeserializerProvider _deserializers;
        private ISolidHttpEventInvoker _events;
        public SolidHttpOptions(IDeserializerProvider deserializers, ISolidHttpEventInvoker events)
        {
            _deserializers = deserializers;
            _events = events;
        }

        public IDeserializerProvider Deserializers => _deserializers;

        public ISolidHttpEvents Events => _events;
    }
}
