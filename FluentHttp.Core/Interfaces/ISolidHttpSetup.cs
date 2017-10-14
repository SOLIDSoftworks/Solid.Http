using System;
using System.Collections.Generic;
using System.Text;

namespace SolidHttp
{
    /// <summary>
    /// The ISolidHttpSetup interface
    /// </summary>
    public interface ISolidHttpSetup
    {
        /// <summary>
        /// Configure SolidHttp
        /// </summary>
        /// <param name="configure">The method used to configure SolidHttp</param>
        /// <returns>ISolidHttpSetup</returns>
        ISolidHttpSetup Configure(Action<ISolidHttpOptions> configure);
    }
}
