using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class Consts
	{
		public const int DAY_BOUND_MAX = 1000; // これ以上長いスパン(日数)を扱わない。
		public const long SEC_BOUND_MAX = 86400L * DAY_BOUND_MAX; // これ以上長いスパン(秒)を扱わない。
		public const long SAMPLING_MAX = 10000L; // (bound / interval) の上限
	}
}
