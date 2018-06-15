using System;
namespace Solid.Http.Abstractions
{
    public interface ISolidHttpOptionsBuilder<TBuilder>
    {
        /// <summary>
        /// Configure SolidHttp
        /// </summary>
        /// <param name="configure">The method used to configure SolidHttp</param>
        /// <returns>ISolidHttpSetup</returns>
        TBuilder AddSolidHttpOptions(Action<ISolidHttpOptions> configure);
    }
}
