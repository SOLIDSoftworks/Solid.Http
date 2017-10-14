using System;
using System.Collections.Generic;
using System.Text;

namespace SolidHttp
{
    /// <summary>
    /// The event arguments used when a SolidHttpClient is created
    /// </summary>
    public class SolidHttpClientCreatedEventArgs : EventArgs
    {
        /// <summary>
        /// The SolidHttpClient that was created
        /// </summary>
        public SolidHttpClient Client { get; internal set; }
    }
}
