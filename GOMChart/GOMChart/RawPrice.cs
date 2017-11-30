using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte
{
	public class RawPrice
	{
		public long DateTime;
		public double Open;
		public double High;
		public double Low;
		public double Bid;
		public double Ask;

		public RawPrice(string line)
		{
			string[] cells = line.Split(',');
			int c = 0;

			DateTime = long.Parse(cells[c++]);
			Open = double.Parse(cells[c++]);
			High = double.Parse(cells[c++]);
			Low = double.Parse(cells[c++]);
			Bid = double.Parse(cells[c++]);
			Ask = double.Parse(cells[c++]);
		}

		private RawPrice()
		{ }

		public static int Comp(RawPrice a, RawPrice b)
		{
			return LongTools.comp(a.DateTime, b.DateTime);
		}

		public static RawPrice GetFerret(long dateTime)
		{
			return new RawPrice()
			{
				DateTime = dateTime,
			};
		}

		public enum Value_e
		{
			OPEN,
			HIGH,
			LOW,
			BID,
			ASK,
			MID,
		}

		public double this[Value_e eValue]
		{
			get
			{
				switch (eValue)
				{
					case Value_e.OPEN: return Open;
					case Value_e.HIGH: return High;
					case Value_e.LOW: return Low;
					case Value_e.BID: return Bid;
					case Value_e.ASK: return Ask;
					case Value_e.MID: return Mid;
				}
				throw null;
			}
		}

		public double Mid
		{
			get
			{
				return (Bid + Ask) / 2.0;
			}
		}
	}
}
