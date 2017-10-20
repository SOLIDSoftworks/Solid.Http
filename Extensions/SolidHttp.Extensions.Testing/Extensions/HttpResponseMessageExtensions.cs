using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace SolidHttp.Extensions.Testing
{
    internal static class HttpResponseMessageExtensions
    {
        public static IEnumerable<string> GetHeaderValues(this HttpResponseMessage response, string name)
        {
            var values = null as IEnumerable<string>;
            try
            {
                values = response.Headers.GetValues(name);
            }
            catch (InvalidOperationException)
            {
                if (response.Content == null)
                    values = Enumerable.Empty<string>();
                else
                    values = response.Content.Headers.GetValues(name);
            }
            return values;
        }
    }
}
