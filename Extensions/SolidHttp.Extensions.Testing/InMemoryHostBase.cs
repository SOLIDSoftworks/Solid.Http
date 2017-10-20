using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolidHttp.Extensions.Testing
{
    public abstract class InMemoryHostBase : IDisposable
    {
        protected IDisposable Host;

        public InMemoryHostBase(IConfiguration configuration)
        {
            Host = InitializeHost(configuration);
        }

        protected abstract IDisposable InitializeHost(IConfiguration configuration);
        
        public Uri BaseAddress { get; protected set; }

        public void Dispose()
        {
            if (Host != null) Host.Dispose();
        }
    }
}
