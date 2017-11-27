using System;
using System.Collections.Generic;
using System.Text;

namespace Solid.Http.Events
{
    /// <summary>
    /// The event arguments used when a SolidHttpClient is created
    /// </summary>
    public class SolidHttpClientCreatedEventArgs : EventArgs
    {
        /// <summary>
        /// The application service provider
        /// </summary>
        public IServiceProvider Services { get; internal set; }
        /// <summary>
        /// The SolidHttpClient that was created
        /// </summary>
        public SolidHttpClient Client { get; internal set; }
    }
}
