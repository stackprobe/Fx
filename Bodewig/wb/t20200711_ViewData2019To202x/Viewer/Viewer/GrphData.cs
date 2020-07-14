using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.IO;

namespace Charlotte
{
	public class GrphData
	{
		public string CurrPair { get; private set; }

		private string DataFile;

		public class PriceInfo
		{
			public long DateTime;
			public long TTSec;
			public double Low;
			public double Hig;
			public double Mid;
			public double[] MaVals;
		}

		private SortedList<PriceInfo> Prices = new SortedList<PriceInfo>((a, b) => LongTools.Comp(a.DateTime, b.DateTime));

		public GrphData(string currPair)
		{
			this.CurrPair = currPair;
			this.DataFile = Path.Combine(Ground.I.DataDir, this.CurrPair + ".csv");

			using (CsvFileReader reader = new CsvFileReader(this.DataFile))
			{
				for (; ; )
				{
					string[] row = reader.ReadRow();

					if (row == null)
						break;

					PriceInfo price = new PriceInfo();
					int c = 0;

					price.DateTime = long.Parse(row[c++]);
					price.TTSec = long.Parse(row[c++]);
					price.Low = double.Parse(row[c++]);
					price.Hig = double.Parse(row[c++]);
					price.Mid = double.Parse(row[c++]);
					price.MaVals = row.Skip(c).Select(v => double.Parse(v)).ToArray();

					this.Prices.Add(price);
				}
			}

			if (this.Prices.Count < 2)
				throw new Exception("行数が少なすぎます。");
		}

		public int Start = -1; // -1 == Range 未設定
		public int End;
		public int Step;

		public void SetRange(long dateTimeSt, long dateTimeEd)
		{
			this.Start = this.GetIndexByDateTime(dateTimeSt);
			this.End = this.GetIndexByDateTime(dateTimeEd);

			if (this.End <= this.Start)
			{
				this.End = this.Start;

				if (this.Start == 0)
					this.End++;
				else
					this.Start--;
			}

			this.Step = 1;

			while (1000 < (this.End - this.Start) / this.Step) // ? プロット数多すぎ
			{
				if (this.Step < 16)
					this.Step++;
				else
					this.Step *= 2;

				int span = this.End - this.Start;

				span /= this.Step;
				span *= this.Step;

				this.End = this.Start + span;
			}
		}

		private int GetIndexByDateTime(long dt)
		{
			int index = this.Prices.LeftIndexOf(this.Prices.GetFerret(new PriceInfo() { DateTime = dt }));
			index = Math.Min(index, this.Prices.Count - 1);
			return index;
		}

		public PriceInfo GetPrice(int index)
		{
			return this.Prices.Get(index);
		}
	}
}
