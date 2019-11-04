using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.TradingTimeChart;

namespace Charlotte.Tests.TradingTimeChart
{
	public class TTCommonTest
	{
		public void Test01()
		{
			Test01_a(19700101000000);
			Test01_a(20191101060000);
			Test01_a(29991231235959);
		}

		private void Test01_a(long dateTime)
		{
			long ttSec = TTCommon.DateTimeToTTSec(dateTime);
			long ret = TTCommon.TTSecToDateTime(ttSec);

			Console.WriteLine(dateTime + " -> " + ttSec + " -> ...");
			Console.WriteLine(ret);
		}
	}
}
