using System;
using System.Collections.Generic;
using System.Text;

namespace SolidHttp.Extensions.Testing.MSTest
{
    public class MSTestServer<TStartup> : TestServer<TStartup, MSTestAsserter>
        where TStartup : class
    {
    }
}
