using Microsoft.Extensions.DependencyInjection;
using Solid.Http.Abstractions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Solid.Http.Tests
{
    public class HandlerTests
    {

        [Fact]
        public async Task ShouldInvokeMultipleHandlers()
        {
            var asserted1 = false;
            var assertion1 = new Action<HttpResponseMessage>(_ => asserted1 = true);
            var asserted2 = false;
            var assertion2 = new Action<HttpResponseMessage>(_ => asserted2 = true);

            var services = new ServiceCollection();
            services.AddSolidHttp();
            var root = services.BuildServiceProvider();
            using (var scope = root.CreateScope())
            {
                var provider = scope.ServiceProvider;
                var factory = provider.GetService<ISolidHttpClientFactory>();
                var client = factory.Create();
                await client
                    .GetAsync("https://jsonplaceholder.typicode.com/posts/1")
                    .On(HttpStatusCode.OK, response => assertion1(response))
                    .On(HttpStatusCode.OK, response => assertion2(response))
                ;
            }
            Assert.True(asserted1);
            Assert.True(asserted2);
        }

        [Fact]
        public async Task ShouldFailOnSingleHandlerFailure()
        {
            var services = new ServiceCollection();
            services.AddSolidHttp();
            var root = services.BuildServiceProvider();
            using (var scope = root.CreateScope())
            {
                var provider = scope.ServiceProvider;
                var factory = provider.GetService<ISolidHttpClientFactory>();
                var client = factory.Create();
                var exception = null as Exception;
                try
                {
                    await client
                        .GetAsync("https://jsonplaceholder.typicode.com/posts/1")
                        .On(HttpStatusCode.OK, response => throw new Exception("Exception"))
                        .On(HttpStatusCode.OK, _ => { })
                    ;
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
                Assert.NotNull(exception);
                Assert.Equal("Exception", exception.Message);
            }
        }

        [Fact]
        public async Task ShouldNotFailOnBaseAddressClientCreation()
        {
            var services = new ServiceCollection();
            services.AddSolidHttp();
            var root = services.BuildServiceProvider();
            using (var scope = root.CreateScope())
            {
                var provider = scope.ServiceProvider;
                var factory = provider.GetService<ISolidHttpClientFactory>();
                var client = factory.CreateWithBaseAddress("https://jsonplaceholder.typicode.com/");
                    await client
                        .GetAsync("posts/1")
                    ;
            }
        }
    }
}
