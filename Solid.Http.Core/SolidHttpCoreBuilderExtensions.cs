using Microsoft.Extensions.DependencyInjection;
using Solid.Http.Abstractions;
using Solid.Http.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solid.Http
{
    //public static class SolidHttpCoreBuilderExtensions
    //{
    //    public static ISolidHttpCoreBuilder AddDeserializer(this ISolidHttpCoreBuilder builder, IResponseDeserializerFactory factory, string mimeType, params string[] more)
    //    {
    //        builder.Services.AddDeserializer(factory, mimeType, more);
    //        return builder;
    //    }

    //    public static ISolidHttpCoreBuilder AddDeserializer<TFactory>(this ISolidHttpCoreBuilder builder, string mimeType, params string[] more)
    //        where TFactory : class, IResponseDeserializerFactory
    //    {
    //        builder.Services.AddDeserializer<TFactory>(mimeType, more);
    //        return builder;
    //    }
    //}
}
