using Microsoft.Extensions.Configuration;
using System;
namespace SolidHttp.Extensions.Testing
{
    public abstract class TestServer<TStartup, TAsserter> : TestServer<TStartup>
        where TStartup : class
        where TAsserter : class, IAsserter
    {
        private IServiceProvider _provider;
        public TestServer()
            : base()
        {
            Builder.Setup(s =>
            {
                s.Configure(o => o.Events.OnClientCreated += (sender, args) =>
                {
                    // this could be a problem with more complex asserters. We'll cross that bridge when it comes to it.
                    args.Client.AddProperty(Constants.AsserterKey, Activator.CreateInstance<TAsserter>());
                });
            });
        }
    }

    public class TestServer<TStartup> : IDisposable
        where TStartup : class
    {
        protected SolidHttpClientFactoryBuilder<TestingHttpClientFactory<TStartup>> Builder;
        private ISolidHttpClientFactory _factory;

        public TestServer()
        {
            Builder = new SolidHttpClientFactoryBuilder<TestingHttpClientFactory<TStartup>>();
        }

        public TestServer<TStartup> AddConfiguration(IConfiguration configuration)
        {
            if (configuration != null)
            {
                _factory = null;
                Builder.AddConfiguration(configuration);
            }
            return this;
        }

        public TestServer<TStartup> Setup(Action<ISolidHttpSetup> setup)
        {
            if (setup != null)
            {
                _factory = null;
                Builder.Setup(setup);
            }
            return this;
        }

        public SolidHttpClient CreateClient()
        {
            if (_factory == null)
                _factory = Builder.Build();
            return _factory.Create();
        }

        public void Dispose()
        {
            if (_factory != null)
                _factory.Dispose();
            Builder.Dispose();
        }
    }
}
