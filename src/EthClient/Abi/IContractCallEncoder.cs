namespace Eth.Abi
{
    public interface IContractCallEncoder
    {
        byte[] Encode(string functionName, params IAbiValue[] parameters);
        void Decode(byte[] data, params IAbiValue[] returns);
    }
}
