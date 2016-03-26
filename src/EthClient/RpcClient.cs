using Eth.Json;
using Eth.Rpc;
using Eth.Utilities;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace Eth
{
    public class RpcClient : BaseClient, IDisposable
    {
        protected readonly Uri _nodeAddress;
        protected readonly HttpClient _httpClient;
        protected readonly IJsonSerializer _jsonSerializer;

        /// <summary>
        /// Creates an RpcClient pointing at a node running on the localhost and default RPC port: 8545.
        /// </summary>
        public RpcClient() : this(DefaultUri, DefaultHttpClient, DefaultJsonSerializer) { }

        /// <summary>
        /// Creates an RpcClient pointing at node running at the specified address and port
        /// </summary>
        /// <param name="nodeAddress">Address and port number of the node. eg. "127.0.0.1:8545"</param>
        public RpcClient(string nodeAddress) : this(nodeAddress, DefaultHttpClient, DefaultJsonSerializer) { }

        /// <summary>
        /// Creates an RpcClient pointing at node running at the specified address and port,
        /// with specified httpClient and serializer.
        /// </summary>
        /// <param name="nodeAddress">Address and port number of the node. eg. "127.0.0.1:8545"</param>
        /// <param name="httpClient">The HttpClient to make the calls with</param>
        /// <param name="jsonSerializer">Serializer used to serialize/deserialize the json returned by httpClient</param>
        public RpcClient(string nodeAddress, HttpClient httpClient, IJsonSerializer jsonSerializer)
        {
            Ensure.EnsureStringIsNotNullOrEmpty(nodeAddress, "nodeAddress");
            Ensure.EnsureParameterIsNotNull(httpClient, "httpClient");
            Ensure.EnsureParameterIsNotNull(jsonSerializer, "jsonSerialzer");

            _nodeAddress = new UriBuilder(nodeAddress).Uri;
            _httpClient = httpClient;
            _jsonSerializer = jsonSerializer;
        }

        private static HttpClient DefaultHttpClient
        {
            get { return new HttpClient(); }
        }

        private static IJsonSerializer DefaultJsonSerializer
        {
            get { return new JsonSerializer(); }
        }

        private static string DefaultUri
        {
            get { return "http://127.0.0.1:8545"; }
        }

        /// <summary>
        /// Post raw RPC request
        /// </summary>
        /// <typeparam name="T">The json type of the expected response (eg. string, bool)</typeparam>
        /// <param name="request">The RpcRequest</param>
        /// <exception cref="Eth.EthException">Thrown if the RPC call returns an error</exception>
        /// <exception cref="System.ObjectDisposedException">Thrown if this RpcClient has been disposed</exception>
        /// <returns>The raw RPC response</returns>
        public override async Task<RpcResponse<T>> PostRpcRequestAsync<T>(RpcRequest request)
        {
            string jsonRequest = _jsonSerializer.Serialize(request);
            Debug.WriteLine(String.Format("Serialized Request: {0}", jsonRequest));

            HttpContent jsonContent = new StringContent(jsonRequest);
            HttpResponseMessage httpResponse = await _httpClient.PostAsync(_nodeAddress, jsonContent);

            httpResponse.EnsureSuccessStatusCode();

            string jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            Debug.WriteLine(String.Format("Serialized Response: {0}", jsonResponse));

            RpcError rpcError = _jsonSerializer.Deserialize<RpcError>(jsonResponse);

            if (rpcError.Error != null)
            {
                throw new EthException(rpcError.Error.Code, rpcError.Error.Message);
            }

            return _jsonSerializer.Deserialize<RpcResponse<T>>(jsonResponse);
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                if (_httpClient != null) _httpClient.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
