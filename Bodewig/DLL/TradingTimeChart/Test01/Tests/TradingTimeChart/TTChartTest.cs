using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.TradingTimeChart;
using Charlotte.ChartStorage;

namespace Charlotte.Tests.TradingTimeChart
{
	public class TTChartTest
	{
		public void Test01()
		{
			CSChartManager cscm = new CSChartManager();
			TTChart ttc = new TTChart(cscm.GetCSChart("USDJPY"));

			long ttSec = TTCommon.DateTimeToTTSec(20191101060000);

			Console.WriteLine(ttc.GetPrice(ttSec++).Ask);
			Console.WriteLine(ttc.GetPrice(ttSec++).Ask);
			Console.WriteLine(ttc.GetPrice(ttSec++).Ask);
		}
	}
}
