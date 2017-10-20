using SolidHttp.Extensions.Testing.Abstractions;
using SolidHttp.Extensions.Testing.Assertions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SolidHttp.Extensions.Testing
{
    public static class SolidHttpRequestTestingExtensions
    {
        public static Assertion ShouldResponseWith(this SolidHttpRequest request, HttpStatusCode statusCode)
        {
            return request.ShouldRespondWith((int)statusCode);
        }

        public static Assertion ShouldRespondWith(this SolidHttpRequest request, int statusCode)
        {
            var assertion = new Assertion(request);
            assertion.Request.OnResponse += (sender, args) =>
            {
                var r = (SolidHttpRequest)sender;
                var asserter = r.GetAsserter();
                asserter.AreEqual(statusCode, (int)args.Response.StatusCode, $"Expected {statusCode} status code. Got {args.Response.StatusCode} instead.");
            };
            return assertion;
        }

        public static Assertion ShouldRespondSuccessfully(this SolidHttpRequest request)
        {
            var assertion = new Assertion(request);
            assertion.Request.OnResponse += (sender, args) =>
            {
                var r = (SolidHttpRequest)sender;
                var asserter = r.GetAsserter();
                asserter.IsTrue(args.Response.IsSuccessStatusCode, $"Expected successful status code. Got {args.Response.StatusCode} instead.");
            };
            return assertion;
        }

        internal static IAsserter GetAsserter(this SolidHttpRequest request)
        {
            return request.Client.GetProperty<IAsserter>(Constants.AsserterKey);
        }
    }
}
