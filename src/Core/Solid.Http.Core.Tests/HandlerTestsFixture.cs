using Microsoft.Extensions.DependencyInjection;
using Solid.Http.Core.Tests.Stubs;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;

namespace Solid.Http.Core.Tests
{
    public class HandlerTestsFixture
    {
        private ServiceProvider _services;
        private ManualResetEvent _manualReset;

        public HandlerTestsFixture()
        {
            var services = new ServiceCollection();
            services.AddStaticHttpMessageHandler(options =>
            {
                ConfigureStaticHttpMessageHandler(options);
            });
            services.AddManualChangeToken(out _manualReset);
            services.AddSolidHttpCore();
            _services = services.BuildServiceProvider();
        }

        private static readonly Action<StaticHttpMessageHandlerOptions> _noop = _ => { };

        private Action<StaticHttpMessageHandlerOptions> _configureStaticHttpMessageHandler;
        public Action<StaticHttpMessageHandlerOptions> ConfigureStaticHttpMessageHandler
        {
            get => _configureStaticHttpMessageHandler;
            set
            {
                _configureStaticHttpMessageHandler = value;
                _manualReset.Set();
            }
        }
        public ISolidHttpClientFactory Factory
        {
            get
            {
                return _services.GetService<ISolidHttpClientFactory>();
            }
        }

        public void Reset()
            => ConfigureStaticHttpMessageHandler = _noop;
    }
}
