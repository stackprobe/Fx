using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Charlotte.Tools;
using System.Windows.Forms.DataVisualization.Charting;

namespace Charlotte
{
	/// <summary>
	/// 
	/// フォーマット = 通貨ペア:日時:インターバル:期間:移動平均インターバル:移動平均_1:移動平均_2 ...
	/// 
	/// 移動平均は省略可能
	/// 日時 = YYYYMMDDhhmmss
	/// インターバル, 期間, 移動平均インターバル, 移動平均_x = 99d or 99h or 99m or 99
	/// 移動平均インターバル, 移動平均_x = 省略可能
	/// 
	/// Enter = グラフ更新
	/// Alt + 左 = 期間の(1/4)過去へ
	/// Alt + 右 = 期間の(1/4)未来へ
	/// Ctrl + 左 = Y軸をキープして、期間の(1/4)過去へ
	/// Ctrl + 右 = Y軸をキープして、期間の(1/4)未来へ
	/// Ctrl + 上 = Y軸を狭める
	/// Ctrl + 下 = Y軸を広げる
	/// Roll-Up = 期間を狭める
	/// Roll-Down = 期間を広げる
	/// 
	/// </summary>
	public partial class Win単純移動平均 : Form
	{
		public Win単純移動平均()
		{
			InitializeComponent();
		}

		private void Win単純移動平均_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void Args_TextChanged(object sender, EventArgs e)
		{
			// noop
		}

		private void MChart_Click(object sender, EventArgs e)
		{
			// noop
		}

