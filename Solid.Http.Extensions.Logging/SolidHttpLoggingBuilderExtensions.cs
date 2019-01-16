using Microsoft.Extensions.DependencyInjection.Extensions;
using Solid.Http.Abstractions;
using Solid.Http.Extensions.Logging;
using Solid.Http.Extensions.Logging.Abstractions;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// SolidHttpLoggingBuilderExtensions
    /// </summary>
    public static class SolidHttpLoggingBuilderExtensions
    {
        /// <summary>
        /// Adds logging for request and response
        /// </summary>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <returns>ISolidHttpBuilder</returns>
        public static ISolidHttpBuilder AddLogging(this ISolidHttpBuilder builder) => builder.AddLogging<SolidHttpLogger>();

        /// <summary>
        /// Adds logging for request and response
        /// </summary>
        /// <typeparam name="TLogger">The IHttpLogger implementation that performs the logging</typeparam>
        /// <param name="builder">The extended ISolidHttpBuilder</param>
        /// <returns>ISolidHttpBuilder</returns>
        public static ISolidHttpBuilder AddLogging<TLogger>(this ISolidHttpBuilder builder) where TLogger : class, IHttpLogger
        {
            builder.Services.AddLogging();
            builder.Services.TryAddSingleton<IHttpLogger, TLogger>();
            builder.OnRequest(async (provider, request) =>
            {
                var logger = provider.GetService<IHttpLogger>();
                await logger.LogRequestAsync(request);
            });
            builder.OnResponse(async (provider, response) =>
            {
                var logger = provider.GetService<IHttpLogger>();
                await logger.LogResponseAsync(response);
            });
            return builder;
        }
    }
}
