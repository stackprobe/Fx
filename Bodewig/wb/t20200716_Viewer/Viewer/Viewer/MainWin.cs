using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Security.Permissions;
using System.Windows.Forms;
using Charlotte.Tools;
using Charlotte.Chocomint.Dialogs;
using Charlotte.TradingTimeChart;
using Charlotte.MovingAverageChart;
using Charlotte.Utils;
using System.Windows.Forms.DataVisualization.Charting;

namespace Charlotte
{
	public partial class MainWin : Form
	{
		#region ALT_F4 抑止

		private bool XPressed = false;

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			const int WM_SYSCOMMAND = 0x112;
			const long SC_CLOSE = 0xF060L;

			if (m.Msg == WM_SYSCOMMAND && (m.WParam.ToInt64() & 0xFFF0L) == SC_CLOSE)
			{
				this.XPressed = true;
				return;
			}
			base.WndProc(ref m);
		}

		#endregion

		private I2Size OrigSize;
		private I4Rect OrigChartsRect;

		public MainWin()
		{
			InitializeComponent();

			this.MinimumSize = this.Size;

			this.OrigSize = new I2Size(this.Width, this.Height);
			this.OrigChartsRect = I4Rect.LTRB(
				this.MaChart.Left,
				this.MaChart.Top,
				this.DmaChart.Right,
				this.DmaChart.Bottom
				);

			this.South.Text = "";
		}

		private void MainWin_Load(object sender, EventArgs e)
		{
			// noop
		}

		/// <summary>
		/// 表示期間の終端の TTSec
		/// </summary>
		private long TTSecEnd;

		/// <summary>
		/// 表示期間のプロット間隔 TTSec
		/// </summary>
		private long TTSecStep;

		private void MainWin_Shown(object sender, EventArgs e)
		{
			// -- 0001

			try
			{
				this.MainWin_Resize(null, null);

				{
					string currPair = InputComboDlgTools.Show(
						"通貨ペア選択",
						"通貨ペアを選択して下さい。",
						Consts.CurrPairs.Select(v => new InputComboDlgTools.Item<string>(v, v)),
						false,
						Consts.DefaultCurrPair
						);

					if (currPair == null)
						throw new Exception("currPair == null");

					ChartSrvc.I = new ChartSrvc(currPair);
				}

				ChartSrvc.I.Macs = new ChartSrvc.MacInfo[]
				{
					new ChartSrvc.MacInfo(60 * 24 * 5), // 5 days
					new ChartSrvc.MacInfo(60 * 24 * 25), // 25 days
				};

				Consts.TTSEC_END_MIN = TTCommon.DateTimeToTTSec(20000101000000);
				Consts.TTSEC_END_MAX = TTCommon.DateTimeToTTSec(DateTimeToSec.Now.GetDateTime());

				this.TTSecEnd = Consts.TTSEC_END_MAX;
				this.TTSecStep = Consts.TTSEC_STEP_MIN;

				this.DrawCharts();
			}
			catch (Exception ex)
			{
				MessageDlgTools.Error("Error @ Shown", ex);

				Environment.Exit(1); // fatal
			}

			// ----

			this.MTBusy.Leave();
		}

		private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.MTBusy.Enter(); // 2bs

			// ----

			// none

