using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Charlotte.Tools;
using System.Threading;

namespace Charlotte
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				onBoot();

				if (1 <= args.Length && args[0].ToUpper() == "//R")
				{
					main2(File.ReadAllLines(args[1], Encoding.GetEncoding(932)));
				}
				else
				{
					main2(args);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
#if DEBUG
			Console.WriteLine("Press ENTER");
			Console.ReadLine();
#endif
		}

		public const string APP_IDENT = "{8c98efea-6fbf-48eb-a381-1074eae628d0}";
		public const string APP_TITLE = "GaitameOnlineMonitor";

		public static string selfFile;
		public static string selfDir;

		public static void onBoot()
		{
			selfFile = Assembly.GetEntryAssembly().Location;
			selfDir = Path.GetDirectoryName(selfFile);
		}

		private static void main2(string[] args)
		{
			using (MutexObject mo = new MutexObject(APP_IDENT))
			{
				if (mo.waitForMillis(0))
				{
					try
					{
						main3();
					}
					catch (Exception e)
					{
						FailedOperation.caught(e);
					}
					mo.release();
				}
			}
		}

		private static string wRootDir;

		private static void main3()
		{
			wRootDir = getWRootDir();

			using (NamedEventObject evStop = new NamedEventObject(Consts.EV_STOP))
			{
				long nextTimeSec = -1L;

				while (evStop.waitForMillis(2000) == false)
				{
					if (Console.KeyAvailable)
					{
						if (Console.ReadKey(true).KeyChar == 0x1b)
						{
							Console.WriteLine("ESCAPE Pressed");
							throw new Ended();
						}
						else
							Console.WriteLine("Press ESCAPE to exit.");
					}
					long timeSec = DateTimeToSec.Now.getSec();

					if (nextTimeSec <= timeSec)
					{
#if true
						procRatePage();
						nextTimeSec = (timeSec / 20L + 1L) * 20L;
#else // old
                        if (procRatePage())
                        {
                            nextTimeSec = (timeSec / 60L + 1L) * 60L;
                        }
                        else
                        {
                            nextTimeSec = (timeSec / 20L + 1L) * 20L;
                        }
#endif
						GC.Collect();
					}
				}
			}
		}

		private static string getWRootDir()
		{
			string dir = @"C:\BlueFish\BlueFish\Fx\data";

			if (Directory.Exists(dir) == false)
				dir = @"C:\temp"; // devenv

			return dir;
		}

		private static bool procRatePage() // ret: ? 成功
		{
			try
			{
				parseRatePage(getRatePage());
				writeRatePage();
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine("ページの取得に失敗しました。" + e);
			}
			return false;
		}

		private static byte[] getRatePage()
		{
			HTTPClient hc = new HTTPClient(
				////////////////////////////////////////////// // $_git:secret
				);
			hc.connectionTimeoutMillis = 10000;
			hc.timeoutMillis = 15000;
			hc.noTrafficTimeoutMillis = 5000;
			hc.get();
			return hc.resBody;
		}

		private class CurrencyData
		{
			public string sOpen;
			public string sHigh;
			public string sLow;
			public string sAsk;
			public string sBid;
		}

		private static long parsedDateTime;
		private static Dictionary<string, CurrencyData> currencyPairs = DictionaryTools.create<CurrencyData>();

		private static void parseRatePage(byte[] resBody)
		{
			parsedDateTime = DateTimeToSec.Now.getDateTime();
			currencyPairs.Clear();

			ObjectMap root = (ObjectMap)JsonTools.decode(resBody);
			ObjectList olCurrencyPairs = (ObjectList)root["quotes"];

			foreach (object objCurrencyPair in olCurrencyPairs.getList())
			{
				ObjectMap currencyPair = (ObjectMap)objCurrencyPair;

				string sCode = (string)currencyPair["currencyPairCode"];
				string sOpen = (string)currencyPair["open"];
				string sHigh = (string)currencyPair["high"];
				string sLow = (string)currencyPair["low"];
				string sAsk = (string)currencyPair["ask"];
				string sBid = (string)currencyPair["bid"];

				if (StringTools.toFormat(sCode) != "AAAAAA")
					throw new Exception("sCode broken");

				if (!isPositiveRealNumber(sOpen))
					throw new Exception("sOpen broken");

				if (!isPositiveRealNumber(sHigh))
					throw new Exception("sHigh broken");

				if (!isPositiveRealNumber(sLow))
					throw new Exception("sLow broken");

				if (!isPositiveRealNumber(sAsk))
					throw new Exception("sAsk broken");

				if (!isPositiveRealNumber(sBid))
					throw new Exception("sBid broken");

				currencyPairs.Add(sCode, new CurrencyData()
				{
					sOpen = sOpen,
					sHigh = sHigh,
					sLow = sLow,
					sAsk = sAsk,
					sBid = sBid,
				});
			}
			if (currencyPairs.Count == 0)
				throw new Exception("通貨ペアが１つもありません。");
		}

		private static bool isPositiveRealNumber(string str)
		{
			str = StringTools.toDigitFormat(str, true);

			return
				str == "9" ||
				str == "9.9";
		}

		private static void writeRatePage()
		{
			long parsedDate = parsedDateTime / 1000000L;

			using (MutexObject.section(Consts.MTX_OUTPUT))
			{
				foreach (string code in currencyPairs.Keys)
				{
					CurrencyData i = currencyPairs[code];

					string wFile = Path.Combine(wRootDir, parsedDate + "_" + code + ".act");
					string line =
						parsedDateTime + "," +
						i.sOpen + "," +
						i.sHigh + "," +
						i.sLow + "," +
						i.sAsk + "," +
						i.sBid;

					Console.WriteLine("< " + line);
					Console.WriteLine("> " + wFile);

					File.AppendAllLines(wFile, new string[] { line }, Encoding.ASCII);

					Console.WriteLine("write-ok");
				}
			}
		}
	}
}
