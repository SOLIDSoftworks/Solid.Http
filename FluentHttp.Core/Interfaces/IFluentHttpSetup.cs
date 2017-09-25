using System;
using System.Collections.Generic;
using System.Text;

namespace FluentHttp
{
    public interface IFluentHttpSetup
    {
        IFluentHttpSetup Configure(Action<IFluentHttpOptions> configure);
    }
}
