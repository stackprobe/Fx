﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Tests.ChartStorage;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{373dd60f-6070-4ae9-ae18-466bf7eb94c8}";
		public const string APP_TITLE = "ChartStorage";

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
			new ChartStorage0001Test().Test01(); // -- 0001
		}
	}
}
