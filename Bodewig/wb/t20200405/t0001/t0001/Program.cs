using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Charlotte.Tools;
using Charlotte.ChartStorage;
using Charlotte.TradingTimeChart;
using Charlotte.MovingAverageChart;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{1a378f59-2498-4872-9167-da6e9f2a80d1}";
		public const string APP_TITLE = "t0001";

		static void Main(string[] args)
		{
			ProcMain.CUIMain(new Program().Main2, APP_IDENT, APP_TITLE);

#if DEBUG
			//if (ProcMain.CUIError)
			{
				Console.WriteLine("Press ENTER.");
				Console.ReadLine();
			}
#endif
		}

		private void Main2(ArgsReader ar)
		{
			//Test01();
			new Test0001().Test01();
		}

		private void Test01()
		{
			CSChartManager cscm = new CSChartManager();
			CSChart csc = cscm.GetCSChart("USDJPY");
			TTChart ttc = new TTChart(csc);
			MAChart mac = new MAChart(ttSec => ttc.GetPrice(ttSec).Mid, 10, 60);

			Console.WriteLine(mac.GetPrice(TTCommon.DateTimeToTTSec(20200401000000)));
		}
	}
}
