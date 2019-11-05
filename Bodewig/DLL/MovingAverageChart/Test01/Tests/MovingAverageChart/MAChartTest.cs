using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.MovingAverageChart;
using Charlotte.ChartStorage;
using Charlotte.TradingTimeChart;

namespace Charlotte.Tests.MovingAverageChart
{
	public class MAChartTest
	{
		public void Test01()
		{
			CSChartManager cscm = new CSChartManager();
			CSChart csc = cscm.GetCSChart("USDJPY");
			TTChart ttc = new TTChart(csc);
			MAChart mac = new MAChart(ttSec => ttc.GetPrice(ttSec).Mid, 10, 60);

			for (int c = 0; c < 100; c++)
			{
				long ttSec = TTCommon.DateTimeToTTSec(20191101060000) + c * 60;

				Console.WriteLine(ttc.GetPrice(ttSec).Mid.ToString("F9") + " " + mac.GetPrice(ttSec).ToString("F9"));
			}
		}
	}
}
