using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.ChartStorage;

namespace Charlotte.TradingTimeChart
{
	public static class TTConsts
	{
		public const long TTSEC_MIN = 44683383600; // == TTCommon.DateTimeToTTSec(CSConsts.DATE_TIME_MIN)
		public const long TTSEC_MAX = 67036179599; // == TTCommon.DateTimeToTTSec(CSConsts.DATE_TIME_MAX)

		public const long DTSEC_MIN = 63082281600; // == DateTimeToSec.ToSec(CSConsts.DATE_TIME_MIN)
		public const long DTSEC_MAX = 94639276799; // == DateTimeToSec.ToSec(CSConsts.DATE_TIME_MAX)
	}
}
