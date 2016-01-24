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
            EthHex.ByteArrayToHexString(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowIfNull()
        {
            EthHex.HexStringToByteArray(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ShouldThrowIfEmpty()
        {
            EthHex.HexStringToByteArray(String.Empty);
        }

        [TestMethod]
        public void ShouldHandlePaddingForConvertingToInts()
        {
            string sTwo = "0x2";
            int iTwo = (int)EthHex.HexStringToInt(sTwo);
            Assert.IsTrue(Equals(iTwo, 2));

            sTwo = "0x02";
            iTwo = (int)EthHex.HexStringToInt(sTwo);
            Assert.IsTrue(Equals(iTwo, 2));
        }

        [TestMethod]
        public void HexConverterShouldConvertZeroCorrectly()
        {
            string sZero = EthHex.IntToHexString(BigInteger.Zero);
            Assert.IsTrue(Equals(sZero, "0x00"));

            BigInteger bZero = EthHex.HexStringToInt("0x0");
            Assert.IsTrue(Equals(BigInteger.Zero, bZero));

            bZero = EthHex.HexStringToInt("0x00");
            Assert.IsTrue(Equals(BigInteger.Zero, bZero));

            bZero = EthHex.HexStringToInt("0");
            Assert.IsTrue(Equals(BigInteger.Zero, bZero));

            bZero = EthHex.HexStringToInt("0x0000000000000000000");
            Assert.IsTrue(Equals(BigInteger.Zero, bZero));

            int iZero = (int)EthHex.HexStringToInt("0x0");
            Assert.IsTrue(Equals(0, iZero));

            iZero = (int)EthHex.HexStringToInt("0x00");
            Assert.IsTrue(Equals(0, iZero));

            iZero = (int)EthHex.HexStringToInt("0x000000000000000000000");
            Assert.IsTrue(Equals(0, iZero));
        }

        [TestMethod]
        public void TwoDifferentStringShouldNotHaveEqualValuesOrQuantities()
        {
            string hex0 = "0x123";
            string hex1 = "0x321";

            BigInteger quantity0 = EthHex.HexStringToInt(hex0);
            BigInteger quantity1 = EthHex.HexStringToInt(hex1);

            byte[] value0 = EthHex.HexStringToByteArray(hex0);
            byte[] value1 = EthHex.HexStringToByteArray(hex1);

            Assert.IsFalse(Equals(quantity0, quantity1));
            Assert.IsFalse(Equals(value0, value1));
        }
    }
}
