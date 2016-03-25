namespace Eth.Rpc
{
    public class EthProofOfWork
    {
        public byte[] Nonce { get; set; }
        public byte[] PowHash { get; set; }
        public byte[] MixDigest { get; set; }
    }
}
