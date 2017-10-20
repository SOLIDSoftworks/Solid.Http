using System;
using System.Collections.Generic;
using System.Text;

namespace SolidHttp.Extensions.Testing.Xunit
{
    public class XUnitTestServer<TStartup> : TestServer<TStartup, XUnitAsserter>
        where TStartup : class
    {
    }
}
