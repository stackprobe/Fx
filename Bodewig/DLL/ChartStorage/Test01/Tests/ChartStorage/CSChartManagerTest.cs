using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.ChartStorage;

namespace Charlotte.Tests.ChartStorage
{
	public class CSChartManagerTest
	{
		public void Test01()
		{
			CSChartManager cscm = new CSChartManager();
			CSChart csc = cscm.GetCSChart("USDJPY");
			CSPrice price = csc.GetPrice(20191101060531);

			Console.WriteLine(price.Hig);
		}
	}
}
