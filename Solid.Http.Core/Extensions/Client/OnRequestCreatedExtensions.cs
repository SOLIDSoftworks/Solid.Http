using Solid.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http.Abstractions
{
    /// <summary>
    /// OnRequestCreated extensions methods
    /// </summary>
    public static class OnRequestCreatedExtensions
    {
        /// <summary>
        /// Add a handler to be run when Solid.Http request object is created.
        /// </summary>
        /// <param name="client">The Solid.Http client</param>
        /// <param name="action">The handler to be run</param>
        /// <returns>ISolidHttpClient</returns>
        public static ISolidHttpClient OnRequestCreated(this ISolidHttpClient client, Action<ISolidHttpRequest> action) =>
            client.OnRequestCreated((_, r) => action(r));
    }
}
