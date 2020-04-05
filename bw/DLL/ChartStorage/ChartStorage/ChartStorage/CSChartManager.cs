using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.ChartStorage.Internal;

namespace Charlotte.ChartStorage
{
	public class CSChartManager
	{
		private Dictionary<string, CSChart> CurrencyPair2CSChart = new Dictionary<string, CSChart>();

		public CSChart GetCSChart(string currencyPair)
		{
			Validators.CheckCurrencyPair(currencyPair);

			if (this.CurrencyPair2CSChart.ContainsKey(currencyPair) == false)
				this.CurrencyPair2CSChart.Add(currencyPair, new CSChart() { CurrencyPair = currencyPair });

			return this.CurrencyPair2CSChart[currencyPair];
		}
	}
}
