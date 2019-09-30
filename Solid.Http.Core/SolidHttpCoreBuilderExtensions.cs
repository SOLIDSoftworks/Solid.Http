using Solid.Http;
using Solid.Http.Abstractions;
using Solid.Http.Events;
using Solid.Http.Extensions;
using Solid.Http.Providers;
using Solid.Http.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// ISolidHttpBuilder extensions methods
    /// </summary>
    public static class SolidHttpCoreBuilderExtensions
    {
        /// <summary>
        /// Adds a deserializer factory used to deserialize the specified mime types
        /// </summary>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="factory">The deserializer factory instance</param>
        /// <param name="mimeType">The mime type to deserialize</param>
        /// <param name="more">More mime types</param>
        /// <returns>ISolidHttpBuilder</returns>
        public static ISolidHttpCoreBuilder AddDeserializer(this ISolidHttpCoreBuilder builder, IResponseDeserializerFactory factory, string mimeType, params string[] more)
        {
            builder.Services.AddSingleton<IDeserializer>(new Deserializer(mimeType, factory));
            foreach (var mime in more)
                builder.Services.AddSingleton<IDeserializer>(new Deserializer(mime, factory));
            return builder;
        }

        /// <summary>
        /// Adds a deserializer factory used to deserialize the specified mime types
        /// </summary>
        /// <typeparam name="TFactory">The deserializer factory type</typeparam>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="mimeType">The mime type to deserialize</param>
        /// <param name="more">More mime types</param>
        /// <returns>ISolidHttpBuilder</returns>
        public static ISolidHttpCoreBuilder AddDeserializer<TFactory>(this ISolidHttpCoreBuilder builder, string mimeType, params string[] more)
            where TFactory : class, IResponseDeserializerFactory
        {
            builder.Services.AddSingleton<TFactory>();
            builder.Services.AddSingleton<IDeserializer>(p => new Deserializer<TFactory>(mimeType, p.GetRequiredService<TFactory>()));
            foreach (var mime in more)
                builder.Services.AddSingleton<IDeserializer>(p => new Deserializer<TFactory>(mime, p.GetRequiredService<TFactory>()));
            return builder;
        }

        /// <summary>
        /// Configures Solid.Http to use one HttpClient for the whole application
        /// </summary>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <returns>ISolidHttpBuilder</returns>
        public static ISolidHttpCoreBuilder UseSingleInstanceHttpClientProvider(this ISolidHttpCoreBuilder builder) =>
            builder.UseHttpClientProvider<SingleInstanceHttpClientProvider>();

        /// <summary>
        /// Configures Solid.Http to use one HttpClient for each host requested.
        /// </summary>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <returns>ISolidHttpBuilder</returns>
        public static ISolidHttpCoreBuilder UseInstancePerHostHttpClientProvider(this ISolidHttpCoreBuilder builder) =>
            builder.UseHttpClientProvider<InstancePerHostHttpClientProvider>();

        /// <summary>
        /// Configures Solid.Http to use a custom HttpClientProvider
        /// </summary>
        /// <typeparam name="TProvider">The custom HttpClientProvider type</typeparam>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="instance">The HttpClientFactory instance</param>
        /// <returns>ISolidHttpBuilder</returns>
        public static ISolidHttpCoreBuilder UseHttpClientProvider<TProvider>(this ISolidHttpCoreBuilder builder, TProvider instance) where TProvider : HttpClientProvider
        {
            var descriptor = builder.Services.FirstOrDefault(d => d.ServiceType == typeof(IHttpClientProvider));
            if (descriptor != null)
                builder.Services.Remove(descriptor);
            builder.Services.AddSingleton<IHttpClientProvider>(instance);
            return builder;
        }

        /// <summary>
        /// Configures Solid.Http to use a custom HttpClientProvider
        /// </summary>
        /// <typeparam name="TProvider">The custom HttpClientProvider type</typeparam>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="factory">The factory used to create the HttpClientFactory</param>
        /// <returns>ISolidHttpBuilder</returns>
        public static ISolidHttpCoreBuilder UseHttpClientProvider<TProvider>(this ISolidHttpCoreBuilder builder, Func<IServiceProvider, TProvider> factory) where TProvider : HttpClientProvider
        {
            var descriptor = builder.Services.FirstOrDefault(d => d.ServiceType == typeof(IHttpClientProvider));
            if (descriptor != null)
                builder.Services.Remove(descriptor);
            builder.Services.AddSingleton<IHttpClientProvider>(factory);
            return builder;
        }

        /// <summary>
        /// Configures Solid.Http to use a custom HttpClientProvider
        /// </summary>
        /// <typeparam name="TProvider">The custom HttpClientProvider type</typeparam>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <returns>ISolidHttpBuilder</returns>
        public static ISolidHttpCoreBuilder UseHttpClientProvider<TProvider>(this ISolidHttpCoreBuilder builder) where TProvider : HttpClientProvider
        {
            var descriptor = builder.Services.FirstOrDefault(d => d.ServiceType == typeof(IHttpClientProvider));
            if (descriptor != null)
                builder.Services.Remove(descriptor);
            builder.Services.AddSingleton<IHttpClientProvider, TProvider>();
            return builder;
        }

        /// <summary>
        /// Add a global handler to be run when every Solid.Http client object is created.
        /// </summary>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="action">The handler to be run</param>
        /// <returns>The builder</returns>
        public static ISolidHttpCoreBuilder OnClientCreated(this ISolidHttpCoreBuilder builder, Action<ISolidHttpClient> action)
            => builder.OnClientCreated((_, c) => action(c));
        /// <summary>
        /// Add a global handler to be run when every Solid.Http client object is created.
        /// </summary>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="action">The handler to be run</param>
        /// <returns>The builder</returns>
        public static ISolidHttpCoreBuilder OnClientCreated(this ISolidHttpCoreBuilder builder, Action<IServiceProvider, ISolidHttpClient> action)
            => builder.On(action);


        /// <summary>
        /// Add a global handler to be run when every Solid.Http request object is created.
        /// </summary>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="action">The handler to be run</param>
        /// <returns>The builder</returns>
        public static ISolidHttpCoreBuilder OnRequestCreated(this ISolidHttpCoreBuilder builder, Action<ISolidHttpRequest> action)
            => builder.OnRequestCreated((_, c) => action(c));
        /// <summary>
        /// Add a global handler to be run when every Solid.Http request object is created.
        /// </summary>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="action">The handler to be run</param>
        /// <returns>The builder</returns>
        public static ISolidHttpCoreBuilder OnRequestCreated(this ISolidHttpCoreBuilder builder, Action<IServiceProvider, ISolidHttpRequest> action)
            => builder.On(action);

        /// <summary>
        /// Add a global handler to be run just before every request is sent
        /// </summary>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="action">The handler to be run</param>
        /// <returns>The builder</returns>
        public static ISolidHttpCoreBuilder OnRequest(this ISolidHttpCoreBuilder builder, Action<HttpRequestMessage> action)
            => builder.OnRequest((_, c) => action(c));
        /// <summary>
        /// Add a global handler to be run just before every request is sent
        /// </summary>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="action">The handler to be run</param>
        /// <returns>The builder</returns>
        public static ISolidHttpCoreBuilder OnRequest(this ISolidHttpCoreBuilder builder, Action<IServiceProvider, HttpRequestMessage> action)
            => builder.OnRequest(action.ToAsyncFunc());
        /// <summary>
        /// Add a global handler to be run just before every request is sent
        /// </summary>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="func">The handler to be run</param>
        /// <returns>The builder</returns>
        public static ISolidHttpCoreBuilder OnRequest(this ISolidHttpCoreBuilder builder, Func<HttpRequestMessage, Task> func)
            => builder.OnRequest((_, c) => func(c));
        /// <summary>
        /// Add a global handler to be run just before every request is sent
        /// </summary>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="func">The handler to be run</param>
        /// <returns>The builder</returns>
        public static ISolidHttpCoreBuilder OnRequest(this ISolidHttpCoreBuilder builder, Func<IServiceProvider, HttpRequestMessage, Task> func)
            => builder.On(func);

        /// <summary>
        /// Add a global handler to be run the moment every response is received
        /// </summary>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="action">The handler to be run</param>
        /// <returns>The builder</returns>
        public static ISolidHttpCoreBuilder OnResponse(this ISolidHttpCoreBuilder builder, Action<HttpResponseMessage> action)
            => builder.OnResponse((_, c) => action(c));
        /// <summary>
        /// Add a global handler to be run the moment every response is received
        /// </summary>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="action">The handler to be run</param>
        /// <returns>The builder</returns>
        public static ISolidHttpCoreBuilder OnResponse(this ISolidHttpCoreBuilder builder, Action<IServiceProvider, HttpResponseMessage> action)
            => builder.OnResponse(action.ToAsyncFunc());

        /// <summary>
        /// Add a globa
        /// <param name="builder">The extended ISolidHttpBuilder</param>l handler to be run the moment every response is received
        /// </summary>
        /// <param name="func">The handler to be run</param>
        /// <returns>The builder</returns>
        public static ISolidHttpCoreBuilder OnResponse(this ISolidHttpCoreBuilder builder, Func<HttpResponseMessage, Task> func)
            => builder.OnResponse((_, c) => func(c));
        /// <summary>
        /// Add a global handler to be run the moment every response is received
        /// </summary>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="func">The handler to be run</param>
        /// <returns>The builder</returns>
        public static ISolidHttpCoreBuilder OnResponse(this ISolidHttpCoreBuilder builder, Func<IServiceProvider, HttpResponseMessage, Task> func)
            => builder.On(func);  

        private static ISolidHttpCoreBuilder On<T>(this ISolidHttpCoreBuilder builder, Func<IServiceProvider, T, Task> func)
        {
            if (func == null) return builder;
            var descriptor = builder.Services.First(d => d.ServiceType == typeof(SolidAsyncEventHandler<T>));
            ((SolidAsyncEventHandler<T>)descriptor.ImplementationInstance).Handler += func;
            return builder;
        }
        private static ISolidHttpCoreBuilder On<T>(this ISolidHttpCoreBuilder builder, Action<IServiceProvider, T> action)
        {
            if (action == null) return builder;
            var descriptor = builder.Services.First(d => d.ServiceType == typeof(SolidEventHandler<T>));
            ((SolidEventHandler<T>)descriptor.ImplementationInstance).Handler += action;
            return builder;
        }
    }
}
