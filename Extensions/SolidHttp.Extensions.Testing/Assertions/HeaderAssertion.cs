using System;
using System.Collections.Generic;
using System.Text;

namespace SolidHttp.Extensions.Testing.Assertions
{
    public class HeaderAssertion : Assertion
    {
        internal HeaderAssertion(AssertionContinuation continuation, string name) 
            : base(continuation.Assertion.Request)
        {
            Continuation = continuation;
            Name = name;
        }

        public AssertionContinuation Continuation { get; }
        internal string Name { get; }
    }
}
