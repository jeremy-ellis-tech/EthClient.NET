using System;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Eth.Utilities
{
    public static class Hex
    {
        public static BigInteger HexStringToInt(string hex)
        {
            return new BigInteger(HexStringToByteArray(hex));
        }

        public static string IntToHexString(BigInteger value)
        {
            return String.Format("0x{0}", value.ToString("X2"));
        }

        public static byte[] HexStringToByteArray(string hex)
        {
            if (hex == null)
            {
                throw new ArgumentNullException("hex");
            }
             
            if (Equals(String.Empty, hex))
            {
                throw new ArgumentOutOfRangeException("hex");
            }

            var sb = new StringBuilder(hex.ToUpperInvariant());

            if (sb.Length > 2 && sb.ToString(0, 2).Equals("0X"))
            {
                sb.Remove(0, 2);
            }

            if (sb.Length % 2 != 0)
            {
                sb.Insert(0, '0');
            }

            return Enumerable.Range(0, sb.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(sb.ToString(x, 2), 16))
                .ToArray();
        }

        public static string ByteArrayToHexString(byte[] arr)
        {
            if(arr == null)
            {
                throw new ArgumentNullException("arr");
            }

            if(arr.Length < 1)
            {
                throw new ArgumentOutOfRangeException("arr");
            }

            var sb = new StringBuilder();

            sb.Append("0x");

            foreach (byte b in arr)
            {
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
