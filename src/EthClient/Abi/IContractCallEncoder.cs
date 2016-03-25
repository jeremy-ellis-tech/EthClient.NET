using System.Collections.Generic;

namespace Eth.Abi
{
    public interface IContractCallEncoder
    {
        byte[] Encode(string functionName, params IAbiValue[] parameters);
        IEnumerable<IAbiValue> Decode(byte[] data);
    }
}
