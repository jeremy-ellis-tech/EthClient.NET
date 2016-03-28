using Eth.Abi;
using Eth.Rpc;
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
        private static string DefaultJsonRpc = "2.0";
        private static int DefaultRequestId = 0;

        private readonly IContractCallEncoder _defaultEncoder = new ContractCallEncoder(new Keccak());

        private static RpcRequest BuildRpcRequest(string methodName, params object[] parameters)
        {
            return new RpcRequest
            {
                ID = DefaultRequestId,
                JsonRpc = DefaultJsonRpc,
                MethodName = methodName,
                Parameters = parameters != null && parameters.Length > 0 ? parameters : Enumerable.Empty<object>()
            };
        }

        public abstract Task<RpcResponse<T>> PostRpcRequestAsync<T>(RpcRequest request);

        /// <summary>
        /// Get a contract located at an address on the block-chain.
        /// </summary>
        /// <param name="address">The contract's address</param>
        /// <returns>An ethereum contract</returns>
        public EthContract GetContractAt(byte[] address)
        {
            return new EthContract(address, this, _defaultEncoder);
        }

        /// <summary>
        /// Returns the current client version.
        /// </summary>
        /// <returns>The current client version</returns>
        public async Task<string> Web3ClientVersionAsync()
        {
            RpcRequest request = BuildRpcRequest("web3_clientVersion");
            RpcResponse<string> response = await PostRpcRequestAsync<string>(request);
            return response.Result;
        }

        /// <summary>
        /// Returns Keccak-256 (not the standardized SHA3-256) hash of the given data.
        /// </summary>
        /// <param name="data">The data to hash</param>
        /// <exception cref="NullReferenceException">Thrown if data is null</exception>
        /// <returns>The hash of the given data</returns>
        public async Task<byte[]> Web3Sha3Async(byte[] data)
        {
            Ensure.EnsureParameterIsNotNull(data, "data");

            RpcRequest request = BuildRpcRequest("web3_sha3", data);
            RpcResponse<byte[]> response = await PostRpcRequestAsync<byte[]>(request);
            return response.Result;
        }

        /// <summary>
        /// Returns the current network protocol version
        /// </summary>
        /// <returns>The current network protocol version</returns>
        public async Task<string> NetVersionAsync()
        {
            RpcRequest request = BuildRpcRequest("net_version");
            RpcResponse<string> response = await PostRpcRequestAsync<string>(request);
            return response.Result;
        }

        /// <summary>
        /// Returns number of peers currenly connected to the client.
        /// </summary>
        /// <returns>The number of connected peers.</returns>
        public async Task<BigInteger> NetPeerCountAsync()
        {
            RpcRequest request = BuildRpcRequest("net_peerCount");
            RpcResponse<BigInteger> response = await PostRpcRequestAsync<BigInteger>(request);
            return response.Result;
        }

        /// <summary>
        /// Returns true if client is actively listening for network connections.
        /// </summary>
        /// <returns>true when listening, otherwise false.</returns>
        public async Task<bool> NetListeningAsync()
        {
            RpcRequest request = BuildRpcRequest("net_listening");
            RpcResponse<bool> response = await PostRpcRequestAsync<bool>(request);
            return response.Result;
        }

        /// <summary>
        /// Returns the current ethereum protocol version.
        /// </summary>
        /// <returns>The current ethereum protocol version</returns>
        public async Task<string> EthProtocolVersionAsync()
        {
            RpcRequest request = BuildRpcRequest("eth_protocolVersion");
            RpcResponse<string> response = await PostRpcRequestAsync<string>(request);
            return response.Result;
        }

        /// <summary>
        /// Returns an object with data about the sync status or False.
        /// </summary>
        /// <returns>An object with sync status data or False, when not syncing</returns>
        public async Task<EthSyncing> EthSyncingAsync()
        {
            RpcRequest request = BuildRpcRequest("eth_syncing");
            RpcResponse<EthSyncing> response = await PostRpcRequestAsync<EthSyncing>(request);
            return response.Result;
        }

        /// <summary>
        /// Returns the client coinbase address.
        /// </summary>
        /// <returns>20 bytes - the current coinbase address.</returns>
        public async Task<byte[]> EthCoinbaseAsync()
        {
            RpcRequest request = BuildRpcRequest("eth_coinbase");
            RpcResponse<byte[]> response = await PostRpcRequestAsync<byte[]>(request);
            return response.Result;
        }

        /// <summary>
        /// Returns true if the client is actively mining new blocks.
        /// </summary>
        /// <returns>Returns true if the client is mining, otherwise false</returns>
        public async Task<bool> EthMiningAsync()
        {
            RpcRequest request = BuildRpcRequest("eth_mining");
            RpcResponse<bool> response = await PostRpcRequestAsync<bool>(request);
            return response.Result;
        }

        /// <summary>
        /// Returns the number of hashes per second that the node is mining with.
        /// </summary>
        /// <returns>number of hashes per second.</returns>
        public async Task<BigInteger> EthHashRateAsync()
        {
            RpcRequest request = BuildRpcRequest("eth_hashrate");
            RpcResponse<BigInteger> response = await PostRpcRequestAsync<BigInteger>(request);
            return response.Result;
        }

        /// <summary>
        /// Returns the current price per gas in wei.
        /// </summary>
        /// <returns>Current gas price in wei.</returns>
        public async Task<BigInteger> EthGasPriceAsync()
        {
            RpcRequest request = BuildRpcRequest("eth_gasPrice");
            RpcResponse<BigInteger> response = await PostRpcRequestAsync<BigInteger>(request);
            return response.Result;
        }

        /// <summary>
        /// Returns a list of addresses owned by the client
        /// </summary>
        /// <returns>Array of 20 bytes, address owned by the client</returns>
        public async Task<IList<byte[]>> EthAccountsAsync()
        {
            RpcRequest request = BuildRpcRequest("eth_accounts");
            RpcResponse<IList<byte[]>> response = await PostRpcRequestAsync<IList<byte[]>>(request);
            return response.Result;
        }

        /// <summary>
        /// Returns the number of most recent block.
        /// </summary>
        /// <returns>integer of the current block number the client is on.</returns>
        public async Task<BigInteger> EthBlockNumberAsync()
        {
            RpcRequest request = BuildRpcRequest("eth_blockNumber");
            RpcResponse<BigInteger> response = await PostRpcRequestAsync<BigInteger>(request);
            return response.Result;
        }

        /// <summary>
        /// Returns the balance of the account of given address.
        /// </summary>
        /// <param name="address">20 Bytes - address to check for balance.</param>
        /// <param name="defaultBlock">integer block number, or the string "latest", "earliest" or "pending"</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if address is not 20 bytes long</exception>
        /// <returns>Integer of the current balance in wei.</returns>
        public async Task<BigInteger> EthGetBalanceAsync(byte[] address, DefaultBlock defaultBlock)
        {
            Ensure.EnsureCountIsCorrect(address, EthSpecs.AddressLength, "address");

            RpcRequest request = BuildRpcRequest("eth_getBalance", address, defaultBlock);
            RpcResponse<BigInteger> response = await PostRpcRequestAsync<BigInteger>(request);
            return response.Result;
        }

        /// <summary>
        /// Returns the value from a storage position at a given address.
        /// </summary>
        /// <param name="address">20 Bytes - address of the storage.</param>
        /// <param name="position">integer of the position in the storage.</param>
        /// <param name="defaultBlock">integer block number, or the string "latest", "earliest" or "pending"</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if address is not 20 bytes long</exception>
        /// <exception cref="ArgumentNullException">Thrown if address or defaultBlock is null</exception>
        /// <returns></returns>
        public async Task<byte[]> EthGetStorageAtAsync(byte[] address, BigInteger position, DefaultBlock defaultBlock)
        {
            Ensure.EnsureParameterIsNotNull(address, "address");
            Ensure.EnsureParameterIsNotNull(defaultBlock, "defaultBlock");
            Ensure.EnsureCountIsCorrect(address, EthSpecs.AddressLength, "address");

            RpcRequest request = BuildRpcRequest("eth_getStorageAt", address, position, defaultBlock);
            RpcResponse<byte[]> response = await PostRpcRequestAsync<byte[]>(request);
            return response.Result;
        }

        /// <summary>
        /// Returns the number of transactions sent from an address.
        /// </summary>
        /// <param name="address"> 20 Bytes - address.</param>
        /// <param name="defaultBlock">integer block number, or the string "latest", "earliest" or "pending"</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if address is not 20 bytes long</exception>
        /// <returns></returns>
        public async Task<BigInteger> EthGetTransactionCountAsync(byte[] address, DefaultBlock defaultBlock)
        {
            Ensure.EnsureCountIsCorrect(address, EthSpecs.AddressLength, "address");

            RpcRequest request = BuildRpcRequest("eth_getTransactionCount", address, defaultBlock);
            RpcResponse<BigInteger> response = await PostRpcRequestAsync<BigInteger>(request);
            return response.Result;
        }

        /// <summary>
        /// Returns the number of transactions in a block from a block matching the given block hash.
        /// </summary>
        /// <param name="hash">32 Bytes - hash of a block</param>
        /// <returns>integer of the number of transactions in this block.</returns>
        public async Task<BigInteger> EthGetBlockTransactionCountByHashAsync(byte[] hash)
        {
            RpcRequest request = BuildRpcRequest("eth_getBlockTransactionCountByHash", hash);
            RpcResponse<BigInteger> response = await PostRpcRequestAsync<BigInteger>(request);
            return response.Result;
        }

        /// <summary>
        /// Returns the number of transactions in a block from a block matching the given block number.
        /// </summary>
        /// <param name="defaultBlock">integer of a block number, or the string "earliest", "latest" or "pending"</param>
        /// <returns>integer of the number of transactions in this block.</returns>
        public async Task<BigInteger> EthGetBlockTransactionCountByNumberAsync(DefaultBlock defaultBlock)
        {
            RpcRequest request = BuildRpcRequest("eth_getBlockTransactionCountByNumber", defaultBlock);
            RpcResponse<BigInteger> response = await PostRpcRequestAsync<BigInteger>(request);
            return response.Result;
        }

        /// <summary>
        /// Returns the number of transactions in a block from a block matching the given block hash.
        /// </summary>
        /// <param name="blockHash">32 Bytes - hash of a block</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if blockHash is not 32 bytes long</exception>
        /// <returns>integer of the number of transactions in this block.</returns>
        public async Task<BigInteger> EthGetTransactionCountByHashAsync(byte[] blockHash)
        {
            Ensure.EnsureCountIsCorrect(blockHash, EthSpecs.BlockHashLength, "blockHash");

            RpcRequest request = BuildRpcRequest("eth_getBlockTransactionCountByHash", blockHash);
            RpcResponse<BigInteger> response = await PostRpcRequestAsync<BigInteger>(request);
            return response.Result;
        }

        /// <summary>
        /// Returns the number of transactions in a block from a block matching the given block number.
        /// </summary>
        /// <param name="defaultBlock">integer of a block number, or the string "earliest", "latest" or "pending"</param>
        /// <returns>integer of the number of transactions in this block.</returns>
        public async Task<BigInteger> EthGetTransactionCountByNumberAsync(DefaultBlock defaultBlock)
        {
            RpcRequest request = BuildRpcRequest("eth_getBlockTransactionCountByNumber", defaultBlock);
            RpcResponse<BigInteger> response = await PostRpcRequestAsync<BigInteger>(request);
            return response.Result;
        }

        /// <summary>
        /// Returns the number of uncles in a block from a block matching the given block hash.
        /// </summary>
        /// <param name="blockHash">32 Bytes - hash of a block</param>
        /// <returns>integer of the number of uncles in this block.</returns>
        public async Task<BigInteger> EthGetUncleCountByBlockHashAsync(byte[] blockHash)
        {
            RpcRequest request = BuildRpcRequest("eth_getUncleCountByBlockHash", blockHash);
            RpcResponse<BigInteger> response = await PostRpcRequestAsync<BigInteger>(request);
            return response.Result;
        }

        /// <summary>
        /// Returns the number of uncles in a block from a block matching the given block number.
        /// </summary>
        /// <param name="defaultBlock">integer of a block number, or the string "latest", "earliest" or "pending"</param>
        /// <returns>integer of the number of uncles in this block.</returns>
        public async Task<BigInteger> EthGetUncleCountByBlockNumberAsync(DefaultBlock defaultBlock)
        {
            RpcRequest request = BuildRpcRequest("eth_getUncleCountByBlockNumber", defaultBlock);
            RpcResponse<BigInteger> response = await PostRpcRequestAsync<BigInteger>(request);
            return response.Result;
        }

        /// <summary>
        /// Returns code at a given address.
        /// </summary>
        /// <param name="address">20 Bytes - address</param>
        /// <param name="defaultBlock">integer block number, or the string "latest", "earliest" or "pending"</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if address is not 20 bytes long</exception>
        /// <returns>The code from the given address.</returns>
        public async Task<byte[]> EthGetCodeAsync(byte[] address, DefaultBlock defaultBlock)
        {
            Ensure.EnsureCountIsCorrect(address, EthSpecs.AddressLength, "address");

            RpcRequest request = BuildRpcRequest("eth_getCode", address, defaultBlock);
            RpcResponse<byte[]> response = await PostRpcRequestAsync<byte[]>(request);
            return response.Result;
        }

        /// <summary>
        /// Signs data with a given address.
        /// NB. the address to sign must be unlocked.
        /// </summary>
        /// <param name="address">20 Bytes - address</param>
        /// <param name="data">Data to sign</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if address is not 20 bytes long</exception>
        /// <returns>Signed data</returns>
        public async Task<byte[]> EthSignAsync(byte[] address, byte[] data)
        {
            Ensure.EnsureCountIsCorrect(address, EthSpecs.AddressLength, "address");

            RpcRequest request = BuildRpcRequest("eth_sign", address, data);
            RpcResponse<byte[]> response = await PostRpcRequestAsync<byte[]>(request);
            return response.Result;
        }

        /// <summary>
        /// Creates new message call transaction or a contract creation, if the data field contains code.
        /// </summary>
        /// <param name="transaction">The transaction to send</param>
        /// <returns>The transaction hash, or the zero hash if the transaction is not yet available</returns>
        public async Task<byte[]> EthSendTransactionAsync(EthTransaction transaction)
        {
            RpcRequest request = BuildRpcRequest("eth_sendTransaction", transaction);
            RpcResponse<byte[]> response = await PostRpcRequestAsync<byte[]>(request);
            return response.Result;
        }

        /// <summary>
        /// Creates new message call transaction or a contract creation for signed transactions.
        /// </summary>
        /// <param name="data">The signed transaction data</param>
        /// <returns>32 Bytes - the transaction hash, or the zero hash if the transaction is not yet available.
        /// Use eth_getTransactionReceipt to get the contract address, after the transaction was mined, when you created a contract.</returns>
        public async Task<byte[]> EthSendRawTransactionAsync(byte[] data)
        {
            RpcRequest request = BuildRpcRequest("eth_sendRawTransaction", data);
            RpcResponse<byte[]> response = await PostRpcRequestAsync<byte[]>(request);
            return response.Result;
        }

        /// <summary>
        /// Executes a new message call immediately without creating a transaction on the block chain.
        /// </summary>
        /// <param name="call">The transaction call object</param>
        /// <param name="defaultBlock">integer block number, or the string "latest", "earliest" or "pending"</param>
        /// <returns>the return value of executed contract</returns>
        public async Task<byte[]> EthCallAsync(EthCall call, DefaultBlock defaultBlock)
        {
            RpcRequest request = BuildRpcRequest("eth_call", call, defaultBlock);
            RpcResponse<byte[]> response = await PostRpcRequestAsync<byte[]>(request);
            return response.Result;
        }

        /// <summary>
        /// Makes a call or transaction, which won't be added to the blockchain and returns the used gas, which can be used for estimating the used gas.
        /// </summary>
        /// <param name="call">The transaction call object</param>
        /// <param name="defaultBlock">integer block number, or the string "latest", "earliest" or "pending"</param>
        /// <returns>the amount of gas used.</returns>
        public async Task<BigInteger> EthEstimateGasAsync(EthCall call, DefaultBlock defaultBlock)
        {
            RpcRequest request = BuildRpcRequest("eth_estimateGas", call, defaultBlock);
            RpcResponse<BigInteger> response = await PostRpcRequestAsync<BigInteger>(request);
            return response.Result;
        }

        /// <summary>
        /// Returns information about a block by hash.
        /// </summary>
        /// <param name="blockHash">32 Bytes - Hash of a block.</param>
        /// <param name="fullTransaction">If true it returns the full transaction objects, if false only the hashes of the transactions</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if blockHash is not 32 bytes long</exception>
        /// <returns>A block object, or null when no block was found</returns>
        public async Task<EthBlock> EthGetBlockByHashAsync(byte[] blockHash, bool fullTransaction)
        {
            Ensure.EnsureCountIsCorrect(blockHash, EthSpecs.BlockHashLength, "blockHash");

            RpcRequest request = BuildRpcRequest("eth_getBlockByHash", blockHash, fullTransaction);
            RpcResponse<EthBlock> response = await PostRpcRequestAsync<EthBlock>(request);
            return response.Result;
        }

        /// <summary>
        /// Returns information about a block by block number.
        /// </summary>
        /// <param name="defaultBlock">integer of a block number, or the string "earliest", "latest" or "pending"</param>
        /// <param name="fullTransaction">If true it returns the full transaction objects, if false only the hashes of the transactions</param>
        /// <returns></returns>
        public async Task<EthBlock> EthGetBlockByNumberAsync(DefaultBlock defaultBlock, bool fullTransaction)
        {
            RpcRequest request = BuildRpcRequest("eth_getBlockByNumber", defaultBlock, fullTransaction);
            RpcResponse<EthBlock> response = await PostRpcRequestAsync<EthBlock>(request);
            return response.Result;
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

            RpcRequest request = BuildRpcRequest("eth_getTransactionByHash", transactionHash);
            RpcResponse<EthTransaction> response = await PostRpcRequestAsync<EthTransaction>(request);
            return response.Result;
        }

        /// <summary>
        /// Returns information about a transaction by block hash and transaction index position.
        /// </summary>
        /// <param name="blockHash">32 Bytes - hash of a block.</param>
        /// <param name="index">integer of the transaction index position.</param>
        /// <returns>The transaction at the position in the block</returns>
        public async Task<EthTransaction> EthGetTransactionByBlockHashAndIndexAsync(byte[] blockHash, BigInteger index)
        {
            RpcRequest request = BuildRpcRequest("eth_getTransactionByBlockHashAndIndex", blockHash, index);
            RpcResponse<EthTransaction> response = await PostRpcRequestAsync<EthTransaction>(request);
            return response.Result;
        }

        /// <summary>
        /// Returns information about a transaction by block number and transaction index position.
        /// </summary>
        /// <param name="defaultBlock">a block number, or the string "earliest", "latest" or "pending"</param>
        /// <param name="position">the transaction index position.</param>
        /// <returns>A transaction object, or null when no transaction was found</returns>
        public async Task<EthTransaction> EthGetTransactionByBlockNumberAndIndexAsync(DefaultBlock defaultBlock, BigInteger position)
        {
            RpcRequest request = BuildRpcRequest("eth_getTransactionByBlockNumberAndIndex", defaultBlock, position);
            RpcResponse<EthTransaction> response = await PostRpcRequestAsync<EthTransaction>(request);
            return response.Result;
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

            RpcRequest request = BuildRpcRequest("eth_getTransactionReceipt", transactionHash);
            RpcResponse<EthTransactionReceipt> response = await PostRpcRequestAsync<EthTransactionReceipt>(request);
            return response.Result;
        }

        /// <summary>
        /// Returns information about a uncle of a block by hash and uncle index position.
        /// </summary>
        /// <param name="blockHash">32 Bytes - hash a block.</param>
        /// <param name="index">the uncle's index position.</param>
        /// <returns>The uncle block</returns>
        public async Task<EthBlock> EthGetUncleByBlockHashAndIndexAsync(byte[] blockHash, BigInteger index)
        {
            RpcRequest request = BuildRpcRequest("eth_getUncleByBlockHashAndIndex", blockHash, index);
            RpcResponse<EthBlock> response = await PostRpcRequestAsync<EthBlock>(request);
            return response.Result;
        }

        /// <summary>
        /// Returns information about a uncle of a block by number and uncle index position.
        /// </summary>
        /// <param name="defaultBlock">a block number, or the string "earliest", "latest" or "pending"</param>
        /// <param name="index">the uncle's index position</param>
        /// <returns>The uncle block</returns>
        public async Task<EthBlock> EthGetUncleByBlockNumberAndIndexAsync(DefaultBlock defaultBlock, BigInteger index)
        {
            RpcRequest request = BuildRpcRequest("eth_getUncleByBlockNumberAndIndex", defaultBlock, index);
            RpcResponse<EthBlock> response = await PostRpcRequestAsync<EthBlock>(request);
            return response.Result;
        }

        /// <summary>
        /// Returns a list of available compilers in the client.
        /// </summary>
        /// <returns>Available compilers</returns>
        public async Task<IList<string>> EthGetCompilersAsync()
        {
            RpcRequest request = BuildRpcRequest("eth_getCompilers");
            RpcResponse<IList<string>> response = await PostRpcRequestAsync<IList<string>>(request);

            if (response.Result == null)
            {
                return Enumerable.Empty<string>().ToList();
            }

            return response.Result;
        }

        /// <summary>
        /// Returns compiled LLL code
        /// </summary>
        /// <param name="sourceCode">The source code</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if sourceCode is an empty string</exception>
        /// <returns>The compiled source code</returns>
        public async Task<byte[]> EthCompileLllAsync(string sourceCode)
        {
            Ensure.EnsureStringIsNotNullOrEmpty(sourceCode, "sourceCode");

            RpcRequest request = BuildRpcRequest("eth_compileLLL", sourceCode);
            RpcResponse<byte[]> response = await PostRpcRequestAsync<byte[]>(request);
            return response.Result;
        }

        /// <summary>
        /// Returns compiled solidity code.
        /// </summary>
        /// <param name="sourceCode">The source code.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if sourceCode is an empty string</exception>
        /// <returns>The compiled source code</returns>
        public async Task<IList<EthSolidityContract>> EthCompileSolidityAsync(string sourceCode)
        {
            Ensure.EnsureStringIsNotNullOrEmpty(sourceCode, "sourceCode");

            RpcRequest request = BuildRpcRequest("eth_compileSolidity", sourceCode);
            RpcResponse<IList<EthSolidityContract>> response = await PostRpcRequestAsync<IList<EthSolidityContract>>(request);
            return response.Result;
        }

        /// <summary>
        /// Returns compiled Serpent code
        /// </summary>
        /// <param name="sourceCode">The source code</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if sourceCode is an empty string</exception>
        /// <returns>The compiled source code</returns>
        public async Task<byte[]> EthCompileSerpentAsync(string sourceCode)
        {
            Ensure.EnsureStringIsNotNullOrEmpty(sourceCode, "sourceCode");

            RpcRequest request = BuildRpcRequest("eth_compileSerpent", sourceCode);
            RpcResponse<byte[]> response = await PostRpcRequestAsync<byte[]>(request);
            return response.Result;
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

            RpcRequest request = BuildRpcRequest("eth_newFilter", filterOptions);
            RpcResponse<BigInteger> response = await PostRpcRequestAsync<BigInteger>(request);
            return response.Result;
        }

        /// <summary>
        /// Creates a filter in the node, to notify when a new block arrives. To check if the state has changed, call EthGetFilterChangesAsync().
        /// </summary>
        /// <returns>A filter id</returns>
        public async Task<BigInteger> EthNewBlockFilterAsync()
        {
            RpcRequest request = BuildRpcRequest("eth_newBlockFilter");
            RpcResponse<BigInteger> rpcResponse = await PostRpcRequestAsync<BigInteger>(request);
            return rpcResponse.Result;
        }

        /// <summary>
        /// Creates a filter in the node, to notify when new pending transactions arrive. To check if the state has changed, call EthGetFilterChangesAsync().
        /// </summary>
        /// <returns>A filter id</returns>
        public async Task<BigInteger> EthNewPendingTransactionFilterAsync()
        {
            RpcRequest request = BuildRpcRequest("eth_newPendingTransactionFilter");
            RpcResponse<BigInteger> rpcResponse = await PostRpcRequestAsync<BigInteger>(request);
            return rpcResponse.Result;
        }

        /// <summary>
        /// Uninstalls a filter with given id. Should always be called when watch is no longer needed.
        /// Additonally Filters timeout when they aren't requested with eth_getFilterChanges for a period of time.
        /// </summary>
        /// <param name="filterId">The filter id</param>
        /// <returns>true if the filter was successfully uninstalled, otherwise false</returns>
        public async Task<bool> EthUninstallFilterAsync(BigInteger filterId)
        {
            RpcRequest request = BuildRpcRequest("eth_uninstallFilter", filterId);
            RpcResponse<bool> rpcResponse = await PostRpcRequestAsync<bool>(request);
            return rpcResponse.Result;
        }

        /// <summary>
        /// Polling method for a filter, which returns an array of logs which occurred since last poll.
        /// </summary>
        /// <param name="filterId"> the filter id.</param>
        /// <returns>Array of log objects, or an empty array if nothing has changed since last poll.</returns>
        public async Task<EthLog> EthGetFilterChangesAsync(BigInteger filterId)
        {
            RpcRequest request = BuildRpcRequest("eth_getFilterChanges", filterId);
            RpcResponse<EthLog> rpcResponse = await PostRpcRequestAsync<EthLog>(request);
            return rpcResponse.Result;
        }

        /// <summary>
        /// Returns an array of all logs matching a given filter object.
        /// </summary>
        /// <param name="filterOptions">the filter object</param>
        /// <returns>Array of log objects, or an empty array if nothing has changed since last poll</returns>
        public async Task<IList<EthLog>> EthGetLogsAsync(EthFilterOptions filterOptions)
        {
            RpcRequest request = BuildRpcRequest("eth_getLogs", filterOptions);
            RpcResponse<IList<EthLog>> rpcResponse = await PostRpcRequestAsync<IList<EthLog>>(request);
            return rpcResponse.Result;
        }

        /// <summary>
        /// Returns the hash of the current block, the seedHash, and the boundary condition to be met 
        /// </summary>
        /// <returns>EthWork with current block hash, seed hash, and boundary condition</returns>
        public async Task<EthWork> EthGetWorkAsync()
        {
            RpcRequest request = BuildRpcRequest("eth_getWork");
            RpcResponse<EthWork> rpcResponse = await PostRpcRequestAsync<EthWork>(request);
            return rpcResponse.Result;
        }

        /// <summary>
        /// Used for submitting a proof-of-work solution.
        /// </summary>
        /// <param name="proofOfWork">The proof-of-work solution</param>
        /// <returns>returns true if the provided solution is valid, otherwise false</returns>
        public async Task<bool> EthSubmitWorkAsync(EthProofOfWork proofOfWork)
        {
            RpcRequest request = BuildRpcRequest("eth_submitWork", proofOfWork);
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

            RpcRequest request = BuildRpcRequest("eth_submitHashrate", hashRate, clientID);
            RpcResponse<bool> response = await PostRpcRequestAsync<bool>(request);

            return response.Result;
        }

        /// <summary>
        /// Stores a string in the local database
        /// </summary>
        /// <param name="dbName">Database name</param>
        /// <param name="keyName">Key name</param>
        /// <param name="value">String to store</param>
        /// <returns>returns true if the value was stored, otherwise false</returns>
        [Obsolete("This function is deprecated and will be removed in the future")]
        public async Task<bool> DbPutStringAsync(string dbName, string keyName, string value)
        {
            RpcRequest request = BuildRpcRequest("db_putString", dbName, keyName, value);
            RpcResponse<bool> response = await PostRpcRequestAsync<bool>(request);
            return response.Result;
        }

        /// <summary>
        /// Returns string from the local database.
        /// </summary>
        /// <param name="dbName">Database name</param>
        /// <param name="keyName">Key name</param>
        /// <returns>The previously stored string</returns>
        [Obsolete("This function is deprecated and will be removed in the future")]
        public async Task<string> DbGetStringAsync(string dbName, string keyName)
        {
            RpcRequest request = BuildRpcRequest("db_getString", dbName, keyName);
            RpcResponse<string> response = await PostRpcRequestAsync<string>(request);
            return response.Result;
        }

        /// <summary>
        /// Stores binary data in the local database.
        /// </summary>
        /// <param name="dbName">Database name</param>
        /// <param name="keyName">Key name</param>
        /// <param name="data">The data to store</param>
        /// <returns>returns true if the value was stored, otherwise false</returns>
        [Obsolete("This function is deprecated and will be removed in the future")]
        public async Task<bool> DbPutHexAsync(string dbName, string keyName, byte[] data)
        {
            RpcRequest request = BuildRpcRequest("db_putHex", dbName, keyName, data);
            RpcResponse<bool> response = await PostRpcRequestAsync<bool>(request);
            return response.Result;
        }

        /// <summary>
        /// Returns binary data from the local database.
        /// </summary>
        /// <param name="dbName">Database name</param>
        /// <param name="keyName">Key name</param>
        /// <returns>The previously stored data</returns>
        [Obsolete("This function is deprecated and will be removed in the future")]
        public async Task<byte[]> DbGetHexAsync(string dbName, string keyName)
        {
            RpcRequest request = BuildRpcRequest("db_getHex", dbName, keyName);
            RpcResponse<byte[]> response = await PostRpcRequestAsync<byte[]>(request);
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
            var request = BuildRpcRequest("personal_newAccount", password);
            RpcResponse<byte[]> response = await PostRpcRequestAsync<byte[]>(request);
            return response.Result;
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
            var request = BuildRpcRequest("personal_unlockAccount", address, password, (int)duration.TotalSeconds);
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
        /// Returns the current whisper protocol version.
        /// </summary>
        /// <returns>The current whisper protocol version</returns>
        public async Task<string> ShhVersionAsync()
        {
            RpcRequest request = BuildRpcRequest("shh_version");
            RpcResponse<string> response = await PostRpcRequestAsync<string>(request);
            return response.Result;
        }

        /// <summary>
        /// Sends a whisper message.
        /// </summary>
        /// <param name="post">The whisper post object</param>
        /// <returns>returns true if the message was send, otherwise false.</returns>
        public async Task<bool> ShhPostAsync(ShhPost post)
        {
            RpcRequest request = BuildRpcRequest("shh_version", post);
            RpcResponse<bool> response = await PostRpcRequestAsync<bool>(request);
            return response.Result;
        }

        /// <summary>
        /// Creates new whisper identity in the client.
        /// </summary>
        /// <returns>60 Bytes - the address of the new identiy.</returns>
        public async Task<byte[]> ShhNewIdentityAsync()
        {
            RpcRequest request = BuildRpcRequest("shh_newIdentity");
            RpcResponse<byte[]> response = await PostRpcRequestAsync<byte[]>(request);
            return response.Result;
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

            RpcRequest request = BuildRpcRequest("shh_hasIdentity", address);
            RpcResponse<bool> response = await PostRpcRequestAsync<bool>(request);
            return response.Result;
        }

        /// <summary>
        /// ?
        /// </summary>
        /// <returns>60 Bytes - the address of the new group</returns>
        public async Task<byte[]> ShhNewGroupAsync()
        {
            RpcRequest request = BuildRpcRequest("shh_newGroup");
            RpcResponse<byte[]> response = await PostRpcRequestAsync<byte[]>(request);
            return response.Result;
        }

        /// <summary>
        /// ?
        /// </summary>
        /// <param name="address">60 Bytes - The identity address to add to a group</param>
        /// <returns>returns true if the identity was successfully added to the group, otherwise false</returns>
        public async Task<bool> ShhAddToGroupAsync(byte[] address)
        {
            RpcRequest request = BuildRpcRequest("shh_addToGroup", address);
            RpcResponse<bool> response = await PostRpcRequestAsync<bool>(request);
            return response.Result;
        }

        /// <summary>
        /// Creates filter to notify, when client receives whisper message matching the filter options.
        /// </summary>
        /// <param name="options">The filter options:</param>
        /// <returns>The newly created filter.</returns>
        public async Task<BigInteger> ShhNewFilterAsync(ShhFilterOptions options)
        {
            RpcRequest request = BuildRpcRequest("shh_newFilter", options);
            RpcResponse<BigInteger> response = await PostRpcRequestAsync<BigInteger>(request);
            return response.Result;
        }

        /// <summary>
        /// Uninstalls a filter with given id. Should always be called when watch is no longer needed.
        /// Additonally Filters timeout when they aren't requested with shh_getFilterChanges for a period of time.
        /// </summary>
        /// <param name="filterId">The filter id.</param>
        /// <returns>true if the filter was successfully uninstalled, otherwise false.</returns>
        public async Task<bool> ShhUninstallFilterAsync(BigInteger filterId)
        {
            RpcRequest request = BuildRpcRequest("shh_uninstallFilter", filterId);
            RpcResponse<bool> response = await PostRpcRequestAsync<bool>(request);
            return response.Result;
        }

        /// <summary>
        /// Polling method for whisper filters. Returns new messages since the last call of this method.
        /// 
        /// Note calling the shh_getMessages method, will reset the buffer for this method, so that you won't receive duplicate messages.
        /// </summary>
        /// <param name="filterId">The filter id.</param>
        /// <returns>Array of messages received since last poll</returns>
        public async Task<IList<ShhMessage>> ShhGetFilterChangesAsync(BigInteger filterId)
        {
            RpcRequest request = BuildRpcRequest("shh_getFilterChanges", filterId);
            RpcResponse<IList<ShhMessage>> response = await PostRpcRequestAsync<IList<ShhMessage>>(request);
            return response.Result;
        }

        /// <summary>
        /// Get all messages matching a filter. Unlike shh_getFilterChanges this returns all messages.
        /// </summary>
        /// <param name="filterId">The filter id.</param>
        /// <returns>Array of messages</returns>
        public async Task<IList<ShhMessage>> ShhGetMessages(BigInteger filterId)
        {
            RpcRequest request = BuildRpcRequest("shh_getMessages", filterId);
            RpcResponse<IList<ShhMessage>> response = await PostRpcRequestAsync<IList<ShhMessage>>(request);
            return response.Result;
        }
    }
}

