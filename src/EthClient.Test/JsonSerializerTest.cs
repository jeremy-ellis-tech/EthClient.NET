using Eth;
using Eth.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using EthSyncing = Eth.EthSyncing;

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
        public void EthSyncingShouldBeSerializedCorrectly()
        {
            EthSyncing ethSyncing = new EthSyncing(false);
            string expected = "false";
            string actual = _serializer.Serialize(ethSyncing);
            Assert.IsTrue(Equals(expected, actual));

            ethSyncing = new EthSyncing(1, 2, 3);
            expected = "\"startingBlock\":\"0x1\",\"currentBlock\":\"0x2\",\"highestBlock\":\"0x3\"";
            actual = _serializer.Serialize(ethSyncing);
            Assert.IsTrue(Equals(expected, actual));
        }

        [TestMethod]
        public void EthSyncingShouldBeDeserializedCorrectly()
        {
            string json = "false";
            EthSyncing expected = new EthSyncing(false);
            EthSyncing actual = _serializer.Deserialize<EthSyncing>(json);
            Assert.IsTrue(Equals(expected, actual));

            //json = "{\"startingBlock\":\"0x1\",\"currentBlock\":\"0x2\",\"highestBlock\":\"0x3\"}";
            //expected = new EthSyncing(1, 2, 3);
            //actual = _serializer.Deserialize<EthSyncing>(json);
            //Assert.IsTrue(Equals(expected, actual));
        }

        [TestMethod]
        public void EthWorkShouldBeDeserializedCorrectly()
        {
            string json = "{\"jsonrpc\":\"2.0\",\"id\":0,\"result\":[\"0x6a992e2ed05b076ce7e285599540bea98a192f6fb95932bad8f5bd3e6924a0b8\",\"0xc6320e3c1c3456002aa6ccc5b60cf5bf054d7e9392712f8b7764869abe630035\",\"0x000029ea0a9508bff47f86989a59532801509fa4fcee45a3a030f9979d63d94a\"]}\n";
            RpcResponse<EthWork> actual = _serializer.Deserialize<RpcResponse<EthWork>>(json);
            EthWork expected = new EthWork { };
            Assert.IsTrue(Equals(expected, actual.Result));
        }
    }
}
