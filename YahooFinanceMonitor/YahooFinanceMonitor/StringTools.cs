﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YahooFinanceMonitor
{
	public static class StringTools
	{
		public static readonly Encoding ENCODING_SJIS = Encoding.GetEncoding(932);

		public static bool ContainsOnly(string str, string validChrs)
		{
			foreach (char chr in str)
				if (validChrs.Contains(chr) == false)
					return false;

			return true;
		}

		public const string DIGIT = "0123456789";
		public const string ALPHA = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		public const string alpha = "abcdefghijklmnopqrstuvwxyz";
		public const string HEXADECIMAL = "0123456789ABCDEF";
		public const string hexadecimal = "0123456789abcdef";

		/// <summary>
		/// hex string -> int
		/// </summary>
		/// <param name="hex"></param>
		/// <returns></returns>
		public static int HexToInt(string hex)
		{
			int value = 0;

			hex = hex.ToLower();

			foreach (char chr in hex)
			{
				value *= 16;
				value += hexadecimal.IndexOf(chr);
			}
			return value;
		}

		/// <summary>
		/// int -> hex string
		/// </summary>
		/// <param name="value"></param>
		/// <param name="minlen"></param>
		/// <returns></returns>
		public static string ToHex(int value, int minlen = 1)
		{
			StringBuilder buff = new StringBuilder();

			while (0 < value)
			{
				buff.Append(hexadecimal[value % 16]);
				value /= 16;
			}
			return ZPad(buff.ToString(), minlen);
		}

		public static string ZPad(int value, int minlen = 1)
		{
			return ZPad("" + value, minlen);
		}

		public static string ZPad(string str, int minlen = 1)
		{
			return LPad(str, '0', minlen);
		}

		public static string LPad(string str, char padChr, int minlen)
		{
			while (str.Length < minlen)
			{
				str = padChr + str;
			}
			return str;
		}

		public const string S_TRUE = "true";
		public const string S_FALSE = "false";

		/// <summary>
		/// hex string -> byte[]
		/// </summary>
		/// <param name="hex"></param>
		/// <returns></returns>
		public static byte[] Hex(string hex)
		{
			List<byte> buff = new List<byte>();

			hex = hex.ToLower();

			for (int index = 0; index < hex.Length; index += 2)
			{
				buff.Add((byte)(
					(hexadecimal.IndexOf(hex[index + 0]) << 4) |
					(hexadecimal.IndexOf(hex[index + 1]) << 0)
					));
			}
			return buff.ToArray();
		}

		/// <summary>
		/// byte[] -> hex string
		/// </summary>
		/// <param name="block"></param>
		/// <returns></returns>
		public static string ToHex(byte[] block)
		{
			StringBuilder buff = new StringBuilder();

			foreach (byte chr in block)
			{
				buff.Append(hexadecimal[chr >> 4]);
				buff.Append(hexadecimal[chr & 15]);
			}
			return buff.ToString();
		}

		public static string ToFormat(string str)
		{
			str = ReplaceChar(str, DIGIT, '9');
			str = ReplaceChar(str, ALPHA, 'A');
			str = ReplaceChar(str, alpha, 'a');

			return str;
		}

		public static string ReplaceChar(string str, string fromChrs, char toChr)
		{
			foreach (char fromChr in fromChrs)
			{
				str = str.Replace(fromChr, toChr);
			}
			return str;
		}

		public static string ReplaceChar(string str, string fromChrs, string toChrs)
		{
			for (int index = 0; index < fromChrs.Length; index++)
			{
				str = str.Replace(fromChrs[index], toChrs[index]);
			}
			return str;
		}

		public static string ReplaceLoop(string str, string fromPtn, string toPtn, int max)
		{
			for (int c = 1; c <= max; c++)
			{
				str = str.Replace(fromPtn, toPtn);
			}
			return str;
		}

		public static List<string> NumericTokenize(string str)
		{
			return MeaningTokenize(str, DIGIT);
		}

		public static List<string> MeaningTokenize(string str, string meanings)
		{
			return Tokenize(str, meanings, true, true);
		}

		public static List<string> Tokenize(string str, string delimiters, bool meaningFlag = false, bool ignoreEmpty = false)
		{
			List<string> tokens = new List<string>();
			StringBuilder buff = new StringBuilder();

			foreach (char chr in str)
			{
				if (delimiters.Contains(chr) == meaningFlag)
				{
					buff.Append(chr);
				}
				else
				{
					tokens.Add(buff.ToString());
					buff = new StringBuilder();
				}
			}
			tokens.Add(buff.ToString());

			if (ignoreEmpty)
				RemoveEmpty(tokens);

			return tokens;
		}

		public static void RemoveEmpty(List<string> list)
		{
			for (int index = list.Count - 1; 0 <= index; index--)
			{
				if (list[index] == null || list[index] == "")
				{
					list.RemoveAt(index);
				}
			}
		}

		public static int IndexOfChar(string str, string chrs)
		{
			int ret = int.MaxValue;

			foreach (char chr in chrs)
			{
				int index = str.IndexOf(chr);

				if (index != -1)
					ret = Math.Min(ret, index);
			}
			if (ret == int.MaxValue)
				ret = -1;

			return ret;
		}

		public static string MakeUUID()
		{
			return Guid.NewGuid().ToString("B");
		}

		public static bool IsSame(List<string> a, List<string> b)
		{
			if (a == null && b == null)
				return true;

			if (a == null || b == null)
				return false;

			if (a.Count != b.Count)
				return false;

			for (int index = 0; index < a.Count; index++)
				if (a[index] != b[index])
					return false;

			return true;
		}

		public static int ExIndexOf(string str, string ptn, int startIndex = 0)
		{
			int index = str.IndexOf(ptn, startIndex);

			if (index == -1)
				throw new Exception("パターンが見つかりません。書式が変更されたのかも？");

			return index;
		}

		public static int ExTailIndexOf(string str, string ptn, int startIndex = 0)
		{
			return ExIndexOf(str, ptn, startIndex) + ptn.Length;
		}
	}
}
