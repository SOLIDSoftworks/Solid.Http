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
    public static class SolidHttpBuilderExtensions
    {
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
            builder.Core.AddDeserializer(factory, mimeType, more);
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
            builder.Core.AddDeserializer<TFactory>(mimeType, more);
            return builder;
        }

        /// <summary>
        /// Configures Solid.Http to use one HttpClient for the whole application
        /// </summary>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <returns>ISolidHttpBuilder</returns>
        public static ISolidHttpBuilder UseSingleInstanceHttpClientProvider(this ISolidHttpBuilder builder)
        {
            builder.Core.UseSingleInstanceHttpClientProvider();
            return builder;
        }

        /// <summary>
        /// Configures Solid.Http to use one HttpClient for each host requested.
        /// </summary>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <returns>ISolidHttpBuilder</returns>
        public static ISolidHttpBuilder UseInstancePerHostHttpClientProvider(this ISolidHttpBuilder builder)
        {
            builder.Core.UseInstancePerHostHttpClientProvider();
            return builder;
        }

        /// <summary>
        /// Configures Solid.Http to use a custom HttpClientProvider
        /// </summary>
        /// <typeparam name="TProvider">The custom HttpClientProvider type</typeparam>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="instance">The HttpClientFactory instance</param>
        /// <returns>ISolidHttpBuilder</returns>
        public static ISolidHttpBuilder UseHttpClientProvider<TProvider>(this ISolidHttpBuilder builder, TProvider instance) where TProvider : HttpClientProvider
        {
            builder.Core.UseHttpClientProvider<TProvider>(instance);
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
            builder.Core.UseHttpClientProvider<TProvider>(factory);
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
            builder.Core.UseHttpClientProvider<TProvider>();
            return builder;
        }
               
        /// <summary>
        /// Add a global handler to be run when every Solid.Http client object is created.
        /// </summary>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="action">The handler to be run</param>
        /// <returns>The builder</returns>
        public static ISolidHttpBuilder OnClientCreated(this ISolidHttpBuilder builder, Action<ISolidHttpClient> action)
        {
            builder.Core.OnClientCreated(action);
            return builder;
        }
        /// <summary>
        /// Add a global handler to be run when every Solid.Http client object is created.
        /// </summary>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="action">The handler to be run</param>
        /// <returns>The builder</returns>
        public static ISolidHttpBuilder OnClientCreated(this ISolidHttpBuilder builder, Action<IServiceProvider, ISolidHttpClient> action)
        {
            builder.Core.OnClientCreated(action);
            return builder;
        }

        /// <summary>
        /// Add a global handler to be run when every Solid.Http request object is created.
        /// </summary>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="action">The handler to be run</param>
        /// <returns>The builder</returns>
        public static ISolidHttpBuilder OnRequestCreated(this ISolidHttpBuilder builder, Action<ISolidHttpRequest> action)
        {
            builder.Core.OnRequestCreated(action);
            return builder;
        }
        /// <summary>
        /// Add a global handler to be run when every Solid.Http request object is created.
        /// </summary>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="action">The handler to be run</param>
        /// <returns>The builder</returns>
        public static ISolidHttpBuilder OnRequestCreated(this ISolidHttpBuilder builder, Action<IServiceProvider, ISolidHttpRequest> action)
        {
            builder.Core.OnRequestCreated(action);
            return builder;
        }

        /// <summary>
        /// Add a global handler to be run just before every request is sent
        /// </summary>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="action">The handler to be run</param>
        /// <returns>The builder</returns>
        public static ISolidHttpBuilder OnRequest(this ISolidHttpBuilder builder, Action<HttpRequestMessage> action)
        {
            builder.Core.OnRequest(action);
            return builder;
        }
        /// <summary>
        /// Add a global handler to be run just before every request is sent
        /// </summary>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="action">The handler to be run</param>
        /// <returns>The builder</returns>
        public static ISolidHttpBuilder OnRequest(this ISolidHttpBuilder builder, Action<IServiceProvider, HttpRequestMessage> action)
        {
            builder.Core.OnRequest(action);
            return builder;
        }
        /// <summary>
        /// Add a global handler to be run just before every request is sent
        /// </summary>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="func">The handler to be run</param>
        /// <returns>The builder</returns>
        public static ISolidHttpBuilder OnRequest(this ISolidHttpBuilder builder, Func<HttpRequestMessage, Task> func)
        {
            builder.Core.OnRequest(func);
            return builder;
        }
        /// <summary>
        /// Add a global handler to be run just before every request is sent
        /// </summary>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="func">The handler to be run</param>
        /// <returns>The builder</returns>
        public static ISolidHttpBuilder OnRequest(this ISolidHttpBuilder builder, Func<IServiceProvider, HttpRequestMessage, Task> func)
        {
            builder.Core.OnRequest(func);
            return builder;
        }

        /// <summary>
        /// Add a global handler to be run the moment every response is received
        /// </summary>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="action">The handler to be run</param>
        /// <returns>The builder</returns>
        public static ISolidHttpBuilder OnResponse(this ISolidHttpBuilder builder, Action<HttpResponseMessage> action)
        {
            builder.Core.OnResponse(action);
            return builder;
        }
        /// <summary>
        /// Add a global handler to be run the moment every response is received
        /// </summary>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="action">The handler to be run</param>
        /// <returns>The builder</returns>
        public static ISolidHttpBuilder OnResponse(this ISolidHttpBuilder builder, Action<IServiceProvider, HttpResponseMessage> action)
        {
            builder.Core.OnResponse(action);
            return builder;
        }
        /// <summary>
        /// Add a globa
        /// <param name="builder">The extended ISolidHttpBuilder</param>l handler to be run the moment every response is received
        /// </summary>
        /// <param name="func">The handler to be run</param>
        /// <returns>The builder</returns>
        public static ISolidHttpBuilder OnResponse(this ISolidHttpBuilder builder, Func<HttpResponseMessage, Task> func)
        {
            builder.Core.OnResponse(func);
            return builder;
        }
        /// <summary>
        /// Add a global handler to be run the moment every response is received
        /// </summary>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <param name="func">The handler to be run</param>
        /// <returns>The builder</returns>
        public static ISolidHttpBuilder OnResponse(this ISolidHttpBuilder builder, Func<IServiceProvider, HttpResponseMessage, Task> func)
        {
            builder.Core.OnResponse(func);
            return builder;
        }
    }
}
