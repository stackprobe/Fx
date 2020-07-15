using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.ChartStorage;
using Charlotte.TradingTimeChart;
using Charlotte.MovingAverageChart;
using Charlotte.Tools;
using System.IO;
using Charlotte.Utils;

namespace Charlotte
{
	public class Main0001
	{
		private const string W_DIR = @"C:\temp\Data2019To202x";

		private long DateTimeSt = 20190101000000;
		//private long DateTimeSt = 20200701000000; // test
		private long DateTimeEd = (long)DateToDay.Now.GetDate() * 1000000; // 今日の 00:00:00

		public void Main01()
		{
			FileTools.Delete(W_DIR);
			FileTools.CreateDir(W_DIR);

			File.WriteAllLines(Path.Combine(W_DIR, "_Period.txt"), new string[]
			{
				"DateTime_ST: " + this.DateTimeSt.ToString(),
				"DateTime_ED: " + this.DateTimeEd.ToString(),
			});

			foreach (string currPair in Consts.CurrPairs)
			{
				MakeDataFile(currPair);

				GC.Collect(); // 2bs
			}
		}

		private static int[] MA_DAYS = new int[]
		{
			1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
			11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
			21, 22, 23, 24, 25, 26, 27, 28, 29, 30,
			35, 40, 45, 50, 55, 60, 65, 70, 75, 80,
		};

		private void MakeDataFile(string currPair)
		{
			Console.WriteLine("currPair: " + currPair); // test cout

			CSChartManager cscm = new CSChartManager();
			CSChart csc = cscm.GetCSChart(currPair);
			TTChart ttc = new TTChart(csc);

			MAChart[] macs = new MAChart[MA_DAYS.Length];

			for (int index = 0; index < MA_DAYS.Length; index++)
				macs[index] = new MAChart(ttSec => ttc.GetPrice(ttSec).Mid, 60 * 24 * MA_DAYS[index], 60);

			long ttSecSt = TTCommon.DateTimeToTTSec(this.DateTimeSt);
			long ttSecEd = TTCommon.DateTimeToTTSec(this.DateTimeEd);

			Pulser pulser = new Pulser();

			using (CsvFileWriter writer = new CsvFileWriter(Path.Combine(W_DIR, currPair + ".csv")))
			{
				for (long ttSec = ttSecSt; ttSec <= ttSecEd; ttSec += 60)
				{
					if (pulser.Invoke())
						Console.WriteLine(TTCommon.TTSecToDateTime(ttSec) + " @ " + DateTime.Now); // test cout

					writer.WriteCell(TTCommon.TTSecToDateTime(ttSec).ToString());
					writer.WriteCell(ttSec.ToString());
					writer.WriteCell(ttc.GetPrice(ttSec).Low.ToString("F9"));
					writer.WriteCell(ttc.GetPrice(ttSec).Hig.ToString("F9"));
					writer.WriteCell(ttc.GetPrice(ttSec).Mid.ToString("F9"));

					foreach (MAChart mac in macs)
						writer.WriteCell(mac.GetPrice(ttSec).ToString("F9"));

					writer.EndRow();
				}
			}
			Console.WriteLine("done"); // test cout
		}
	}
}
