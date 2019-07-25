using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace YahooFinanceMonitor
{
	public class Program
	{
		private const string MUTEX_NAME_OUT_FILE = "{b560234e-3af9-41cb-a51c-81c7d0506cd7}"; // shared_uuid
		private static Mutex _mutexOutFile;

		static void Main(string[] args)
		{
			bool 多重起動した = false;

			_mutexOutFile = new Mutex(false, MUTEX_NAME_OUT_FILE);

			try
			{
				using (Mutex mtx = new Mutex(false, "{1cd8c4e1-35f6-4826-b694-37b5e6ff0a54}"))
				{
					if (mtx.WaitOne(0) == false)
					{
						多重起動した = true;
						throw new Exception("多重起動");
					}
					new Program().Main2();
					mtx.ReleaseMutex();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			_mutexOutFile.Dispose();
			_mutexOutFile = null;

			if (多重起動した)
			{
				Console.WriteLine("\\e_10s");
				Thread.Sleep(10000);
			}
			else
			{
				Console.WriteLine("\\e");
				Console.ReadLine();
			}
		}

		private void Main2()
		{
			//this.Test01();
			//this.Test02();
			//this.Test03();
			this.Test04();
		}

		private void Test01()
		{
			Console.WriteLine(
				Encoding.GetEncoding("UTF-8").GetString(new HttpClient(
					//////////////////////////////////////////////////////////////// $_git:secret
					).Perform()));
			Console.ReadLine();
		}

		private void Test02()
		{
			const int CYCLE_SEC = 20;
			long next_t = (TimeData.Now.T / CYCLE_SEC + 1) * CYCLE_SEC;

			for (; ; )
			{
				WrLine("----");
				WrLine("日時: " + TimeData.Now);

				// TEST_日本円/USドル
				try
				{
					byte[] resBody = new HttpClient(
						//////////////////////////////////////////////////////////////// $_git:secret
						).Perform();
					WrLine("resBody_length: " + resBody.Length);
					string strResBody = Encoding.UTF8.GetString(resBody);
					WrLine("strResBody_length: " + strResBody.Length);

					{
						int bgn = StringTools.ExTailIndexOf(strResBody, "<td class=\"newest\">");
						int end = StringTools.ExIndexOf(strResBody, "<span>", bgn);

						string part = strResBody.Substring(bgn, end - bgn);

						WrLine("$TEST_JPYUSD:" + TimeData.Now.GetCompactString() + ":" + part);
					}
				}
				catch (Exception e)
				{
					WrLine("" + e);
				}

				// 日本円/USドル
				try
				{
					byte[] resBody = new HttpClient(
						/////////////////////////////////////////////////////////////////// $_git:secret
						).Perform();
					WrLine("resBody_length: " + resBody.Length);
					string strResBody = Encoding.UTF8.GetString(resBody);
					WrLine("strResBody_length: " + strResBody.Length);

					{
						string part = TagData.GetTagData(strResBody, "<td class=\"stoksPrice\">", "</td>").GetInnerString();

						WrLine("$JPYUSD:" + TimeData.Now.GetCompactString() + ":" + part);
					}
				}
				catch (Exception e)
				{
					WrLine("" + e);
				}

				// 日本円/ユーロ
				try
				{
					byte[] resBody = new HttpClient(
						/////////////////////////////////////////////////////////////////// $_git:secret
						).Perform();
					WrLine("resBody_length: " + resBody.Length);
					string strResBody = Encoding.UTF8.GetString(resBody);
					WrLine("strResBody_length: " + strResBody.Length);

					{
						string part = TagData.GetTagData(strResBody, "<td class=\"stoksPrice\">", "</td>").GetInnerString();

						WrLine("$JPYEUR:" + TimeData.Now.GetCompactString() + ":" + part);
					}
				}
				catch (Exception e)
				{
					WrLine("" + e);
				}

				FxDetail("USDJPY");
				FxDetail("EURJPY");

				while (TimeData.Now.T < next_t)
				{
					while (Console.KeyAvailable)
					{
						if (Console.ReadKey().KeyChar == 0x1b) // ? esc
						{
							throw new Exception("プロセス停止");
						}
					}
					Thread.Sleep(2000);
				}
				next_t += CYCLE_SEC;
			}
		}

		private static void FxDetail(string code) // code: "USDJPY" etc.
		{
			try
			{
				byte[] resBody = new HttpClient(
					//////////////////////////////////////////////////////////////////// $_git:secret
					).Perform();
				WrLine("resBody_length: " + resBody.Length);
				string strResBody = Encoding.UTF8.GetString(resBody);
				WrLine("strResBody_length: " + strResBody.Length);

				string stamp = TimeData.Now.GetCompactString();

				{
					string part = TagData.GetTagData(strResBody, "<dd id=\"" + code + "_detail_bid\">", "</dd>").GetInnerString();
					part = RemoveTag(part);

					WrLine("$" + code + "_BID:" + stamp + ":" + part);
				}

				{
					string part = TagData.GetTagData(strResBody, "<dd id=\"" + code + "_detail_ask\">", "</dd>").GetInnerString();
					part = RemoveTag(part);

					WrLine("$" + code + "_ASK:" + stamp + ":" + part);
				}

				{
					string part = TagData.GetTagData(strResBody, "<dd id=\"" + code + "_detail_change\">", "</dd>").GetInnerString();

					WrLine("$" + code + "_CHANGE:" + stamp + ":" + part);
				}

				{
					string part = TagData.GetTagData(strResBody, "<dd id=\"" + code + "_detail_open\">", "</dd>").GetInnerString();
					part = RemoveTag(part);

					WrLine("$" + code + "_OPEN:" + stamp + ":" + part);
				}

				{
					string part = TagData.GetTagData(strResBody, "<dd id=\"" + code + "_detail_high\">", "</dd>").GetInnerString();
					part = RemoveTag(part);

					WrLine("$" + code + "_HIGH:" + stamp + ":" + part);
				}

				{
					string part = TagData.GetTagData(strResBody, "<dd id=\"" + code + "_detail_low\">", "</dd>").GetInnerString();
					part = RemoveTag(part);

					WrLine("$" + code + "_LOW:" + stamp + ":" + part);
				}

				{
					string part = TagData.GetTagData(strResBody, "<div class=\"buyBox clearFix\">", "</div>").GetInnerString();
					part = TagData.GetTagData(part, "<strong>", "</strong>").GetInnerString();

					WrLine("$" + code + "_BUYPCT:" + stamp + ":" + part);
				}

				{
					string part = TagData.GetTagData(strResBody, "<div class=\"sellBox clearFix\">", "</div>").GetInnerString();
					part = TagData.GetTagData(part, "<strong>", "</strong>").GetInnerString();

					WrLine("$" + code + "_SELLPCT:" + stamp + ":" + part);
				}

				{
					string part = TagData.GetTagData(strResBody, "<div class=\"box\">", "</div>").GetInnerString();
					part = TagData.GetTagData(part, "<span>", "</span>").GetInnerString();

					WrLine("$" + code + "_BUYNUM:" + stamp + ":" + part);
				}

				{
					string part = TagData.GetTagData(strResBody, "<div class=\"box right\">", "</div>").GetInnerString();
					part = TagData.GetTagData(part, "<span>", "</span>").GetInnerString();

					WrLine("$" + code + "_SELLNUM:" + stamp + ":" + part);
				}
			}
			catch (Exception e)
			{
				WrLine("" + e);
			}
		}

		private static void WrLine(string line)
		{
			Console.WriteLine(line);

			using (FileStream afs = new FileStream(@"C:\temp\YFM_Output.txt", FileMode.Append, FileAccess.Write))
			{
				byte[] binLine = Encoding.UTF8.GetBytes(line + "\n");

				afs.Write(binLine, 0, binLine.Length);
			}
		}

		private static string RemoveTag(string str)
		{
			for (; ; )
			{
				TagData td = TagData.GetTagData(str, "<", ">");

				if (td == null)
					break;

				str = str.Substring(0, td.BgnPP.BgnPos) + str.Substring(td.EndPP.EndPos);
			}
			return str;
		}

		private TimeData T3_Now;
		private string T3_ResBody;

		private void Test03()
		{
			const int CYCLE_SEC = 2;

			for (; ; )
			{
				long next_t = (TimeData.Now.T / CYCLE_SEC + 1) * CYCLE_SEC;

				try
				{
					T3_Now = TimeData.Now;

					byte[] binResBody = new HttpClient(
						///////////////////////////////////////////// $_git:secret
						).Perform();
					string resBody = Encoding.UTF8.GetString(binResBody);

					T3_ResBody = resBody;

					FxList("USDJPY");
					FxList("EURJPY");
					FxList("AUDJPY");
					FxList("GBPJPY");
					FxList("NZDJPY");
					FxList("CADJPY");
					FxList("CHFJPY");
					FxList("ZARJPY");
					FxList("CNHJPY");
					FxList("EURUSD");
					FxList("GBPUSD");
					FxList("AUDUSD");
					FxList("NZDUSD");
					FxList("HKDJPY");
					FxList("EURGBP");
					FxList("EURAUD");
					FxList("USDCHF");
					FxList("EURCHF");
					FxList("GBPCHF");
					FxList("AUDCHF");
					FxList("CADCHF");
					FxList("USDHKD");
				}
				catch (Exception e)
				{
					WrLine(
						T3_Now.GetCompactString() + ",Exception," +
						e.Message
						);
				}

				while (TimeData.Now.T < next_t)
				{
					while (Console.KeyAvailable)
					{
						if (Console.ReadKey().KeyChar == 0x1b) // ? esc
						{
							throw new Exception("プロセス停止");
						}
					}
					Thread.Sleep(100);
				}
			}
		}

		private void FxList(string code)
		{
			TagData bid = TagData.GetTagData(this.T3_ResBody, "<span id=\"" + code + "_chart_bid\">", "</span>");
			TagData ask = TagData.GetTagData(this.T3_ResBody, "<span id=\"" + code + "_chart_ask\">", "</span>");

			double d_bid = double.Parse(bid.GetInnerString());
			double d_ask = double.Parse(ask.GetInnerString());

			WrLine(
				T3_Now.GetCompactString() + "," +
				code + "," +
				d_bid + "," +
				d_ask
				);
		}

		private string T4_OutDir;
		private TimeData T4_Now;
		private string T4_ResBody;
		private string T4_Code;

		private void Test04()
		{
			const int CYCLE_SEC = 2;
			//const int CYCLE_SEC = 10;

			T4_OutDir = @"C:\pub\Fx";

			if (Directory.Exists(T4_OutDir) == false)
			{
				T4_OutDir = @"C:\temp";
			}

			for (; ; )
			{
				T4_Now = new TimeData((TimeData.Now.T / CYCLE_SEC + 1) * CYCLE_SEC);

				while (TimeData.Now.T < T4_Now.T)
				{
					while (Console.KeyAvailable)
					{
						if (Console.ReadKey().KeyChar == 0x1b) // ? esc
						{
							throw new Exception("プロセス停止");
						}
					}
					Thread.Sleep(100);
				}

				try
				{
					byte[] binResBody = new HttpClient(
						///////////////////////////////////////////// $_git:secret
						).Perform();
					string resBody = Encoding.UTF8.GetString(binResBody);

					T4_ResBody = resBody;

					T4_FxList("USDJPY");
					T4_FxList("EURJPY");
					T4_FxList("AUDJPY");
					T4_FxList("GBPJPY");
					T4_FxList("NZDJPY");
					T4_FxList("CADJPY");
					T4_FxList("CHFJPY");
					T4_FxList("ZARJPY");
					T4_FxList("CNHJPY");
					T4_FxList("EURUSD");
					T4_FxList("GBPUSD");
					T4_FxList("AUDUSD");
					T4_FxList("NZDUSD");
					T4_FxList("HKDJPY");
					T4_FxList("EURGBP");
					T4_FxList("EURAUD");
					T4_FxList("USDCHF");
					T4_FxList("EURCHF");
					T4_FxList("GBPCHF");
					T4_FxList("AUDCHF");
					T4_FxList("CADCHF");
					T4_FxList("USDHKD");
				}
				catch (Exception e)
				{
					T4_Error(e);
				}
			}
		}

		private void T4_FxList(string code)
		{
			Console.WriteLine("code: " + code);

			T4_Code = code;

			TagData bid = TagData.GetTagData(this.T4_ResBody, "<span id=\"" + code + "_chart_bid\">", "</span>");
			TagData ask = TagData.GetTagData(this.T4_ResBody, "<span id=\"" + code + "_chart_ask\">", "</span>");

			//double d_bid = double.Parse(bid.GetInnerString());
			//double d_ask = double.Parse(ask.GetInnerString());
			string s_bid = bid.GetInnerString();
			string s_ask = ask.GetInnerString();

			Check_BID_ASK_String(s_bid);
			Check_BID_ASK_String(s_ask);

			using (new MutexSection(_mutexOutFile))
			using (FileStream afs = new FileStream(
				Path.Combine(T4_OutDir, T4_Now.GetString("YMD") + "_" + code + ".csv"),
				FileMode.Append,
				FileAccess.Write
				))
			{
				WrLine(afs, T4_Now.GetCompactString() + "," + s_bid + "," + s_ask);
			}
		}

		private void T4_Error(Exception e)
		{
			using (new MutexSection(_mutexOutFile))
			using (FileStream afs = new FileStream(
				Path.Combine(T4_OutDir, T4_Now.GetString("YMD") + "_Error.txt"),
				FileMode.Append,
				FileAccess.Write
				))
			{
				WrLine(afs, "====");
				WrLine(afs, "[" + T4_Now + "]");
				WrLine(afs, "" + e);
				WrLine(afs, "" + T4_ResBody);
				WrLine(afs, "" + T4_Code);
				WrLine(afs, "");
			}
		}

		private void WrLine(FileStream fs, string line)
		{
			Console.WriteLine(line);

			{
				byte[] binLine = Encoding.UTF8.GetBytes(line + "\n");

				fs.Write(binLine, 0, binLine.Length);
			}
		}

		private void Check_BID_ASK_String(string str)
		{
			str = StringTools.ReplaceChar(str, "012345678", '9');
			str = StringTools.ReplaceLoop(str, "99", "9", 10);

			if (str == "9") // ? ok
				return;

			if (str == "9.9") // ? ok
				return;

			throw new Exception("BID_ASKフォーマットエラー");
		}
	}
}
