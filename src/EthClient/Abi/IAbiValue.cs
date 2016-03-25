namespace Eth.Abi
{
    public interface IAbiValue
    {
        //Type name as used by the function selector
        string Name { get; }

        //Head/tail bytes as defined in encoding specification
        //See https://github.com/ethereum/wiki/wiki/Ethereum-Contract-ABI#formal-specification-of-the-encoding
        byte[] Head { get; }
        byte[] Tail { get; }
    }
}
