﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public static class LongTools
	{
		public static Comparison<long> comp = delegate(long a, long b)
		{
			if (a < b)
			{
				return -1;
			}
			if (a > b)
			{
				return 1;
			}
			return 0;
		};

		public static bool isRange(long value, long minval, long maxval)
		{
			return minval <= value && value <= maxval;
		}

		public static long toInt(double value)
		{
			return value < 0.0 ? (long)(value - 0.5) : (long)(value + 0.5);
		}
	}
}
