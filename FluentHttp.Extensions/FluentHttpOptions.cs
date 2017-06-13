using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentHttp
{
    public class FluentHttpOptions
    {
        public IConfigurationRoot Configuration { get; set; }
        public Action<object, FluentHttpClientCreatedEventArgs> OnClientCreated;
        public Action<object, FluentHttpRequestCreatedEventArgs> OnRequestCreated;
    }
}
