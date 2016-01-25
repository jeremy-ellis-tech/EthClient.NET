using System;
using System.Collections.Generic;
using System.Numerics;

namespace Eth
{
    public class EthBlock
    {
        /// <summary>
        /// the block number. null when its pending block.
        /// </summary>
        public BigInteger? Number { get; set; }

        /// <summary>
        /// 32 Bytes - hash of the block. null when its pending block.
        /// </summary>
        public byte[] Hash { get; set; }

        /// <summary>
        /// 32 Bytes - hash of the parent block.
        /// </summary>
        public byte[] ParentHash { get; set; }

        /// <summary>
        /// 8 Bytes - hash of the generated proof-of-work. null when its pending block.
        /// </summary>
        public byte[] Nonce { get; set; }

        /// <summary>
        /// 32 Bytes - SHA3 of the uncles data in the block.
        /// </summary>
        public byte[] Sha3Uncles { get; set; }

        /// <summary>
        /// 256 Bytes - the bloom filter for the logs of the block. null when its pending block.
        /// </summary>
        public byte[] LogsBloom { get; set; }

        /// <summary>
        /// 32 Bytes - the root of the transaction trie of the block.
        /// </summary>
        public byte[] TransactionRoot { get; set; }

        /// <summary>
        /// 32 Bytes - the root of the final state trie of the block.
        /// </summary>
        public byte[] StateRoot { get; set; }

        /// <summary>
        /// 32 Bytes - the root of the receipts trie of the block.
        /// </summary>
        public byte[] ReceiptsRoot { get; set; }

        /// <summary>
        /// 20 Bytes - the address of the beneficiary to whom the mining rewards were given.
        /// </summary>
        public byte[] Miner { get; set; }

        /// <summary>
        /// integer of the difficulty for this block.
        /// </summary>
        public BigInteger Difficulty { get; set; }

        /// <summary>
        /// integer of the total difficulty of the chain until this block.
        /// </summary>
        public BigInteger TotalDifficulty { get; set; }

        /// <summary>
        /// the "extra data" field of this block.
        /// </summary>
        public byte[] ExtraData { get; set; }

        /// <summary>
        /// integer the size of this block in bytes.
        /// </summary>
        public BigInteger Size { get; set; }

        /// <summary>
        ///  the maximum gas allowed in this block.
        /// </summary>
        public BigInteger GasLimit { get; set; }

        /// <summary>
        /// the total used gas by all transactions in this block.
        /// </summary>
        public BigInteger GasUsed { get; set; }

        /// <summary>
        /// the unix timestamp for when the block was collated.
        /// </summary>
        public DateTimeOffset TimeStamp { get; set; }

        /// <summary>
        /// Array of transaction objects, or 32 Bytes transaction hashes depending on the last given parameter.
        /// </summary>
        public IEnumerable<object> Transactions { get; set; }

        /// <summary>
        /// Array of uncle hashes.
        /// </summary>
        public IEnumerable<byte[]> Unlces { get; set; }
    }
}
