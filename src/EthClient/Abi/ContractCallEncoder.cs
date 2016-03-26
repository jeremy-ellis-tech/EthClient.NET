using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eth.Abi
{
    public class ContractCallEncoder : IContractCallEncoder
    {
        public IEnumerable<IAbiValue> Decode(byte[] data, params AbiReturnType[] returnTypes)
        {
            int offset = 0;
            foreach (var returnType in returnTypes)
            {
                byte[] head = data.Skip(offset).Take(32).ToArray();

                IAbiValue value;
                switch (returnType)
                {
                    case AbiReturnType.UInt256:
                        value = new UInt256AbiValue();
                        break;
                    default:
                        throw new NotImplementedException();
                }

                if (value.IsDynamic)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    value.Head = head;
                }

                yield return value;

                offset += 32;
            }
        }

        public byte[] Encode(string functionName, params IAbiValue[] parameters)
        {
            KeccakDigest kd = new KeccakDigest();
            byte[] input = Encoding.UTF8.GetBytes(String.Concat(functionName, "(", String.Join(",", parameters.Select(x => x.Name)), ")"));
            kd.BlockUpdate(input, 0, input.Length);
            byte[] functionSelector = new byte[kd.GetDigestSize()];
            kd.DoFinal(functionSelector, 0);

            List<byte> serialzed = new List<byte>();

            serialzed.AddRange(functionSelector.Take(4));

            foreach (var p in parameters)
            {
                if (p.Tail != null && p.Tail.Length != 0)
                {
                    throw new NotImplementedException("Dynamic types not implemented yet");
                }

                serialzed.AddRange(p.Head);
            }

            return serialzed.ToArray();
        }
    }
}
