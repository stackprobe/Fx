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

		private SortedList<CSPrice> GetDayPrices(int day)
		{
			if (day < FirstDay)
			{
				if (PriceTable.Count < FirstDay - day)
				{
					PriceTable.Clear();
					PriceTable.Add((SortedList<CSPrice>)null);
				}
				else
				{
					PriceTable.InsertRange(0, Enumerable.Repeat((SortedList<CSPrice>)null, FirstDay - day));

					if (Consts.LOAD_DAY_MAX < PriceTable.Count)
					{
						PriceTable.RemoveRange(Consts.LOAD_DAY_MAX, PriceTable.Count - Consts.LOAD_DAY_MAX);
					}
				}
				FirstDay = day;
			}
			else if (FirstDay + PriceTable.Count <= day)
			{
				if (Consts.LOAD_DAY_MAX * 2 <= day - FirstDay)
				{
					PriceTable.Clear();
					PriceTable.Add((SortedList<CSPrice>)null);
					FirstDay = day;
				}
				else
				{
					PriceTable.AddRange(Enumerable.Repeat((SortedList<CSPrice>)null, day - (FirstDay + PriceTable.Count) + 1));

					if (Consts.LOAD_DAY_MAX < PriceTable.Count)
					{
						int removeCount = PriceTable.Count - Consts.LOAD_DAY_MAX;

						PriceTable.RemoveRange(0, removeCount);
						FirstDay += removeCount;
					}
				}
			}

			{
				int index = day - FirstDay;

				if (PriceTable[index] == null)
					PriceTable[index] = LoadDayPrices(day);

				return PriceTable[index];
			}
		}

		private SortedList<CSPrice> LoadDayPrices(int day)
		{
			SortedList<CSPrice> ret = new SortedList<CSPrice>(Comp_CSPrice);
			ret.AddRange(StorageLoader.LoadStorage(DateToDay.ToDate(day), CurrencyPair));
			return ret;
		}

		private int Comp_CSPrice(CSPrice a, CSPrice b)
		{
			return LongTools.Comp(a.DateTime, b.DateTime);
		}

		public CSPrice GetPrice(long dateTime)
		{
			Validators.CheckDateTime(dateTime);

			int day = DateToDay.ToDay((int)(dateTime / 1000000));
			SortedList<CSPrice> prices = GetDayPrices(day);
			CSPrice[] matched = prices.GetMatchWithEdge(prices.GetFerret(new CSPrice() { DateTime = dateTime })).ToArray();

			if (matched.Length == 3)
				return matched[1];

			if (matched.Length != 2)
				throw null; // 2bs -- 同じ日時のデータは無いはず！

			if (matched[0] == null)
			{
				for (int c = 1; c <= Consts.MARGIN_DAY_MAX; c++)
				{
					prices = GetDayPrices(day - c);

					if (1 <= prices.Count)
					{
						matched[0] = prices.Get(prices.Count - 1);
						break;
					}
				}
			}
			if (matched[1] == null)
			{
				for (int c = 1; c <= Consts.MARGIN_DAY_MAX; c++)
				{
					prices = GetDayPrices(day + c);

					if (1 <= prices.Count)
					{
						matched[0] = prices.Get(0);
						break;
					}
				}
			}
			matched = matched.Where(v => v != null).ToArray();

			if (matched.Length == 0)
				return new CSPrice() { DateTime = dateTime };

			if (matched.Length == 1)
				return matched[0];

			long sec1 = DateTimeToSec.ToSec(matched[0].DateTime);
			long sec2 = DateTimeToSec.ToSec(dateTime);
			long sec3 = DateTimeToSec.ToSec(matched[1].DateTime);

			double rate = (sec2 - sec1) * 1.0 / (sec3 - sec1);

			return new CSPrice()
			{
				DateTime = dateTime,
				Hig = matched[0].Hig + rate * (matched[1].Hig - matched[0].Hig),
				Low = matched[0].Low + rate * (matched[1].Low - matched[0].Low),
				Ask = matched[0].Ask + rate * (matched[1].Ask - matched[0].Ask),
				Bid = matched[0].Bid + rate * (matched[1].Bid - matched[0].Bid),
			};
		}
	}
}
