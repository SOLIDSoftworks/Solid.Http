using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace FluentHttp
{
    public class ResponseEventArgs : EventArgs
    {
        public HttpResponseMessage Response { get; internal set; }
    }
}
