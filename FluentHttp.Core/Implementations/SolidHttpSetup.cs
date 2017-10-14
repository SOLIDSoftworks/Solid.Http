using System;
using System.Collections.Generic;
using System.Text;

namespace SolidHttp
{
    internal class SolidHttpSetup : ISolidHttpSetup
    {
        private ISolidHttpOptions _options;

        public SolidHttpSetup(ISolidHttpOptions options)
        {
            _options = options;
        }

        public ISolidHttpSetup Configure(Action<ISolidHttpOptions> configure)
        {
            configure(_options);
            return this;
        }
    }
}
