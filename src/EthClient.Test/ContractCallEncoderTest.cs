using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Eth.Abi;
using Eth.Utilities;
using System.Linq;

namespace EthClient.Test
{
    [TestClass]
    public class ContractCallEncoderTest
    {
        private readonly IContractCallEncoder _encoder;

        public ContractCallEncoderTest()
        {
            _encoder = new ContractCallEncoder();
        }

        [TestMethod]
        public void ShouldEncodeCallCorrectly()
        {
            //Expected value taken from https://github.com/ethereum/wiki/wiki/Ethereum-Contract-ABI#examples
            byte[] expected = EthHex.HexStringToByteArray("0xcdcd77c000000000000000000000000000000000000000000000000000000000000000450000000000000000000000000000000000000000000000000000000000000001");
            byte[] actual = _encoder.Encode("baz", new UInt32AbiValue(69), new BoolAbiValue(true));

            Assert.IsTrue(actual.SequenceEqual(expected));
        }
    }
}
