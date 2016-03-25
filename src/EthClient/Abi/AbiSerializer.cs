using System;
using System.Collections.Generic;

namespace Eth.Abi
{
    public class AbiSerializer : IAbiSerializer
    {
        public IEnumerable<IAbiValue> Deserialize(byte[] data)
        {
            throw new NotImplementedException();
        }

        public byte[] Serialize(params IAbiValue[] parameters)
        {
            List<byte> serialzed = new List<byte>();

            foreach (var p in parameters)
            {
                if(p.Tail != null && p.Tail.Length != 0)
                {
                    throw new NotImplementedException("Dynamic types not implemented yet");
                }

                serialzed.AddRange(p.Head);
            }

            return serialzed.ToArray();
        }
    }
}
