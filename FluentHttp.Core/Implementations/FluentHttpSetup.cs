using System;
using System.Collections.Generic;
using System.Text;

namespace FluentHttp
{
    internal class FluentHttpSetup : IFluentHttpSetup
    {
        private IFluentHttpOptions _options;

        public FluentHttpSetup(IFluentHttpOptions options)
        {
            _options = options;
        }

        public IFluentHttpSetup Configure(Action<IFluentHttpOptions> configure)
        {
            configure(_options);
            return this;
        }
    }
}
