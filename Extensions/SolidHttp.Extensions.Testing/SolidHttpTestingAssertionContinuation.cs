using System;
using System.Collections.Generic;
using System.Text;

namespace SolidHttp.Extensions.Testing
{
    public class SolidHttpTestingAssertionContinuation
    {
        public SolidHttpTestingAssertionContinuation(SolidHttpTestingAssertion assertion)
        {
            Assertion = assertion;
        }
        internal SolidHttpTestingAssertion Assertion { get; private set; }
    }
}
