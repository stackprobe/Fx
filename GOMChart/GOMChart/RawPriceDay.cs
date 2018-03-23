using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Tools;

namespace Charlotte
{
	public class RawPriceDay
	{
		public RawPriceDay(int date, string currencyPair)
		{
			Init(date, currencyPair);
		}

		public RawPriceDay(string file)
		{
			Init(file);
		}

		private void Init(int date, string currencyPair)
		{
			Init(@"C:\var\Fx\" + date + "_" + currencyPair + ".csv");
		}

		private string DatFile;
		private SortedList<RawPrice> Prices = new SortedList<RawPrice>(RawPrice.Comp);

		private void Init(string file)
		{
			DatFile = file;
			Reload();
		}

		public void Reload()
		{
			Prices.clear();

			if (File.Exists(DatFile))
			{
				foreach (string line in File.ReadAllLines(DatFile, Encoding.ASCII))
				{
					Prices.add(new RawPrice(line));
				}
			}
		}

		public bool IsEmpty()
		{
			return Prices.size() == 0;
		}

		public RawPrice GetFirstPrice()
		{
			if (Prices.size() == 0)
				throw new Exception("この日はデータがありません。");

			return Prices[0];
		}

		public RawPrice GetLastPrice()
		{
			if (Prices.size() == 0)
				throw new Exception("この日はデータがありません。");

			return Prices[Prices.size() - 1];
		}

		public bool HasPrice(long dateTime)
		{
			return Prices.contains(RawPrice.GetTarget(dateTime));
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="dateTime"></param>
		/// <returns>dateTimeより前のPriceがある。</returns>
		public bool HasPrevPrice(long dateTime)
		{
			if (Prices.size() == 0)
				return false;

			return GetFirstPrice().DateTime < dateTime;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="dateTime"></param>
		/// <returns>dateTimeより後のPriceがある。</returns>
		public bool HasNextPrice(long dateTime)
		{
			if (Prices.size() == 0)
				return false;

			return dateTime < GetLastPrice().DateTime;
		}

		public RawPrice GetPrevPrice(long dateTime)
		{
			int index = Prices.leftIndexOf(RawPrice.GetTarget(dateTime)) - 1;

			if (index < 0)
				throw new Exception(dateTime + " より前の Price はありません。");

			return Prices[index];
		}

		public RawPrice GetNextPrice(long dateTime)
		{
			int index = Prices.rightIndexOf(RawPrice.GetTarget(dateTime)) + 1;

			if (Prices.size() <= index)
				throw new Exception(dateTime + " より後の Price はありません。");

			return Prices[index];
		}

		public RawPrice GetPrice(long dateTime)
		{
			int index = Prices.indexOf(RawPrice.GetTarget(dateTime));

			if (index == -1)
				throw new Exception(dateTime + " の Price はありません。");

			return Prices[index];
		}
	}
}
