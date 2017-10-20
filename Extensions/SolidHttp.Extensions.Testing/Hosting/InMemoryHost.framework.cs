#if NET461
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace SolidHttp.Extensions.Testing.Hosting
{
    internal class InMemoryHost<TStartup> : InMemoryHostBase, IDisposable
        where TStartup : class
    {
        public InMemoryHost(IConfiguration configuration) 
            : base(configuration)
        {
        }

        protected override IDisposable InitializeHost(IConfiguration configuration)
        {
            var options = new StartOptions
            {
                Port = 0                
            };
            var host = WebApp.Start<TStartup>(options);

            //BaseAddress = "yeah";

            return host;
        }
    }
}
#endif