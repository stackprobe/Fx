using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte
{
	public class CurrencyPair
	{
		public string Code;

		private RawPriceDayCache Cache;

		public CurrencyPair(string currencyPair)
		{
			this.Code = currencyPair;
			this.Cache = new RawPriceDayCache(currencyPair);
		}

		public Price GetPrice(long dateTime)
		{
			return new Price(Cache, dateTime);
		}

		public void FxCollected()
		{
			long dateTime = DateTimeToSec.Now.getDateTime();
			int date = (int)(dateTime / 1000000L);
			int time = (int)(dateTime % 1000000L);

			TryReload(date);

			if (time < 500) // ? 日付更新から 5 min 未満 -> 昨日もリロードしておく
			{
				date = DateToDay.toDate(DateToDay.toDay(date) - 1); // 昨日
				TryReload(date);
			}
		}

		private void TryReload(int date)
		{
			RawPriceDay pDay = Cache.TryGet(date);

			if (pDay != null)
				pDay.Reload();
		}
	}
}
