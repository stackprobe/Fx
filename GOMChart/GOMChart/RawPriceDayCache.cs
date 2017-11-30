using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte
{
	public class RawPriceDayCache
	{
		private string CurrencyPair;

		public RawPriceDayCache(string currencyPair)
		{
			this.CurrencyPair = currencyPair;
		}

		private const int CACHE_MAX = Consts.DAY_BOUND_MAX + 3; // +margin
		private Dictionary<int, RawPriceDay> PriceDaies = new Dictionary<int, RawPriceDay>();
		private Queue<int> AddedDates = new Queue<int>();

		public RawPriceDay Get(int date)
		{
			if (PriceDaies.ContainsKey(date))
				return PriceDaies[date];

			if (CACHE_MAX < PriceDaies.Count)
				PriceDaies.Remove(AddedDates.Dequeue());

			RawPriceDay ret = new RawPriceDay(date, this.CurrencyPair);
			PriceDaies.Add(date, ret);
			AddedDates.Enqueue(date);
			return ret;
		}

		public RawPriceDay this[int date]
		{
			get
			{
				return Get(date);
			}
		}

		public RawPriceDay TryGet(int date)
		{
			if (PriceDaies.ContainsKey(date))
				return PriceDaies[date];

			return null;
		}
	}
}
