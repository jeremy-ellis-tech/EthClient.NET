using Eth.Utilities;

namespace Eth.RpcTypes
{
    public class ProofOfWork
    {
        public ProofOfWork(byte[] nonce, byte[] headerPowHash, byte[] mixDigest)
        {
            Ensure.EnsureParameterIsNotNull(nonce, "nonce");
            Ensure.EnsureCountIsCorrect(nonce, EthSpecs.NonceLength, "nonce");

            Ensure.EnsureParameterIsNotNull(headerPowHash, "headerPowHash");
            Ensure.EnsureCountIsCorrect(headerPowHash, EthSpecs.HeaderPowHashLength, "headerPowHash");

            Ensure.EnsureParameterIsNotNull(mixDigest, "mixDigest");
            Ensure.EnsureCountIsCorrect(mixDigest, EthSpecs.MixDigestLength, "mixDigest");

            Nonce = nonce;
            HeaderPowHash = headerPowHash;
            MixDigest = mixDigest;
        }

        public byte[] Nonce { get; private set; }

        public byte[] HeaderPowHash { get; private set; }

        public byte[] MixDigest { get; set; }
    }
}
