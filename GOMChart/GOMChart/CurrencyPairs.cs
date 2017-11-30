using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class CurrencyPairs
	{
		public static CurrencyPair AUDCHF = new CurrencyPair("AUDCHF");
		public static CurrencyPair AUDJPY = new CurrencyPair("AUDJPY");
		public static CurrencyPair AUDNZD = new CurrencyPair("AUDNZD");
		public static CurrencyPair AUDUSD = new CurrencyPair("AUDUSD");
		public static CurrencyPair CADJPY = new CurrencyPair("CADJPY");
		public static CurrencyPair CHFJPY = new CurrencyPair("CHFJPY");
		public static CurrencyPair EURAUD = new CurrencyPair("EURAUD");
		public static CurrencyPair EURCAD = new CurrencyPair("EURCAD");
		public static CurrencyPair EURCHF = new CurrencyPair("EURCHF");
		public static CurrencyPair EURGBP = new CurrencyPair("EURGBP");
		public static CurrencyPair EURJPY = new CurrencyPair("EURJPY");
		public static CurrencyPair EURNZD = new CurrencyPair("EURNZD");
		public static CurrencyPair EURUSD = new CurrencyPair("EURUSD");
		public static CurrencyPair GBPAUD = new CurrencyPair("GBPAUD");
		public static CurrencyPair GBPCHF = new CurrencyPair("GBPCHF");
		public static CurrencyPair GBPJPY = new CurrencyPair("GBPJPY");
		public static CurrencyPair GBPNZD = new CurrencyPair("GBPNZD");
		public static CurrencyPair GBPUSD = new CurrencyPair("GBPUSD");
		public static CurrencyPair NZDJPY = new CurrencyPair("NZDJPY");
		public static CurrencyPair NZDUSD = new CurrencyPair("NZDUSD");
		public static CurrencyPair USDCAD = new CurrencyPair("USDCAD");
		public static CurrencyPair USDCHF = new CurrencyPair("USDCHF");
		public static CurrencyPair USDJPY = new CurrencyPair("USDJPY");
		public static CurrencyPair ZARJPY = new CurrencyPair("ZARJPY");

		public static CurrencyPair[] All = new CurrencyPair[]
		{
			AUDCHF,
			AUDJPY,
			AUDNZD,
			AUDUSD,
			CADJPY,
			CHFJPY,
			EURAUD,
			EURCAD,
			EURCHF,
			EURGBP,
			EURJPY,
			EURNZD,
			EURUSD,
			GBPAUD,
			GBPCHF,
			GBPJPY,
			GBPNZD,
			GBPUSD,
			NZDJPY,
			NZDUSD,
			USDCAD,
			USDCHF,
			USDJPY,
			ZARJPY,
		};

		public static int IndexOf(string code)
		{
			code = code.ToUpper();

			for (int index = 0; index < All.Length; index++)
				if (All[index].Code == code)
					return index;

			return -1; // not found
		}

		// ----

		public static void FxCollect()
		{
			Utils.FxCollect();

			foreach (CurrencyPair cPair in All)
			{
				cPair.FxCollected();
			}
		}
	}
}
