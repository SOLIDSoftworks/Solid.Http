# FluentHttp

FluentHttp is a library to simplify http calls in C#. It's designed to be async and fully extendable.

## Basic use

    public class SomeClass
    {
        private IFluentHttpClientFactory _factory;
        public SomeClass(IFluentHttpClientFactory factory)
        {
            // The FluentHttpClientFactory class would be injected here
            _factory = factory;
        }

        public Task<HttpResponseMessage> GetSomethingAsync(CancellationToken cancellationToken)
        {
            var consumer = _factory.Create();
            return await consumer
                .GetAsync("http://someurl.com", cancellationToken);
        }
    }