using SolidHttp.Extensions.Testing.Assertions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolidHttp.Extensions.Testing
{
    public static class AssertionExtensions
    {
        public static AssertionContinuation And(this Assertion assertion)
        {
            return new AssertionContinuation(assertion);
        }
    }
}
