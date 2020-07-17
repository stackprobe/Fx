using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Utils
{
	public static class StringUtils
	{
		// ---- Sec <--> UIString

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

		// ----

		public static string SecSpanToUIString(long secSpan)
		{
			long t = secSpan;

			int s = (int)(t % 60);
			t /= 60;
			int m = (int)(t % 60);
			t /= 60;
			int h = (int)(t % 24);
			t /= 24;
			int d = (int)t;

			return string.Format("{0}d {1:D2}:{2:D2}:{3:D2} ({4:F3}h)", d, h, m, s, secSpan / 3600.0);
		}
	}
}
