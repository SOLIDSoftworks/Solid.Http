using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FluentHttp.Tests
{
    public class FluentHttpClientFactoryTests
    {
        private Mock<IConfiguration> _configurationMock;
        private Mock<IHttpClientCache> _cacheMock;

        private readonly string _baseUrl = "https://test.uri";

        public FluentHttpClientFactoryTests()
        {
            var sectionMock = new Mock<IConfigurationSection>();
            sectionMock.Setup(s => s["test"]).Returns(_baseUrl);
            _configurationMock = new Mock<IConfiguration>();
            _configurationMock.Setup(c => c.GetSection("ConnectionStrings")).Returns(() => sectionMock.Object);
            _cacheMock = new Mock<IHttpClientCache>();
        }

        [Fact]
        public void ShouldCreateAbsoluteUrlClient()
        {
            var factory = new FluentHttpClientFactory(_cacheMock.Object, null, _configurationMock.Object);
            var client = factory.Create();

            _cacheMock.Verify(c => c.Exists(FluentHttpClientFactory.ABSOLUTE_CLIENT_KEY), Times.Once());
            _cacheMock.Verify(c => c.Cache(FluentHttpClientFactory.ABSOLUTE_CLIENT_KEY, It.Is<HttpClient>(http => http.BaseAddress == null)), Times.Once());
            _cacheMock.Verify(c => c.Get(FluentHttpClientFactory.ABSOLUTE_CLIENT_KEY), Times.Once());

            Assert.NotNull(client);
        }

        [Fact]
        public void ShouldGetCachedAbsoluteUrlClient()
        {
            _cacheMock.Setup(c => c.Exists(FluentHttpClientFactory.ABSOLUTE_CLIENT_KEY)).Returns(true);

            var factory = new FluentHttpClientFactory(_cacheMock.Object, null, _configurationMock.Object);
            var client = factory.Create();

            _cacheMock.Verify(c => c.Exists(FluentHttpClientFactory.ABSOLUTE_CLIENT_KEY), Times.Once());
            _cacheMock.Verify(c => c.Cache(FluentHttpClientFactory.ABSOLUTE_CLIENT_KEY, It.IsAny<HttpClient>()), Times.Never());
            _cacheMock.Verify(c => c.Get(FluentHttpClientFactory.ABSOLUTE_CLIENT_KEY), Times.Once());

			Assert.NotNull(client);
        }

        [Fact]
        public void ShouldThrowExceptionIfNoConfiguration()
        {
            var key = "test";
            var factory = new FluentHttpClientFactory(_cacheMock.Object, null);

			//var message =
				//"FluentHttpClientFactory was created with a null configuration." + Environment.NewLine +
				//"If you are creating FluentHttpClientFactory manually, please provide an IConfigurationRoot in the constructor of FluentHttpClientFactory." + Environment.NewLine +
				//"If you are calling services.UseFluentHttp(), then change that call to services.UseFluentHttp(configuration) where configuration is the application configuration.";
            Assert.Throws<InvalidOperationException>(() => factory.Create(key));
        }

        [Fact]
        public void ShouldCreateConnectionStringClient()
        {
            var key = "test";
            var factory = new FluentHttpClientFactory(_cacheMock.Object, null, _configurationMock.Object);
            var client = factory.Create(key);

            _cacheMock.Verify(c => c.Exists(key), Times.Once());
            _cacheMock.Verify(c => c.Cache(key, It.Is<HttpClient>(http => http.BaseAddress == new Uri(_baseUrl))), Times.Once());
            _cacheMock.Verify(c => c.Get(key), Times.Once());

			Assert.NotNull(client);
        }

        [Fact]
        public void ShouldGetCachedConnectionStringClient()
        {
            var key = "test";
            _cacheMock.Setup(c => c.Exists(key)).Returns(true);

            var factory = new FluentHttpClientFactory(_cacheMock.Object, null, _configurationMock.Object);
            var client = factory.Create(key);

            _cacheMock.Verify(c => c.Exists(key), Times.Once());
            _cacheMock.Verify(c => c.Cache(key, It.IsAny<HttpClient>()), Times.Never());
            _cacheMock.Verify(c => c.Get(key), Times.Once());

			Assert.NotNull(client);
        }
    }
}
