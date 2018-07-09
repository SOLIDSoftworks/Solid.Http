
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Solid.Http.Json;

namespace Solid.Http
{
    public class SolidHttpBuilder : SolidHttpBuilderBase, ISolidHttpBuilder, IDisposable
    {
        private ISolidHttpCoreBuilder _core;

        public SolidHttpBuilder()
            : this(new SolidHttpCoreBuilder())
        {
        }

        public SolidHttpBuilder(IServiceCollection services)
            : this(services.AddSolidHttpCore())
        {
        }

        public SolidHttpBuilder(ISolidHttpCoreBuilder core)
            : base(core.Services)
        {
            _core = core;
            _core.AddJson();
        }

        public ISolidHttpBuilder AddSolidHttpOptions(Action<ISolidHttpOptions> configure)
        {
            _core.AddSolidHttpOptions(configure);
            return this;
        }
    }
}
