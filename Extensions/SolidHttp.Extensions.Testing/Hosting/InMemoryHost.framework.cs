#if NET461
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Net.Sockets;
using System.Net;

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
            var listenerType = typeof(Microsoft.Owin.Host.HttpListener.OwinServerFactory);

            var port = GetFreeTcpPort();
            var options = new StartOptions
            {
                Port = port,
                //ServerFactory = typeof(TStartup).Assembly.GetName().Name
            };
            var host = WebApp.Start<TStartup>(options);

            BaseAddress = new Uri($"http://localhost:{port}");

            return host;
        }

        private int GetFreeTcpPort()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            int port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }
    }
}
#endif