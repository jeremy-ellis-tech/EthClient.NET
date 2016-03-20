using Eth.Json;
using Eth.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace Eth
{
    /// <summary>
    /// The super simple Ethereum JSON RPC client
    /// </summary>
    public abstract class BaseClient
    {
        protected static string DefaultJsonRpc = "2.0";
        protected static int DefaultRequestId = 0;

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

            RpcResponse<string> response = await PostRpcRequestAsync<string>(request);

            return response.Result;
        }

        /// <summary>
        /// Returns Keccak-256 (not the standardized SHA3-256) hash of the given data.
        /// </summary>
        /// <param name="data">The data to hash</param>
        /// <exception cref="System.NullReferenceException">Thrown if data is null</exception>
        /// <returns>The hash of the given data</returns>
        public async Task<byte[]> Web3Sha3Async(byte[] data)
        {
            Ensure.EnsureParameterIsNotNull(data, "data");

            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "web3_sha3",
                Parameters = new[] { data }.Select(x => EthHex.ToHexString(x))
            };

            RpcResponse<string> response = await PostRpcRequestAsync<string>(request);

            return EthHex.HexStringToByteArray(response.Result);
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

            RpcResponse<string> response = await PostRpcRequestAsync<string>(request);

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

            RpcResponse<bool> response = await PostRpcRequestAsync<bool>(request);

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

            RpcResponse<string> response = await PostRpcRequestAsync<string>(request);

            return EthHex.HexStringToInt(response.Result);
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

            RpcResponse<string> response = await PostRpcRequestAsync<string>(request);

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

            RpcResponse<string> response = await PostRpcRequestAsync<string>(request);

            return EthHex.HexStringToByteArray(response.Result);
        }

        /// <summary>
        /// Returns true if the client is actively mining new blocks.
        /// </summary>
        /// <returns>Returns true if the client is mining, otherwise false</returns>
        public async Task<bool> EthMiningAsync()
        {
            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_mining",
                Parameters = RpcRequest.EmptyParameters
            };

            RpcResponse<bool> response = await PostRpcRequestAsync<bool>(request);

            return response.Result;
        }

        /// <summary>
        /// Returns the number of hashes per second that the node is mining with.
        /// </summary>
        /// <returns>number of hashes per second.</returns>
        public async Task<BigInteger> EthHashRateAsync()
        {
            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_hashrate",
                Parameters = RpcRequest.EmptyParameters
            };

            RpcResponse<string> response = await PostRpcRequestAsync<string>(request);

            return EthHex.HexStringToInt(response.Result);
        }

        /// <summary>
        /// Returns the current price per gas in wei.
        /// </summary>
        /// <returns>Current gas price in wei.</returns>
        public async Task<BigInteger> EthGasPriceAsync()
        {
            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_gasPrice",
                Parameters = RpcRequest.EmptyParameters
            };

            RpcResponse<string> response = await PostRpcRequestAsync<string>(request);

            return EthHex.HexStringToInt(response.Result);
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
                                        from = EthHex.ToHexString(x.From),
                                        to = x.To != null ? EthHex.ToHexString(x.To) : null,
                                        gas = x.Gas != null ? EthHex.ToHexString(x.Gas.Value) : null,
                                        gasPrice = x.GasPrice != null ? EthHex.ToHexString(x.GasPrice.Value) : null,
                                        value = x.Value != null ? EthHex.ToHexString(x.Value.Value) : null,
                                        data = x.Data != null ? EthHex.ToHexString(x.Data) : null,
                                        nonce = x.Nonce != null ? EthHex.ToHexString(x.Nonce.Value) : null
                                    })
            };

            RpcResponse<string> response = await PostRpcRequestAsync<string>(request);

            return EthHex.HexStringToByteArray(response.Result);
        }

        /// <summary>
        /// Creates new message call transaction or a contract creation for signed transactions.
        /// </summary>
        /// <param name="data">The signed transaction data</param>
        /// <returns>32 Bytes - the transaction hash, or the zero hash if the transaction is not yet available.
        /// Use eth_getTransactionReceipt to get the contract address, after the transaction was mined, when you created a contract.</returns>
        public async Task<byte[]> EthSendRawTransactionAsync(byte[] data)
        {
            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_sendRawTransaction",
                Parameters = new[] { EthHex.ToHexString(data) }
            };

            RpcResponse<string> response = await PostRpcRequestAsync<string>(request);

            return EthHex.HexStringToByteArray(response.Result);
        }

        /// <summary>
        /// Returns an object object with data about the sync status or False.
        /// </summary>
        /// <exception cref="System.InvalidCastException">Thrown if json returned cannot be deserialized to bool or Json.EthSyncing</exception>
        /// <returns>An object with sync status data or False, when not syncing</returns>
        public async Task<EthSyncing> EthSyncingAsync()
        {
            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_syncing",
                Parameters = RpcRequest.EmptyParameters
            };

            RpcResponse<dynamic> response = await PostRpcRequestAsync<dynamic>(request);

            if (response.Result is bool)
            {
                return new EthSyncing(false);
            }

            Json.EthSyncing isSynchingResponse = response.Result as Json.EthSyncing;

            if (isSynchingResponse != null)
            {
                return new EthSyncing(EthHex.HexStringToInt(isSynchingResponse.StartingBlock),
                    EthHex.HexStringToInt(isSynchingResponse.CurrentBlock),
                    EthHex.HexStringToInt(isSynchingResponse.HighestBlock));
            }
            else
            {
                throw new InvalidCastException("RPC response was of an unknown type");
            }
        }

        /// <summary>
        /// Returns a list of addresses owned by the client
        /// </summary>
        /// <returns>Array of 20 bytes, address owned by the client</returns>
        public async Task<IList<byte[]>> EthAccountsAsync()
        {
            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_accounts",
                Parameters = RpcRequest.EmptyParameters
            };

            RpcResponse<IEnumerable<string>> response = await PostRpcRequestAsync<IEnumerable<string>>(request);

            if(response.Result == null)
            {
                return Enumerable.Empty<byte[]>().ToList();
            }

            return response.Result.Select(x => EthHex.HexStringToByteArray(x)).ToList();
        }

        /// <summary>
        /// Returns the balance of the account of given address.
        /// </summary>
        /// <param name="address">20 Bytes - address to check for balance.</param>
        /// <param name="defaultBlock">integer block number, or the string "latest", "earliest" or "pending"</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if address is not 20 bytes long</exception>
        /// <returns>Integer of the current balance in wei.</returns>
        public async Task<BigInteger> EthGetBalanceAsync(byte[] address, DefaultBlock defaultBlock)
        {
            Ensure.EnsureCountIsCorrect(address, EthSpecs.AddressLength, "address");

            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_getBalance",
                Parameters = new[]
                {
                    EthHex.ToHexString(address),
                    defaultBlock.BlockNumber.HasValue ? EthHex.ToHexString(defaultBlock.BlockNumber.Value) : defaultBlock.Option.ToString().ToLowerInvariant()
                }
            };

            RpcResponse<string> response = await PostRpcRequestAsync<string>(request);

            return EthHex.HexStringToInt(response.Result);
        }

        /// <summary>
        /// Returns the value from a storage position at a given address.
        /// </summary>
        /// <param name="address">20 Bytes - address of the storage.</param>
        /// <param name="position">integer of the position in the storage.</param>
        /// <param name="defaultBlock">integer block number, or the string "latest", "earliest" or "pending"</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if address is not 20 bytes long</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if address or defaultBlock is null</exception>
        /// <returns></returns>
        public async Task<byte[]> EthGetStorageAtAsync(byte[] address, BigInteger position, DefaultBlock defaultBlock)
        {
            Ensure.EnsureParameterIsNotNull(address, "address");
            Ensure.EnsureParameterIsNotNull(defaultBlock, "defaultBlock");
            Ensure.EnsureCountIsCorrect(address, EthSpecs.AddressLength, "address");

            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_getStorageAt",
                Parameters = new[]
                {
                    EthHex.ToHexString(address),
                    EthHex.ToHexString(position),
                    defaultBlock.BlockNumber.HasValue ? EthHex.ToHexString(defaultBlock.BlockNumber.Value) : defaultBlock.Option.ToString().ToLowerInvariant()
                }
            };

            RpcResponse<string> response = await PostRpcRequestAsync<string>(request);

            return EthHex.HexStringToByteArray(response.Result);
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

            RpcResponse<string> response = await PostRpcRequestAsync<string>(request);

            return EthHex.HexStringToInt(response.Result);
        }

        /// <summary>
        /// Returns the number of transactions sent from an address.
        /// </summary>
        /// <param name="address"> 20 Bytes - address.</param>
        /// <param name="defaultBlock">integer block number, or the string "latest", "earliest" or "pending"</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if address is not 20 bytes long</exception>
        /// <returns></returns>
        public async Task<BigInteger> EthGetTransactionCountAsync(byte[] address, DefaultBlock defaultBlock)
        {
            Ensure.EnsureCountIsCorrect(address, EthSpecs.AddressLength, "address");

            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_getTransactionCount",
                Parameters = new[]
                {
                    EthHex.ToHexString(address),
                    defaultBlock.BlockNumber.HasValue ? EthHex.ToHexString(defaultBlock.BlockNumber.Value) : defaultBlock.Option.ToString().ToLowerInvariant()
                }
            };

            RpcResponse<string> response = await PostRpcRequestAsync<string>(request);

            return EthHex.HexStringToInt(response.Result);
        }

        /// <summary>
        /// Returns the number of transactions in a block from a block matching the given block hash.
        /// </summary>
        /// <param name="blockHash">32 Bytes - hash of a block</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if blockHash is not 32 bytes long</exception>
        /// <returns>integer of the number of transactions in this block.</returns>
        public async Task<BigInteger> EthGetTransactionCountByHashAsync(byte[] blockHash)
        {
            Ensure.EnsureCountIsCorrect(blockHash, EthSpecs.BlockHashLength, "blockHash");

            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_getBlockTransactionCountByHash",
                Parameters = new[] { EthHex.ToHexString(blockHash) }
            };

            RpcResponse<string> response = await PostRpcRequestAsync<string>(request);

            return EthHex.HexStringToInt(response.Result);
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
                Parameters = new[] { defaultBlock.BlockNumber.HasValue ? EthHex.ToHexString(defaultBlock.BlockNumber.Value) : defaultBlock.Option.Value.ToString().ToLowerInvariant() }
            };

            RpcResponse<string> response = await PostRpcRequestAsync<string>(request);

            return EthHex.HexStringToInt(response.Result);
        }

        /// <summary>
        /// Returns the number of uncles in a block from a block matching the given block hash.
        /// </summary>
        /// <param name="blockHash">32 Bytes - hash of a block</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if blockHash is not 32 bytes long</exception>
        /// <returns>integer of the number of uncles in this block.</returns>
        public async Task<BigInteger> EthGetUncleCountByBlockHashAsync(byte[] blockHash)
        {
            Ensure.EnsureCountIsCorrect(blockHash, EthSpecs.BlockHashLength, "blockHash");

            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_getUncleCountByBlockHash",
                Parameters = new[] { EthHex.ToHexString(blockHash) }
            };

            RpcResponse<string> response = await PostRpcRequestAsync<string>(request);

            return EthHex.HexStringToInt(response.Result);
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
                Parameters = new[] { defaultBlock.BlockNumber.HasValue ? EthHex.ToHexString(defaultBlock.BlockNumber.Value) : defaultBlock.Option.Value.ToString().ToLowerInvariant() }
            };

            RpcResponse<string> response = await PostRpcRequestAsync<string>(request);

            return EthHex.HexStringToInt(response.Result);
        }

        /// <summary>
        /// Returns code at a given address.
        /// </summary>
        /// <param name="address">20 Bytes - address</param>
        /// <param name="defaultBlock">integer block number, or the string "latest", "earliest" or "pending"</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if address is not 20 bytes long</exception>
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
                    EthHex.ToHexString(address),
                    defaultBlock.BlockNumber.HasValue ? EthHex.ToHexString(defaultBlock.BlockNumber.Value) : defaultBlock.Option.Value.ToString().ToLowerInvariant()
                }
            };

            RpcResponse<string> response = await PostRpcRequestAsync<string>(request);

            return EthHex.HexStringToByteArray(response.Result);
        }

        /// <summary>
        /// Signs data with a given address.
        /// </summary>
        /// <param name="address">20 Bytes - address</param>
        /// <param name="data">Data to sign</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if address is not 20 bytes long</exception>
        /// <returns>Signed data</returns>
        public async Task<byte[]> EthSignAsync(byte[] address, byte[] data)
        {
            Ensure.EnsureCountIsCorrect(address, EthSpecs.AddressLength, "address");

            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_sign",
                Parameters = new[] { EthHex.ToHexString(address), EthHex.ToHexString(data) }
            };

            RpcResponse<string> response = await PostRpcRequestAsync<string>(request);

            return EthHex.HexStringToByteArray(response.Result);
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

                    defaultBlock.BlockNumber.HasValue ? EthHex.ToHexString(defaultBlock.BlockNumber.Value) : defaultBlock.Option.ToString().ToLowerInvariant()
                }
            };

            RpcResponse<string> response = await PostRpcRequestAsync<string>(request);

            return EthHex.HexStringToByteArray(response.Result);
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

                    defaultBlock.BlockNumber.HasValue ? EthHex.ToHexString(defaultBlock.BlockNumber.Value) : defaultBlock.Option.ToString().ToLowerInvariant()
                }
            };

            RpcResponse<string> response = await PostRpcRequestAsync<string>(request);

            return EthHex.HexStringToInt(response.Result);
        }

        /// <summary>
        /// Returns information about a block by hash.
        /// </summary>
        /// <param name="blockHash">32 Bytes - Hash of a block.</param>
        /// <param name="fullTransaction">If true it returns the full transaction objects, if false only the hashes of the transactions</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if blockHash is not 32 bytes long</exception>
        /// <returns>A block object, or null when no block was found</returns>
        public async Task<EthBlock> EthGetBlockByHashAsync(byte[] blockHash, bool fullTransaction)
        {
            Ensure.EnsureCountIsCorrect(blockHash, EthSpecs.BlockHashLength, "blockHash");

            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_getBlockByHash",
                Parameters = new object[] { EthHex.ToHexString(blockHash), fullTransaction }
            };

            RpcResponse<Json.EthBlock> response = await PostRpcRequestAsync<Json.EthBlock>(request);

            return GetEthBlock(response.Result);
        }

        /// <summary>
        /// Returns information about a block by block number.
        /// </summary>
        /// <param name="defaultBlock">integer of a block number, or the string "earliest", "latest" or "pending"</param>
        /// <param name="fullTransaction">If true it returns the full transaction objects, if false only the hashes of the transactions</param>
        /// <returns></returns>
        public async Task<EthBlock> EthGetBlockByNumberAsync(DefaultBlock defaultBlock, bool fullTransaction)
        {
            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_getBlockByNumber",
                Parameters = new object[]
                {
                    defaultBlock.BlockNumber.HasValue ? EthHex.ToHexString(defaultBlock.BlockNumber.Value) : defaultBlock.Option.ToString().ToLowerInvariant(),
                    fullTransaction
                }
            };

            RpcResponse<Json.EthBlock> response = await PostRpcRequestAsync<Json.EthBlock>(request);

            return GetEthBlock(response.Result);
        }

        private static EthBlock GetEthBlock(Json.EthBlock ethBlock)
        {
            return new EthBlock
            {
                Number = String.IsNullOrEmpty(ethBlock.Number) ? (BigInteger?)null : EthHex.HexStringToInt(ethBlock.Number),
                Hash = String.IsNullOrEmpty(ethBlock.Number) ? null : EthHex.HexStringToByteArray(ethBlock.Hash),
                ParentHash = EthHex.HexStringToByteArray(ethBlock.ParentHash),
                Nonce = String.IsNullOrEmpty(ethBlock.Nonce) ? null : EthHex.HexStringToByteArray(ethBlock.Nonce),
                Sha3Uncles = EthHex.HexStringToByteArray(ethBlock.Sha3Uncles),
                LogsBloom = String.IsNullOrEmpty(ethBlock.LogsBloom) ? null : EthHex.HexStringToByteArray(ethBlock.LogsBloom),
                TransactionRoot = EthHex.HexStringToByteArray(ethBlock.TransactionRoot),
                StateRoot = EthHex.HexStringToByteArray(ethBlock.StateRoot),
                ReceiptsRoot = EthHex.HexStringToByteArray(ethBlock.ReceiptsRoot),
                Miner = EthHex.HexStringToByteArray(ethBlock.Miner),
                Difficulty = EthHex.HexStringToInt(ethBlock.Difficulty),
                TotalDifficulty = EthHex.HexStringToInt(ethBlock.TotalDifficulty),
                ExtraData = EthHex.HexStringToByteArray(ethBlock.ExtraData),
                Size = EthHex.HexStringToInt(ethBlock.Size),
                GasLimit = EthHex.HexStringToInt(ethBlock.GasLimit),
                GasUsed = EthHex.HexStringToInt(ethBlock.GasUsed),
                TimeStamp = DateTimeOffset.Parse(ethBlock.TimeStamp),
                Transactions = ethBlock.Transactions, //TODO
                Unlces = ethBlock.Unlces.Select(x => EthHex.HexStringToByteArray(x))
            };
        }

        /// <summary>
        /// Returns the information about a transaction requested by transaction hash.
        /// </summary>
        /// <param name="transactionHash">32 Bytes - hash of a transaction</param>
        /// <returns>A transaction object, or null when no transaction was found</returns>
        public async Task<EthTransaction> EthGetTransactionByHashAsync(byte[] transactionHash)
        {
            Ensure.EnsureParameterIsNotNull(transactionHash, "transactionHash");
            Ensure.EnsureCountIsCorrect(transactionHash, EthSpecs.TransactionHashLength, "transactionHash");

            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_getTransactionByHash",
                Parameters = new[] { EthHex.ToHexString(transactionHash) }
            };

            RpcResponse<Json.EthTransaction> response = await PostRpcRequestAsync<Json.EthTransaction>(request);

            return GetEthTransaction(response.Result);
        }

        /// <summary>
        /// Returns information about a transaction by block number and transaction index position.
        /// </summary>
        /// <param name="defaultBlock">a block number, or the string "earliest", "latest" or "pending"</param>
        /// <param name="position">the transaction index position.</param>
        /// <returns>A transaction object, or null when no transaction was found</returns>
        public async Task<EthTransaction> EthGetTransactionByBlockNumberAndIndexAsync(DefaultBlock defaultBlock, BigInteger position)
        {
            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_getTransactionByBlockNumberAndIndex",
                Parameters = new object[]
                {
                    defaultBlock.BlockNumber.HasValue ? EthHex.ToHexString(defaultBlock.BlockNumber.Value) : defaultBlock.Option.ToString().ToLowerInvariant(),
                    position
                }
            };

            RpcResponse<Json.EthTransaction> response = await PostRpcRequestAsync<Json.EthTransaction>(request);

            return GetEthTransaction(response.Result);
        }

        private static EthTransaction GetEthTransaction(Json.EthTransaction transaction)
        {
            if (transaction == null)
            {
                return null;
            }

            return new EthTransaction(null, null); //TODO
        }

        /// <summary>
        /// Returns the receipt of a transaction by transaction hash.
        /// Note That the receipt is not available for pending transactions.
        /// </summary>
        /// <param name="transactionHash">32 Bytes - hash of a transaction</param>
        /// <returns>A transaction receipt object, or null when no receipt was found</returns>
        public async Task<EthTransactionReceipt> EthGetTransactionReceiptAsync(byte[] transactionHash)
        {
            Ensure.EnsureParameterIsNotNull(transactionHash, "transactionHash");
            Ensure.EnsureCountIsCorrect(transactionHash, EthSpecs.TransactionHashLength, "transactionHash");

            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_getTransactionReceipt",
                Parameters = new[] { EthHex.ToHexString(transactionHash) }
            };

            RpcResponse<Json.EthTransactionReceipt> response = await PostRpcRequestAsync<Json.EthTransactionReceipt>(request);

            Json.EthTransactionReceipt result = response.Result;

            if (result == null) return null;

            return new EthTransactionReceipt
            {
                BlockHash = EthHex.HexStringToByteArray(result.BlockHash),
                BlockNumber = EthHex.HexStringToInt(result.BlockNumber),
                ContractAddress = result.ContractAddress != null ? EthHex.HexStringToByteArray(result.ContractAddress) : null,
                CumulativeGasUsed = EthHex.HexStringToInt(result.CumulativeGasUsed),
                GasUsed = EthHex.HexStringToInt(result.GasUsed),
                TransactionIndex = EthHex.HexStringToInt(result.TransactionIndex),
                TransactionHash = EthHex.HexStringToByteArray(result.TransactionHash),
                Logs = result.Logs
            };
        }

        /// <summary>
        /// Returns a list of available compilers in the client.
        /// </summary>
        /// <returns>Available compilers</returns>
        public async Task<IList<string>> EthGetCompilersAsync()
        {
            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_getCompilers",
                Parameters = RpcRequest.EmptyParameters
            };

            RpcResponse<IList<string>> response = await PostRpcRequestAsync<IList<string>>(request);

            if(response.Result == null)
            {
                return Enumerable.Empty<string>().ToList();
            }

            return response.Result;
        }

        /// <summary>
        /// Returns compiled solidity code.
        /// </summary>
        /// <param name="sourceCode">The source code.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if sourceCode is an empty string</exception>
        /// <returns>The compiled source code</returns>
        public async Task<IList<SolidityContract>> EthCompileSolidityAsync(string sourceCode)
        {
            Ensure.EnsureStringIsNotNullOrEmpty(sourceCode, "sourceCode");

            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_compileSolidity",
                Parameters = new[] { sourceCode }
            };

            RpcResponse<IDictionary<string, SolidityContract>> response = await PostRpcRequestAsync<IDictionary<string, SolidityContract>>(request);

            return response.Result.Select(x => { x.Value.ContractName = x.Key; return x.Value; }).ToList();
        }

        /// <summary>
        /// Returns compiled LLL code
        /// </summary>
        /// <param name="sourceCode">The source code</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if sourceCode is an empty string</exception>
        /// <returns>The compiled source code</returns>
        public async Task<byte[]> EthCompileLllAsync(string sourceCode)
        {
            Ensure.EnsureStringIsNotNullOrEmpty(sourceCode, "sourceCode");

            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_compileLLL",
                Parameters = new[] { sourceCode }
            };

            RpcResponse<string> response = await PostRpcRequestAsync<string>(request);

            return EthHex.HexStringToByteArray(response.Result);
        }

        /// <summary>
        /// Returns compiled Serpent code
        /// </summary>
        /// <param name="sourceCode">The source code</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if sourceCode is an empty string</exception>
        /// <returns>The compiled source code</returns>
        public async Task<byte[]> EthCompileSerpentAsync(string sourceCode)
        {
            Ensure.EnsureStringIsNotNullOrEmpty(sourceCode, "sourceCode");

            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_compileSerpent",
                Parameters = new[] { sourceCode }
            };

            RpcResponse<string> response = await PostRpcRequestAsync<string>(request);

            return EthHex.HexStringToByteArray(response.Result);
        }

        /// <summary>
        /// Creates a filter object, based on filter options, to notify when the state changes (logs).
        /// To check if the state has changed, call EthGetFilterChangesAsync()
        /// </summary>
        /// <param name="filterOptions">The filter options</param>
        /// <returns>A filter id</returns>
        public async Task<BigInteger> EthNewFilterAsync(EthFilterOptions filterOptions)
        {
            Ensure.EnsureParameterIsNotNull(filterOptions, "filterOptions");

            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_newFilter",
                Parameters = new[]
                {
                    new
                    {
                        fromBlock = filterOptions.FromBlock != null ? filterOptions.FromBlock.BlockNumber.HasValue ? EthHex.ToHexString(filterOptions.FromBlock.BlockNumber.Value) : filterOptions.FromBlock.Option.ToString().ToLowerInvariant() : null,
                        toBlock = filterOptions.ToBlock != null ? filterOptions.ToBlock.BlockNumber.HasValue ? EthHex.ToHexString(filterOptions.ToBlock.BlockNumber.Value) : filterOptions.ToBlock.Option.ToString().ToLowerInvariant() : null,
                        address = filterOptions.Address,
                        topics = filterOptions.Topics != null ? filterOptions.Topics : null
                    }
                }
            };

            RpcResponse<string> response = await PostRpcRequestAsync<string>(request);

            return EthHex.HexStringToInt(response.Result);
        }

        /// <summary>
        /// Creates a filter in the node, to notify when a new block arrives. To check if the state has changed, call EthGetFilterChangesAsync().
        /// </summary>
        /// <returns>A filter id</returns>
        public async Task<BigInteger> EthNewBlockFilterAsync()
        {
            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_newBlockFilter",
                Parameters = RpcRequest.EmptyParameters
            };

            RpcResponse<string> rpcResponse = await PostRpcRequestAsync<string>(request);

            return EthHex.HexStringToInt(rpcResponse.Result);
        }

        /// <summary>
        /// Creates a filter in the node, to notify when new pending transactions arrive. To check if the state has changed, call EthGetFilterChangesAsync().
        /// </summary>
        /// <returns>A filter id</returns>
        public async Task<BigInteger> EthNewPendingTransactionFilterAsync()
        {
            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_newPendingTransactionFilter",
                Parameters = RpcRequest.EmptyParameters
            };

            RpcResponse<string> rpcResponse = await PostRpcRequestAsync<string>(request);

            return EthHex.HexStringToInt(rpcResponse.Result);
        }

        /// <summary>
        /// Uninstalls a filter with given id. Should always be called when watch is no longer needed.
        /// Additonally Filters timeout when they aren't requested with eth_getFilterChanges for a period of time.
        /// </summary>
        /// <param name="filterId">The filter id</param>
        /// <returns>true if the filter was successfully uninstalled, otherwise false</returns>
        public async Task<bool> EthUninstallFilterAsync(BigInteger filterId)
        {
            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_uninstallFilter",
                Parameters = new[] { EthHex.ToHexString(filterId) }
            };

            RpcResponse<bool> rpcResponse = await PostRpcRequestAsync<bool>(request);

            return rpcResponse.Result;
        }

        /// <summary>
        /// Polling method for a filter, which returns an array of logs which occurred since last poll.
        /// </summary>
        /// <param name="filterId"> the filter id.</param>
        /// <returns>Array of log objects, or an empty array if nothing has changed since last poll.</returns>
        public async Task<dynamic> EthGetFilterChangesAsync(BigInteger filterId)
        {
            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_getFilterChanges",
                Parameters = new[] { EthHex.ToHexString(filterId) }
            };

            RpcResponse<dynamic> rpcResponse = await PostRpcRequestAsync<dynamic>(request);

            return rpcResponse.Result;
        }

        /// <summary>
        /// Returns an array of all logs matching a given filter object.
        /// </summary>
        /// <param name="filterOptions">the filter object</param>
        /// <returns>Array of log objects, or an empty array if nothing has changed since last poll</returns>
        public async Task<dynamic> EthGetLogsAsync(EthFilterOptions filterOptions)
        {
            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_getLogs",
                Parameters = new[]
                {
                    new
                    {
                        fromBlock = filterOptions.FromBlock != null ? filterOptions.FromBlock.BlockNumber.HasValue ? EthHex.ToHexString(filterOptions.FromBlock.BlockNumber.Value) : filterOptions.FromBlock.Option.ToString().ToLowerInvariant() : null,
                        toBlock = filterOptions.ToBlock != null ? filterOptions.ToBlock.BlockNumber.HasValue ? EthHex.ToHexString(filterOptions.ToBlock.BlockNumber.Value) : filterOptions.ToBlock.Option.ToString().ToLowerInvariant() : null,
                        address = filterOptions.Address,
                        topics = filterOptions.Topics != null ? filterOptions.Topics : null
                    }
                }
            };

            RpcResponse<dynamic> rpcResponse = await PostRpcRequestAsync<dynamic>(request);

            return rpcResponse.Result;
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

            RpcResponse<IList<string>> rpcResponse = await PostRpcRequestAsync<IList<string>>(request);

            IList<string> result = rpcResponse.Result;

            return new EthWork(EthHex.HexStringToByteArray(result[0]), EthHex.HexStringToByteArray(result[1]), EthHex.HexStringToByteArray(result[2]));
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

            RpcResponse<bool> response = await PostRpcRequestAsync<bool>(request);

            return response.Result;
        }

        /// <summary>
        /// Used for submitting mining hashrate.
        /// </summary>
        /// <param name="hashRate">the hash rate</param>
        /// <param name="clientID">A random ID identifying the client</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if hash rate is longer than 32 bytes as a byte array</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if clientID is longer than 32 bytes</exception>
        /// <returns>returns true if submitting went through succesfully and false otherwise</returns>
        public async Task<bool> EthSubmitHashRateAsync(BigInteger hashRate, byte[] clientID)
        {
            Ensure.EnsureCountIsCorrect(clientID, EthSpecs.ClientIDLength, "clientID");

            if (hashRate.ToByteArray().Length > EthSpecs.HashRateMaxLength)
            {
                throw new ArgumentOutOfRangeException("hashRate");
            }

            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "eth_submitHashrate",
                Parameters = new[] { EthHex.ToHexString(hashRate), EthHex.ToHexString(clientID) }
            };

            RpcResponse<bool> response = await PostRpcRequestAsync<bool>(request);

            return response.Result;
        }


        /// <summary>
        /// Creates a new account
        /// Not availible over json RPC by default.
        /// You must start geth with --rpcapi "personal,web3,eth"
        /// to use "Personal*" methods.
        /// </summary>
        /// <param name="password">The password to use for the account</param>
        /// <returns>The address of the created account</returns>
        public async Task<byte[]> PersonalNewAccountAsync(string password)
        {
            var request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "personal_newAccount",
                Parameters = new[] { password }
            };

            RpcResponse<string> response = await PostRpcRequestAsync<string>(request);

            return EthHex.HexStringToByteArray(response.Result);
        }

        /// <summary>
        /// Unlock account to send transactions/sign etc.
        /// </summary>
        /// <param name="address">Address of account to unlock</param>
        /// <param name="password">The password with which to unlock the account</param>
        /// <param name="duration">The duration the account should remain unlocked</param>
        /// <returns>True if account was successfully unlocked, false otherwise</returns>
        public async Task<bool> PersonalUnlockAccountAsync(byte[] address, string password, TimeSpan duration)
        {
            var request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "personal_unlockAccount",
                Parameters = new object[] { EthHex.ToHexString(address), password, (int)duration.TotalSeconds }
            };

            RpcResponse<bool> response = await PostRpcRequestAsync<bool>(request);

            return response.Result;
        }

        /// <summary>
        /// Unlock account for 1 minute to send transactions/sign etc.
        /// </summary>
        /// <param name="address">Address of account to unlock</param>
        /// <param name="password">The password with which to unlock the account</param>
        /// <returns>True if account was successfully unlocked, false otherwise</returns>
        public async Task<bool> PersonalUnlockAccountAsync(byte[] address, string password)
        {
            return await PersonalUnlockAccountAsync(address, password, TimeSpan.FromMinutes(1.0));
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

            RpcResponse<string> response = await PostRpcRequestAsync<string>(request);

            return EthHex.HexStringToByteArray(response.Result);
        }

        /// <summary>
        /// Checks if the client hold the private keys for a given identity.
        /// </summary>
        /// <param name="address">60 Bytes - The identity address to check.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if address is longer than 60 bytes</exception>
        /// <returns>returns true if the client holds the privatekey for that identity, otherwise false.</returns>
        public async Task<bool> ShhHasIdentityAsync(byte[] address)
        {
            Ensure.EnsureCountIsCorrect(address, EthSpecs.WhisperIdentityLength, "address");

            RpcRequest request = new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = "shh_hasIdentity",
                Parameters = new[] { EthHex.ToHexString(address) }
            };

            RpcResponse<bool> response = await PostRpcRequestAsync<bool>(request);

            return response.Result;
        }

        public abstract Task<RpcResponse<T>> PostRpcRequestAsync<T>(RpcRequest request);
    }
}

