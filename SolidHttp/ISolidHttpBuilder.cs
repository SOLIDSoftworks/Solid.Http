using Microsoft.Extensions.DependencyInjection;
using SolidHttp.Abstractions;
using System;

namespace SolidHttp
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