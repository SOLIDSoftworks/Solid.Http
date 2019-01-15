using Solid.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http.Abstractions
{
    public static class OnRequestCreatedExtensions
    {
        public static ISolidHttpClient OnRequestCreated(this ISolidHttpClient client, Action<ISolidHttpRequest> action) =>
            client.OnRequestCreated((_, r) => action(r));
    }
}
