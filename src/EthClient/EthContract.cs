using Eth.Abi;
using Eth.Rpc;
using System;
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
        /// Call a contract's function without making a trasaction.
        /// The function must be declared as <code>constant</code>
        /// and can therefore not change the contract's state.
        /// </summary>
        /// <param name="functionName">The function name</param>
        /// <param name="parameters">The function parameters</param>
        /// <returns>The return value of the function call</returns>
        public async Task<IEnumerable<IAbiValue>> CallAsync(string functionName, IEnumerable<IAbiValue> parameters, IEnumerable<AbiReturnType> returnTypes)
        {
            EthCall call = new EthCall
            {
                To = _address,
                Data = _encoder.Encode(functionName, parameters.ToArray())
            };

            byte[] data = await _client.EthCallAsync(call, DefaultBlock.Latest);

            return _encoder.Decode(data, returnTypes.ToArray());
        }

        /// <summary>
        /// Transact with a contract's function.
        /// </summary>
        /// <param name="functionName">The function name that changes the contract's state</param>
        /// <param name="parameters">The function parameters</param>
        /// <param name="from">The account address to send the transaction from</param>
        /// <param name="gas">The gas limit (optional)</param>
        /// <param name="gasPrice">The gas price (optional)</param>
        /// <param name="value">The value to send with the transaction (optional)</param>
        /// <returns>The return value of the fuction</returns>
        public async Task<byte[]> TransactAsync(string functionName, IEnumerable<IAbiValue> parameters, byte[] from, BigInteger? gas = null, BigInteger? gasPrice = null, BigInteger? value = null)
        {
            throw new NotImplementedException();

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

            return transactionHash; //TODO
        }
    }
}
