using System;
using System.Collections.Generic;
using System.Text;

namespace FluentHttp
{
    public interface IFluentHttpClientFactory
    {
        FluentHttpClient Create();
        FluentHttpClient Create(string connectionStringName);

        event EventHandler<FluentHttpClientCreatedEventArgs> ClientCreated;
    }
}
