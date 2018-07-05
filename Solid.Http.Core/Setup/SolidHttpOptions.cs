using Solid.Http.Abstractions;
using Solid.Http.Models;
using System;
using System.Collections.Generic;

namespace Solid.Http.Setup
{
    internal class SolidHttpOptions : ISolidHttpOptions
    {
        private ISolidHttpEvents _events;

        public SolidHttpOptions(ISolidHttpEvents events, IEnumerable<Action<ISolidHttpOptions>> actions)
        {
            _events = events;
            foreach (var configure in actions)
                configure(this);
        }
        
        public ISolidHttpEvents Events => _events;

        public HttpClientStrategy Strategy { get; set; }
    }
}
