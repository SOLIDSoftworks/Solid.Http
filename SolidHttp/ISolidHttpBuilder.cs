using Microsoft.Extensions.DependencyInjection;
using Solid.Http.Abstractions;
using System;

namespace Solid.Http
{
    public interface ISolidHttpBuilder
    {
        /// <summary>
        /// Configure SolidHttp
        /// </summary>
        /// <param name="configure">The method used to configure SolidHttp</param>
        /// <returns>ISolidHttpSetup</returns>
        ISolidHttpBuilder AddSolidHttpOptions(Action<ISolidHttpOptions> configure);

        IServiceCollection Services { get; }
    }
}