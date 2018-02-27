using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Utilities
{
    public static class Utilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe int ToInt32(this byte[] value, int startIndex)
        {
            fixed (byte* pbyte = &value[startIndex])
            {
                return *((int*)pbyte);
            }
        }

        public static string ToHexString(this IEnumerable<byte> value)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in value)
                sb.AppendFormat("{0:x2}", b);
            return sb.ToString();
        }
    }
}
