using System.Collections.Generic;

namespace Eth.Abi
{
    public interface IAbiSerializer
    {
        byte[] Serialize(params IAbiValue[] parameters);
        IEnumerable<IAbiValue> Deserialize(byte[] data);
    }
}
