namespace Eth.Abi
{
    public interface IKeccak
    {
        byte[] GetDigest(byte[] data);
    }
}
