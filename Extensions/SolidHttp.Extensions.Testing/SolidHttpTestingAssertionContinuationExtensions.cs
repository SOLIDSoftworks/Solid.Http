using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SolidHttp.Extensions.Testing
{
    public static class SolidHttpTestingAssertionContinuationExtensions
    {
        public static SolidHttpTestingAssertion Should(this SolidHttpTestingAssertionContinuation continuation, Action<HttpResponseMessage, IAsserter> assert)
        {
            continuation.Assertion.Request.OnResponse += (sender, args) =>
            {
                var asserter = continuation.Assertion.Request.GetAsserter();
                assert(args.Response, asserter);
            };
            return continuation.Assertion;
        }
    }
}
