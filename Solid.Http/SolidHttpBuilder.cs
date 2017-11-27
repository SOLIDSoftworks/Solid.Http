using Solid.Http.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Solid.Http.Json;

namespace Solid.Http
{
    public class SolidHttpBuilder<TFactory> : SolidHttpBuilder
        where TFactory : class, IHttpClientFactory
    {
        public SolidHttpBuilder()
            : base(new SolidHttpCoreBuilder<TFactory>())
        {
        }

        public SolidHttpBuilder(IServiceCollection services)
            : this(services.AddSolidHttpCore<TFactory>())
        {
        }

        public SolidHttpBuilder(ISolidHttpCoreBuilder core) 
            : base(core)
        {
        }
    }

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

        public SolidHttpBuilder AddSolidHttpOptions(Action<ISolidHttpOptions> configure)
        {
            _core.AddSolidHttpCoreOptions(configure);
            return this;
        }

        ISolidHttpBuilder ISolidHttpBuilder.AddSolidHttpOptions(Action<ISolidHttpOptions> configure)
        {
            throw new NotImplementedException();
        }
    }
}
