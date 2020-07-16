using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.ChartStorage;
using Charlotte.TradingTimeChart;
using Charlotte.MovingAverageChart;

namespace Charlotte
{
	public class ChartSrvc
	{
		public class MacInfo
		{
			public MAChart Mac;
			public int Span;

			public MacInfo(int span)
			{
				if (span < Consts.MA_SPAN_MIN || Consts.MA_SPAN_MAX < span)
					throw new ArgumentException("Bad span: " + span);

				this.Mac = new MAChart(ttSec => ChartSrvc.I.Ttc.GetPrice(ttSec).Mid, span, Consts.MA_STEP);
				this.Span = span;
			}
		}

		public static ChartSrvc I;

		public string CurrPair;
		public CSChartManager Cscm;
		public CSChart Csc;
		public TTChart Ttc;
		public MacInfo[] Macs = new MacInfo[0];

		public ChartSrvc(string currPair)
		{
			this.CurrPair = currPair;
			this.Cscm = new CSChartManager();
			this.Csc = this.Cscm.GetCSChart(this.CurrPair);
			this.Ttc = new TTChart(this.Csc);
		}
	}
}
