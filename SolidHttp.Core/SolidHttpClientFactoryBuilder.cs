using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolidHttp
{
    public class SolidHttpClientFactoryBuilder
    {
        private ServiceCollection _collection;
        private ISolidHttpSetup _setup;
        
        public SolidHttpClientFactoryBuilder()
        {
            _collection = new ServiceCollection();
            _setup = _collection.AddSolidHttp();
        }

        public SolidHttpClientFactoryBuilder AddConfiguration(IConfiguration configuration)
        {
            _collection.AddSingleton<IConfiguration>(configuration);
            return this;
        }

        public SolidHttpClientFactoryBuilder Setup(Action<ISolidHttpSetup> add)
        {
            add(_setup);
            return this;
        }

        //public SolidHttpClientFactoryBuilder Configure(Action<ISolidHttpOptions> configure)
        //{
        //    return AddSetup(s => s.Configure(configure));
        //}

        public ISolidHttpClientFactory Build()
        {
            var provider = _collection.BuildServiceProvider();
            return provider.GetService<ISolidHttpClientFactory>();
        }
    }
}
