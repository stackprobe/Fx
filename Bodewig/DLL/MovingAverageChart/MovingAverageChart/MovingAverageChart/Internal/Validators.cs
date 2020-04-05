using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.MovingAverageChart.Internal
{
	internal static class Validators
	{
		public static void CheckTTSec(long ttSec)
		{
			if (ttSec < 0L || long.MaxValue / 2L < ttSec)
				throw new Exception("Bad ttSec: " + ttSec);
		}
	}
}
