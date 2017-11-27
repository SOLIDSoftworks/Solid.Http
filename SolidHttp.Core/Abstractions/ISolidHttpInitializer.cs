using System;
using System.Collections.Generic;
using System.Text;

namespace Solid.Http.Abstractions
{
    public interface ISolidHttpInitializer
    {
        bool IsInitialized { get; }
        void Initialize();
    }
}
