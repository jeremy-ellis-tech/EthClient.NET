using Eth.Abi;
using Eth.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text;

namespace EthClient.Test
{
    [TestClass]
    public class KeccakTest
    {
        [TestMethod]
        public void ShouldReturnCorrectHash()
        {
            KeccakDigest kd = new KeccakDigest();
            byte[] input = Encoding.UTF8.GetBytes("Hello world");
            kd.BlockUpdate(input, 0, input.Length);
            byte[] actual = new byte[kd.GetDigestSize()];
            kd.DoFinal(actual, 0);

            //Expected value taken from geth console: '> web3.sha3("Hello world")'
            byte[] expected = EthHex.HexStringToByteArray("ed6c11b0b5b808960df26f5bfc471d04c1995b0ffd2055925ad1be28d6baadfd");
            Assert.IsTrue(actual.SequenceEqual(expected));

            kd = new KeccakDigest();
            input = Encoding.UTF8.GetBytes("Ethereum");
            kd.BlockUpdate(input, 0, input.Length);
            actual = new byte[kd.GetDigestSize()];
            kd.DoFinal(actual, 0);

            //Expected value taken from geth console: '> web3.sha3("Ethereum")'
            expected = EthHex.HexStringToByteArray("564ccaf7594d66b1eaaea24fe01f0585bf52ee70852af4eac0cc4b04711cd0e2");

            Assert.IsTrue(actual.SequenceEqual(expected));
        }
    }
}
