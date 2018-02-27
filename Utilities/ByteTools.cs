using System;
using System.Linq;
using System.Text;

namespace Utilities
{
    public static class ByteTools
    {
        public static byte[] HexToBytes(this string hex)
        {
            if (string.IsNullOrEmpty(hex)) return new byte[] { };

            if (hex.IndexOf("0x", StringComparison.Ordinal) == 0)
                hex = hex.Substring(2);

            return Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
        }

        public static string BytesToHex(this byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length *2);
            foreach (var d in data)
            {
                sb.Append(d.ToString("x02"));
            }

            return sb.ToString();
        }
    } 
}
