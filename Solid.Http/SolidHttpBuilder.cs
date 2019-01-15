
//using System;
//using System.Collections.Generic;
//using System.Text;
//using Microsoft.Extensions.DependencyInjection;
//using Solid.Http.Abstractions;
//using Solid.Http.Json;

//namespace Solid.Http
//{
//    internal class SolidHttpBuilder : ISolidHttpBuilder
//    {
//        private ISolidHttpCoreBuilder _core;
        
//        public SolidHttpBuilder(ISolidHttpCoreBuilder core)
//        {
//            _core = core;
//        }

//        public IServiceCollection Services => _core.Services;

//        public IDictionary<string, object> Properties => _core.Properties;
//    }
//}
