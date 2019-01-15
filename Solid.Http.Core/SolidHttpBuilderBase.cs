//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Solid.Http
//{
//    public class SolidHttpBuilderBase : IDisposable
//    {
//        private IServiceCollection _services;
//        private IServiceScope _scope;
//        protected SolidHttpBuilderBase(IServiceCollection services)
//        {
//            _services = services;
//        }

//        public IServiceCollection Services => _services;

//        public ServiceProvider Provider { get; private set; }

//        public ISolidHttpClientFactory Build()
//        {
//            if (Provider == null)
//                Provider = _services.BuildServiceProvider();

//            if (_scope != null)
//                _scope.Dispose();
//            _scope = Provider.CreateScope();
//            return _scope.ServiceProvider.GetRequiredService<ISolidHttpClientFactory>();
//        }

//        public void Dispose()
//        {
//            if (Provider != null)
//                Provider.Dispose();

//            if (_scope != null)
//                _scope.Dispose();
//        }
//    }
//}
