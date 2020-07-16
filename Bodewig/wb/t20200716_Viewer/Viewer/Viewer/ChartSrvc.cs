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
			public int SecSpan;
			public int Step;

			public MacInfo(int secSpan, int step)
			{
				if (secSpan < Consts.MA_SEC_SPAN_MIN || Consts.MA_SEC_SPAN_MAX < secSpan)
					throw new ArgumentException("Bad secSpan: " + secSpan);

				if (step < Consts.MA_STEP_MIN || Consts.MA_STEP_MAX < step)
					throw new ArgumentException("Bad step: " + step);

				if (secSpan % step != 0)
					throw new ArgumentException("Bad secSpan, step: " + secSpan + ", " + step);

				this.Mac = new MAChart(ttSec => ChartSrvc.I.Ttc.GetPrice(ttSec).Mid, secSpan / step, step);
				this.SecSpan = secSpan;
				this.Step = step;
			}

			public static MacInfo Safe(int secSpan, int step)
			{
				secSpan /= step;
				secSpan = Math.Max(secSpan, 1);
				secSpan *= step;

				return new MacInfo(secSpan, step);
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
