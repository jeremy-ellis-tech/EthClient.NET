using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eth
{
    public class EthProofOfWork
    {
        public byte[] Nonce { get; set; }
        public byte[] PowHash { get; set; }
        public byte[] MixDigest { get; set; }
    }
}
