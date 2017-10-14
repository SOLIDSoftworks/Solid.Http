using System;
using System.Collections.Generic;
using System.Text;

namespace SolidHttp
{
    /// <summary>
    /// The event arguments used when a SolidHttpRequest is created
    /// </summary>
    public class SolidHttpRequestCreatedEventArgs : EventArgs
    {
        /// <summary>
        /// The SolidHttpRequest that was created
        /// </summary>
        public SolidHttpRequest Request { get; internal set; }
    }
}
