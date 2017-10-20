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

        public static SolidHttpTestingAssertion ShouldHaveResponseHeader(this SolidHttpTestingAssertionContinuation continuation, string name)
        {
            return continuation.Should((response, asserter) =>
            {
                var contains = response.Headers.Contains(name);
                if (!contains && response.Content != null)
                    contains = response.Content.Headers.Contains(name);
                asserter.IsTrue(contains, $"Expected '{name}' header");
            });            
        }
    }
}
