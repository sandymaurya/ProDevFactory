using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Skarth.Utilities
{
	public static class EnumExtension
	{
		public static string DisplayName(this Enum val)
		{
			var displayAttribute = val.GetAttributeOfType<DisplayAttribute>();

			return displayAttribute != null ? displayAttribute.Name : val.ToString();
		}

		public static List<string> GetDisplayNames<TEnum>() where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			if (!typeof(TEnum).IsEnum)
				throw new ArgumentException("Type must be an enumeration");

			var values = Enum.GetValues(typeof(TEnum));

			return (from object val in values select ((Enum)val).DisplayName()).ToList();
		}
        
        public static string Description(this Enum val)
        {
            var descriptionAttribute = val.GetAttributeOfType<DescriptionAttribute>();

            return descriptionAttribute != null ? descriptionAttribute.Description : val.ToString();
        }

        public static List<string> GetDescriptions<TEnum>() where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("Type must be an enumeration");

            var values = Enum.GetValues(typeof(TEnum));

            return (from object val in values select ((Enum)val).Description()).ToList();
        }

		/// <summary>
		/// Gets an attribute on an enum field value
		/// </summary>
		/// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
		/// <param name="enumVal">The enum value</param>
		/// <returns>The attribute of type T that exists on the enum value</returns>
		private static T GetAttributeOfType<T>(this Enum enumVal) where T : Attribute
		{
			var type = enumVal.GetType();
			var memInfo = type.GetMember(enumVal.ToString());
			var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
			return (attributes.Length > 0) ? (T)attributes[0] : null;
		}
	}

    public static class Enum<T> where T : struct, IComparable, IFormattable, IConvertible
	{
		public static T Parse(string value)
		{
			return Parse(value, true);
		}

		public static T Parse(string value, bool ignoreCase)
		{
            Check();
			return (T)Enum.Parse(typeof(T), value, ignoreCase); 
		}

		public static bool TryParse(string value, out T returnedValue)
		{
			return TryParse(value, true, out returnedValue);
		}

		public static bool TryParse(string value, bool ignoreCase, out T returnedValue)
		{
            Check();
			try
			{
				returnedValue = (T)Enum.Parse(typeof(T), value, ignoreCase);
				return true;
			}
			catch
			{
				returnedValue = default(T);
				return false;
			}
		}

        public static List<T> GetValues()
        {
            Check();
            return ((T[])Enum.GetValues(typeof(T))).ToList();
        }

        public static List<string> GetNames()
        {
            Check();
            return Enum.GetNames(typeof(T)).ToList();
        }

        public static List<string> GetDisplayNames()
        {
            Check();
            var values = Enum.GetValues(typeof(T));
            return (from object val in values select ((Enum)val).DisplayName()).ToList();
        }

        private static void Check()
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("Type must be an enumeration");
        }
	}
}