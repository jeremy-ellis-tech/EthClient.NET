using Eth.Abi;
using Eth.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace EthClient.Test
{
    [TestClass]
    public class ContractCallEncoderTest
    {
        private readonly IContractCallEncoder _encoder;
        private readonly IKeccak _keccak;

        public ContractCallEncoderTest()
        {
            _keccak = new Keccak();
            _encoder = new ContractCallEncoder(_keccak);
        }

        [TestMethod]
        public void ShouldEncodeCallCorrectly()
        {
            //Expected values taken from https://github.com/ethereum/wiki/wiki/Ethereum-Contract-ABI#examples

            byte[] expected = EthHex.HexStringToByteArray("0xcdcd77c000000000000000000000000000000000000000000000000000000000000000450000000000000000000000000000000000000000000000000000000000000001");
            byte[] actual = _encoder.Encode("baz", new UInt32AbiValue(69), new BoolAbiValue(true));

            Assert.IsTrue(actual.SequenceEqual(expected));

            expected = EthHex.HexStringToByteArray("0xa5643bf20000000000000000000000000000000000000000000000000000000000000060000000000000000000000000000000000000000000000000000000000000000100000000000000000000000000000000000000000000000000000000000000a0000000000000000000000000000000000000000000000000000000000000000464617665000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000003000000000000000000000000000000000000000000000000000000000000000100000000000000000000000000000000000000000000000000000000000000020000000000000000000000000000000000000000000000000000000000000003");
            actual = _encoder.Encode("sam", new BytesAbiValue("dave"), new BoolAbiValue(true), new UInt256ArrayAbiValue(new BigInteger[] { 1,2,3}));

            Assert.IsTrue(actual.SequenceEqual(expected));
        }

        [TestMethod]
        public void ShouldDecodeCallCorrectly()
        {
            UInt256AbiValue expected = new UInt256AbiValue(42);
            UInt256AbiValue actual = new UInt256AbiValue();
            byte[] data = EthHex.HexStringToByteArray("0x00000000000000000000000000000000000000000000000000000000000002A");
            _encoder.Decode(data, actual);
            Assert.IsTrue(Equals((BigInteger)expected, (BigInteger)actual));
        }
    }
}
