using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Solid.Http.Core.Tests
{
    public class SolidHttpClientFactoryTests
    {
        [Theory]
        [InlineData("https://jsonplaceholder.typicode.com")]
        [InlineData("https://jsonplaceholder.typicode.com/")]
        public async Task ShouldNotFailOnBaseAddressClientCreation(string baseAddress)
        {
            var services = new ServiceCollection();
            services.AddSolidHttpCore();
            var provider = services.BuildServiceProvider();
            var factory = provider.GetService<ISolidHttpClientFactory>();
            var client = factory.CreateWithBaseAddress(baseAddress);
            await client
                .GetAsync("posts/1")
            ;
        }
    }
}
