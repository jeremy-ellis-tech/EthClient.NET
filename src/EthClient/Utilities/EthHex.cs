using System;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Eth.Utilities
{
    /// <summary>
    /// Utility class for converting between Ethereum & .NET
    /// quantities and data.
    /// </summary>
    public static class EthHex
    {
        /// <summary>
        /// Turn a base-16 encoded string into a quantity
        /// </summary>
        /// <param name="hex">The hex string (ie. 0x2a)</param>
        /// <returns>Hex string as a quantity</returns>
        public static BigInteger HexStringToInt(string hex)
        {
            return new BigInteger(HexStringToByteArray(hex).Reverse().ToArray());
        }

        /// <summary>
        /// Decode a big endian hex string into an array of bytes.
        /// </summary>
        /// <param name="hex">Big endian hex string ie. 0x2a</param>
        /// <exception cref="System.ArgumentNullException">Thrown if hex is null</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if hex is empty</exception>
        /// <returns>The byte array value</returns>
        public static byte[] HexStringToByteArray(string hex)
        {
            if(hex == null)
            {
                return null;
            }

            if(String.Equals(String.Empty, hex))
            {
                return Enumerable.Empty<byte>().ToArray();
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

        /// <summary>
        /// Turn a integer quantity into it's big endian hexadecimal string representation
        /// </summary>
        /// <param name="value">The integer quantity</param>
        /// <returns>Base-16 value representing the quantity</returns>
        public static string ToHexString(BigInteger value)
        {
            return String.Format("0x{0}", value.ToString("X"));
        }

        /// <summary>
        /// Turn a byte array into a base-16 string.
        /// </summary>
        /// <param name="arr">Array of byte values</param>
        /// <exception cref="System.ArgumentNullException">Thrown if arr is null</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if arr is empty</exception>
        /// <returns>Hex encoded values as a string</returns>
        public static string ToHexString(byte[] arr)
        {
            Ensure.EnsureParameterIsNotNull(arr, "arr");

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
