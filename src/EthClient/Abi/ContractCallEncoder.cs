using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eth.Abi
{
    public class ContractCallEncoder : IContractCallEncoder
    {
        private readonly IKeccak _keccak;
        public ContractCallEncoder(IKeccak keccak)
        {
            _keccak = keccak;
        }

        public void Decode(byte[] data, params IAbiValue[] returns)
        {
            int offset = 0;
            foreach (var ret in returns)
            {
                ret.Head = data.Skip(offset).Take(32).ToArray();

                if(ret.IsDynamic)
                {
                    throw new NotImplementedException();
                }

                offset += 32;
            }
        }

        public byte[] Encode(string functionName, params IAbiValue[] parameters)
        {
            List<byte> serialzed = new List<byte>();

            string paramPart = parameters == null || parameters.Count() == 0 ? String.Empty : String.Join(",", parameters.Select(x => x.Name));
            serialzed.AddRange(_keccak.GetDigest(Encoding.UTF8.GetBytes(String.Concat(functionName, "(", paramPart, ")"))).Take(4).ToArray());

            foreach (var p in parameters)
            {
                if(p.IsDynamic)
                {
                    throw new NotImplementedException("Dynamic types not implemented yet");
                }

                serialzed.AddRange(p.Head);
            }

            return serialzed.ToArray();
        }
    }
}
