using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace FluentHttp
{
    public class RequestEventArgs : EventArgs
    {
        public HttpRequestMessage Request { get; internal set; }
    }
}
