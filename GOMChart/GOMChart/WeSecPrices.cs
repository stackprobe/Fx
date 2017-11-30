using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class WeSecPrices
	{
		private CurrencyPair CPair;

		public WeSecPrices(CurrencyPair cPair)
		{
			this.CPair = cPair;
		}

		public Price GetPrice(long weSec)
		{
			return this.CPair.GetPrice(WeSec.WeSecToDateTime(weSec));
		}
	}
}
