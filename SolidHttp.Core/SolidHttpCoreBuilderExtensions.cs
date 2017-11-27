using Microsoft.Extensions.DependencyInjection;
using Solid.Http.Abstractions;
using Solid.Http.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solid.Http
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
