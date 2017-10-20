using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SolidHttp.Extensions.Testing
{
    internal class InMemoryLocalHttpServerClient<TStartup> : HttpClient, IDisposable
        where TStartup : class
    {
        private IDisposable _server;
        private Uri _baseAddress;

        public InMemoryLocalHttpServerClient()
            : this(new HttpClientHandler())
        {
        }

        public InMemoryLocalHttpServerClient(HttpMessageHandler handler) 
            : this(handler, true)
        {
        }

        public InMemoryLocalHttpServerClient(HttpMessageHandler handler, bool disposeHandler) 
            : base(handler, disposeHandler)
        {
            _server = StartServer();
        }

        public override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Making sure that you can't change the base address
            this.BaseAddress = _baseAddress;
            if (request.RequestUri.IsAbsoluteUri)
                throw new ArgumentException("SolidHttp.Extensions.Testing is designed to be used with relative urls only.");
            
            return base.SendAsync(request, cancellationToken);
        }

        private IDisposable StartServer()
        {
            // there are seperate ways to do this for .Net Framework and for .Net Core
            var host = new InMemoryHost<TStartup>();
            _baseAddress = BaseAddress = host.BaseAddress;
            return host;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing && _server != null)
                _server.Dispose();
        }
    }
}
