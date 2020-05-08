using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Solid.Http.Core.Tests
{
    public class BaseAddressTests
    {
        private ISolidHttpClientFactory _factory;

        public BaseAddressTests()
        {
            var services = new ServiceCollection();
            services.AddSolidHttpCore();
            var provider = services.BuildServiceProvider();
            _factory = provider.GetService<ISolidHttpClientFactory>();
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionOnNullUri()
        {
            var exception = null as ArgumentNullException;
            try
            {
                _factory.CreateWithBaseAddress(null as Uri);
            }
            catch (ArgumentNullException ex)
            {
                exception = ex;
            }
            finally
            {
                Assert.NotNull(exception);
                Assert.Equal("baseAddress", exception.ParamName);
            }
        }

        [Theory]
        [InlineData("http://some.url?")]
        [InlineData("http://some.url?query")]
        [InlineData("http://some.url?query=string")]
        public void ShouldThrowArgumentExceptionOnQueryString(string url)
        {
            var exception = null as ArgumentException;
            try
            {
                _factory.CreateWithBaseAddress(url);
            }
            catch (ArgumentException ex)
            {
                exception = ex;
            }
            finally
            {
                Assert.NotNull(exception);
                Assert.Equal("baseAddress", exception.ParamName);
            }
        }

        [Fact]
        public void ShouldSetBaseAddress()
        {
            var client = _factory.CreateWithBaseAddress("http://some.url/path");
            Assert.NotNull(client.BaseAddress);
        }

        [Fact]
        public void ShouldEnsureTrailingSlash()
        {
            var client = _factory.CreateWithBaseAddress("http://some.url/path");
            var url = client.BaseAddress?.ToString();
            Assert.Equal("http://some.url/path/", url);
        }
    }
}