		private void Args_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)13) // enter
			{
				executeArgs();
				e.Handled = true;
			}
		}

		private void Win単純移動平均_Shown(object sender, EventArgs e)
		{
			this.Args.Text = "USDJPY:" + DateTimeToSec.Now.getDateTime() + ":10m:7d:10m:5d:10d";
			//this.Args.Text = "USDJPY:" + DateTimeToSec.Now.getDateTime() + ":10m:7d:5d:10d"; // old
			//this.Args.Text = "USDJPY:" + DateTimeToSec.Now.getDateTime() + ":3m:7d:5d:10d"; // old
			//this.Args.Text = "USDJPY:" + DateTimeToSec.Now.getDateTime() + ":30:7d:5d:10d"; // old
			executeArgs();
		}

		private double LastAxYMin = 90.0;
		private double LastAxYMax = 110.0;
		private long LastStartWeSec = WeSec.SecToWeSec(DateTimeToSec.toSec(20170606000000L));

		private void executeArgs(bool keepAxY = false)
		{
			string args = this.Args.Text;

			try
			{
				args = StringTools.tokenize(args, "#")[0];

				// ---- args -> vars ----

				Queue<string> argq = new Queue<string>(StringTools.tokenize(args, ":"));

				CurrencyPair cPair = CurrencyPairs.All[CurrencyPairs.IndexOf(argq.Dequeue())];
				long dateTime = long.Parse(argq.Dequeue());

				if (DateTimeToSec.isFairDateTime(dateTime) == false)
					throw new Exception("Bad dateTime");

				long secStart = DateTimeToSec.toSec(dateTime);
				long secInterval = StringToSec(argq.Dequeue());
				long secBound = StringToSec(argq.Dequeue());
				long secMvAvgInterval = -1L;

				if (1 <= argq.Count) // 省略可能につき
					secMvAvgInterval = StringToSec(argq.Dequeue());

				List<long> secMvAvgs = new List<long>();

				while (1 <= argq.Count) // 省略可能につき
					secMvAvgs.Add(StringToSec(argq.Dequeue()));

				argq = null;

				if (secInterval < 1L)
					throw new Exception("interval < 1");

				if (secBound < secInterval)
					throw new Exception("bound < interval");

				if (secBound % secInterval != 0)
					throw new Exception("期間はインターバルの倍数にしてね！");

				if (Consts.SAMPLING_MAX < secBound / secInterval)
					throw new Exception("インターバル刻みすぎ！");

				if (Consts.SEC_BOUND_MAX < secBound)
					throw new Exception("期間長過ぎ！");

				//secBound /= secInterval;
				//secBound *= secInterval;

				// secMvAvgInterval ここでチェックしない。MovingAverage ctor に任せる。

				// ----

				//CurrencyPairs.FxCollect();

				this.MChart.Series.Clear();
				//this.MChart.Legends.Clear();
				this.MChart.ChartAreas.Clear();

				// ここから sec -> weSec に切り替えていく。

				secStart = WeSec.SecToWeSec(secStart);

				double minMid = double.MaxValue;
				double maxMid = double.MinValue;

				WeSecPrices wsp = new WeSecPrices(cPair);
				List<MovingAverage> mvAvgs = new List<MovingAverage>();

				foreach (long secMvAvg in secMvAvgs)
					mvAvgs.Add(new MovingAverage(wsp, secMvAvgInterval, secMvAvg));

				{
					Series srs = new Series();
					srs.ChartType = SeriesChartType.Line;
					srs.Color = Color.Green;

					for (long sec = 0; sec <= secBound; sec += secInterval)
					{
						double mid = wsp.GetPrice(secStart - sec).Mid;
						//double mid = cPair.GetPrice(DateTimeToSec.toDateTime(secStart - sec)).Mid;

						minMid = Math.Min(minMid, mid);
						maxMid = Math.Max(maxMid, mid);

						srs.Points.AddXY(-sec / 86400.0, mid);
					}
					this.MChart.Series.Add(srs);
				}

				Color[] MVAVG_COLORS = new Color[]
				{
					Color.Red,
					Color.Blue,
					Color.Purple,
					Color.DarkCyan,
					Color.DarkOrange,
					Color.DarkGray,
				};

				for (int index = 0; index < mvAvgs.Count; index++)
				{
					MovingAverage mvAvg = mvAvgs[index];

					Series srs = new Series();
					srs.ChartType = SeriesChartType.Line;
					srs.Color = MVAVG_COLORS[index % MVAVG_COLORS.Length];

					for (long sec = 0; ; sec += secMvAvgInterval)
					{
						double mid = mvAvg.GetMid(secStart - sec);
						//double mid = cPair.GetPrice(DateTimeToSec.toDateTime(secStart - sec)).Mid;

						minMid = Math.Min(minMid, mid);
						maxMid = Math.Max(maxMid, mid);

						srs.Points.AddXY(-sec / 86400.0, mid);

						if (secBound <= sec)
							break;
					}
					this.MChart.Series.Add(srs);
				}

				if (keepAxY)
				{
					minMid = LastAxYMin;
					maxMid = LastAxYMax;
				}
				else
				{
					LastAxYMin = minMid;
					LastAxYMax = maxMid;
				}
				LastStartWeSec = secStart;

				ChartArea ca = new ChartArea();

				{
					double METER_SCALE = 1000.0;

					minMid = (long)(minMid * METER_SCALE) / METER_SCALE;
					maxMid = ((long)(maxMid * METER_SCALE) + 1) / METER_SCALE;
				}

				double axInterval;

				if (secBound < 86400L)
					axInterval = 1.0 / 24.0;
				else if (secBound < 86400L * 3)
					axInterval = 1.0 / 2.0;
				else if (secBound < 86400L * 25)
					axInterval = 1.0;
				else if (secBound < 86400L * 100)
					axInterval = 5.0;
				else if (secBound < 86400L * 250)
					axInterval = 25.0;
				else if (secBound < 86400L * 500)
					axInterval = 50.0;
				else
					axInterval = 100.0;

				ca.AxisX.Minimum = -secBound / 86400.0;
				ca.AxisX.Maximum = 0.0;
				ca.AxisX.Interval = axInterval;
				ca.AxisY.Minimum = minMid;
				ca.AxisY.Maximum = maxMid;

				this.MChart.ChartAreas.Add(ca);

				// ----

				// 成功

				this.Args.ForeColor = Color.Black;
				this.Args.Text = args;
				this.Args.SelectAll();
			}
			catch (Exception e)
			{
				// 失敗

				this.TTip.SetToolTip(this.Args, "" + e);

				this.Args.ForeColor = Color.Red;
				this.Args.Text = args + "#" + e.Message;
				this.Args.SelectionStart = 0;
				this.Args.SelectionLength = args.Length;
			}
			GC.Collect(); // 2bs
		}

		private long StringToSec(string str)
		{
			long ret = StringToSec_Main(str);

			if (ret < 1L || Consts.SEC_BOUND_MAX < ret)
				throw new Exception("Bad sec:" + ret);

			return ret;
		}

		private long StringToSec_Main(string str)
		{
			str = str.ToLower();

			if (str.EndsWith("d"))
			{
				str = str.Substring(0, str.Length - 1);
				return long.Parse(str) * 86400L;
			}
			if (str.EndsWith("h"))
			{
				str = str.Substring(0, str.Length - 1);
				return long.Parse(str) * 3600L;
			}
			if (str.EndsWith("m"))
			{
				str = str.Substring(0, str.Length - 1);
				return long.Parse(str) * 60L;
			}
			return long.Parse(str);
		}

		private string SecToString(long sec)
		{
			if (sec < 1L || Consts.SEC_BOUND_MAX < sec)
				throw new Exception("Bad sec:" + sec);

			if (sec % 86400L == 0L)
				return (sec / 86400L) + "d";

			if (sec % 3600L == 0L)
				return (sec / 3600L) + "h";

			if (sec % 60L == 0L)
				return (sec / 60L) + "m";

			return "" + sec;
		}

		private void Args_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Alt && e.KeyValue == 37) // Alt + 左
			{
				DoAltCommand("戻る");
				e.Handled = true;
				return;
			}
			if (e.Alt && e.KeyValue == 39) // Alt + 右
			{
				DoAltCommand("進む");
				e.Handled = true;
				return;
			}
			if (e.Control && e.KeyValue == 37) // Ctrl + 左
			{
				DoAltCommand("戻る", true);
				e.Handled = true;
				return;
			}
			if (e.Control && e.KeyValue == 39) // Ctrl + 右
			{
				DoAltCommand("進む", true);
				e.Handled = true;
				return;
			}
			if (e.Control && e.KeyValue == 38) // Ctrl + 上
			{
				DoAltCommand("Y-拡大");
				e.Handled = true;
				return;
			}
			if (e.Control && e.KeyValue == 40) // Ctrl + 下
			{
				DoAltCommand("Y-縮小");
				e.Handled = true;
				return;
			}
			if (e.KeyValue == 33) // Roll-Up
			{
				DoAltCommand("拡大");
				e.Handled = true;
				return;
			}
			if (e.KeyValue == 34) // Roll-Down
			{
				DoAltCommand("縮小");
				e.Handled = true;
				return;
			}
		}

		private void DoAltCommand(string command, bool keepAxY = false)
		{
			try
			{
				string args = this.Args.Text;
				List<string> tokens = StringTools.tokenize(args, ":");
				long dateTime = long.Parse(tokens[1]);
				long sec = DateTimeToSec.toSec(dateTime);
				long weSec = WeSec.SecToWeSec(sec);

				switch (command)
				{
					case "戻る":
						{
							long secBound = this.StringToSec(tokens[3]);

							weSec -= secBound / 4;
						}
						break;

					case "進む":
						{
							long secBound = this.StringToSec(tokens[3]);

							weSec += secBound / 4;
						}
						break;

					case "Y-拡大":
						{
							expandAxY(1.0 / 1.5);
							keepAxY = true;
						}
						break;

					case "Y-縮小":
						{
							expandAxY(1.5);
							keepAxY = true;
						}
						break;

					case "拡大":
						{
							long secInterval = this.StringToSec(tokens[2]);
							long secBound = this.StringToSec(tokens[3]);

							secInterval /= 2;
							secBound /= 2;

							// 調整 TODO
							{
								if (secBound % secInterval != 0)
								{
									secBound /= secInterval;
									secBound *= secInterval;
								}
							}

							tokens[2] = this.SecToString(secInterval);
							tokens[3] = this.SecToString(secBound);
						}
						break;

					case "縮小":
						{
							long secInterval = this.StringToSec(tokens[2]);
							long secBound = this.StringToSec(tokens[3]);

							secInterval *= 2;
							secBound *= 2;

							tokens[2] = this.SecToString(secInterval);
							tokens[3] = this.SecToString(secBound);
						}
						break;

					default:
						throw null;
				}

				sec = WeSec.WeSecToSec(weSec);
				dateTime = DateTimeToSec.toDateTime(sec);
				tokens[1] = "" + dateTime;
				args = string.Join(":", tokens);
				this.Args.Text = args;

				executeArgs(keepAxY);
			}
			catch
			{ }
		}

		private void expandAxY(double rate)
		{
			double center = (LastAxYMin + LastAxYMax) / 2.0;

			LastAxYMin -= center;
			LastAxYMin *= rate;
			LastAxYMin += center;

			LastAxYMax -= center;
			LastAxYMax *= rate;
			LastAxYMax += center;
		}

		private int MCMM_LastX;
		private int MCMM_LastY;

		private void MChart_MouseMove(object sender, MouseEventArgs e)
		{
			int x = e.X;
			int y = e.Y;

			if (this.MCMM_LastX == x && this.MCMM_LastY == y)
				return;

			this.MCMM_LastX = x;
			this.MCMM_LastY = y;

			try
			{
				double aX = this.MChart.ChartAreas[0].AxisX.PixelPositionToValue(x);
				double aY = this.MChart.ChartAreas[0].AxisY.PixelPositionToValue(y);

				long sec = this.LastStartWeSec + LongTools.toInt(aX * 86400.0);
				sec = WeSec.WeSecToSec(sec);
				long dateTime = DateTimeToSec.toDateTime(sec);

				this.TTip.SetToolTip(
					this.MChart,
					Utils.DateTimeToString(dateTime) + "\n" +
					aY
					);
			}
			catch
			{ }
		}
	}
}
