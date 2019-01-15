using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Solid.Http;
using Solid.Http.Abstractions;
using Solid.Http.Factories;
using Solid.Http.Providers;
using Solid.Http.Serialization;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions method for the service collection
    /// </summary>
    public static class SolidHttpCoreServiceCollectionExtensions
    {
        public static IServiceCollection AddSolidHttpCore(this IServiceCollection services, Action<ISolidHttpCoreBuilder> action)
        {
            var builder = new SolidHttpBuilder
            {
                Services = services,
                Properties = new Dictionary<string, object>()
            };
            services.AddSolidHttpCore();
            action(builder);
            return services;
        }

        public static IServiceCollection AddSolidHttpCore(this IServiceCollection services)
        {
            services.TryAddTransient<ISolidHttpClient, SolidHttpClient>();
            //services.TryAddSingleton<ISolidHttpEvents, SolidHttpEvents>();
            services.TryAddSingleton<IHttpClientProvider, SingleInstanceHttpClientProvider>();
            services.TryAddSingleton<ISolidHttpClientFactory, SolidHttpClientFactory>();
            //services.TryAddSingleton<ISolidHttpOptions, SolidHttpOptions>();

            return services;
        }



        ///// <summary>
        ///// Add SolidHttp to the service collection
        ///// </summary>
        ///// <typeparam name="TFactory">The custom IHttpClientFactory type</typeparam>
        ///// <param name="services">The service collection</param>
        ///// <returns>ISolidHttpSetup</returns>
        //public static ISolidHttpCoreBuilder AddSolidHttpCore(this IServiceCollection services)
        //{
        //    var builder = new SolidHttpCoreBuilder(services);
        //    return builder;
        //}

        ///// <summary>
        ///// Add SolidHttp to the service collection
        ///// </summary>
        ///// <typeparam name="TFactory">The custom IHttpClientFactory type</typeparam>
        ///// <param name="services">The service collection</param>
        ///// <returns>ISolidHttpSetup</returns>
        //public static ISolidHttpCoreBuilder AddSolidHttpCore(this IServiceCollection services, Action<ISolidHttpOptions> configure)
        //{
        //    services.AddSingleton(configure);
        //    return services.AddSolidHttpCore();
        //}

        //public static IServiceCollection AddSolidHttpDeserializer(this IServiceCollection services, IResponseDeserializerFactory factory, string mimeType, params string[] more)
        //{
        //    services.AddSingleton<IDeserializer>(new Deserializer(mimeType, factory));
        //    foreach (var mime in more)
        //        services.AddSingleton<IDeserializer>(new Deserializer(mime, factory));
        //    return services;
        //}

        //public static IServiceCollection AddSolidHttpDeserializer<TFactory>(this IServiceCollection services, string mimeType, params string[] more)
        //    where TFactory : class, IResponseDeserializerFactory
        //{
        //    services.AddSingleton<TFactory>();
        //    services.AddSingleton<IDeserializer>(p => new Deserializer<TFactory>(mimeType, p.GetRequiredService<TFactory>()));
        //    foreach (var mime in more)
        //        services.AddSingleton<IDeserializer>(p => new Deserializer<TFactory>(mime, p.GetRequiredService<TFactory>()));
        //    return services;
        //}
    }
}
