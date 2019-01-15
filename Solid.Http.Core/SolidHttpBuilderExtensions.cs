using Solid.Http;
using Solid.Http.Abstractions;
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
    public static class SolidHttpBuilderExtensions
    {
        /// <summary>
        /// Adds a deserializer factory used to deserialize the specified mime types
        /// </summary>
        /// <param name="builder">The extended ISolidHttpCoreBuilder</param>
        /// <param name="factory">The deserializer factory instance</param>
        /// <param name="mimeType">The mime type to deserialize</param>
        /// <param name="more">More mime types</param>
        /// <returns>ISolidHttpCoreBuilder</returns>
        public static ISolidHttpCoreBuilder AddDeserializer(this ISolidHttpCoreBuilder builder, IResponseDeserializerFactory factory, string mimeType, params string[] more)
            => ((ISolidHttpBuilder)builder).AddDeserializer(factory, mimeType, more) as ISolidHttpCoreBuilder;

        /// <summary>
        /// Adds a deserializer factory used to deserialize the specified mime types
        /// </summary>
        /// <typeparam name="TFactory">The deserializer factory type</typeparam>
        /// <param name="builder">The extended ISolidHttpCoreBuilder</param>
        /// <param name="mimeType">The mime type to deserialize</param>
        /// <param name="more">More mime types</param>
        /// <returns>ISolidHttpCoreBuilder</returns>
        public static ISolidHttpCoreBuilder AddDeserializer<TFactory>(this ISolidHttpCoreBuilder builder, string mimeType, params string[] more)
            where TFactory : class, IResponseDeserializerFactory
            => ((ISolidHttpBuilder)builder).AddDeserializer<TFactory>(mimeType, more) as ISolidHttpCoreBuilder;


        /// <summary>
        /// Adds a deserializer factory used to deserialize the specified mime types
        /// </summary>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="factory">The deserializer factory instance</param>
        /// <param name="mimeType">The mime type to deserialize</param>
        /// <param name="more">More mime types</param>
        /// <returns>ISolidHttpBuilder</returns>
        public static ISolidHttpBuilder AddDeserializer(this ISolidHttpBuilder builder, IResponseDeserializerFactory factory, string mimeType, params string[] more)
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
        public static ISolidHttpBuilder AddDeserializer<TFactory>(this ISolidHttpBuilder builder, string mimeType, params string[] more)
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
        public static ISolidHttpBuilder UseSingleInstanceHttpClientProvider(this ISolidHttpBuilder builder) =>
            builder.UseHttpClientProvider<SingleInstanceHttpClientProvider>();

        /// <summary>
        /// Configures Solid.Http to use one HttpClient for each host requested.
        /// </summary>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <returns>ISolidHttpBuilder</returns>
        public static ISolidHttpBuilder UseInstancePerHostHttpClientProvider(this ISolidHttpBuilder builder) =>
            builder.UseHttpClientProvider<InstancePerHostHttpClientProvider>();

        /// <summary>
        /// Configures Solid.Http to use a custom HttpClientProvider
        /// </summary>
        /// <typeparam name="TProvider">The custom HttpClientProvider type</typeparam>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="instance">The HttpClientFactory instance</param>
        /// <returns>ISolidHttpBuilder</returns>
        public static ISolidHttpBuilder UseHttpClientProvider<TProvider>(this ISolidHttpBuilder builder, TProvider instance) where TProvider : HttpClientProvider
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
        public static ISolidHttpBuilder UseHttpClientProvider<TProvider>(this ISolidHttpBuilder builder, Func<IServiceProvider, TProvider> factory) where TProvider : HttpClientProvider
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
        public static ISolidHttpBuilder UseHttpClientProvider<TProvider>(this ISolidHttpBuilder builder) where TProvider : HttpClientProvider
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
        /// <typeparam name="TBuilder">The builder type</typeparam>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="action">The handler to be run</param>
        /// <returns>The builder</returns>
        public static TBuilder OnClientCreated<TBuilder>(this TBuilder builder, Action<ISolidHttpClient> action) where TBuilder : ISolidHttpBuilder
            => builder.OnClientCreated((_, c) => action(c));
        /// <summary>
        /// Add a global handler to be run when every Solid.Http client object is created.
        /// </summary>
        /// <typeparam name="TBuilder">The builder type</typeparam>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="action">The handler to be run</param>
        /// <returns>The builder</returns>
        public static TBuilder OnClientCreated<TBuilder>(this TBuilder builder, Action<IServiceProvider, ISolidHttpClient> action) where TBuilder : ISolidHttpBuilder
            => builder.OnClientCreated(action.ToAsyncFunc());
        /// <summary>
        /// Add a global handler to be run when every Solid.Http client object is created.
        /// </summary>
        /// <typeparam name="TBuilder">The builder type</typeparam>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="func">The handler to be run</param>
        /// <returns>The builder</returns>
        public static TBuilder OnClientCreated<TBuilder>(this TBuilder builder, Func<ISolidHttpClient, Task> func) where TBuilder : ISolidHttpBuilder
            => builder.OnClientCreated((_, c) => func(c));
        /// <summary>
        /// Add a global handler to be run when every Solid.Http client object is created.
        /// </summary>
        /// <typeparam name="TBuilder">The builder type</typeparam>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="func">The handler to be run</param>
        /// <returns>The builder</returns>
        public static TBuilder OnClientCreated<TBuilder>(this TBuilder builder, Func<IServiceProvider, ISolidHttpClient, Task> func) where TBuilder : ISolidHttpBuilder
            => builder.On(func);


        /// <summary>
        /// Add a global handler to be run when every Solid.Http request object is created.
        /// </summary>
        /// <typeparam name="TBuilder">The builder type</typeparam>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="action">The handler to be run</param>
        /// <returns>The builder</returns>
        public static TBuilder OnRequestCreated<TBuilder>(this TBuilder builder, Action<ISolidHttpRequest> action) where TBuilder : ISolidHttpBuilder
            => builder.OnRequestCreated((_, c) => action(c));
        /// <summary>
        /// Add a global handler to be run when every Solid.Http request object is created.
        /// </summary>
        /// <typeparam name="TBuilder">The builder type</typeparam>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="action">The handler to be run</param>
        /// <returns>The builder</returns>
        public static TBuilder OnRequestCreated<TBuilder>(this TBuilder builder, Action<IServiceProvider, ISolidHttpRequest> action) where TBuilder : ISolidHttpBuilder
            => builder.OnRequestCreated(action.ToAsyncFunc());
        /// <summary>
        /// Add a global handler to be run when every Solid.Http request object is created.
        /// </summary>
        /// <typeparam name="TBuilder">The builder type</typeparam>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="func">The handler to be run</param>
        /// <returns>The builder</returns>
        public static TBuilder OnRequestCreated<TBuilder>(this TBuilder builder, Func<ISolidHttpRequest, Task> func) where TBuilder : ISolidHttpBuilder
            => builder.OnRequestCreated((_, c) => func(c));
        /// <summary>
        /// Add a global handler to be run when every Solid.Http request object is created.
        /// </summary>
        /// <typeparam name="TBuilder">The builder type</typeparam>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="func">The handler to be run</param>
        /// <returns>The builder</returns>
        public static TBuilder OnRequestCreated<TBuilder>(this TBuilder builder, Func<IServiceProvider, ISolidHttpRequest, Task> func) where TBuilder : ISolidHttpBuilder
            => builder.On(func);


        /// <summary>
        /// Add a global handler to be run just before every request is sent
        /// </summary>
        /// <typeparam name="TBuilder">The builder type</typeparam>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="action">The handler to be run</param>
        /// <returns>The builder</returns>
        public static TBuilder OnRequest<TBuilder>(this TBuilder builder, Action<HttpRequestMessage> action) where TBuilder : ISolidHttpBuilder
            => builder.OnRequest((_, c) => action(c));
        /// <summary>
        /// Add a global handler to be run just before every request is sent
        /// </summary>
        /// <typeparam name="TBuilder">The builder type</typeparam>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="action">The handler to be run</param>
        /// <returns>The builder</returns>
        public static TBuilder OnRequest<TBuilder>(this TBuilder builder, Action<IServiceProvider, HttpRequestMessage> action) where TBuilder : ISolidHttpBuilder
            => builder.OnRequest(action.ToAsyncFunc());
        /// <summary>
        /// Add a global handler to be run just before every request is sent
        /// </summary>
        /// <typeparam name="TBuilder">The builder type</typeparam>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="func">The handler to be run</param>
        /// <returns>The builder</returns>
        public static TBuilder OnRequest<TBuilder>(this TBuilder builder, Func<HttpRequestMessage, Task> func) where TBuilder : ISolidHttpBuilder
            => builder.OnRequest((_, c) => func(c));
        /// <summary>
        /// Add a global handler to be run just before every request is sent
        /// </summary>
        /// <typeparam name="TBuilder">The builder type</typeparam>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="func">The handler to be run</param>
        /// <returns>The builder</returns>
        public static TBuilder OnRequest<TBuilder>(this TBuilder builder, Func<IServiceProvider, HttpRequestMessage, Task> func) where TBuilder : ISolidHttpBuilder
            => builder.On(func);


        /// <summary>
        /// Add a global handler to be run the moment every response is received
        /// </summary>
        /// <typeparam name="TBuilder">The builder type</typeparam>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="action">The handler to be run</param>
        /// <returns>The builder</returns>
        public static TBuilder OnResponse<TBuilder>(this TBuilder builder, Action<HttpResponseMessage> action) where TBuilder : ISolidHttpBuilder
            => builder.OnResponse((_, c) => action(c));
        /// <summary>
        /// Add a global handler to be run the moment every response is received
        /// </summary>
        /// <typeparam name="TBuilder">The builder type</typeparam>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="action">The handler to be run</param>
        /// <returns>The builder</returns>
        public static TBuilder OnResponse<TBuilder>(this TBuilder builder, Action<IServiceProvider, HttpResponseMessage> action) where TBuilder : ISolidHttpBuilder
            => builder.OnResponse(action.ToAsyncFunc());
        /// <summary>
        /// Add a globa
        /// <typeparam name="TBuilder">The builder type</typeparam>
        /// <param name="builder">The extended ISolidHttpBuilder</param>l handler to be run the moment every response is received
        /// </summary>
        /// <param name="func">The handler to be run</param>
        /// <returns>The builder</returns>
        public static TBuilder OnResponse<TBuilder>(this TBuilder builder, Func<HttpResponseMessage, Task> func) where TBuilder : ISolidHttpBuilder
            => builder.OnResponse((_, c) => func(c));
        /// <summary>
        /// Add a global handler to be run the moment every response is received
        /// </summary>
        /// <typeparam name="TBuilder">The builder type</typeparam>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="func">The handler to be run</param>
        /// <returns>The builder</returns>
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
