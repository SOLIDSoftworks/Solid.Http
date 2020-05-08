using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Solid.Http.Core.Tests.Stubs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class Solid_Http_Core_Tests_ServiceCollectionExtensions
    {
        public static IServiceCollection AddStaticHttpMessageHandler(this IServiceCollection services, Action<StaticHttpMessageHandlerOptions> configureOptions)
        {            
            services.Configure<StaticHttpMessageHandlerOptions>(options => configureOptions(options));
            services.RemoveAll<IHttpMessageHandlerFactory>();
            services.RemoveAll<IHttpClientFactory>();
            services.AddTransient<IHttpMessageHandlerFactory, StaticHttpMessageHandlerFactory>();
            services.AddTransient<IHttpClientFactory, StaticHttpClientFactory>();
            return services;
        }

        public static IServiceCollection AddManualChangeToken(this IServiceCollection services, out ManualResetEvent manualReset)
        {
            manualReset = new ManualResetEvent(false);
            services.AddSingleton(manualReset);
            services.AddSingleton(typeof(IOptionsChangeTokenSource<>), typeof(ManualChangeTokenSource<>));
            return services;
        }

        class StaticHttpMessageHandlerFactory : IHttpMessageHandlerFactory
        {
            private IOptionsMonitor<StaticHttpMessageHandlerOptions> _options;

            public StaticHttpMessageHandlerFactory(IOptionsMonitor<StaticHttpMessageHandlerOptions> options)
            {
                _options = options;
            }
            public HttpMessageHandler CreateHandler(string _)
                => new StaticHttpMessageHandler(_options.CurrentValue);
        }

        class StaticHttpClientFactory : IHttpClientFactory
        {
            private IHttpMessageHandlerFactory _handlerFactory;

            public StaticHttpClientFactory(IHttpMessageHandlerFactory handlerFactory)
            {
                _handlerFactory = handlerFactory;
            }
            public HttpClient CreateClient(string name)
            {
                var handler = _handlerFactory.CreateHandler(name);
                return new HttpClient(handler);
            }
        }

        class ManualChangeTokenSource<T> : IOptionsChangeTokenSource<T>
        {
            private ManualResetEvent _resetEvent;

            public ManualChangeTokenSource(ManualResetEvent resetEvent)
            {
                _resetEvent = resetEvent;
            }
            public string Name => null;

            public IChangeToken GetChangeToken()
                => new ManualChangeToken(_resetEvent);
        }

        class ManualChangeToken : IChangeToken, IDisposable
        {
            private CancellationTokenSource _source;
            private ManualResetEvent _resetEvent;
            private ConcurrentDictionary<Guid, ManualChangeCallback> _callbacks = new ConcurrentDictionary<Guid, ManualChangeCallback>();

            public ManualChangeToken(ManualResetEvent resetEvent)
            {
                _resetEvent = resetEvent;
                _source = new CancellationTokenSource();
                Task.Factory.StartNew(LongRunningTask, _source.Token, TaskCreationOptions.LongRunning, TaskScheduler.Current);
            }

            public bool HasChanged { get; set; }

            public bool ActiveChangeCallbacks => true;
            
            public IDisposable RegisterChangeCallback(Action<object> callback, object state)
            {
                var id = Guid.NewGuid();
                return _callbacks.GetOrAdd(id, key => new ManualChangeCallback(key, callback, state, guid => _callbacks.Remove(guid, out _)));
                
            }
            public void Dispose()
            {
                _source.Cancel();
                _resetEvent.Set();
            }

            private void LongRunningTask()
            {
                while (!_source.IsCancellationRequested)
                {
                    _resetEvent.WaitOne();
                    if (_source.IsCancellationRequested) return;
                    HasChanged = true;
                    foreach (var callback in _callbacks.Values)
                        callback.Invoke();
                    HasChanged = false;
                    _resetEvent.Reset();
                }
            }
        }

        class ManualChangeCallback : IDisposable
        {
            private Guid _id;
            private Action<object> _callback;
            private object _state;
            private Action<Guid> _unregister;

            public ManualChangeCallback(Guid id, Action<object> callback, object state, Action<Guid> unregister)
            {
                _id = id;
                _callback = callback;
                _state = state;
                _unregister = unregister;
            }

            public void Invoke()
            {
                _callback(_state);
            }

            public void Dispose()
            {
                _unregister(_id);
            }
        }

        class StaticHttpMessageHandlerOptionsChangeTokenSource : IOptionsChangeTokenSource<StaticHttpMessageHandlerOptions>
        {
            public string Name => null;

            public IChangeToken GetChangeToken()
                => new StaticHttpMessageHandlerOptionsChangeToken();

            class StaticHttpMessageHandlerOptionsChangeToken : IChangeToken
            {
                public bool HasChanged => true;

                public bool ActiveChangeCallbacks => false;

                public IDisposable RegisterChangeCallback(Action<object> callback, object state)
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}
