using System;
using System.Collections.Generic;
using System.Text;

namespace SolidHttp.Extensions.Testing.Assertions
{
    public class AssertionContinuation
    {
        public AssertionContinuation(Assertion assertion)
        {
            Assertion = assertion;
        }
        internal Assertion Assertion { get; private set; }
    }
}
