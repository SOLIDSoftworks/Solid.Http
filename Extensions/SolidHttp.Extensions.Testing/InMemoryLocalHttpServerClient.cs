using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SolidHttp
{
    internal class InMemoryLocalHttpServerClient<TStartup> : HttpClient, IDisposable
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
        }

        public override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Making sure that you can't change the base address
            this.BaseAddress = _baseAddress;
            if (request.RequestUri.IsAbsoluteUri)
                throw new ArgumentException("SolidHttp.Extensions.Testing is designed to be used with relative urls only.");
            
            return base.SendAsync(request, cancellationToken);
        }

        private IDisposable InitializeServer()
        {
            _baseAddress = CreateUrl();
            BaseAddress = _baseAddress;
            return StartServer(_baseAddress);
        }

        private Uri CreateUrl()
        {
            // port 0 should be assigned by the OS
            return new Uri($"http://localhost:0");
        }

        private IDisposable StartServer(Uri url)
        {
            // there are seperate ways to do this for .Net Framework and for .Net Core
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing && _server != null)
                _server.Dispose();
        }
    }
}
