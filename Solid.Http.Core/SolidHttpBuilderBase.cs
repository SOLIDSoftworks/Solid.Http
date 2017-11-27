using Microsoft.Extensions.DependencyInjection;
using Solid.Http.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solid.Http
{
    public class SolidHttpBuilderBase : IDisposable
    {
        private IServiceCollection _services;
        private ServiceProvider _provider;
        private IServiceScope _scope;
        protected SolidHttpBuilderBase(IServiceCollection services)
        {
            _services = services;
        }

        public IServiceCollection Services => _services;

        public ISolidHttpClientFactory Build()
        {
            if (_provider == null)
                _provider = _services.BuildServiceProvider();

            var initializer = _provider.GetRequiredService<ISolidHttpInitializer>();
            initializer.Initialize();

            if (_scope != null)
                _scope.Dispose();
            _scope = _provider.CreateScope();
            return _scope.ServiceProvider.GetRequiredService<ISolidHttpClientFactory>();
        }

        public void Dispose()
        {
            if (_provider != null)
                _provider.Dispose();

            if (_scope != null)
                _scope.Dispose();
        }
    }
}
