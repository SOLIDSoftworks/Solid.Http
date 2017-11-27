using Solid.Http.Events;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Solid.Http.Abstractions
{
    /// <summary>
    /// The ISolidHttpClientFactoryEvents interface
    /// </summary>
    public interface ISolidHttpEvents
    {
        /// <summary>
        /// The event triggered when a SolidHttpClient is created
        /// </summary>
        event EventHandler<SolidHttpClientCreatedEventArgs> OnClientCreated;

        /// <summary>
        /// The event triggered when a SolidHttpRequest is created
        /// </summary>
        event EventHandler<SolidHttpRequestCreatedEventArgs> OnRequestCreated;

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
    /// The ISolidHttpClientFactoryEventInvoker interface
    /// </summary>
    public interface ISolidHttpEventInvoker
    {
        /// <summary>
        /// Invokes any and all OnClientCreated events configured
        /// </summary>
        /// <param name="invoker">The invoker of the event</param>
        /// <param name="client">The created client</param>
        void InvokeOnClientCreated(object invoker, SolidHttpClient client);

        /// <summary>
        /// Invokes any and all OnRequestCreated events that are globally configured
        /// </summary>
        /// <param name="invoker">The invoker of the event</param>
        /// <param name="request">The created SolidHttpRequest</param>
        void InvokeOnRequestCreated(object invoker, SolidHttpRequest request);

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

        RequestEventArgs CreateArgs(HttpRequestMessage request);
        ResponseEventArgs CreateArgs(HttpResponseMessage response);
        SolidHttpClientCreatedEventArgs CreateArgs(SolidHttpClient client);
        SolidHttpRequestCreatedEventArgs CreateArgs(SolidHttpRequest request);
    }
}
