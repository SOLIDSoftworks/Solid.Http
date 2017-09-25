using System;
using System.Collections.Generic;
using System.Text;

namespace FluentHttp
{
    public class FluentHttpClientCreatedEventArgs : EventArgs
    {
        public FluentHttpClient Client { get; internal set; }
    }
}
