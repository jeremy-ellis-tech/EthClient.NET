using Eth.Json;
using Eth.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Numerics;
using System.Threading.Tasks;

namespace Eth
{
    public class RpcClient : IDisposable
    {
        private static HttpClient DefaultHttpClient = new HttpClient();
        private static string DefaultUri = "http://127.0.0.1:8545";
        private static IJsonSerializer DefaultJsonSerializer = new Json.JsonSerializer();

        private static string DefaultJsonRpc = "2.0";
        private static int DefaultRequestId = 0;

        protected readonly Uri _nodeAddress;
        protected readonly HttpClient _httpClient;
        protected readonly IJsonSerializer _jsonSerializer;

        /// <summary>
        /// The super simple Ethereum JSON RPC client
        /// Connects to the default node address: 127.0.0.1:8545
        /// </summary>
        public RpcClient() : this(DefaultUri, DefaultHttpClient, DefaultJsonSerializer) { }

        /// <summary>
        /// The super simple Ethereum JSON RPC client
        /// </summary>
        /// <param name="nodeAddress">Address and port number of the node. eg. "127.0.0.1:8545"</param>
        public RpcClient(string nodeAddress) : this(nodeAddress, DefaultHttpClient, DefaultJsonSerializer) { }

        /// <summary>
        /// The super simple Ethereum JSON RPC client
        /// </summary>
        /// <param name="nodeAddress">Address and port number of the node. eg. "127.0.0.1:8545"</param>
        /// <param name="httpClient">The HttpClient to make the connections with</param>
        /// <param name="jsonSerializer">Serializer used to serialize/deserialize the json returned</param>
        public RpcClient(string nodeAddress, HttpClient httpClient, IJsonSerializer jsonSerializer)
        {
            EnsureStringNotNullOrEmpty(nodeAddress, "nodeAddress");
            EnsureObjectIsNotNull(httpClient, "httpClient");
            EnsureObjectIsNotNull(jsonSerializer, "jsonSerialzer");

            _nodeAddress = new UriBuilder(nodeAddress).Uri;
            _httpClient = httpClient;
            _jsonSerializer = jsonSerializer;
        }

        /// <summary>
        /// Returns the current client version.
        /// </summary>
        /// <returns>The current client version</returns>
        public async Task<string> Web3ClientVersionAsync()
        {
            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "web3_clientVersion",
                Parameters = RpcRequest.EmptyParameters
            };

            RpcResponse<string> response = await PostRequestAsync<string>(request);

            return response.Result;
        }

        /// <summary>
        /// Returns Keccak-256 (not the standardized SHA3-256) hash of the given data.
        /// </summary>
        /// <param name="data">The data to hash</param>
        /// <returns>The hash of the given data</returns>
        public async Task<byte[]> Web3Sha3Async(byte[] data)
        {
            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "web3_sha3",
                Parameters = new[] { data }.Select(x => Hex.ByteArrayToHexString(x))
            };

            RpcResponse<string> response = await PostRequestAsync<string>(request);

