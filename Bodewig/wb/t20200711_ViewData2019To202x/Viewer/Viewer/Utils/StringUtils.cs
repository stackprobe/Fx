using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Utils
{
	public static class StringUtils
	{
		public static string KVToValue(string line, char kvDelim = ':')
		{
			int p = line.IndexOf(kvDelim);

			if (p == -1)
				throw new Exception("not KV");

			string value = line.Substring(p + 1);
			value = value.Trim();
			return value;
		}
	}
}
