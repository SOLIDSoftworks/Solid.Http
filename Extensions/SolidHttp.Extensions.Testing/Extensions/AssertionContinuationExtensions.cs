using SolidHttp.Extensions.Testing.Abstractions;
using SolidHttp.Extensions.Testing.Assertions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SolidHttp.Extensions.Testing
{
    public static class AssertionContinuationExtensions
    {
        public static Assertion Should(this AssertionContinuation continuation, Action<HttpResponseMessage, IAsserter> assert)
        {
            continuation.Assertion.Request.OnResponse += (sender, args) =>
            {
                var asserter = continuation.Assertion.Request.GetAsserter();
                assert(args.Response, asserter);
            };
            return continuation.Assertion;
        }

        public static HeaderAssertion ShouldHaveResponseHeader(this AssertionContinuation continuation, string name)
        {
            continuation.Should((response, asserter) =>
            {
                var contains = false;
                try
                {
                    contains = response.Headers.Contains(name);
                }
                catch(InvalidOperationException) // if checking on content header
                {
                    if(response.Content != null)
                        contains = response.Content.Headers.Contains(name);
                }       
                asserter.IsTrue(contains, $"Expected '{name}' header");
            });
            return new HeaderAssertion(continuation, name);            
        }
    }
}
