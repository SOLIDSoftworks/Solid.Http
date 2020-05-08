using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Solid.Http.Core.Tests.Stubs;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Solid.Http.Core.Tests
{
    public class HandlerTests : IClassFixture<HandlerTestsFixture>
    {
        private HandlerTestsFixture _fixture;

        public HandlerTests(HandlerTestsFixture fixture)
        {
            fixture.Reset();
            _fixture = fixture;
        }

        [Fact]
        public async Task ShouldInvokeMultipleMixedRequestHandlers()
        {
            var asserted1 = false;
            var assertion1 = new Action<HttpRequestMessage>(_ => asserted1 = true);
            var asserted2 = false;
            var assertion2 = new Action<IServiceProvider, HttpRequestMessage>((_, __) => asserted2 = true);
            var asserted3 = false;
            var assertion3 = new Func<HttpRequestMessage, ValueTask>(_ =>
            {
                asserted3 = true;
                return new ValueTask();
            });
            var asserted4 = false;
            var assertion4 = new Func<IServiceProvider, HttpRequestMessage, ValueTask>((_, __) =>
            {
                asserted4 = true;
                return new ValueTask();
            });

            var client = _fixture.Factory.Create();
            await client
                .GetAsync($"http://{nameof(ShouldInvokeMultipleMixedRequestHandlers)}")
                .OnHttpRequest(assertion1)
                .OnHttpRequest(assertion2)
                .OnHttpRequest(assertion3)
                .OnHttpRequest(assertion4)
            ;
            Assert.True(asserted1);
            Assert.True(asserted2);
            Assert.True(asserted3);
            Assert.True(asserted4);
        }

        [Fact]
        public async Task ShouldInvokeMultipleMixedResponseHandlers()
        {
            var asserted1 = false;
            var assertion1 = new Action<HttpResponseMessage>(_ => asserted1 = true);
            var asserted2 = false;
            var assertion2 = new Action<IServiceProvider, HttpResponseMessage>((_, __) => asserted2 = true);
            var asserted3 = false;
            var assertion3 = new Func<HttpResponseMessage, ValueTask>(_ =>
            {
                asserted3 = true;
                return new ValueTask();
            });
            var asserted4 = false;
            var assertion4 = new Func<IServiceProvider, HttpResponseMessage, ValueTask>((_, __) =>
            {
                asserted4 = true;
                return new ValueTask();
            });

            var client = _fixture.Factory.Create();
            await client
                .GetAsync($"http://{nameof(ShouldInvokeMultipleMixedResponseHandlers)}")
                .OnHttpResponse(assertion1)
                .OnHttpResponse(assertion2)
                .OnHttpResponse(assertion3)
                .OnHttpResponse(assertion4)
            ;
            Assert.True(asserted1);
            Assert.True(asserted2);
            Assert.True(asserted3);
            Assert.True(asserted4);
        }

        [Fact]
        public async Task ShouldFailOnSingleRequestHandlerFailure()
        {
            var client = _fixture.Factory.Create();
            var exception = null as Exception;
            try
            {
                await client
                    .GetAsync($"http://{nameof(ShouldFailOnSingleRequestHandlerFailure)}")
                    .OnHttpRequest(response => throw new Exception("Exception"))
                    .OnHttpRequest(_ => { })
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
        public async Task ShouldFailOnSingleResponseHandlerFailure()
        {
            var client = _fixture.Factory.Create();
            var exception = null as Exception;
            try
            {
                await client
                    .GetAsync($"http://{nameof(ShouldFailOnSingleResponseHandlerFailure)}")
                    .OnHttpResponse(response => throw new Exception("Exception"))
                    .OnHttpResponse(_ => { })
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
        public Task ShouldInvokeRequestHandlerWithSingleParameterAction()
            => ShouldInvokeHandlerWithSingleParameterAction<HttpRequestMessage>(nameof(ShouldInvokeRequestHandlerWithSingleParameterAction), (request, handler) => request.OnHttpRequest(handler));

        [Fact]
        public Task ShouldInvokeResponseHandlerWithSingleParameterAction()
            => ShouldInvokeHandlerWithSingleParameterAction<HttpResponseMessage>(nameof(ShouldInvokeResponseHandlerWithSingleParameterAction), (request, handler) => request.OnHttpResponse(handler));

        [Fact]
        public Task ShouldInvokeRequestHandlerWithDoubleParameterAction()
            => ShouldInvokeHandlerWithDoubleParameterAction<HttpRequestMessage>(nameof(ShouldInvokeRequestHandlerWithDoubleParameterAction), (request, handler) => request.OnHttpRequest(handler));

        [Fact]
        public Task ShouldInvokeResponseHandlerWithDoubleParameterAction()
            => ShouldInvokeHandlerWithDoubleParameterAsyncFunc<HttpResponseMessage>(nameof(ShouldInvokeResponseHandlerWithDoubleParameterAction), (request, handler) => request.OnHttpResponse(handler));

        [Fact]
        public Task ShouldInvokeRequestHandlerWithSingleParameterAsyncFunc()
            => ShouldInvokeHandlerWithSingleParameterAsyncFunc<HttpRequestMessage>(nameof(ShouldInvokeRequestHandlerWithSingleParameterAsyncFunc), (request, handler) => request.OnHttpRequest(handler));

        [Fact]
        public Task ShouldInvokeResponseHandlerWithSingleParameterAsyncFunc()
            => ShouldInvokeHandlerWithSingleParameterAsyncFunc<HttpResponseMessage>(nameof(ShouldInvokeResponseHandlerWithSingleParameterAsyncFunc), (request, handler) => request.OnHttpResponse(handler));

        [Fact]
        public Task ShouldInvokeRequestHandlerWithDoubleParameterAsyncFunc()
            => ShouldInvokeHandlerWithDoubleParameterAsyncFunc<HttpRequestMessage>(nameof(ShouldInvokeRequestHandlerWithDoubleParameterAsyncFunc), (request, handler) => request.OnHttpRequest(handler));

        [Fact]
        public Task ShouldInvokeResponseHandlerWithDoubleParameterAsyncFunc()
            => ShouldInvokeHandlerWithDoubleParameterAsyncFunc<HttpResponseMessage>(nameof(ShouldInvokeResponseHandlerWithDoubleParameterAsyncFunc), (request, handler) => request.OnHttpResponse(handler));

        private async Task ShouldInvokeHandlerWithSingleParameterAction<T>(string name, Action<ISolidHttpRequest, Action<T>> addHandler)
        {
            var asserted = false;
            var assertion = new Action<T>(_ => asserted = true);
            var client = _fixture.Factory.Create();
            var request = client
                .GetAsync($"http://{name}")
            ;
            addHandler(request, assertion);
            await request;
            Assert.True(asserted);
        }

        private async Task ShouldInvokeHandlerWithDoubleParameterAction<T>(string name, Action<ISolidHttpRequest, Action<IServiceProvider, T>> addHandler)
        {
            var asserted = false;
            var assertion = new Action<IServiceProvider, T>((_, __) => asserted = true);
            var client = _fixture.Factory.Create();
            var request = client
                .GetAsync($"http://{name}")
            ;
            addHandler(request, assertion);
            await request;
            Assert.True(asserted);
        }

        private async Task ShouldInvokeHandlerWithSingleParameterAsyncFunc<T>(string name, Action<ISolidHttpRequest, Func<T, ValueTask>> addHandler)
        {
            var asserted = false;
            var assertion = new Func<T, ValueTask>(_ =>
            {
                asserted = true;
                return new ValueTask();
            });
            var client = _fixture.Factory.Create();
            var request = client
                .GetAsync($"http://{name}")
            ;
            addHandler(request, assertion);
            await request;
            Assert.True(asserted);
        }

        private async Task ShouldInvokeHandlerWithDoubleParameterAsyncFunc<T>(string name, Action<ISolidHttpRequest, Func<IServiceProvider, T, ValueTask>> addHandler)
        {
            var asserted = false;
            var assertion = new Func<IServiceProvider, T, ValueTask>((_, __) =>
            {
                asserted = true;
                return new ValueTask();
            });
            var client = _fixture.Factory.Create();
            var request = client
                .GetAsync($"http://{name}")
            ;
            addHandler(request, assertion);
            await request;
            Assert.True(asserted);
        }

        //[Theory]
        //[InlineData(200)]
        //[InlineData(400)]
        //[InlineData(404)]
        //[InlineData(500)]
        //public async Task ShouldInvokeIntegerStatusCodeResponseHandlerWithSingleParameterAction(int statusCode)
        //{
        //    _fixture.ConfigureStaticHttpMessageHandler = options =>
        //    {
        //        options.StatusCode = (HttpStatusCode)statusCode;
        //    };

        //    var asserted = false;
        //    var assertion = new Action<HttpResponseMessage>(_ => asserted = true);
        //    var client = _fixture.Factory.Create();
        //    var response = await client
        //        .GetAsync($"http://{nameof(ShouldInvokeIntegerStatusCodeResponseHandlerWithSingleParameterAction)}_{statusCode}")
        //        .On(statusCode, assertion)
        //    ;
        //    Assert.Equal(statusCode, (int)response.StatusCode);
        //    Assert.True(asserted);
        //}

        class ConfigureNamedStaticHttpMessageHandlerOptions : IConfigureNamedOptions<StaticHttpMessageHandlerOptions>
        {
            public void Configure(string name, StaticHttpMessageHandlerOptions options)
            {
                throw new NotImplementedException();
            }

            public void Configure(StaticHttpMessageHandlerOptions options)
            {
                throw new NotImplementedException();
            }
        }
    }
}
