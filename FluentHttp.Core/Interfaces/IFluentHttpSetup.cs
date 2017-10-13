using System;
using System.Collections.Generic;
using System.Text;

namespace FluentHttp
{
    /// <summary>
    /// The IFluentHttpSetup interface
    /// </summary>
    public interface IFluentHttpSetup
    {
        /// <summary>
        /// Configure FluentHttp
        /// </summary>
        /// <param name="configure">The method used to configure FluentHttp</param>
        /// <returns>IFluentHttpSetup</returns>
        IFluentHttpSetup Configure(Action<IFluentHttpOptions> configure);
    }
}
