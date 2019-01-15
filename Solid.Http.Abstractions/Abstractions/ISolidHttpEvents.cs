//using System;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;

//namespace Solid.Http
//{
//    /// <summary>
//    /// The ISolidHttpClientFactoryEvents interface
//    /// </summary>
//    public interface ISolidHttpEvents
//    {
//        /// <summary>
//        /// The event triggered when a SolidHttpClient is created
//        /// </summary>
//        void OnClientCreated(Action<IServiceProvider, ISolidHttpClient> handler);
//        IEnumerable<Action<IServiceProvider, ISolidHttpClient>> ClientCreatedHandlers { get; }

//        /// <summary>
//        /// The event triggered when a SolidHttpRequest is created
//        /// </summary>
//        void OnRequestCreated(Action<IServiceProvider, ISolidHttpRequest> handler);

//        /// <summary>
//        /// The event triggered before an http request is sent
//        /// </summary>
//        void OnRequest(Action<IServiceProvider, HttpRequestMessage> handler);
//        void OnRequest(Func<IServiceProvider, HttpRequestMessage, Task> handler);

//        /// <summary>
//        /// The event triggered after an http response is received
//        /// </summary>
//        void OnResponse(Action<IServiceProvider, HttpResponseMessage> handler);
//        void OnResponse(Func<IServiceProvider, HttpResponseMessage, Task> handler);
//    }
//}
