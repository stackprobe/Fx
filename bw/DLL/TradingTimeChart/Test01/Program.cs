using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Tests.TradingTimeChart;
using Charlotte.TradingTimeChart;
using Charlotte.ChartStorage;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{c23a3d4e-e4f5-4330-a1e2-107a2ff02c95}";
		public const string APP_TITLE = "TradingTimeChart";

		static void Main(string[] args)
		{
			ProcMain.CUIMain(new Program().Main2, APP_IDENT, APP_TITLE);

			//if (ProcMain.CUIError)
			{
				Console.WriteLine("Press ENTER.");
				Console.ReadLine();
			}
		}

		private void Main2(ArgsReader ar)
		{
			//new TTCommonTest().Test01();
			//new TTChartTest().Test01();
			Test01();
		}

		private void Test01()
		{
			Console.WriteLine("TTConsts.TTSEC_MIN: " + TTConsts.TTSEC_MIN);
			Console.WriteLine("TTConsts.TTSEC_MAX: " + TTConsts.TTSEC_MAX);

			Console.WriteLine("TTConsts.DTSEC_MIN: " + DateTimeToSec.ToSec(CSConsts.DATE_TIME_MIN));
			Console.WriteLine("TTConsts.DTSEC_MAX: " + DateTimeToSec.ToSec(CSConsts.DATE_TIME_MAX));
		}
	}
}
