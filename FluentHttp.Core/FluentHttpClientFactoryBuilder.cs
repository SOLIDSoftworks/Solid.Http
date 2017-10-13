using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentHttp
{
    public class FluentHttpClientFactoryBuilder
    {
        private ServiceCollection _collection;
        private IFluentHttpSetup _setup;
        
        public FluentHttpClientFactoryBuilder()
        {
            _collection = new ServiceCollection();
            _setup = _collection.AddFluentHttp();
        }

        public FluentHttpClientFactoryBuilder AddConfiguration(IConfiguration configuration)
        {
            _collection.AddSingleton<IConfiguration>(configuration);
            return this;
        }

        public FluentHttpClientFactoryBuilder Setup(Action<IFluentHttpSetup> add)
        {
            add(_setup);
            return this;
        }

        //public FluentHttpClientFactoryBuilder Configure(Action<IFluentHttpOptions> configure)
        //{
        //    return AddSetup(s => s.Configure(configure));
        //}

        public IFluentHttpClientFactory Build()
        {
            var provider = _collection.BuildServiceProvider();
            return provider.GetService<IFluentHttpClientFactory>();
        }
    }
}
