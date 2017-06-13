using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;

namespace FluentHttp.Tests
{
    [TestClass]
    public class FluentHttpClientTests
    {
        [TestMethod]
        public void ShouldCreateFluentRequest()
        {
            var http = new HttpClient();
            var client = new FluentHttpClient(http);
            var source = new CancellationTokenSource();
            var url = new Uri("https://unused.uri");
            var request = client.PerformRequestAsync(HttpMethod.Get, url, source.Token);

            request.Should().NotBeNull();

            request.BaseClient.Should().NotBeNull();
            request.BaseClient.Should().Be(http);

            request.BaseRequest.Should().NotBeNull();
            request.BaseRequest.Method.Should().Be(HttpMethod.Get);
            request.BaseRequest.RequestUri.Should().Be(url);

            request.CancellationToken.Should().NotBeNull();
            request.CancellationToken.Should().Be(source.Token);
        }
    }
}
