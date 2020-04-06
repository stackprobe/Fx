using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.MovingAverageChart;
using Charlotte.ChartStorage;
using Charlotte.TradingTimeChart;

namespace Charlotte
{
	public class Test0001
	{
		public void Test01()
		{
			//Test01_a(20190101, 20200401);
			Test01_a(20150101, 20200401);
		}

		private class Info
		{
			public double MvAvgPrice;
			public double Price;
			public double NextDayPrice;
		}

		private void Test01_a(int dateStart, int dateEnd)
		{
			for (int maDay = 5; maDay <= 30; maDay += 5)
			{
				List<Info> infos = new List<Info>();

				CSChartManager cscm = new CSChartManager();
				CSChart csc = cscm.GetCSChart("USDJPY");
				TTChart ttc = new TTChart(csc);
				MAChart mac = new MAChart(ttSec => ttc.GetPrice(ttSec).Mid, 60 * 24 * maDay, 60);

				for (int date = dateStart; date <= dateEnd; )
				{
					int nextDate = date;

					do
					{
						nextDate = DateToDay.ToDate(DateToDay.ToDay(nextDate) + 1);
					}
					while ("土日".Contains(DateTimeUnit.FromDate(nextDate).GetWeekday()));

					int bNextDate = Test01_b(nextDate, date, csc);

					long dt = date * 1000000L + 90000;
					long bNextDt = bNextDate * 1000000L + 90000;

					// ----

					infos.Add(new Info()
					{
						MvAvgPrice = mac.GetPrice(TTCommon.DateTimeToTTSec(dt)),
						Price = csc.GetPrice(dt).Mid,
						NextDayPrice = csc.GetPrice(bNextDt).Mid,
					});

					// ----

					date = nextDate;
				}

				using (CsvFileWriter writer = new CsvFileWriter(string.Format(@"C:\temp\maDay={0}.csv", maDay)))
				{
					foreach (Info info in infos)
					{
						writer.WriteCell(info.Price.ToString("F9"));
						writer.WriteCell(info.MvAvgPrice.ToString("F9"));
						writer.WriteCell(info.NextDayPrice.ToString("F9"));
						writer.WriteCell((info.MvAvgPrice / info.Price).ToString("F9"));
						writer.WriteCell((info.NextDayPrice / info.Price).ToString("F9"));
						writer.EndRow();
					}
				}
			}
		}

		private int Test01_b(int nextDate, int date, CSChart csc)
		{
			for (int c = 0; c <
				4
				//10
				//20
				; c++)
			{
				{
					long dt = date * 1000000L + 90000;
					long nextDt = nextDate * 1000000L + 90000;

					if (csc.GetPrice(dt).Mid < csc.GetPrice(nextDt).Mid)
						break;
				}

				do
				{
					nextDate = DateToDay.ToDate(DateToDay.ToDay(nextDate) + 1);
				}
				while ("土日".Contains(DateTimeUnit.FromDate(nextDate).GetWeekday()));
			}
			return nextDate;
		}
	}
}
