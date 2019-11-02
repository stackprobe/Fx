using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.ChartStorage.Internal;

namespace Charlotte.ChartStorage
{
	public class CSChart
	{
		public string CurrencyPair { get; internal set; }

		// <---- prm

		private List<SortedList<CSPrice>> PriceTable = new List<SortedList<CSPrice>>();
		private int FirstDay = 0;

		public CSPrice GetPrice(long dateTime)
		{
			throw null; // TODO
		}
	}
}
