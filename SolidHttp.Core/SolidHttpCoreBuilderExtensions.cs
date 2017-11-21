using Microsoft.Extensions.DependencyInjection;
using SolidHttp.Abstractions;
using SolidHttp.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolidHttp
{
    public static class SolidHttpCoreBuilderExtensions
    {
        public static ISolidHttpCoreBuilder AddDeserializer(this ISolidHttpCoreBuilder builder, IResponseDeserializerFactory factory, string mimeType, params string[] more)
        {
            builder.Services.AddSingleton<IDeserializer>(new Deserializer(mimeType, factory));
            foreach (var mime in more)
                builder.Services.AddSingleton<IDeserializer>(new Deserializer(mime, factory));
            return builder;
        }

        public static ISolidHttpCoreBuilder AddDeserializer<TFactory>(this ISolidHttpCoreBuilder builder, string mimeType, params string[] more)
            where TFactory : class, IResponseDeserializerFactory
        {
            builder.Services.AddSingleton<TFactory>();
            builder.Services.AddSingleton<IDeserializer>(p => new Deserializer<TFactory>(mimeType, p.GetRequiredService<TFactory>()));
            foreach (var mime in more)
                builder.Services.AddSingleton<IDeserializer>(p => new Deserializer<TFactory>(mimeType, p.GetRequiredService<TFactory>()));
            return builder;
        }
    }
}
