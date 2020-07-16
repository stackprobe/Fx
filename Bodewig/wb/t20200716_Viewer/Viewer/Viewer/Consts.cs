using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.TradingTimeChart;
using Charlotte.Tools;
using System.Drawing;

namespace Charlotte
{
	public static class Consts
	{
		public static readonly string[] CurrPairs = new string[]
		{
			"AUDCHF",
			"AUDJPY",
			"AUDNZD",
			"AUDUSD",
			"CADJPY",
			"CHFJPY",
			"EURAUD",
			"EURCAD",
			"EURCHF",
			"EURGBP",
			"EURJPY",
			"EURNZD",
			"EURUSD",
			"GBPAUD",
			"GBPCHF",
			"GBPJPY",
			"GBPNZD",
			"GBPUSD",
			"NZDJPY",
			"NZDUSD",
			"USDCAD",
			"USDCHF",
			"USDJPY",
			"ZARJPY",
		};

		public const string DefaultCurrPair = "USDJPY";

		public static long TTSEC_END_MIN;
		public static long TTSEC_END_MAX;

		public const long TTSEC_STEP_MIN = 60; // 1 mins
		public const long TTSEC_STEP_MAX = 86400; // 1 days

		public const int MA_SPAN_MIN = 60; // 1 hours
		public const int MA_SPAN_MAX = 60 * 24 * 100; // 100 days
		public const int MA_STEP = 60; // 1 mins
		public const int MA_NUM_MAX = 5;

		public static Color[] MaColors = new Color[]
		{
			Color.Red,
			Color.Blue,
			Color.Purple,
			Color.DarkCyan,
			Color.DarkOrange,
		};

		public const int PLOT_NUM = 1000;

		public const double DMA_HIG_03 = 0.007;
		public const double DMA_HIG_02 = 0.005;
		public const double DMA_HIG_01 = 0.003;

		public const double DMA_LOW_01 = -0.003;
		public const double DMA_LOW_02 = -0.005;
		public const double DMA_LOW_03 = -0.007;
	}
}
