using SolidHttp.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace SolidHttp
{
    internal class SolidHttpBuilder : ISolidHttpBuilder
    {
        private ISolidHttpCoreBuilder _core;

        public SolidHttpBuilder()
        {
        }

        public SolidHttpBuilder(ISolidHttpCoreBuilder core)
        {
            _core = core;
        }

        public IServiceCollection Services => _core.Services;

        public ISolidHttpBuilder AddSolidHttpOptions(Action<ISolidHttpOptions> configure)
        {
            _core.AddSolidHttpCoreOptions(configure);
            return this;
        }
    }
}
