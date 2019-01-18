using Microsoft.Extensions.DependencyInjection;
using Solid.Http.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solid.Http
{
    internal class SolidHttpBuilder : ISolidHttpBuilder
    {
        private ISolidHttpCoreBuilder _core;

        public SolidHttpBuilder(ISolidHttpCoreBuilder core)
        {
            _core = core;
        }

        public IServiceCollection Services => Core.Services;

        public IDictionary<string, object> Properties => Core.Properties;

        public ISolidHttpCoreBuilder Core => _core;
    }
}
