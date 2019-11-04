using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.ChartStorage;

namespace Charlotte.TradingTimeChart.Internal
{
	internal class Validators
	{
		// TTSEC_MIN, TTSEC_MAX を見ると循環参照になるので見ない。

		public static void CheckTTSec(long ttSec)
		{
			if (ttSec < 0L || long.MaxValue / 2L < ttSec)
				throw new Exception("Bad ttSec: " + ttSec);
		}

		public static void CheckDTSec(long dtSec)
		{
			if (dtSec < 0L || long.MaxValue / 2L < dtSec)
				throw new Exception("Bad dtSec: " + dtSec);
		}
	}
}
