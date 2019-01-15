using Solid.Http;
using Solid.Http.Abstractions;
using Solid.Http.Extensions;
using Solid.Http.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SolidHttpBuilderExtensions
    {
        public static ISolidHttpCoreBuilder AddDeserializer(this ISolidHttpCoreBuilder builder, IResponseDeserializerFactory factory, string mimeType, params string[] more)
            => ((ISolidHttpBuilder)builder).AddDeserializer(factory, mimeType, more) as ISolidHttpCoreBuilder;
        public static ISolidHttpCoreBuilder AddDeserializer<TFactory>(this ISolidHttpCoreBuilder builder, string mimeType, params string[] more)
            where TFactory : class, IResponseDeserializerFactory
            => ((ISolidHttpBuilder)builder).AddDeserializer<TFactory>(mimeType, more) as ISolidHttpCoreBuilder;

        public static ISolidHttpBuilder AddDeserializer(this ISolidHttpBuilder builder, IResponseDeserializerFactory factory, string mimeType, params string[] more)
        {
            builder.Services.AddSingleton<IDeserializer>(new Deserializer(mimeType, factory));
            foreach (var mime in more)
                builder.Services.AddSingleton<IDeserializer>(new Deserializer(mime, factory));
            return builder;
        }

        public static ISolidHttpBuilder AddDeserializer<TFactory>(this ISolidHttpBuilder builder, string mimeType, params string[] more)
            where TFactory : class, IResponseDeserializerFactory
        {
            builder.Services.AddSingleton<TFactory>();
            builder.Services.AddSingleton<IDeserializer>(p => new Deserializer<TFactory>(mimeType, p.GetRequiredService<TFactory>()));
            foreach (var mime in more)
                builder.Services.AddSingleton<IDeserializer>(p => new Deserializer<TFactory>(mime, p.GetRequiredService<TFactory>()));
            return builder;
        }

        public static TBuilder OnClientCreated<TBuilder>(this TBuilder builder, Action<ISolidHttpClient> action) where TBuilder : ISolidHttpBuilder
            => builder.OnClientCreated((_, c) => action(c));
        public static TBuilder OnClientCreated<TBuilder>(this TBuilder builder, Action<IServiceProvider, ISolidHttpClient> action) where TBuilder : ISolidHttpBuilder
            => builder.OnClientCreated(action.ToAsyncFunc());
        public static TBuilder OnClientCreated<TBuilder>(this TBuilder builder, Func<ISolidHttpClient, Task> func) where TBuilder : ISolidHttpBuilder
            => builder.OnClientCreated((_, c) => func(c));
        public static TBuilder OnClientCreated<TBuilder>(this TBuilder builder, Func<IServiceProvider, ISolidHttpClient, Task> func) where TBuilder : ISolidHttpBuilder
            => builder.On(func);

        
        public static TBuilder OnRequestCreated<TBuilder>(this TBuilder builder, Action<ISolidHttpRequest> action) where TBuilder : ISolidHttpBuilder
            => builder.OnRequestCreated((_, c) => action(c));
        public static TBuilder OnRequestCreated<TBuilder>(this TBuilder builder, Action<IServiceProvider, ISolidHttpRequest> action) where TBuilder : ISolidHttpBuilder
            => builder.OnRequestCreated(action.ToAsyncFunc());
        public static TBuilder OnRequestCreated<TBuilder>(this TBuilder builder, Func<ISolidHttpRequest, Task> func) where TBuilder : ISolidHttpBuilder
            => builder.OnRequestCreated((_, c) => func(c));
        public static TBuilder OnRequestCreated<TBuilder>(this TBuilder builder, Func<IServiceProvider, ISolidHttpRequest, Task> func) where TBuilder : ISolidHttpBuilder
            => builder.On(func);


        public static TBuilder OnRequest<TBuilder>(this TBuilder builder, Action<HttpRequestMessage> action) where TBuilder : ISolidHttpBuilder
            => builder.OnRequest((_, c) => action(c));
        public static TBuilder OnRequest<TBuilder>(this TBuilder builder, Action<IServiceProvider, HttpRequestMessage> action) where TBuilder : ISolidHttpBuilder
            => builder.OnRequest(action.ToAsyncFunc());
        public static TBuilder OnRequest<TBuilder>(this TBuilder builder, Func<HttpRequestMessage, Task> func) where TBuilder : ISolidHttpBuilder
            => builder.OnRequest((_, c) => func(c));
        public static TBuilder OnRequest<TBuilder>(this TBuilder builder, Func<IServiceProvider, HttpRequestMessage, Task> func) where TBuilder : ISolidHttpBuilder
            => builder.On(func);


        public static TBuilder OnResponse<TBuilder>(this TBuilder builder, Action<HttpResponseMessage> action) where TBuilder : ISolidHttpBuilder
            => builder.OnResponse((_, c) => action(c));
        public static TBuilder OnResponse<TBuilder>(this TBuilder builder, Action<IServiceProvider, HttpResponseMessage> action) where TBuilder : ISolidHttpBuilder
            => builder.OnResponse(action.ToAsyncFunc());
        public static TBuilder OnResponse<TBuilder>(this TBuilder builder, Func<HttpResponseMessage, Task> func) where TBuilder : ISolidHttpBuilder
            => builder.OnResponse((_, c) => func(c));
        public static TBuilder OnResponse<TBuilder>(this TBuilder builder, Func<IServiceProvider, HttpResponseMessage, Task> func) where TBuilder : ISolidHttpBuilder
            => builder.On(func);

        private static TBuilder On<T, TBuilder>(this TBuilder builder, Func<IServiceProvider, T, Task> func) where TBuilder : ISolidHttpBuilder
        {
            if (func == null) return builder;
            var descriptor = builder.Services.FirstOrDefault(d => d.ServiceType == func.GetType());
            if (descriptor == null)
                builder.Services.AddSingleton(func);
            else
            {
                var registered = (Func<IServiceProvider, T, Task>)descriptor.ImplementationInstance;
                registered += func;
            }
            return builder;
        }
    }
}