            return Hex.HexStringToByteArray(response.Result);
        }

        /// <summary>
        /// Returns the current network protocol version
        /// </summary>
        /// <returns>The current network protocol version</returns>
        public async Task<string> NetVersionAsync()
        {
            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "net_version",
                Parameters = RpcRequest.EmptyParameters
            };

            RpcResponse<string> response = await PostRequestAsync<string>(request);

            return response.Result;
        }

        /// <summary>
        /// Returns true if client is actively listening for network connections
        /// </summary>
        /// <returns>true when listening, otherwise false</returns>
        public async Task<bool> NetListeningAsync()
        {
            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "net_listening",
                Parameters = RpcRequest.EmptyParameters
            };

            RpcResponse<bool> response = await PostRequestAsync<bool>(request);

            return response.Result;
        }

        /// <summary>
        /// Returns number of peers currenly connected to the client.
        /// </summary>
        /// <returns>The number of connected peers.</returns>
        public async Task<BigInteger> NetPeerCountAsync()
        {
            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "net_peerCount",
                Parameters = RpcRequest.EmptyParameters
            };

            RpcResponse<string> response = await PostRequestAsync<string>(request);

            return Hex.HexStringToInt(response.Result);
        }

        /// <summary>
        /// Returns the current ethereum protocol version.
        /// </summary>
        /// <returns>The current ethereum protocol version</returns>
        public async Task<string> EthProtocolVersionAsync()
        {
            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_protocolVersion",
                Parameters = RpcRequest.EmptyParameters
            };

            RpcResponse<string> response = await PostRequestAsync<string>(request);

            return response.Result;
        }

        /// <summary>
        /// Returns the client coinbase address.
        /// </summary>
        /// <returns>20 bytes - the current coinbase address.</returns>
        public async Task<byte[]> EthCoinbaseAsync()
        {
            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_coinbase",
                Parameters = RpcRequest.EmptyParameters
            };

            RpcResponse<string> response = await PostRequestAsync<string>(request);

            return Hex.HexStringToByteArray(response.Result);
        }

        /// <summary>
        /// Creates new message call transaction or a contract creation, if the data field contains code.
        /// </summary>
        /// <param name="transaction">The transaction to send</param>
        /// <returns>The transaction hash, or the zero hash if the transaction is not yet available</returns>
        public async Task<byte[]> EthSendTransactionAsync(EthTransaction transaction)
        {
            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_sendTransaction",
                Parameters = new[] { transaction }
                                    .Select(x => new
                                    {
                                        from = Hex.ByteArrayToHexString(x.From),
                                        to = Hex.ByteArrayToHexString(x.To),
                                        gas = x.Gas != null ? Hex.IntToHexString(x.Gas.Value) : null,
                                        gasPrice = x.GasPrice != null ? Hex.IntToHexString(x.GasPrice.Value) : null,
                                        value = x.Value != null ? Hex.IntToHexString(x.Value.Value) : null,
                                        data = Hex.ByteArrayToHexString(x.Data),
                                        nonce = x.Nonce != null ? Hex.IntToHexString(x.Nonce.Value) : null
                                    })
            };

            RpcResponse<string> response = await PostRequestAsync<string>(request);

            return Hex.HexStringToByteArray(response.Result);
        }

        /// <summary>
        /// Returns the balance of the account of given address.
        /// </summary>
        /// <param name="address">20 Bytes - address to check for balance.</param>
        /// <param name="defaultBlock">integer block number, or the string "latest", "earliest" or "pending"</param>
        /// <returns>Integer of the current balance in wei.</returns>
        public async Task<BigInteger> EthGetBalanceAsync(byte[] address, DefaultBlock defaultBlock)
        {
            if(address.Length > EthSpecs.AddressLength)
            {
                throw new ArgumentOutOfRangeException("address");
            }

            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_getBalance",
                Parameters = new[]
                {
                    Hex.ByteArrayToHexString(address),
                    defaultBlock.BlockNumber.HasValue ? Hex.IntToHexString(defaultBlock.BlockNumber.Value) : defaultBlock.Option.ToString().ToLowerInvariant()
                }
            };

            RpcResponse<string> response = await PostRequestAsync<string>(request);

            return Hex.HexStringToInt(response.Result);
        }

        /// <summary>
        /// Returns the number of most recent block.
        /// </summary>
        /// <returns>integer of the current block number the client is on.</returns>
        public async Task<BigInteger> EthBlockNumberAsync()
        {
            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_blockNumber",
                Parameters = RpcRequest.EmptyParameters
            };

            RpcResponse<string> response = await PostRequestAsync<string>(request);

            return Hex.HexStringToInt(response.Result);
        }

        /// <summary>
        /// Returns the number of transactions sent from an address.
        /// </summary>
        /// <param name="address"> 20 Bytes - address.</param>
        /// <param name="defaultBlock">integer block number, or the string "latest", "earliest" or "pending"</param>
        /// <returns></returns>
        public async Task<BigInteger> EthGetTransactionCountAsync(byte[] address, DefaultBlock defaultBlock)
        {
            if (address.Length > EthSpecs.AddressLength)
            {
                throw new ArgumentOutOfRangeException("address");
            }

            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_getTransactionCount",
                Parameters = new[]
                {
                    Hex.ByteArrayToHexString(address),
                    defaultBlock.BlockNumber.HasValue ? Hex.IntToHexString(defaultBlock.BlockNumber.Value) : defaultBlock.Option.ToString().ToLowerInvariant()
                }
            };

            RpcResponse<string> response = await PostRequestAsync<string>(request);

            return Hex.HexStringToInt(response.Result);
        }

        /// <summary>
        /// Returns the number of transactions in a block from a block matching the given block hash.
        /// </summary>
        /// <param name="blockHash">32 Bytes - hash of a block</param>
        /// <returns>integer of the number of transactions in this block.</returns>
        public async Task<BigInteger> EthGetTransactionCountByHashAsync(byte[] blockHash)
        {
            if(blockHash.Length != EthSpecs.BlockHashLength)
            {
                throw new ArgumentOutOfRangeException("blockHash");
            }

            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_getBlockTransactionCountByHash",
                Parameters = new[] { Hex.ByteArrayToHexString(blockHash) }
            };

            RpcResponse<string> response = await PostRequestAsync<string>(request);

            return Hex.HexStringToInt(response.Result);
        }

        /// <summary>
        /// Returns the number of transactions in a block from a block matching the given block number.
        /// </summary>
        /// <param name="defaultBlock">integer of a block number, or the string "earliest", "latest" or "pending"</param>
        /// <returns>integer of the number of transactions in this block.</returns>
        public async Task<BigInteger> EthGetBlockTransactionCountByNumberAsync(DefaultBlock defaultBlock)
        {
            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_getBlockTransactionCountByNumber",
                Parameters = new[] { defaultBlock.BlockNumber.HasValue ? Hex.IntToHexString(defaultBlock.BlockNumber.Value) : defaultBlock.Option.Value.ToString().ToLowerInvariant() }
            };

            RpcResponse<string> response = await PostRequestAsync<string>(request);

            return Hex.HexStringToInt(response.Result);
        }

        /// <summary>
        /// Returns the number of uncles in a block from a block matching the given block hash.
        /// </summary>
        /// <param name="blockHash">32 Bytes - hash of a block</param>
        /// <returns>integer of the number of uncles in this block.</returns>
        public async Task<BigInteger> EthGetUncleCountByBlockHashAsync(byte[] blockHash)
        {
            Ensure.EnsureCountIsCorrect(blockHash, EthSpecs.BlockHashLength, "blockHash");

            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_getUncleCountByBlockHash",
                Parameters = new[] { Hex.ByteArrayToHexString(blockHash) }
            };

            RpcResponse<string> response = await PostRequestAsync<string>(request);

            return Hex.HexStringToInt(response.Result);
        }

        /// <summary>
        /// Returns the number of uncles in a block from a block matching the given block number.
        /// </summary>
        /// <param name="defaultBlock">integer of a block number, or the string "latest", "earliest" or "pending"</param>
        /// <returns>integer of the number of uncles in this block.</returns>
        public async Task<BigInteger> EthGetUncleCountByBlockNumberAsync(DefaultBlock defaultBlock)
        {
            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_getUncleCountByBlockNumber",
                Parameters = new[] { defaultBlock.BlockNumber.HasValue ? Hex.IntToHexString(defaultBlock.BlockNumber.Value) : defaultBlock.Option.Value.ToString().ToLowerInvariant() }
            };

            RpcResponse<string> response = await PostRequestAsync<string>(request);

            return Hex.HexStringToInt(response.Result);
        }

        /// <summary>
        /// Returns code at a given address.
        /// </summary>
        /// <param name="address">20 Bytes - address</param>
        /// <param name="defaultBlock">integer block number, or the string "latest", "earliest" or "pending"</param>
        /// <returns>The code from the given address.</returns>
        public async Task<byte[]> EthGetCodeAsync(byte[] address, DefaultBlock defaultBlock)
        {
            Ensure.EnsureCountIsCorrect(address, EthSpecs.AddressLength, "address");

            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_getCode",
                Parameters = new[]
                {
                    Hex.ByteArrayToHexString(address),
                    defaultBlock.BlockNumber.HasValue ? Hex.IntToHexString(defaultBlock.BlockNumber.Value) : defaultBlock.Option.Value.ToString().ToLowerInvariant()
                }
            };

            RpcResponse<string> response = await PostRequestAsync<string>(request);

            return Hex.HexStringToByteArray(response.Result);
        }

        /// <summary>
        /// Signs data with a given address.
        /// </summary>
        /// <param name="address">20 Bytes - address</param>
        /// <param name="data">Data to sign</param>
        /// <returns>Signed data</returns>
        public async Task<byte[]> EthSignAsync(byte[] address, byte[] data)
        {
            Ensure.EnsureCountIsCorrect(address, EthSpecs.AddressLength, "address");

            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_sign",
                Parameters = new[] { Hex.ByteArrayToHexString(address), Hex.ByteArrayToHexString(data) }
            };

            RpcResponse<string> response = await PostRequestAsync<string>(request);

            return Hex.HexStringToByteArray(response.Result);
        }

        /// <summary>
        /// Executes a new message call immediately without creating a transaction on the block chain.
        /// </summary>
        /// <param name="call">The transaction call object</param>
        /// <param name="defaultBlock">integer block number, or the string "latest", "earliest" or "pending"</param>
        /// <returns>the return value of executed contract</returns>
        public async Task<byte[]> EthCallAsync(EthCall call, DefaultBlock defaultBlock)
        {
            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_call",
                Parameters = new object[]
                {
                    new
                    {
                        from = call.From,
                        to = call.To,
                        gas = call.Gas,
                        gasPrice = call.GasPrice,
                        value = call.Value,
                        data = call.Data
                    },

                    defaultBlock.BlockNumber.HasValue ? Hex.IntToHexString(defaultBlock.BlockNumber.Value) : defaultBlock.Option.ToString().ToLowerInvariant()
                }
            };

            RpcResponse<string> response = await PostRequestAsync<string>(request);

            return Hex.HexStringToByteArray(response.Result);
        }

        /// <summary>
        /// Makes a call or transaction, which won't be added to the blockchain and returns the used gas, which can be used for estimating the used gas.
        /// </summary>
        /// <param name="call">The transaction call object</param>
        /// <param name="defaultBlock">integer block number, or the string "latest", "earliest" or "pending"</param>
        /// <returns>the amount of gas used.</returns>
        public async Task<BigInteger> EthEstimateGasAsync(EthCall call, DefaultBlock defaultBlock)
        {
            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_estimateGas",
                Parameters = new object[]
                {
                    new
                    {
                        from = call.From,
                        to = call.To,
                        gas = call.Gas,
                        gasPrice = call.GasPrice,
                        value = call.Value,
                        data = call.Data
                    },

                    defaultBlock.BlockNumber.HasValue ? Hex.IntToHexString(defaultBlock.BlockNumber.Value) : defaultBlock.Option.ToString().ToLowerInvariant()
                }
            };

            RpcResponse<string> response = await PostRequestAsync<string>(request);

            return Hex.HexStringToInt(response.Result);
        }

        /// <summary>
        /// Returns information about a block by hash.
        /// </summary>
        /// <param name="blockHash">32 Bytes - Hash of a block.</param>
        /// <param name="fullTransaction">If true it returns the full transaction objects, if false only the hashes of the transactions</param>
        /// <returns>A block object, or null when no block was found</returns>
        public async Task<EthBlock> EthGetBlockByHashAsync(byte[] blockHash, bool fullTransaction)
        {
            Ensure.EnsureCountIsCorrect(blockHash, EthSpecs.BlockHashLength, "blockHash");

            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_getBlockByHash",
                Parameters = new object[] { Hex.ByteArrayToHexString(blockHash), fullTransaction }
            };

            RpcResponse<EthBlock> response = await PostRequestAsync<EthBlock>(request);

            return response.Result;
        }

        /// <summary>
        /// Returns a list of available compilers in the client.
        /// </summary>
        /// <returns>Available compilers</returns>
        public async Task<IEnumerable<string>> EthGetCompilersAsync()
        {
            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_getCompilers",
                Parameters = RpcRequest.EmptyParameters
            };

            RpcResponse<IEnumerable<string>> response = await PostRequestAsync<IEnumerable<string>>(request);

            return response.Result;
        }

        /// <summary>
        /// Returns compiled solidity code.
        /// </summary>
        /// <param name="sourceCode">The source code.</param>
        /// <returns>The compiled source code</returns>
        public async Task<dynamic> EthCompileSolidityAsync(string sourceCode)
        {
            EnsureStringNotNullOrEmpty(sourceCode, "sourceCode");

            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_compileSolidity",
                Parameters = new[]{ sourceCode }
            };

            RpcResponse<dynamic> response = await PostRequestAsync<dynamic>(request);

            return response.Result;
        }

        /// <summary>
        /// Returns compiled LLL code
        /// </summary>
        /// <param name="sourceCode">The source code</param>
        /// <returns>The compiled source code</returns>
        public async Task<byte[]> EthCompileLllAsync(string sourceCode)
        {
            EnsureStringNotNullOrEmpty(sourceCode, "sourceCode");

            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_compileLLL",
                Parameters = new[] { sourceCode }
            };

            RpcResponse<string> response = await PostRequestAsync<string>(request);

            return Hex.HexStringToByteArray(response.Result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceCode"></param>
        /// <returns></returns>
        public async Task<byte[]> EthCompileSerpentAsync(string sourceCode)
        {
            EnsureStringNotNullOrEmpty(sourceCode, "sourceCode");

            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_compileSerpent",
                Parameters = new[] { sourceCode }
            };

            RpcResponse<string> response = await PostRequestAsync<string>(request);

            return Hex.HexStringToByteArray(response.Result);
        }

        /// <summary>
        /// Returns the hash of the current block, the seedHash, and the boundary condition to be met 
        /// </summary>
        /// <returns>EthWork with current block hash, seed hash, and boundary condition</returns>
        public async Task<EthWork> EthGetWorkAsync()
        {
            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_getWork",
                Parameters = RpcRequest.EmptyParameters
            };

            RpcResponse<IList<string>> rpcResponse = await PostRequestAsync<IList<string>>(request);

            IList<string> result = rpcResponse.Result;

            return new EthWork(Hex.HexStringToByteArray(result[0]), Hex.HexStringToByteArray(result[1]), Hex.HexStringToByteArray(result[2]));
        }

        /// <summary>
        /// Used for submitting a proof-of-work solution.
        /// </summary>
        /// <param name="proofOfWork">The proof-of-work solution</param>
        /// <returns>returns true if the provided solution is valid, otherwise false</returns>
        public async Task<bool> EthSubmitWorkAsync(ProofOfWork proofOfWork)
        {
            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_submitWork",
                Parameters = new[] { proofOfWork.Nonce, proofOfWork.HeaderPowHash, proofOfWork.MixDigest }
            };

            RpcResponse<bool> response = await PostRequestAsync<bool>(request);

            return response.Result;
        }

        /// <summary>
        /// Used for submitting mining hashrate.
        /// </summary>
        /// <param name="hashRate">the hash rate</param>
        /// <param name="clientID">A random ID identifying the client</param>
        /// <returns>returns true if submitting went through succesfully and false otherwise</returns>
        public async Task<bool> EthSubmitHashRateAsync(BigInteger hashRate, byte[] clientID)
        {
            Ensure.EnsureCountIsCorrect(clientID, EthSpecs.ClientIDLength, "clientID");

            if(hashRate.ToByteArray().Length > EthSpecs.HashRateMaxLength)
            {
                throw new ArgumentOutOfRangeException("hashRate");
            }

            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_submitHashrate",
                Parameters = new[] { Hex.IntToHexString(hashRate), Hex.ByteArrayToHexString(clientID) }
            };

            RpcResponse<bool> response = await PostRequestAsync<bool>(request);

            return response.Result;
        }

        /// <summary>
        /// Creates new whisper identity in the client.
        /// </summary>
        /// <returns>60 Bytes - the address of the new identiy.</returns>
        public async Task<byte[]> ShhNewIdentityAsync()
        {
            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "shh_newIdentity",
                Parameters = RpcRequest.EmptyParameters
            };

            RpcResponse<string> response = await PostRequestAsync<string>(request);

            return Hex.HexStringToByteArray(response.Result);
        }

        /// <summary>
        /// Checks if the client hold the private keys for a given identity.
        /// </summary>
        /// <param name="address">60 Bytes - The identity address to check.</param>
        /// <returns>returns true if the client holds the privatekey for that identity, otherwise false.</returns>
        public async Task<bool> ShhHasIdentityAsync(byte[] address)
        {
            Ensure.EnsureCountIsCorrect(address, EthSpecs.WhisperIdentityLength, "address");

            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "shh_hasIdentity",
                Parameters = new[] { Hex.ByteArrayToHexString(address) }
            };

            RpcResponse<bool> response = await PostRequestAsync<bool>(request);

            return response.Result;
        }

        private static void EnsureStringNotNullOrEmpty(string value, string paramName)
        {
            EnsureObjectIsNotNull(value, paramName);

            if (Equals(value, String.Empty))
            {
                throw new ArgumentOutOfRangeException("paramName");
            }
        }

        private static void EnsureObjectIsNotNull(object value, string paramName)
        {
            if(value == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        /// <summary>
        /// Post raw RPC request
        /// </summary>
        /// <typeparam name="T">The json type of the expected response (eg. string, bool)</typeparam>
        /// <param name="request">The RpcRequest</param>
        /// <returns>The raw RPC response</returns>
        public virtual async Task<RpcResponse<T>> PostRequestAsync<T>(RpcRequest request)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("RpcClient");
            }

            string jsonRequest = _jsonSerializer.Serialize(request);

            Debug.WriteLine(String.Format("Serialized Request: {0}", jsonRequest));

            HttpContent jsonContent = new StringContent(jsonRequest);
            HttpResponseMessage httpResponse = await _httpClient.PostAsync(_nodeAddress, jsonContent);
            string jsonResponse = await httpResponse.Content.ReadAsStringAsync();

            Debug.WriteLine(String.Format("Serialized Response: {0}", jsonResponse));

            RpcError rpcError = _jsonSerializer.Deserialize<RpcError>(jsonResponse);

            if(rpcError.Error != null)
            {
                throw new EthException(rpcError.Error.ErrorCode, rpcError.Error.Message);
            }

            RpcResponse<T> rpcResponse = _jsonSerializer.Deserialize<RpcResponse<T>>(jsonResponse);

            return rpcResponse;
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
