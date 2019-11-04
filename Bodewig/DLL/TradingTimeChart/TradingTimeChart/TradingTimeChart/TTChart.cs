using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.ChartStorage;
using Charlotte.TradingTimeChart.Internal;

namespace Charlotte.TradingTimeChart
{
	public class TTChart
	{
		private CSChart Inner;

		public TTChart(CSChart inner_binding)
		{
			this.Inner = inner_binding;
		}

		public CSPrice GetPrice(long ttSec)
		{
			return this.Inner.GetPrice(TTCommon.TTSecToDateTime(ttSec));
		}
	}
}
