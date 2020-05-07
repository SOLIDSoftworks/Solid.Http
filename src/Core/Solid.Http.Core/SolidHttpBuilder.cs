using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solid.Http
{
    public class SolidHttpBuilder
    {
        internal SolidHttpBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }

        public SolidHttpBuilder Configure(Action<SolidHttpOptions> configureOptions)
        {
            Services.Configure(configureOptions);
            return this;
        }

        public SolidHttpBuilder AddDeserializer<T>()
            where T : class, IDeserializer
        {
            Services.TryAddEnumerable(ServiceDescriptor.Singleton<IDeserializer, T>());
            return this;
        }

        public SolidHttpBuilder AddDeserializer<T>(T instance)
            where T : class, IDeserializer
        {
            Services.TryAddEnumerable(ServiceDescriptor.Singleton<IDeserializer>(instance));
            return this;
        }

        public SolidHttpBuilder AddDeserializer<T>(Func<IServiceProvider, T> factory)
            where T : class, IDeserializer
        {
            Services.TryAddEnumerable(ServiceDescriptor.Singleton<IDeserializer, T>(factory));
            return this;
        }
    }
}
