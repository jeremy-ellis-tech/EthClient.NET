using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eth.RpcTypes
{
    public class ProofOfWork
    {
        public static readonly int NonceLength = 8; //bytes
        public static readonly int HeaderPowHashLength = 32; //bytes
        public static readonly int MixDigestLenght = 32; //bytes

        public ProofOfWork(byte[] nonce, byte[] headerPowHash, byte[] mixDigest)
        {
            if(nonce.Length != NonceLength)
            {
                throw new ArgumentOutOfRangeException("nonce");
            }

            if (headerPowHash.Length != HeaderPowHashLength)
            {
                throw new ArgumentOutOfRangeException("headerPowHash");
            }

            if (mixDigest.Length != MixDigestLenght)
            {
                throw new ArgumentOutOfRangeException("mixDigest");
            }

            Nonce = nonce;
            HeaderPowHash = headerPowHash;
            MixDigest = mixDigest;
        }

        public byte[] Nonce { get; private set; }
        public byte[] HeaderPowHash { get; private set; }
        public byte[] MixDigest { get; set; }
    }
}
