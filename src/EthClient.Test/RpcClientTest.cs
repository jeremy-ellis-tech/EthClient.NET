using Eth;
using Eth.Json;
using Eth.Rpc;
using EthClient.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Net.Http;

namespace EthClient.Test
{
    [TestClass]
    public class RpcClientTest
    {
        [TestMethod]
        public void ShouldParseCorrectErrorCodeAndMessageInException()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent("{\"id\":0,\"jsonrpc\":\"2.0\",\"error\":{\"code\":-32000,\"message\":\"mining work not ready\"}}");
            var mockHttpClient = new HttpClient(new MockHttpMessageHandler(response));

            var rpcClient = new RpcClient("127.0.0.1:8545", mockHttpClient, new JsonSerializer());

            EthException thrownException = null;

            try
            {
                var rpcResponse = rpcClient.EthGetWorkAsync().Result;
            }
            catch (AggregateException ex)
            {
                thrownException = ex.InnerException as EthException;
            }

            Assert.IsTrue(thrownException != null);

            int expectedErrorCode = -32000;
            string expectedErrorMessage = "mining work not ready";
            Assert.IsTrue(Equals(expectedErrorCode, thrownException.ErrorCode));
            Assert.IsTrue(Equals(expectedErrorMessage, thrownException.Message));
        }

        [TestMethod]
        [ExpectedException(typeof(HttpRequestException))]
        public void ShouldThrowIfResponseIsNotSuccessStatus()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
            var mockHttpMessageHandler = new MockHttpMessageHandler(response);
            var mockHttpClient = new HttpClient(mockHttpMessageHandler);

            var rpcClient = new RpcClient("127.0.0.1:8545", mockHttpClient, new JsonSerializer());

            try
            {
                var rpcResponse = rpcClient.PostRpcRequestAsync<string>(new RpcRequest
                {
                    ID = 0,
                    JsonRpc = "2.0",
                    MethodName = "eth_getWork",
                    Parameters = null
                }).Result;
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void ShouldThrowIfDisposed()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
            var mockHttpMessageHandler = new MockHttpMessageHandler(response);
            var mockHttpClient = new HttpClient(mockHttpMessageHandler);

            var rpcClient = new RpcClient("127.0.0.1:8545", mockHttpClient, new JsonSerializer());

            rpcClient.Dispose();

            try
            {
                var rpcResponse = rpcClient.PostRpcRequestAsync<string>(new RpcRequest
                {
                    ID = 0,
                    JsonRpc = "2.0",
                    MethodName = "eth_getWork",
                    Parameters = null
                }).Result;
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(UriFormatException))]
        public void ShouldNotBeAbleToCreateRpcClientWithInvalidUri()
        {
            BaseClient client = new RpcClient("127.0.0.1:-1");
        }
    }
}
