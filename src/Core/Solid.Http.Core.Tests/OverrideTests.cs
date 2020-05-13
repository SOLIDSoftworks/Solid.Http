using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Solid.Http.Core.Tests
{
    public class OverrideTests
    {
        [Fact]
        public async Task ShouldOverrideHttpClientFactory()
        {
            var handler = new Mock<HttpMessageHandler>();
            var factory = new Mock<IHttpClientFactory>();
            factory.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(() => new HttpClient(handler.Object));
            var services = new ServiceCollection()
                .AddSingleton<IHttpClientFactory>(factory.Object)
                .AddSolidHttpCore()
                .BuildServiceProvider()
            ;

            var client = services.GetService<ISolidHttpClientFactory>().Create();
            try
            {
                _ = await client.GetAsync("http://notused");
            }
            catch(InvalidOperationException) { }

            factory.Verify(f => f.CreateClient(It.IsAny<string>()));
        }
    }
}
