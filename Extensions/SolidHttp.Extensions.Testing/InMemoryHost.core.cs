#if NETCOREAPP2_0
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SolidHttp.Extensions.Testing
{
    internal class InMemoryHost<TStartup> : InMemoryHostBase, IDisposable
        where TStartup : class
    {

        protected override IDisposable InitializeHost()
        {
            var builder = new WebHostBuilder();
            var host = builder
                .UseKestrel()
                .UseStartup<TStartup>()
                .Start("http://127.0.0.1:0" /*, "http://[::1]:0"*/);

            var urls = host.ServerFeatures.Get<IServerAddressesFeature>();
            BaseAddress = urls.Addresses.Select(s => new Uri(s)).First();

            return host;
        }
    }
}
#endif