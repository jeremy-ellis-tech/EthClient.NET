using Eth.Abi;
using Eth.Rpc;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace Eth
{
    public class EthContract
    {
        private readonly byte[] _address;
        private readonly BaseClient _client;
        private readonly IContractCallEncoder _encoder;

        internal EthContract(byte[] address, BaseClient client, IContractCallEncoder encoder)
        {
            _address = address;
            _client = client;
            _encoder = encoder;
        }

        /// <summary>
        /// Call a contract's function without sending a transaction
        /// This function must be declared <code>constant</code>
        /// and cannot change the contract's state
        /// </summary>
        /// <param name="functionName">The name of the function</param>
        /// <param name="parameters">An enumerable of abi values for function parameters</param>
        /// <param name="returns">An enumerable of return values. This will be populated with decoded values</param>
        /// <returns></returns>
        public async Task CallAsync(string functionName, IEnumerable<IAbiValue> parameters, IEnumerable<IAbiValue> returns)
        {
            EthCall call = new EthCall
            {
                To = _address,
                Data = _encoder.Encode(functionName, parameters.ToArray())
            };

            byte[] data = await _client.EthCallAsync(call, DefaultBlock.Latest);

            _encoder.Decode(data, returns.ToArray());
        }

        /// <summary>
        /// Call a contract's function, which has one return value,
        /// without sending a transaction
        /// This function must be declared <code>constant</code>
        /// and cannot change the contract's state
        /// </summary>
        /// <typeparam name="T">The expected AbiValue return type</typeparam>
        /// <param name="functionName">The function name</param>
        /// <param name="parameters">The function parameters</param>
        /// <returns>The return value of the function, wrapped in the appropriate IAbiValue container</returns>
        public async Task<T> CallAsync<T>(string functionName, params IAbiValue[] parameters) where T : class, IAbiValue, new()
        {
            List<IAbiValue> returns = new List<IAbiValue> { new T() };
            await CallAsync(functionName, parameters, returns);
            return returns[0] as T;
        }

        /// <summary>
        /// Call a contract's function with a transaction.
        /// </summary>
        /// <param name="functionName">The name of the function</param>
        /// <param name="parameters">The function parameters</param>
        /// <param name="from">The account address to send the transaction from (must be unlocked before sending)</param>
        /// <param name="gas">gas limit (optional)</param>
        /// <param name="gasPrice">gas price (optional)</param>
        /// <param name="value">value (optional)</param>
        public async Task<EthTransactionReceipt> TransactAsync(string functionName, IEnumerable<IAbiValue> parameters, byte[] from, BigInteger? gas = null, BigInteger? gasPrice = null, BigInteger? value = null)
        {
            EthTransaction transaction = new EthTransaction
            {
                To = _address,
                From = from,
                Gas = gas,
                GasPrice = gasPrice,
                Value = value,
                Data = _encoder.Encode(functionName, parameters.ToArray()),
            };

            byte[] transactionHash = await _client.EthSendTransactionAsync(transaction);

            EthTransactionReceipt receipt = null;

            while (receipt == null)
            {
                receipt = await _client.EthGetTransactionReceiptAsync(transactionHash);
            }

            return receipt;
        }
    }
}
