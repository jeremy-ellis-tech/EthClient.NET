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
            string paramPart = parameters != null && parameters.Count() > 0 ? String.Join(",", parameters.Select(x => x.Name)) : String.Empty;
            byte[] functionSelector = _keccak.GetDigest(Encoding.UTF8.GetBytes(String.Concat(functionName, "(", paramPart, ")"))).Take(4).ToArray();

            List<byte> heads = new List<byte>();
            List<byte> tails = new List<byte>();

            int headOffset = 32 * parameters.Count();

            foreach (var p in parameters)
            {
                //If p is dynamic the head is set as the position in the array
                //That the tail of the encoded value begins.
                if(p.IsDynamic)
                {
                    int offset = headOffset + tails.Count;
                    byte[] b = BitConverter.IsLittleEndian ? BitConverter.GetBytes(offset).Reverse().ToArray() : BitConverter.GetBytes(offset).ToArray();
                    p.Head = Enumerable.Repeat<byte>(0x0, 32 - b.Length).Concat(b).ToArray();
                }

                heads.AddRange(p.Head);
                tails.AddRange(p.Tail);
            }

            return functionSelector.Concat(heads).Concat(tails).ToArray();
        }
    }
}
