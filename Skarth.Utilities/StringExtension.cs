using System;

namespace Skarth.Utilities
{
    public static class StringExtension
    {
        public static bool IsEquals(this string strA, string strB)
        {
            return strA.Equals(strB, StringComparison.InvariantCultureIgnoreCase);
        }

		public static bool HasValue(this string str)
		{
			return string.IsNullOrWhiteSpace(str);
		}
    }
}