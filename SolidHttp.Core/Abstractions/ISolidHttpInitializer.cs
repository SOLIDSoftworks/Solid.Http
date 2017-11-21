using System;
using System.Collections.Generic;
using System.Text;

namespace SolidHttp.Abstractions
{
    public interface ISolidHttpInitializer
    {
        bool IsInitialized { get; }
        void Initialize();
    }
}
