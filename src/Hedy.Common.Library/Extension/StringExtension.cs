using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Hedy.Common.Library.Extension
{
	public static class StringExtension
	{
		public static string UppercaseFirstLetter(this string value)
		{
			if (value.Length > 0)
			{
				char[] array = value.ToCharArray();
				array[0] = char.ToUpper(array[0]);
				return new string(array);
			}
			return value;
		}

		public static int WordCount(this string source)
		{
			return source.Split(new char[3]
			{
			' ',
			'.',
			'?'
			}, StringSplitOptions.RemoveEmptyEntries).Length;
		}

		public static string ToReticence(this string content, int maxLength)
		{
			if (content == null || content.Length <= maxLength)
			{
				return content;
			}
			content = content.Substring(0, (content.Length >= maxLength - 3) ? (maxLength - 3) : content.Length) + "...";
			return content;
		}

		public static string ToBase64(this string source)
		{
			return Convert.ToBase64String(Encoding.UTF8.GetBytes(source));
		}

		public static string FromBase64(this string source)
		{
			byte[] bytes = Convert.FromBase64String(source);
			return Encoding.UTF8.GetString(bytes);
		}

		public static string ToMD5Hash(this string source)
		{
			MD5 mD = MD5.Create();
			byte[] bytes = Encoding.ASCII.GetBytes(source);
			byte[] array = mD.ComputeHash(bytes);
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Append(array[i].ToString("X2"));
			}
			return stringBuilder.ToString();
		}

		public static bool VerifyMd5Hash(this string source, string value)
		{
			string x = ToMD5Hash(value);
			return StringComparer.OrdinalIgnoreCase.Compare(x, source) == 0;
		}

		public static bool EqualsIgnoreCase(this string source, string value)
		{
			return source.Equals(value, StringComparison.InvariantCultureIgnoreCase);
		}

		public static bool IsNullOrEmpty(this string source)
		{
			return string.IsNullOrEmpty(source);
		}

		public static int ToInt32TryParse(this string source)
		{
			int.TryParse(source, out int result);
			return result;
		}

		public static int ToInt32(this string source)
		{
			return int.Parse(source);
		}

		public static int? ToNullableInt32(this string value)
		{
			if (!IsNullOrEmpty(value))
			{
				return ToInt32(value);
			}
			return null;
		}

		public static bool ToBoolean(this string source)
		{
			return bool.Parse(source);
		}

		public static bool ToBooleanTryParse(this string source)
		{
			bool result = false;
			bool.TryParse(source, out result);
			return result;
		}

		public static byte ToByte(this string source)
		{
			return byte.Parse(source);
		}

		public static byte ToByteTryParse(this string source)
		{
			byte result;
			byte.TryParse(source, out result);
			return result;
		}

		public static char ToChar(this string source)
		{
			return char.Parse(source);
		}

		public static char ToCharTryParse(this string source)
		{
			char.TryParse(source, out char result);
			return result;
		}

		public static DateTime ToDateTime(this string source)
		{
			return DateTime.Parse(source);
		}

		public static DateTime ToDateTimeTryParse(this string source)
		{
			DateTime.TryParse(source, out DateTime result);
			return result;
		}

		public static decimal ToDecimal(this string source)
		{
			return decimal.Parse(source);
		}

		public static decimal ToDecimalTryParse(this string source)
		{
			decimal.TryParse(source, out decimal result);
			return result;
		}

		public static double ToDouble(this string source)
		{
			return double.Parse(source);
		}

		public static double ToDoubleTryParse(this string source)
		{
			double.TryParse(source, out double result);
			return result;
		}

		public static short ToInt16(this string source)
		{
			return short.Parse(source);
		}

		public static long ToInt64(this string source)
		{
			return long.Parse(source);
		}

		public static sbyte ToSByte(this string source)
		{
			return sbyte.Parse(source);
		}

		public static float ToSingle(this string source)
		{
			return float.Parse(source);
		}

		public static ushort ToUInt16(this string source)
		{
			return ushort.Parse(source);
		}

		public static ushort ToUInt16TryParse(this string source)
		{
			ushort.TryParse(source, out ushort result);
			return result;
		}

		public static uint ToUInt32(this string source)
		{
			return uint.Parse(source);
		}

		public static uint ToUInt32TryParse(this string source)
		{
			uint.TryParse(source, out uint result);
			return result;
		}

		public static ulong ToUInt64(this string source)
		{
			return ulong.Parse(source);
		}

		public static ulong ToUInt64TryParse(this string source)
		{
			ulong.TryParse(source, out ulong result);
			return result;
		}

		public static bool IsNumeric(this string value)
		{
			double result;
			return double.TryParse(value, out result);
		}

		public static bool IsDateTime(this string value)
		{
			return DateTime.TryParse(value, out _);
		}

		public static bool IsInt32(this string source)
		{
			return int.TryParse(source, out _);
		}

		public static bool IsInt64(this string source)
		{
			long result;
			return long.TryParse(source, out result);
		}

		public static bool IsShort(this string source)
		{
			short result;
			return short.TryParse(source, out result);
		}

		public static bool IsDecimal(this string source)
		{
			decimal result;
			return decimal.TryParse(source, out result);
		}

		public static bool IsBoolean(this string source)
		{
			bool result;
			return bool.TryParse(source, out result);
		}

		public static short ToShort(this string value)
		{
			short.TryParse(value, out short result);
			return result;
		}

		public static long ToLong(this string value)
		{
			long.TryParse(value, out long result);
			return result;
		}

		public static string RemoveLastCharacter(this string instr)
		{
			return instr.Substring(0, instr.Length - 1);
		}

		public static string RemoveLast(this string instr, int number)
		{
			return instr.Substring(0, instr.Length - number);
		}

		public static string RemoveFirstCharacter(this string instr)
		{
			return instr.Substring(1);
		}

		public static string RemoveFirst(this string instr, int number)
		{
			return instr.Substring(number);
		}

		public static string Repeat(this string str, int times)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < times; i++)
			{
				stringBuilder.Append(str);
			}
			return stringBuilder.ToString();
		}

		public static bool IsNullOrWhiteSpace(this string value)
		{
			return string.IsNullOrWhiteSpace(value);
		}

		public static string Join(this string separator, string[] value)
		{
			return string.Join(separator, value);
		}

		public static string Join(this string separator, object[] values)
		{
			return string.Join(separator, values);
		}

		public static string Join<T>(this string separator, IEnumerable<T> values)
		{
			return string.Join(separator, values);
		}

		public static string Join(this string separator, IEnumerable<string> values)
		{
			return string.Join(separator, values);
		}

		public static string Join(this string separator, string[] value, int startIndex, int count)
		{
			return string.Join(separator, value, startIndex, count);
		}

		public static string Format(this string format, object arg0)
		{
			return string.Format(format, arg0);
		}

		public static string Format(this string format, object arg0, object arg1)
		{
			return string.Format(format, arg0, arg1);
		}

		public static string Format(this string format, object arg0, object arg1, object arg2)
		{
			return string.Format(format, arg0, arg1, arg2);
		}

		public static string Format(this string format, object[] args)
		{
			return string.Format(format, args);
		}

		public static string ExtractNumber(this string @this)
		{
			return new string((from x in @this.ToCharArray()
							   where char.IsNumber(x)
							   select x).ToArray());
		}

		public static bool IsEmail(this string input)
		{
			return Regex.Match(input, "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*", RegexOptions.IgnoreCase).Success;
		}

		public static string Reverse(this string input)
		{
			if (string.IsNullOrWhiteSpace(input))
			{
				return string.Empty;
			}
			char[] array = input.ToCharArray();
			Array.Reverse((Array)array);
			return new string(array);
		}

		public static int Occurrence(this string instr, string search)
		{
			return Regex.Matches(instr, search).Count;
		}

		public static int ToInt(this string input)
		{
			if (!int.TryParse(input, out int result))
			{
				return 0;
			}
			return result;
		}
	}
}
