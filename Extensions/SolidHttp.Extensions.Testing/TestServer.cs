using Microsoft.Extensions.Configuration;
using SolidHttp.Extensions.Testing.Abstractions;
using System;
namespace SolidHttp.Extensions.Testing
{
    public abstract class TestServer<TStartup, TAsserter> : TestServer
        where TStartup : class
        where TAsserter : class, IAsserter
    {
        private SolidHttpClientFactoryBuilder<TestingHttpClientFactory<TStartup>> _builder;
        private ISolidHttpClientFactory _factory;

        public TestServer()
            : base()
        {
            _builder = new SolidHttpClientFactoryBuilder<TestingHttpClientFactory<TStartup>>();
            _builder.Setup(s =>
            {
                s.Configure(o => o.Events.OnClientCreated += (sender, args) =>
                {
                    // this could be a problem with more complex asserters. We'll cross that bridge when we come to it.
                    args.Client.AddProperty(Constants.AsserterKey, Activator.CreateInstance<TAsserter>());
                });
            });
        }
        public override TestServer AddConfiguration(IConfiguration configuration)
        {
            if (configuration != null)
            {
                _factory = null;
                _builder.AddConfiguration(configuration);
            }
            return this;
        }

        public override TestServer Setup(Action<ISolidHttpSetup> setup)
        {
            if (setup != null)
            {
                _factory = null;
                _builder.Setup(setup);
            }
            return this;
        }

        public override SolidHttpClient CreateClient()
        {
            if (_factory == null)
                _factory = _builder.Build();
            return _factory.Create();
        }

        public override void Dispose()
        {
            if (_factory != null)
                _factory.Dispose();
            _builder.Dispose();
        }
    }

    public abstract class TestServer : IDisposable
    {
        public abstract SolidHttpClient CreateClient();
        public abstract TestServer Setup(Action<ISolidHttpSetup> setup);
        public abstract TestServer AddConfiguration(IConfiguration configuration);
        public abstract void Dispose();
    }
}