			// -- 9999
		}

		private void CloseWindow()
		{
			using (this.MTBusy.Section())
			{
				try
				{
					// -- 9000

					// none

					// ----
				}
				catch (Exception e)
				{
					MessageDlgTools.Error("Error @ CloseWindow()", e);
				}
				this.MTBusy.Enter();
				this.Close();
			}
		}

		private VisitorCounter MTBusy = new VisitorCounter(1);
		private long MTCount;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (this.MTBusy.HasVisitor())
				return;

			this.MTBusy.Enter();
			try
			{
				// -- 3001

				if (this.XPressed)
				{
					this.XPressed = false;
					this.CloseWindow();
					return;
				}
			}
			catch (Exception ex)
			{
				MessageDlgTools.Error("Error @ Timer", ex);
			}
			finally
			{
				this.MTBusy.Leave();
				this.MTCount++;
			}
		}

		private void UIEvent_Go(Action routine)
		{
			using (this.MTBusy.Section())
			{
				try
				{
					routine();
				}
				catch (Exception e)
				{
					MessageDlgTools.Error("Error @ UIEvent_Go()", e);
				}
			}
		}

		private void MainWin_Resize(object sender, EventArgs e)
		{
			UIEvent_Go(() =>
			{
				int diffX = this.Width - this.OrigSize.W;
				int diffY = this.Height - this.OrigSize.H;

				if (diffX < 0 || diffY < 0) // 最小化した場合
					return;

				int w = this.OrigChartsRect.W + diffX;
				int h = this.OrigChartsRect.H + diffY;

				int chartH = (h - 5) / 2;

				this.MaChart.Left = this.OrigChartsRect.L;
				this.MaChart.Top = this.OrigChartsRect.T;
				this.MaChart.Width = w;
				this.MaChart.Height = chartH;

				this.DmaChart.Left = this.OrigChartsRect.L;
				this.DmaChart.Top = this.OrigChartsRect.T + h - chartH;
				this.DmaChart.Width = w;
				this.DmaChart.Height = chartH;
			});
		}

		private void 終了ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UIEvent_Go(() =>
			{
				this.CloseWindow();
			});
		}

		private void 移動平均入力ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UIEvent_Go(() =>
			{
				try
				{
					string value = string.Join(":", ChartSrvc.I.Macs.Select(v => StringUtils.SecToUIString(v.Span * Consts.MA_STEP)).ToArray());

					value = InputStringDlgTools.Show(
						"移動平均入力",
						"移動平均の時間(NNd,NNh,NNm,NN)を[:]区切りで入力して下さい。例 3d:12h:90m:6660",
						false,
						value
						);

					if (value == null)
						return;

					int[] secs = value.Split(':').Select(v => StringUtils.UIStringToSec(v)).ToArray();

					if (Consts.MA_NUM_MAX < secs.Length)
						throw new Exception("多いよ！");

					foreach (int sec in secs)
						if (sec % Consts.MA_STEP != 0 || sec < Consts.MA_SPAN_MIN * Consts.MA_STEP || Consts.MA_SPAN_MAX * Consts.MA_STEP < sec)
							throw new Exception("不正なスパン：" + StringUtils.SecToUIString(sec));

					ChartSrvc.I.Macs = secs.Select(sec => new ChartSrvc.MacInfo(sec / Consts.MA_STEP)).ToArray();
				}
				catch (Exception ex)
				{
					MessageDlgTools.Warning("移動平均入力_失敗", ex);
				}
			});
		}

		private void MaChart_Click(object sender, EventArgs e)
		{
			// noop
		}

		private void DmaChart_Click(object sender, EventArgs e)
		{
			// noop
		}

		private void 過去へToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UIEvent_Go(() =>
			{
				this.TTSecEnd -= (this.TTSecStep * Consts.PLOT_NUM) / 4;
				this.TTSecEnd = Math.Max(this.TTSecEnd, Consts.TTSEC_END_MIN);

				this.DrawCharts();
			});
		}

		private void 未来へToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UIEvent_Go(() =>
			{
				this.TTSecEnd += (this.TTSecStep * Consts.PLOT_NUM) / 4;
				this.TTSecEnd = Math.Min(this.TTSecEnd, Consts.TTSEC_END_MAX);

				this.DrawCharts();
			});
		}

		private void 表示期間を拡大ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UIEvent_Go(() =>
			{
				this.TTSecStep *= 11;
				this.TTSecStep /= 10;
				this.TTSecStep = Math.Min(this.TTSecStep, Consts.TTSEC_STEP_MAX);

				this.DrawCharts();
			});
		}

		private void 表示期間を縮小ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UIEvent_Go(() =>
			{
				this.TTSecStep *= 10;
				this.TTSecStep /= 11;
				this.TTSecStep = Math.Max(this.TTSecStep, Consts.TTSEC_STEP_MIN);

				this.DrawCharts();
			});
		}

		private void DrawCharts()
		{
			long ttSecStart = this.TTSecEnd - this.TTSecStep * (Consts.PLOT_NUM - 1);

			this.MaChart.Series.Clear();
			this.MaChart.Legends.Clear();
			this.MaChart.ChartAreas.Clear();

			double maYMin = double.MaxValue;
			double maYMax = double.MinValue;

			// Low
			{
				Series srs = new Series();
				srs.ChartType = SeriesChartType.Line;
				srs.Color = Color.LightGray;
				srs.BorderWidth = 2;

				for (int index = 0; index < Consts.PLOT_NUM; index++)
				{
					long ttSec = this.TTSecEnd - this.TTSecStep * index;
					double y = ChartSrvc.I.Ttc.GetPrice(ttSec).Low;

					maYMin = Math.Min(maYMin, y);
					maYMax = Math.Max(maYMax, y);

					srs.Points.AddXY(ttSec / 86400.0, y);
				}
				this.MaChart.Series.Add(srs);
			}

			// Hig
			{
				Series srs = new Series();
				srs.ChartType = SeriesChartType.Line;
				srs.Color = Color.LightGray;
				srs.BorderWidth = 2;

				for (int index = 0; index < Consts.PLOT_NUM; index++)
				{
					long ttSec = this.TTSecEnd - this.TTSecStep * index;
					double y = ChartSrvc.I.Ttc.GetPrice(ttSec).Hig;

					maYMin = Math.Min(maYMin, y);
					maYMax = Math.Max(maYMax, y);

					srs.Points.AddXY(ttSec / 86400.0, y);
				}
				this.MaChart.Series.Add(srs);
			}

			// Mid
			{
				Series srs = new Series();
				srs.ChartType = SeriesChartType.Line;
				srs.Color = Color.Green;

				for (int index = 0; index < Consts.PLOT_NUM; index++)
				{
					long ttSec = this.TTSecEnd - this.TTSecStep * index;
					double y = ChartSrvc.I.Ttc.GetPrice(ttSec).Mid;

					maYMin = Math.Min(maYMin, y);
					maYMax = Math.Max(maYMax, y);

					srs.Points.AddXY(ttSec / 86400.0, y);
				}
				this.MaChart.Series.Add(srs);
			}

			for (int maIndex = 0; maIndex < ChartSrvc.I.Macs.Length; maIndex++)
			{
				ChartSrvc.MacInfo mac = ChartSrvc.I.Macs[maIndex];
				Color maColor = Consts.MaColors[maIndex];

				// ma
				{
					Series srs = new Series();
					srs.ChartType = SeriesChartType.Line;
					srs.Color = maColor;

					for (int index = 0; index < Consts.PLOT_NUM; index++)
					{
						long ttSec = this.TTSecEnd - this.TTSecStep * index;
						double y = mac.Mac.GetPrice(ttSec);

						maYMin = Math.Min(maYMin, y);
						maYMax = Math.Max(maYMax, y);

						srs.Points.AddXY(ttSec / 86400.0, y);
					}
					this.MaChart.Series.Add(srs);
				}
			}

			// ca
			{
				ChartArea ca = new ChartArea();

				ca.AxisX.Minimum = ttSecStart / 86400.0;
				ca.AxisX.Maximum = this.TTSecEnd / 86400.0;
				ca.AxisX.Interval = 1.0;
				ca.AxisY.Minimum = maYMin;
				ca.AxisY.Maximum = maYMax;

				this.MaChart.ChartAreas.Add(ca);
			}

			this.DmaChart.Series.Clear();
			this.DmaChart.Legends.Clear();
			this.DmaChart.ChartAreas.Clear();

			double dmaYMin = double.MaxValue;
			double dmaYMax = double.MinValue;

			for (int maIndex = 0; maIndex < ChartSrvc.I.Macs.Length; maIndex++)
			{
				ChartSrvc.MacInfo mac = ChartSrvc.I.Macs[maIndex];
				Color maColor = Consts.MaColors[maIndex];

				// ma
				{
					Series srs = new Series();
					srs.ChartType = SeriesChartType.Line;
					srs.Color = maColor;

					for (int index = 0; index < Consts.PLOT_NUM; index++)
					{
						long ttSec = this.TTSecEnd - this.TTSecStep * index;
						double prY = ChartSrvc.I.Ttc.GetPrice(ttSec).Mid;
						double maY = mac.Mac.GetPrice(ttSec);
						double y = (prY - maY) / maY;

						dmaYMin = Math.Min(dmaYMin, y);
						dmaYMax = Math.Max(dmaYMax, y);

						srs.Points.AddXY(ttSec / 86400.0, y);
					}
					this.DmaChart.Series.Add(srs);
				}
			}

			// ca
			{
				ChartArea ca = new ChartArea();

				ca.AxisX.Minimum = ttSecStart / 86400.0;
				ca.AxisX.Maximum = this.TTSecEnd / 86400.0;
				ca.AxisX.Interval = 1.0;
				ca.AxisY.Minimum = dmaYMin;
				ca.AxisY.Maximum = dmaYMax;

				this.DmaChart.ChartAreas.Add(ca);
			}
		}
	}
}
