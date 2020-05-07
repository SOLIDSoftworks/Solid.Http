using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Http
{
    public static class Solid_Http_SolidHttpOptionsExtensions
    {
        public static SolidHttpOptions OnClientCreated(this SolidHttpOptions options, Func<IServiceProvider, ISolidHttpClient, ValueTask> handler)
        {
            options.OnClientCreatedAsync = options.OnClientCreatedAsync.Add(handler);
            return options;
        }

        public static SolidHttpOptions OnClientCreated(this SolidHttpOptions options, Action<ISolidHttpClient> handler)
        {
            options.OnClientCreatedAsync = options.OnClientCreatedAsync.Add(handler);
            return options;
        }

        public static SolidHttpOptions OnClientCreated(this SolidHttpOptions options, Action<IServiceProvider, ISolidHttpClient> handler)
        {
            options.OnClientCreatedAsync = options.OnClientCreatedAsync.Add(handler);
            return options;
        }

        public static SolidHttpOptions OnClientCreated(this SolidHttpOptions options, Func<ISolidHttpClient, ValueTask> handler)
        {
            options.OnClientCreatedAsync = options.OnClientCreatedAsync.Add(handler);
            return options;
        }
    }
}
