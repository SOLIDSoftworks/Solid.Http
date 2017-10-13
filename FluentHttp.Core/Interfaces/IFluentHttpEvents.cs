using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace FluentHttp
{
    /// <summary>
    /// The IFluentHttpClientFactoryEvents interface
    /// </summary>
    public interface IFluentHttpEvents
    {
        /// <summary>
        /// The event triggered when a FluentHttpClient is created
        /// </summary>
        event EventHandler<FluentHttpClientCreatedEventArgs> OnClientCreated;

        /// <summary>
        /// The event triggered when a FluentHttpRequest is created
        /// </summary>
        event EventHandler<FluentHttpRequestCreatedEventArgs> OnRequestCreated;

        /// <summary>
        /// The event triggered before an http request is sent
        /// </summary>
        event EventHandler<RequestEventArgs> OnRequest;

        /// <summary>
        /// The event triggered after an http response is received
        /// </summary>
        event EventHandler<ResponseEventArgs> OnResponse;
    }

    /// <summary>
    /// The IFluentHttpClientFactoryEventInvoker interface
    /// </summary>
    public interface IFluentHttpEventInvoker : IFluentHttpEvents
    {
        /// <summary>
        /// Invokes any and all OnClientCreated events configured
        /// </summary>
        /// <param name="invoker">The invoker of the event</param>
        /// <param name="client">The created client</param>
        void InvokeOnClientCreated(object invoker, FluentHttpClient client);

        /// <summary>
        /// Invokes any and all OnRequestCreated events that are globally configured
        /// </summary>
        /// <param name="invoker">The invoker of the event</param>
        /// <param name="request">The created FluentHttpRequest</param>
        void InvokeOnRequestCreated(object invoker, FluentHttpRequest request);

        /// <summary>
        /// Invokes any and all OnRequest events that are globally configured
        /// </summary>
        /// <param name="invoker">The invoker of the event</param>
        /// <param name="request">The HttpRequestMessage to be sent</param>
        void InvokeOnRequest(object invoker, HttpRequestMessage request);

        /// <summary>
        /// Invokes any and all OnResponse events that are globally configured
        /// </summary>
        /// <param name="invoker">The invoker of the event</param>
        /// <param name="response">The received HttpResponseMessage</param>
        void InvokeOnResponse(object invoker, HttpResponseMessage response);
    }
}
