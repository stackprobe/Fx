using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.TradingTimeChart.Internal;

namespace Charlotte.TradingTimeChart
{
	public static class TTCommon
	{
		private const long TT_ZERO = 25200;
		private const long DTSEC_WEEK = 86400 * 7;
		private const long TTSEC_WEEK = 86400 * 5 - 3600;

		public static long DTSecToTTSec(long dtSec)
		{
			Validators.CheckDTSec(dtSec);

			long week = dtSec / DTSEC_WEEK;
			long sec = dtSec % DTSEC_WEEK;

			sec = Math.Max(sec, TT_ZERO);
			sec -= TT_ZERO;
			sec = Math.Min(sec, TTSEC_WEEK);
			sec += week * TTSEC_WEEK;

			return sec;
		}

		public static long TTSecToDTSec(long ttSec)
		{
			Validators.CheckTTSec(ttSec);

			long week = ttSec / TTSEC_WEEK;
			long sec = ttSec % TTSEC_WEEK;

			sec += TT_ZERO;
			sec += week * DTSEC_WEEK;

			return sec;
		}

		public static long DateTimeToTTSec(long dateTime)
		{
			return DTSecToTTSec(DateTimeToSec.ToSec(dateTime));
		}

		public static long TTSecToDateTime(long ttSec)
		{
			return DateTimeToSec.ToDateTime(TTSecToDTSec(ttSec));
		}
	}
}
