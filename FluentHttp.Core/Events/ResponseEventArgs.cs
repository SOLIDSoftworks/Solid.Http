using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace FluentHttp
{
    /// <summary>
    /// The event arguments used when an HttpResponseMessage is received
    /// </summary>
    public class ResponseEventArgs : EventArgs
    {
        /// <summary>
        /// The HttpResponseMessage received
        /// </summary>
        public HttpResponseMessage Response { get; internal set; }
    }
}
