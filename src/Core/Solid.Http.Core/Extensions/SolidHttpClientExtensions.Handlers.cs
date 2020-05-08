using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http
{
    public static class Solid_Http_SolidHttpClientExtensions_Handlers
    {
        public static ISolidHttpClient OnRequestCreated(this ISolidHttpClient client, Action<ISolidHttpRequest> handler)
            => client.OnRequestCreated(handler.Convert());
    }
}
