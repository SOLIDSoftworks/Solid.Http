using System;
using System.Collections.Generic;
using System.Text;

namespace FluentHttp
{
    /// <summary>
    /// The event arguments used when a FluentHttpClient is created
    /// </summary>
    public class FluentHttpClientCreatedEventArgs : EventArgs
    {
        /// <summary>
        /// The FluentHttpClient that was created
        /// </summary>
        public FluentHttpClient Client { get; internal set; }
    }
}
