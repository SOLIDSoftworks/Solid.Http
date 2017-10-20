using System;
using System.Collections.Generic;
using System.Text;

namespace SolidHttp.Extensions.Testing
{
    public abstract class InMemoryHostBase : IDisposable
    {
        protected IDisposable Host;

        public InMemoryHostBase()
        {
            Host = InitializeHost();
        }

        protected abstract IDisposable InitializeHost();
        
        public Uri BaseAddress { get; protected set; }

        public void Dispose()
        {
            if (Host != null) Host.Dispose();
        }
    }
}
