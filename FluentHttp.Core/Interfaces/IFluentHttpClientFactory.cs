using System;
using System.Collections.Generic;
using System.Text;

namespace FluentHttp
{
    public interface IFluentHttpClientFactory
    {
        FluentHttpClient Create();
        FluentHttpClient CreateUsingConnectionString(string connectionStringName);
        FluentHttpClient CreateWithBaseAddress(string baseAddress);
        FluentHttpClient CreateWithBaseAddress(Uri baseAddress);
    }
}
