using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Tests.MovingAverageChart;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{8ef29e48-58e0-43b9-ad05-cb532ea9659d}";
		public const string APP_TITLE = "MovingAverageChart";

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
			new MAChartTest().Test01();
		}
	}
}
