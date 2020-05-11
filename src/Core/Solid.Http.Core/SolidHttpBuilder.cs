using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solid.Http
{
    /// <summary>
    /// A builder to set up Solid.Http.Core.
    /// </summary>
    public class SolidHttpBuilder
    {
        internal SolidHttpBuilder(IServiceCollection services)
        {
            Services = services;
        }

        /// <summary>
        /// The <see cref="IServiceCollection" /> that Solid.HttpCore is being added to.
        /// </summary>
        public IServiceCollection Services { get; }

        /// <summary>
        /// Configures <see cref="SolidHttpOptions" /> using a provided delegate.
        /// </summary>
        /// <param name="configureOptions">A delegate that configures <see cref="SolidHttpOptions" />.</param>
        /// <returns>The <see cref="SolidHttpBuilder" /> so that additional calls can be chained.</returns>
        public SolidHttpBuilder Configure(Action<SolidHttpOptions> configureOptions)
        {
            Services.Configure(configureOptions);
            return this;
        }

        /// <summary>
        /// Adds a deserializer that can be used with Solid.Http.Core.
        /// </summary>
        /// <typeparam name="T">The type that implements <see cref="IDeserializer" />.</typeparam>
        /// <returns>The <see cref="SolidHttpBuilder" /> so that additional calls can be chained.</returns>
        public SolidHttpBuilder AddDeserializer<T>()
            where T : class, IDeserializer
        {
            Services.TryAddEnumerable(ServiceDescriptor.Singleton<IDeserializer, T>());
            return this;
        }

        /// <summary>
        /// Adds a deserializer that can be used with Solid.Http.Core.
        /// </summary>
        /// <param name="instance">A singleton instance of <typeparamref name="T" />.</param>
        /// <typeparam name="T">The type that implements <see cref="IDeserializer" />.</typeparam>
        /// <returns>The <see cref="SolidHttpBuilder" /> so that additional calls can be chained.</returns>
        public SolidHttpBuilder AddDeserializer<T>(T instance)
            where T : class, IDeserializer
        {
            Services.TryAddEnumerable(ServiceDescriptor.Singleton<IDeserializer>(instance));
            return this;
        }

        /// <summary>
        /// Adds a deserializer that can be used with Solid.Http.Core.
        /// </summary>
        /// <param name="factory">A factory for creating <typeparamref name="T" />.</param>
        /// <typeparam name="T">The type that implements <see cref="IDeserializer" />.</typeparam>
        /// <returns>The <see cref="SolidHttpBuilder" /> so that additional calls can be chained.</returns>
        public SolidHttpBuilder AddDeserializer<T>(Func<IServiceProvider, T> factory)
            where T : class, IDeserializer
        {
            Services.TryAddEnumerable(ServiceDescriptor.Singleton<IDeserializer, T>(factory));
            return this;
        }
    }
}
