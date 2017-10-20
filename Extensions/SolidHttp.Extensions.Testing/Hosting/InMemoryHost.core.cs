#if NETCOREAPP2_0
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace SolidHttp.Extensions.Testing.Hosting
{
    internal class InMemoryHost<TStartup> : InMemoryHostBase, IDisposable
        where TStartup : class
    {
        public InMemoryHost(IConfiguration configuration) : base(configuration)
        {
        }

        protected override IDisposable InitializeHost(IConfiguration configuration)
        {
            var builder = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<TStartup>();
            if (configuration != null)
                builder.UseConfiguration(configuration);

            var host = builder
                .Start("http://127.0.0.1:0" /*, "http://[::1]:0"*/);

            var urls = host.ServerFeatures.Get<IServerAddressesFeature>();
            BaseAddress = urls.Addresses.Select(s => new Uri(s)).First();

            return host;
        }
    }
}
#endif