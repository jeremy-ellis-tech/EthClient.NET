using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace EthClient.Test.Mocks
{
    class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage _toReturn;

        public MockHttpMessageHandler(HttpResponseMessage toReturn)
        {
            _toReturn = toReturn;
        }

        public HttpRequestMessage LastRequest { get; private set; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            LastRequest = request;
            return Task.FromResult(_toReturn);
        }
    }
}
