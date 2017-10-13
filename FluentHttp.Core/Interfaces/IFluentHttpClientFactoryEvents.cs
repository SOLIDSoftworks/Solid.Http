using System;
using System.Collections.Generic;
using System.Text;

namespace FluentHttp
{
    /// <summary>
    /// The IFluentHttpClientFactoryEvents interface
    /// </summary>
    public interface IFluentHttpClientFactoryEvents
    {
        /// <summary>
        /// The event triggered when a FluentHttpClient is created
        /// </summary>
        event EventHandler<FluentHttpClientCreatedEventArgs> OnClientCreated;
    }

    /// <summary>
    /// The IFluentHttpClientFactoryEventInvoker interface
    /// </summary>
    public interface IFluentHttpClientFactoryEventInvoker : IFluentHttpClientFactoryEvents
    {
        /// <summary>
        /// Invokes any and all OnClientCreated events configured
        /// </summary>
        /// <param name="invoker">The invoker of the event</param>
        /// <param name="client">The created client</param>
        void InvokeClientCreated(object invoker, FluentHttpClient client);
    }
}
