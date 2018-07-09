using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

using Xunit;

namespace Solid.Http.Core.Tests
{
    public class DeserializationTests : IDisposable
    {
        private ServiceProvider _root;
        private IServiceScope _scope;

        private string _response;
        private HttpStatusCode _statusCode;

        public DeserializationTests()
        {
            var services = new ServiceCollection();
            services.AddSingleton<HttpMessageHandler>(p => new StaticHttpMessageHandler(_response, _statusCode));
            services.AddSingleton<IHttpClientFactory, Factory>();
            services.AddSolidHttpCore();
            _root = services.BuildServiceProvider();
            _scope = _root.CreateScope();
        }

        public void Dispose()
        {
            _scope.Dispose();
            _root.Dispose();
        }

        [Fact]
        public async Task ShouldDeserailizeResponseContent()
        {
            _statusCode = HttpStatusCode.OK;
            _response = "1";

            var factory = _scope.ServiceProvider.GetService<ISolidHttpClientFactory>();
            var consumer = factory.CreateWithBaseAddress("http://notused");
            var response = await consumer
                .GetAsync("")
                .As(async content =>
                {
                    var body = await content.ReadAsStringAsync();
                    return int.Parse(body);
                });

            Assert.Equal(1, response);
        }

        [Fact]
        public async Task ShouldNotThrowExceptionOnNullContent()
        {
            _statusCode = HttpStatusCode.OK;

            var factory = _scope.ServiceProvider.GetService<ISolidHttpClientFactory>();
            var consumer = factory.CreateWithBaseAddress("http://notused");
            var response = await consumer
                .GetAsync("")
                .As(async content =>
                {
                    var body = await content.ReadAsStringAsync();
                    return int.Parse(body);
                });

            Assert.Equal(0, response);
        }


        [Fact]
        public async Task ShouldNotThrowExceptionOnIncorrectContent()
        {
            _statusCode = HttpStatusCode.OK;
            _response = "foo";

            var factory = _scope.ServiceProvider.GetService<ISolidHttpClientFactory>();
            var consumer = factory.CreateWithBaseAddress("http://notused");
            var response = await consumer
                .GetAsync("")
                .IgnoreSerializationError()
                .As(async content =>
                {
                    var body = await content.ReadAsStringAsync();
                    return int.Parse(body);
                });

            Assert.Equal(0, response);
        }

        [Fact]
        public async Task ShouldNotThrowExceptionOnEmptyContent()
        {
            _statusCode = HttpStatusCode.OK;
            _response = "";

            var factory = _scope.ServiceProvider.GetService<ISolidHttpClientFactory>();
            var consumer = factory.CreateWithBaseAddress("http://notused");
            var response = await consumer
                .GetAsync("")
                .As(async content =>
                {
                    var body = await content.ReadAsStringAsync();
                    return int.Parse(body);
                });

            Assert.Equal(0, response);
        }

        class StaticHttpMessageHandler : HttpMessageHandler
        {
            private HttpStatusCode _statusCode;
            private string _response;

            public StaticHttpMessageHandler(string response, HttpStatusCode statusCode)
            {
                _statusCode = statusCode;
                _response = response;
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                var response = new HttpResponseMessage(_statusCode);
                if(!string.IsNullOrEmpty(_response))
                    response.Content = new StringContent(_response, Encoding.UTF8, "text/plain");
                return Task.FromResult(response);
            }
        }

        class Factory : IHttpClientFactory
        {
            private HttpMessageHandler _handler;

            public Factory(HttpMessageHandler handler)
            {
                _handler = handler;
            }

            public HttpClient CreateClient(string name)
            {
                return new HttpClient(_handler);
            }
        }


    }
}
