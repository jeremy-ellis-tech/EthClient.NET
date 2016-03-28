using Eth;
using Eth.Json;
using Eth.Rpc;
using Eth.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace EthClient.Test
{
    [TestClass]
    public class JsonSerializerTest
    {
        private readonly IJsonSerializer _serializer;

        public JsonSerializerTest()
        {
            _serializer = new JsonSerializer();
        }

        [TestMethod]
        public void ByteArrayShouldBeSerializedCorrectly()
        {
            byte[] TestAddress = new byte[] { 0xcd, 0x2a, 0x3d, 0x9f, 0x93, 0x8e, 0x13, 0xcd, 0x94, 0x7e, 0xc0, 0x5a, 0xbc, 0x7f, 0xe7, 0x34, 0xdf, 0x8d, 0xd8, 0x26 };
            string actual = _serializer.Serialize(TestAddress).ToUpperInvariant();
            string expected = "\"0XCD2A3D9F938E13CD947EC05ABC7FE734DF8DD826\"";
            Assert.IsTrue(String.Equals(expected, actual));
        }

        [TestMethod]
        public void ByteArrayShouldBeDeserializedCorrectly()
        {
            string json = "\"0XCD2A3D9F938E13CD947EC05ABC7FE734DF8DD826\"";
            byte[] actual = _serializer.Deserialize<byte[]>(json);
            byte[] expected = new byte[] { 0xcd, 0x2a, 0x3d, 0x9f, 0x93, 0x8e, 0x13, 0xcd, 0x94, 0x7e, 0xc0, 0x5a, 0xbc, 0x7f, 0xe7, 0x34, 0xdf, 0x8d, 0xd8, 0x26 };
            Assert.IsTrue(Equals(expected.Length, actual.Length) && expected.SequenceEqual(actual));
        }

        [TestMethod]
        public void IEnumerableByteArrayShouldBeSerializedCorrectly()
        {
            IEnumerable<byte[]> test = new List<byte[]> { new byte[] { 0x0 }, new byte[] { 0x1 }, new byte[] { 0x2 }, new byte[] { 0x3 } };
            string expected = "[\"0x00\",\"0x01\",\"0x02\",\"0x03\"]";
            string actual = _serializer.Serialize(test);
            Assert.IsTrue(String.Equals(expected, actual));
        }

        [TestMethod]
        public void IEnumerableByteArrayShouldBeDeserializedCorrectly()
        {
            string json = "[\"0x0\",\"0x1\",\"0x2\",\"0x3\"]";
            IList<byte[]> expected = new List<byte[]> { new byte[] { 0x0 }, new byte[] { 0x1 }, new byte[] { 0x2 }, new byte[] { 0x3 } };
            IList<byte[]> actual = _serializer.Deserialize<IList<byte[]>>(json);

            Assert.IsTrue(Equals(expected.Count, actual.Count));

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.IsTrue(Equals(expected[i].Length, actual[i].Length) && expected[i].SequenceEqual(actual[i]));
            }
        }

        [TestMethod]
        public void BigIntegerShouldBeSerializedCorrectly()
        {
            BigInteger bi = new BigInteger(420);
            string actual = _serializer.Serialize(bi).ToUpperInvariant();
            string expected = "\"0X1A4\"";
            Assert.IsTrue(String.Equals(expected, actual));
        }

        [TestMethod]
        public void BigIntegerShouldBeDeserializedCorrectly()
        {
            BigInteger expected = new BigInteger(420);
            string json = "\"0x1A4\"";
            BigInteger actual = _serializer.Deserialize<BigInteger>(json);
            Assert.IsTrue(Equals(expected, actual));
        }

        [TestMethod]
        public void NullableBigIntegerShouldBeSerializedCorrectly()
        {
            BigInteger? bi = new BigInteger(420);
            string actual = _serializer.Serialize(bi).ToUpperInvariant();
            string expected = "\"0X1A4\"";
            Assert.IsTrue(String.Equals(expected, actual));

            bi = null;
            actual = _serializer.Serialize(bi);
            expected = "null";
            Assert.IsTrue(String.Equals(expected, actual));
        }

        [TestMethod]
        public void NullableBigIntegerShouldBeDeserializedCorrectly()
        {
            BigInteger? expected = new BigInteger(420);
            string json = "\"0x1A4\"";
            BigInteger? actual = _serializer.Deserialize<BigInteger?>(json);
            Assert.IsTrue(Equals(expected, actual));

            expected = null;
            json = "null";
            actual = _serializer.Deserialize<BigInteger?>(json);
            Assert.IsTrue(Equals(expected, actual));
        }

        [TestMethod]
        public void DefaultBlockShouldBeSerializedCorrectly()
        {
            DefaultBlock db = DefaultBlock.Latest;
            string actual = _serializer.Serialize(db);
            string expected = "\"latest\"";
            Assert.IsTrue(String.Equals(expected, actual));

            db = DefaultBlock.Earliest;
            actual = _serializer.Serialize(db);
            expected = "\"earliest\"";
            Assert.IsTrue(String.Equals(expected, actual));

            db = DefaultBlock.Pending;
            actual = _serializer.Serialize(db);
            expected = "\"pending\"";
            Assert.IsTrue(String.Equals(expected, actual));

            db = new DefaultBlock(420);
            actual = _serializer.Serialize(db).ToUpperInvariant();
            expected = "\"0X1A4\"";
            Assert.IsTrue(String.Equals(expected, actual));
        }

        [TestMethod]
        public void DefaultBlockShouldBeDeserializedCorrectly()
        {
            string json = "\"latest\"";
            DefaultBlock actual = _serializer.Deserialize<DefaultBlock>(json);
            DefaultBlock expected = DefaultBlock.Latest;
            Assert.IsTrue(Equals(expected, actual));

            json = "\"earliest\"";
            actual = _serializer.Deserialize<DefaultBlock>(json);
            expected = DefaultBlock.Earliest;
            Assert.IsTrue(Equals(expected, actual));

            json = "\"pending\"";
            actual = _serializer.Deserialize<DefaultBlock>(json);
            expected = DefaultBlock.Pending;
            Assert.IsTrue(Equals(expected, actual));

            json = "\"0X1A4\"";
            actual = _serializer.Deserialize<DefaultBlock>(json);
            expected = new DefaultBlock(420);
            Assert.IsTrue(Equals(expected, actual));
        }

        [TestMethod]
        public void EthSyncingShouldBeDeserializedCorrectly()
        {
            string json = "false";
            EthSyncing expected = new EthSyncing(false);
            EthSyncing actual = _serializer.Deserialize<EthSyncing>(json);
            Assert.IsTrue(Equals(expected, actual));

            json = "{\"startingBlock\":\"0x1\",\"currentBlock\":\"0x2\",\"highestBlock\":\"0x3\"}";
            expected = new EthSyncing(1, 2, 3);
            actual = _serializer.Deserialize<EthSyncing>(json);
            Assert.IsTrue(Equals(expected, actual));
        }

        [TestMethod]
        public void EthTopicShouldBeSerializedCorrectly()
        {
            EthTopic topic0 = new EthTopic(EthHex.HexStringToByteArray("0x000000000000000000000000aff3454fce5edbc8cca8697c15331677e6ebccc"));
            EthTopic topic1 = new EthTopic(EthHex.HexStringToByteArray("0x000000000000000000000000a94f5374fce5edbc8e2a8697c15331677e6ebf0b"));
            EthTopic topic3 = new EthTopic(topic1, topic0);
            EthTopic topic4 = new EthTopic();
            EthTopic topic5 = new EthTopic(EthHex.HexStringToByteArray("0x000000000000000000000000a94f5374fce5edbc8e2a8697c15331677e6ebf0b"));
            EthTopic topic6 = new EthTopic(topic5, topic4, topic3);

            string expected = "[\"0x000000000000000000000000a94f5374fce5edbc8e2a8697c15331677e6ebf0b\",null,[\"0x000000000000000000000000a94f5374fce5edbc8e2a8697c15331677e6ebf0b\",\"0x0000000000000000000000000aff3454fce5edbc8cca8697c15331677e6ebccc\"]]".ToUpperInvariant();
            string actual = _serializer.Serialize(topic6).ToUpperInvariant();

            Assert.IsTrue(String.Equals(expected, actual));
        }
    }
}
