using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Utils
{
	public static class StringUtils
	{
		public static string SecToUIString(int sec)
		{
			if (86400 <= sec && sec % 86400 == 0)
				return (sec / 86400) + "d";

			if (3600 <= sec && sec % 3600 == 0)
				return (sec / 3600) + "h";

			if (60 <= sec && sec % 60 == 0)
				return (sec / 60) + "m";

			return sec.ToString();
		}

		public static int UIStringToSec(string str)
		{
			if (str.EndsWith("d"))
				return int.Parse(str.Substring(0, str.Length - 1)) * 86400;

			if (str.EndsWith("h"))
				return int.Parse(str.Substring(0, str.Length - 1)) * 3600;

			if (str.EndsWith("m"))
				return int.Parse(str.Substring(0, str.Length - 1)) * 60;

			return int.Parse(str);
		}
	}
}
