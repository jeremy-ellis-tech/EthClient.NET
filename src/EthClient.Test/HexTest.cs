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
        public void ShouldThrowOnNullParameter()
        {
            EthHex.ToHexString(null);
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
        public void ShouldConvertQuantitiesCorrectly()
        {
            BigInteger quantity = 65;
            string expectedValue = "0x41";
            Assert.IsTrue(Equals(expectedValue, EthHex.ToHexString(quantity)));

            quantity = 1024;
            expectedValue = "0x400";
            Assert.IsTrue(Equals(expectedValue, EthHex.ToHexString(quantity)));

            quantity = BigInteger.Zero;
            expectedValue = "0x0";
            Assert.IsTrue(Equals(expectedValue, EthHex.ToHexString(quantity)));
        }

        [TestMethod]
        public void ShouldConvertDataCorrectly()
        {
            byte[] data = new[] { (byte)0x00, (byte)0x00, (byte)0x00, (byte)0x00 };
            string expectedValue = "0x00000000";

            Assert.IsTrue(Equals(EthHex.ToHexString(data), expectedValue));

            data = new[] { (byte)0x00, (byte)0x00, (byte)0x00, (byte)0x01 };
            expectedValue = "0x00000001";

            Assert.IsTrue(Equals(EthHex.ToHexString(data), expectedValue));
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
