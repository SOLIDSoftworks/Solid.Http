using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentHttp
{
    public interface IFluentHttpClientFactory
    {
        IConfiguration Configuration { get; }
        FluentHttpClient Create();
    }
}
