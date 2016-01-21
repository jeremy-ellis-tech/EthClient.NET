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

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_toReturn);
        }
    }
}
