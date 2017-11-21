using System;
using System.Collections.Generic;

namespace SolidHttp
{
    internal class SolidHttpOptions : ISolidHttpOptions
    {
        private IDeserializerProvider _deserializers;
        private ISolidHttpEvents _events;
        public SolidHttpOptions(IDeserializerProvider deserializers, ISolidHttpEvents events)
        {
            _deserializers = deserializers;
            _events = events;
        }

        public IDeserializerProvider Deserializers => _deserializers;

        public ISolidHttpEvents Events => _events;
    }
}
