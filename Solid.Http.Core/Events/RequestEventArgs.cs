using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Solid.Http.Events
{
    /// <summary>
    /// The event arguments used before an HttpRequestMessage is sent
    /// </summary>
    public class RequestEventArgs : EventArgs
    {
        /// <summary>
        /// The application service provider
        /// </summary>
        public IServiceProvider Services { get; internal set; }
        /// <summary>
        /// The HttpRequestMessage to be sent
        /// </summary>
        public HttpRequestMessage Request { get; internal set; }
    }
}
