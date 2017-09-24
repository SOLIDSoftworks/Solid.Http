using System;
using System.Collections.Generic;
using System.Text;

namespace FluentHttp
{
    public class FluentHttpRequestCreatedEventArgs : EventArgs
    {
        public FluentHttpRequest Request { get; internal set; }
    }
}
