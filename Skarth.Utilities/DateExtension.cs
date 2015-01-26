using System;

namespace Skarth.Utilities
{
	public static class DateExtension
	{
		public static bool HasValue(this DateTime? date)
		{
			return !(date == null || date == DateTime.MinValue);
		}

		public static string ToFString(this DateTime date)
		{
			return date.ToString("dd-MM-yyyy");
		}

		public static string ToFString(this DateTime? date)
		{
			return date.HasValue() ? date.Value.ToString("dd-MM-yyyy") : null;
		}
	}
}