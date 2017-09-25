using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentHttp
{
    //public static class ServiceCollectionExtensions
    //{
    //    public static IServiceCollection AddFluentHttp<TFactory>(this IServiceCollection services, IEnumerable<object> constructorArgs = null, FluentHttpOptions options = null)
    //        where TFactory : FluentHttpClientFactory
    //    {
    //        services.AddSingleton<IHttpClientCache, HttpClientCache>();
    //        services.AddSingleton();
    //        services.AddScoped<IFluentHttpClientFactory>(p =>
    //        {
    //            var args = new List<object>();
    //            args.Add(p.GetRequiredService<IHttpClientCache>());
    //            args.Add(options?.Configuration);
    //            if (constructorArgs != null)
    //                args.AddRange(constructorArgs);

    //            // TODO: performance check this!
    //            var factory = Activator.CreateInstance(typeof(TFactory), args.ToArray()) as TFactory;

    //            if (options != null && options.OnClientCreated != null)
    //                factory.OnClientCreated += (s, a) => options.OnClientCreated(s, a);
    //            if (options != null && options.OnRequestCreated != null)
    //                factory.OnClientCreated += (s1, a1) =>
    //                {
    //                    a1.Client.OnRequestCreated += (s2, a2) =>
    //                    {
    //                        options.OnRequestCreated(s2, a2);
    //                    };
    //                };

    //            return factory;
    //        });
    //        return services;
    //    }

    //    public static IServiceCollection AddFluentHttp(this IServiceCollection services, FluentHttpOptions options = null)
    //    {
    //        return services.AddFluentHttp<FluentHttpClientFactory>(options: options);
    //    }
    //}
}
