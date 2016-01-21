using Eth.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Numerics;

namespace EthClient.Test
{
    [TestClass]
    public class HexTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void HexConverterShouldThrowOnNullParameter()
        {
            Hex.ByteArrayToHexString(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowIfNull()
        {
            Hex.HexStringToByteArray(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ShouldThrowIfEmpty()
        {
            Hex.HexStringToByteArray(String.Empty);
        }

        [TestMethod]
        public void HexConverterShouldConvertIntCorrectly()
        {
            string sTwo = "0x2";
            int iTwo = (int)Hex.HexStringToInt(sTwo);
            Assert.IsTrue(Equals(iTwo, 2));

            sTwo = "0x02";
            iTwo = (int)Hex.HexStringToInt(sTwo);
            Assert.IsTrue(Equals(iTwo, 2));
        }

        [TestMethod]
        public void HexConverterShouldConvertZeroCorrectly()
        {
            string sZero = Hex.IntToHexString(BigInteger.Zero);
            Assert.IsTrue(Equals(sZero, "0x00"));

            BigInteger bZero = Hex.HexStringToInt("0x0");
            Assert.IsTrue(Equals(BigInteger.Zero, bZero));

            bZero = Hex.HexStringToInt("0x00");
            Assert.IsTrue(Equals(BigInteger.Zero, bZero));

            bZero = Hex.HexStringToInt("0");
            Assert.IsTrue(Equals(BigInteger.Zero, bZero));

            bZero = Hex.HexStringToInt("0x0000000000000000000");
            Assert.IsTrue(Equals(BigInteger.Zero, bZero));

            int iZero = (int)Hex.HexStringToInt("0x0");
            Assert.IsTrue(Equals(0, iZero));

            iZero = (int)Hex.HexStringToInt("0x00");
            Assert.IsTrue(Equals(0, iZero));

            iZero = (int)Hex.HexStringToInt("0x000000000000000000000");
            Assert.IsTrue(Equals(0, iZero));
        }
    }
}
