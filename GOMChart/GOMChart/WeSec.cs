using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte
{
	/// <summary>
	/// WeSec == 取引停止時、時間を刻まない時間(秒)
	/// </summary>
	public static class WeSec
	{
		private static readonly long SEC_MIN = DateTimeToSec.toSec(10000101000000L);
		private static readonly long SEC_MAX = DateTimeToSec.toSec(99991231235959L);

		private const long SEC_WE_ZERO = 25200L;
		private const long SEC_WEEK = 86400L * 7;
		private const long SEC_WE_WEEK = 86400L * 5 - 3600;

		public static long SecToWeSec(long sec)
		{
			if (sec < SEC_WE_ZERO)
				return 0L; // dummy we-sec

			sec -= SEC_WE_ZERO;

			long week = sec / SEC_WEEK;

			sec %= SEC_WEEK;
			sec = Math.Min(sec, SEC_WE_WEEK);
			sec += week * SEC_WE_WEEK;

			return sec;
		}

		public static long WeSecToSec(long weSec)
		{
			if (weSec < 0L)
				return 0L; // dummy sec

			if (SEC_MAX < weSec) // we-sec の最大値は SEC_MAX より小さい！
				return 0L; // dummy sec

			long week = weSec / SEC_WE_WEEK;
			long sec = weSec % SEC_WE_WEEK;

			sec += week * SEC_WEEK;
			sec += SEC_WE_ZERO;

			if (SEC_MAX < sec)
				return 0L; // dummy sec

			return sec;
		}

		public static long DateTimeToWeSec(long dateTime)
		{
			return SecToWeSec(DateTimeToSec.toSec(dateTime));
		}

		public static long WeSecToDateTime(long weSec)
		{
			return DateTimeToSec.toDateTime(WeSecToSec(weSec));
		}
	}
}
