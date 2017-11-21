using SolidHttp.Abstractions;
using System;
using System.Collections.Generic;

namespace SolidHttp.Setup
{
    internal class SolidHttpOptions : ISolidHttpOptions
    {
        private ISolidHttpEvents _events;
        public SolidHttpOptions(ISolidHttpEvents events)
        {
            _events = events;
        }
        
        public ISolidHttpEvents Events => _events;
    }
}
