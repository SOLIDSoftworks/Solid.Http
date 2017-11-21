using System;
using System.Collections.Generic;
using System.Text;

namespace SolidHttp.Abstractions
{
    internal interface ISolidHttpEventHandlerProvider
    {
        IEnumerable<Action<object, RequestEventArgs>> GetOnRequestEventHandlers();
        IEnumerable<Action<object, ResponseEventArgs>> GetOnResponseEventHandlers();
        IEnumerable<Action<object, SolidHttpClientCreatedEventArgs>> GetOnClientCreatedEventHandlers();
        IEnumerable<Action<object, SolidHttpRequestCreatedEventArgs>> GetOnRequestCreatedEventHandlers();
    }
}
