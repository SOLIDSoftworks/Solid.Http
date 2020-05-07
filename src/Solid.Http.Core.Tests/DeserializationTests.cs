using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Solid.Http.Core.Tests
{
    public class DeserializationTests
    {
        private string _response;
        private HttpStatusCode _statusCode;
        private ServiceProvider _services;

        public DeserializationTests()
        {
            var services = new ServiceCollection();
            services.ConfigureAll<HttpClientFactoryOptions>(options => options.HttpMessageHandlerBuilderActions.Add(builder => builder.PrimaryHandler = builder.Services.GetService<HttpMessageHandler>()));
            services.AddTransient<HttpMessageHandler>(p => new StaticHttpMessageHandler(_response, _statusCode));            
            services.AddSolidHttpCore();
            _services = services.BuildServiceProvider();
        }

        [Fact]
        public async Task ShouldDeserailizeResponseContent()
        {
            _statusCode = HttpStatusCode.OK;
            _response = "1";

            var factory = _services.GetService<ISolidHttpClientFactory>();
            var consumer = await factory.CreateWithBaseAddressAsync($"http://{nameof(ShouldDeserailizeResponseContent)}");
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

            var factory = _services.GetService<ISolidHttpClientFactory>();
            var consumer = await factory.CreateWithBaseAddressAsync($"http://{nameof(ShouldNotThrowExceptionOnNullContent)}");
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

            var factory = _services.GetService<ISolidHttpClientFactory>();
            var consumer = await factory.CreateWithBaseAddressAsync($"http://{nameof(ShouldNotThrowExceptionOnIncorrectContent)}");
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

            var factory = _services.GetService<ISolidHttpClientFactory>();
            var consumer = await factory.CreateWithBaseAddressAsync($"http://{nameof(ShouldNotThrowExceptionOnEmptyContent)}");
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
                if (!string.IsNullOrEmpty(_response))
                    response.Content = new StringContent(_response, Encoding.UTF8, "text/plain");
                return Task.FromResult(response);
            }
        }
    }
}
