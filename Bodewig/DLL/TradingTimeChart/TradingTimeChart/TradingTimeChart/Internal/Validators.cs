using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.ChartStorage;

namespace Charlotte.TradingTimeChart.Internal
{
	internal class Validators
	{
		public static void CheckTTSec(long ttSec)
		{
			if (ttSec < TTConsts.TTSEC_MIN || TTConsts.TTSEC_MAX < ttSec)
				throw new Exception("Bad ttSec: " + ttSec);
		}

		public static void CheckDTSec(long dtSec)
		{
			if (dtSec < TTConsts.DTSEC_MIN || TTConsts.DTSEC_MAX < dtSec)
				throw new Exception("Bad dtSec: " + dtSec);
		}
	}
}
