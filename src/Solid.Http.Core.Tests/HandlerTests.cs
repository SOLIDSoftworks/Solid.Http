using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Solid.Http.Core.Tests
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
            services.AddSolidHttpCore();
            var provider = services.BuildServiceProvider();
            var factory = provider.GetService<ISolidHttpClientFactory>();
            var client = await factory.CreateAsync();
            await client
                .GetAsync("https://jsonplaceholder.typicode.com/posts/1")
                .On(HttpStatusCode.OK, response => assertion1(response))
                .On(HttpStatusCode.OK, response => assertion2(response))
            ;
            Assert.True(asserted1);
            Assert.True(asserted2);
        }

        [Fact]
        public async Task ShouldFailOnSingleHandlerFailure()
        {
            var services = new ServiceCollection();
            services.AddSolidHttpCore();
            var provider = services.BuildServiceProvider();
            var factory = provider.GetService<ISolidHttpClientFactory>();
            var client = await factory.CreateAsync();
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

        [Fact]
        public async Task ShouldNotFailOnBaseAddressClientCreation()
        {
            var services = new ServiceCollection();
            services.AddSolidHttpCore();
            var provider = services.BuildServiceProvider();
            var factory = provider.GetService<ISolidHttpClientFactory>();
            var client = await factory.CreateWithBaseAddressAsync("https://jsonplaceholder.typicode.com/");
            await client
                .GetAsync("posts/1")
            ;
        }
    }
}
