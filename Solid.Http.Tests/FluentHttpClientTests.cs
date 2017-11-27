using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FluentHttp.Tests
{
    public class FluentHttpClientTests
    {
        [Fact]
        public void ShouldCreateFluentRequest()
        {
            var http = new HttpClient();
            var client = new FluentHttpClient(http, null);
            var source = new CancellationTokenSource();
            var cancellationToken = source.Token;
            var url = new Uri("https://unused.uri");
            var request = client.PerformRequestAsync(HttpMethod.Get, url, cancellationToken);

            Assert.NotNull(request);
            //Assert.NotNull(request.BaseClient);
            //Assert.Same(http, request.BaseClient);
            Assert.NotNull(request.BaseRequest);
            Assert.Same(HttpMethod.Get, request.BaseRequest.Method);
            Assert.Same(url, request.BaseRequest.RequestUri);
            Assert.NotNull(request.CancellationToken);
        }
    }
}
