#if NET461
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolidHttp.Extensions.Testing
{
    internal class InMemoryHost<TStartup> : InMemoryHostBase
        where TStartup : class
    {
        protected override IDisposable InitializeHost()
        {
            var options = new StartOptions
            {
                Port = 0
            };
            var host = WebApp.Start<TStartup>(options);

            //BaseAddress = "poop";

            return host;
        }
    }
}
#endif