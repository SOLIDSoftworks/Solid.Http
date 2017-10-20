using System;
using System.Collections.Generic;
using System.Text;

namespace SolidHttp.Extensions.Testing
{
    public static class SolidHttpTestingAssertionExtensions
    {
        public static SolidHttpTestingAssertionContinuation And(this SolidHttpTestingAssertion assertion)
        {
            return new SolidHttpTestingAssertionContinuation(assertion);
        }
    }
}
