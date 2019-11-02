using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.ChartStorage
{
	public class CSPrice
	{
		/// <summary>
		/// 日時
		/// YYYYMMDDhhmmss
		/// </summary>
		public long DateTime { get; internal set; }

		/// <summary>
		/// 高値
		/// </summary>
		public double Hig { get; internal set; }

		/// <summary>
		/// 安値
		/// </summary>
		public double Low { get; internal set; }

		/// <summary>
		/// 買値
		/// </summary>
		public double Ask { get; internal set; }

		/// <summary>
		/// 売値
		/// </summary>
		public double Bid { get; internal set; }
	}
}
