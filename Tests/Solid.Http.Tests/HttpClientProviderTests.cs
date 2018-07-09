using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Solid.Http.Abstractions;
using Solid.Http.Factories;
using Solid.Http.Providers;
using Xunit;

namespace Solid.Http.Core.Tests
{
    public class HttpClientProviderTests
    {
        [Fact]
        public void ShouldUseFauxHttpClientFactory()
        {
            var builder = new SolidHttpCoreBuilder();
            builder.Build();
            var provider = builder.Provider.CreateScope().ServiceProvider.GetService<IHttpClientProvider>() as HttpClientProvider;
            Assert.IsType<FauxHttpClientFactory>(provider.Factory);
        }

        [Fact]
        public void ShouldUseDefaultHttpClientFactory()
        {
            var services = new ServiceCollection();
            services.AddHttpClient();

            var builder = new SolidHttpCoreBuilder(services);
            builder.Build();
            var provider = builder.Provider.CreateScope().ServiceProvider.GetService<IHttpClientProvider>() as HttpClientProvider;
            var type = provider.Factory.GetType();
            Assert.Equal("Microsoft.Extensions.Http.DefaultHttpClientFactory", type.FullName);
        }

        [Fact]
        public void ShouldUseCustomHttpClientFactory()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IHttpClientFactory, CustomHttpClientFactory>();

            var builder = new SolidHttpCoreBuilder(services);
            builder.Build();
            var provider = builder.Provider.CreateScope().ServiceProvider.GetService<IHttpClientProvider>() as HttpClientProvider;
            Assert.IsType<CustomHttpClientFactory>(provider.Factory);
        }

        class CustomHttpClientFactory : IHttpClientFactory
        {
            public HttpClient CreateClient(string name)
            {
                throw new NotImplementedException();
            }
        }
    }
}
