using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte
{
	public class GrphData
	{
		public string DataFile { get; private set; }

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

		public GrphData(string file)
		{
			this.DataFile = file;

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
		}
	}
}
