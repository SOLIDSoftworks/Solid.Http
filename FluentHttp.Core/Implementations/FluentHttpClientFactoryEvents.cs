using System;
using System.Collections.Generic;
using System.Text;

namespace FluentHttp
{
    internal class FluentHttpClientFactoryEvents : IFluentHttpClientFactoryEventInvoker
    {
        public event EventHandler<FluentHttpClientCreatedEventArgs> OnClientCreated;

        public void InvokeClientCreated(object sender, FluentHttpClient client)
        {
            if (OnClientCreated != null)
                OnClientCreated(sender, new FluentHttpClientCreatedEventArgs { Client = client });
        }
    }
}
