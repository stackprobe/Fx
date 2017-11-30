using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte
{
	public class Price
	{
		private RawPriceDayCache Cache;
		private long DateTime;

		public Price(RawPriceDayCache cache, long dateTime)
		{
			this.Cache = cache;
			this.DateTime = dateTime;

			Init();
		}

		//private const int EMPTY_DAY_MAX = 100; // データを補充したので、長くする必要無くなった。
		private const int EMPTY_DAY_MAX = 7;

		private RawPrice Exac;
		private RawPrice Prev;
		private RawPrice Next;

		private void Init()
		{
			int date = (int)(this.DateTime / 1000000L);

			if (this.Cache[date].HasPrice(this.DateTime))
			{
				Exac = this.Cache[date].GetPrice(this.DateTime);
			}
			else
			{
				int bkDate = date;

				// Prev
				{
					if (this.Cache[date].HasPrevPrice(this.DateTime))
					{
						Prev = this.Cache[date].GetPrevPrice(this.DateTime);
					}
					else
					{
						for (int c = 0; c < EMPTY_DAY_MAX; c++)
						{
							date = DateToDay.toDate(DateToDay.toDay(date) - 1);

							if (this.Cache[date].IsEmpty() == false)
							{
								Prev = this.Cache[date].GetLastPrice();
								break;
							}
						}
					}
				}

				date = bkDate;

				// Next
				{
					if (this.Cache[date].HasNextPrice(this.DateTime))
					{
						Next = this.Cache[date].GetNextPrice(this.DateTime);
					}
					else
					{
						for (int c = 0; c < EMPTY_DAY_MAX; c++)
						{
							date = DateToDay.toDate(DateToDay.toDay(date) + 1);

							if (this.Cache[date].IsEmpty() == false)
							{
								Next = this.Cache[date].GetFirstPrice();
								break;
							}
						}
					}
				}
			}
		}

		private const double UNKNOWN_CURRENCY_PAIR_VALUE = 100.0;

		public double GetValue(RawPrice.Value_e eValue)
		{
			if (Exac != null)
				return Exac[eValue];

			if (Prev == null)
			{
				if (Next == null)
					return UNKNOWN_CURRENCY_PAIR_VALUE;

				return Next[eValue];
			}
			if (Next == null)
				return Prev[eValue];

			long p = DateTimeToSec.toSec(Prev.DateTime);
			long e = DateTimeToSec.toSec(this.DateTime);
			long n = DateTimeToSec.toSec(Next.DateTime);

			double rate = (double)(e - p) / (n - p);

			return Prev[eValue] + (Next[eValue] - Prev[eValue]) * rate;
		}

		public double Open
		{
			get
			{
				return GetValue(RawPrice.Value_e.OPEN);
			}
		}

		public double High
		{
			get
			{
				return GetValue(RawPrice.Value_e.HIGH);
			}
		}

		public double Low
		{
			get
			{
				return GetValue(RawPrice.Value_e.LOW);
			}
		}

		public double Bid
		{
			get
			{
				return GetValue(RawPrice.Value_e.BID);
			}
		}

		public double Ask
		{
			get
			{
				return GetValue(RawPrice.Value_e.ASK);
			}
		}

		public double Mid
		{
			get
			{
				return GetValue(RawPrice.Value_e.MID);
			}
		}

		public double this[RawPrice.Value_e eValue]
		{
			get
			{
				return GetValue(eValue);
			}
		}
	}
}
