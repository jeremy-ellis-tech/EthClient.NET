using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;
using Eth.Utilities;
using System.Linq;
using System.Collections.Generic;

namespace EthClient.Test
{
    [TestClass]
    public class EthCurrencyConverterTest
    {
        [TestMethod]
        public void ShouldConvertFromWeiCorrectly()
        {
            BigInteger value = 42;

            decimal expected = 0.000000000042M;
            decimal actual = EthCurrencyConverter.FromWei(value, EthCurrencyUnit.Szabo);
            Assert.IsTrue(Equals(expected, actual));

            expected = 0.000000000000042M;
            actual = EthCurrencyConverter.FromWei(value, EthCurrencyUnit.Finney);
            Assert.IsTrue(Equals(expected, actual));

            expected = 0.000000000000000042M;
            actual = EthCurrencyConverter.FromWei(value, EthCurrencyUnit.Ether);
            Assert.IsTrue(Equals(expected, actual));
        }

        [TestMethod]
        public void ShouldConvertToWeiCorrectly()
        {
            decimal value = 42;

            BigInteger expected = BigInteger.Parse("42000000000000000000");
            BigInteger actual = EthCurrencyConverter.ToWei(value, EthCurrencyUnit.Ether);
            Assert.IsTrue(Equals(expected, actual));
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void ShouldThrowIfOverflow()
        {
            EthCurrencyConverter.ToWei(100000000000000000.0M, EthCurrencyUnit.Ether);
        }
    }
}
