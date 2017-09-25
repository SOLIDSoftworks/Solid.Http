using System;
using System.Collections.Generic;
using System.Text;

namespace FluentHttp
{
    public interface IFluentHttpClientFactoryEvents
    {
        event EventHandler<FluentHttpClientCreatedEventArgs> OnClientCreated;
    }

    public interface IFluentHttpClientFactoryEventInvoker : IFluentHttpClientFactoryEvents
    {
        void InvokeClientCreated(object invoker, FluentHttpClient client);
    }
}
