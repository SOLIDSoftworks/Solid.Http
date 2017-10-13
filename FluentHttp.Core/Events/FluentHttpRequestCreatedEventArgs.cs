using System;
using System.Collections.Generic;
using System.Text;

namespace FluentHttp
{
    /// <summary>
    /// The event arguments used when a FluentHttpRequest is created
    /// </summary>
    public class FluentHttpRequestCreatedEventArgs : EventArgs
    {
        /// <summary>
        /// The FluentHttpRequest that was created
        /// </summary>
        public FluentHttpRequest Request { get; internal set; }
    }
}
