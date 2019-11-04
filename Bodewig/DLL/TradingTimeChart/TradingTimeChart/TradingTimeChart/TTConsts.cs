using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.ChartStorage;

namespace Charlotte.TradingTimeChart
{
	public static class TTConsts
	{
		public static long TTSEC_MIN
		{
			get
			{
				return TTCommon.DateTimeToTTSec(CSConsts.DATE_TIME_MIN);
			}
		}

		public static long TTSEC_MAX
		{
			get
			{
				return TTCommon.DateTimeToTTSec(CSConsts.DATE_TIME_MAX);
			}
		}
	}
}
