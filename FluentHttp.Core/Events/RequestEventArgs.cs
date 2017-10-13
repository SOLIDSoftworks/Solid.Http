using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace FluentHttp
{
    /// <summary>
    /// The event arguments used before an HttpRequestMessage is sent
    /// </summary>
    public class RequestEventArgs : EventArgs
    {
        /// <summary>
        /// The HttpRequestMessage to be sent
        /// </summary>
        public HttpRequestMessage Request { get; internal set; }
    }
}
