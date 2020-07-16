using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte
{
	public static class Utils
	{
		/// <summary>
		/// CurrencyPairs.FxCollect から呼んでね！
		/// </summary>
		public static void FxCollect()
		{
			ProcessTools.runOnBatch("FxCollect.bat", @"C:\app\Fx");
			//ProcessTools.runOnBatch("FxCollect.bat", @"C:\Dev\Fx\GaitameOnlineMonitor\Tools"); // del @ 2020.7.16
		}

		public static string DateTimeToString(long dateTime)
		{
			int s = (int)(dateTime % 100);
			dateTime /= 100;
			int i = (int)(dateTime % 100);
			dateTime /= 100;
			int h = (int)(dateTime % 100);
			dateTime /= 100;
			int d = (int)(dateTime % 100);
			dateTime /= 100;
			int m = (int)(dateTime % 100);
			int y = (int)(dateTime / 100);

			return
				y + "/" +
				StringTools.zPad(m, 2) + "/" +
				StringTools.zPad(d, 2) + " " +
				StringTools.zPad(h, 2) + ":" +
				StringTools.zPad(i, 2) + ":" +
				StringTools.zPad(s, 2);
		}
	}
}
