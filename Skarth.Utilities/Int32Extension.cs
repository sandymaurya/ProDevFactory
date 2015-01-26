using System.Globalization;

namespace Skarth.Utilities
{
	public static class Int32Extension
	{
		public static string ToIString(this int val)
		{
			return val.ToString(CultureInfo.InvariantCulture);
		}
	}
}