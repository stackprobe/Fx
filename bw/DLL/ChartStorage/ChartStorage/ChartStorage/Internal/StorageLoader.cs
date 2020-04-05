using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Tools;

namespace Charlotte.ChartStorage.Internal
{
	internal static class StorageLoader
	{
		public static CSPrice[] LoadStorage(int date, string currencyPair)
		{
			Validators.CheckDate(date);
			Validators.CheckCurrencyPair(currencyPair);

			string file = Path.Combine(Consts.STORAGE_DIR, date + "_" + currencyPair + ".csv");
			List<CSPrice> prices = new List<CSPrice>();

			if (File.Exists(file))
			{
				try
				{
					using (CsvFileReader reader = new CsvFileReader(file))
					{
						for (; ; )
						{
							string[] row = reader.ReadRow();

							if (row == null)
								break;

							long dateTime = long.Parse(row[0]);

							Validators.CheckDateTime(dateTime);

							double hig = double.Parse(row[2]);
							double low = double.Parse(row[3]);
							double ask = double.Parse(row[4]);
							double bid = double.Parse(row[5]);

							prices.Add(new CSPrice()
							{
								DateTime = dateTime,
								Hig = hig,
								Low = low,
								Ask = ask,
								Bid = bid,
							});
						}
					}
				}
				catch (Exception e)
				{
					throw new Exception(e.Message + ", file: " + file, e);
				}
			}
			return prices.ToArray();
		}
	}
}
