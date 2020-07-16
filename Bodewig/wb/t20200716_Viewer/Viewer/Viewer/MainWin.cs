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

		/// <summary>
		/// 移動平均のプロット間隔 TTSec
		/// </summary>
		private int MaStep;

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

				Consts.TTSEC_END_MIN = TTCommon.DateTimeToTTSec(20000101000000);
				Consts.TTSEC_END_MAX = TTCommon.DateTimeToTTSec(DateTimeToSec.Now.GetDateTime());

				this.TTSecEnd = Consts.TTSEC_END_MAX;
				this.TTSecStep = Consts.TTSEC_STEP_MIN;

				{
					int maStep = InputStringDlgTools.Int(
						"移動平均プロット間隔入力",
						"移動平均のプロット間隔を秒単位で入力して下さい。",
						false,
						60,
						Consts.MA_STEP_MIN,
						Consts.MA_STEP_MAX,
						-1
						);

					if (maStep == -1)
						throw new Exception("maStep == -1");

					this.MaStep = maStep;
				}

				ChartSrvc.I.Macs = new ChartSrvc.MacInfo[]
				{
					ChartSrvc.MacInfo.Safe(86400 * 5, this.MaStep), // 5 days
					ChartSrvc.MacInfo.Safe(86400 * 25, this.MaStep), // 25 days
				};

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
					string value = string.Join(":", ChartSrvc.I.Macs.Select(v => StringUtils.SecToUIString(v.SecSpan)).ToArray());

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
						if (sec % this.MaStep != 0 || sec < Consts.MA_SEC_SPAN_MIN || Consts.MA_SEC_SPAN_MAX < sec)
							throw new Exception("不正なスパン：" + StringUtils.SecToUIString(sec));

					ChartSrvc.I.Macs = secs.Select(sec => new ChartSrvc.MacInfo(sec, this.MaStep)).ToArray();
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
				ca.AxisY.Minimum = maYMin;
				ca.AxisY.Maximum = maYMax;

				{
					double q = (ca.AxisX.Maximum - ca.AxisX.Minimum) / 6.0;

					double x1 = ca.AxisX.Minimum;
					double x2 = ca.AxisX.Minimum + q * 2.0;
					double x3 = ca.AxisX.Minimum + q * 4.0;
					double x4 = ca.AxisX.Maximum;

					double xVal1 = ca.AxisX.Minimum + q * 1.0;
					double xVal2 = ca.AxisX.Minimum + q * 3.0;
					double xVal3 = ca.AxisX.Minimum + q * 5.0;

					//ca.AxisX.CustomLabels.Add(new CustomLabel(x1, x2, xVal1.ToString("F3"), 0, LabelMarkStyle.None, GridTickTypes.All));
					//ca.AxisX.CustomLabels.Add(new CustomLabel(x2, x3, xVal2.ToString("F3"), 0, LabelMarkStyle.None, GridTickTypes.All));
					//ca.AxisX.CustomLabels.Add(new CustomLabel(x3, x4, xVal3.ToString("F3"), 0, LabelMarkStyle.None, GridTickTypes.All));

					long xSec1 = DoubleTools.ToLong(xVal1 * 86400.0);
					long xSec2 = DoubleTools.ToLong(xVal2 * 86400.0);
					long xSec3 = DoubleTools.ToLong(xVal3 * 86400.0);

					string xL1 = StringUtils.SecSpanToUIString(0);
					string xL2 = StringUtils.SecSpanToUIString(xSec2 - xSec1);
					string xL3 = StringUtils.SecSpanToUIString(xSec3 - xSec1);

					ca.AxisX.CustomLabels.Add(new CustomLabel(x1, x2, xL1, 0, LabelMarkStyle.None, GridTickTypes.All));
					ca.AxisX.CustomLabels.Add(new CustomLabel(x2, x3, xL2, 0, LabelMarkStyle.None, GridTickTypes.All));
					ca.AxisX.CustomLabels.Add(new CustomLabel(x3, x4, xL3, 0, LabelMarkStyle.None, GridTickTypes.All));
				}

				{
					double q = (ca.AxisY.Maximum - ca.AxisY.Minimum) / 6.0;

					double y1 = ca.AxisY.Minimum;
					double y2 = ca.AxisY.Minimum + q * 2.0;
					double y3 = ca.AxisY.Minimum + q * 4.0;
					double y4 = ca.AxisY.Maximum;

					double yVal1 = ca.AxisY.Minimum + q * 1.0;
					double yVal2 = ca.AxisY.Minimum + q * 3.0;
					double yVal3 = ca.AxisY.Minimum + q * 5.0;

					ca.AxisY.CustomLabels.Add(new CustomLabel(y1, y2, yVal1.ToString("F3"), 0, LabelMarkStyle.None, GridTickTypes.All));
					ca.AxisY.CustomLabels.Add(new CustomLabel(y2, y3, yVal2.ToString("F3"), 0, LabelMarkStyle.None, GridTickTypes.All));
					ca.AxisY.CustomLabels.Add(new CustomLabel(y3, y4, yVal3.ToString("F3"), 0, LabelMarkStyle.None, GridTickTypes.All));
				}

				this.MaChart.ChartAreas.Add(ca);
			}

			this.DmaChart.Series.Clear();
			this.DmaChart.Legends.Clear();
			this.DmaChart.ChartAreas.Clear();

			double dmaYMin = Consts.DMA_LOW_03;
			double dmaYMax = Consts.DMA_HIG_03;

			{
				Action<double, int, Color> drawHorizontallyLine = (y, width, color) =>
				{
					double x1 = ttSecStart / 86400.0;
					double x2 = this.TTSecEnd / 86400.0;

					Series srs = new Series();
					srs.ChartType = SeriesChartType.Line;
					srs.Color = color;
					srs.BorderWidth = width;

					srs.Points.AddXY(x1, y);
					srs.Points.AddXY(x2, y);

					this.DmaChart.Series.Add(srs);
				};

				drawHorizontallyLine(Consts.DMA_HIG_03, 7, Color.LightBlue);
				drawHorizontallyLine(Consts.DMA_HIG_02, 5, Color.LightBlue);
				drawHorizontallyLine(Consts.DMA_HIG_01, 3, Color.LightBlue);

				drawHorizontallyLine(0.0, 3, Color.LightGray);

				drawHorizontallyLine(Consts.DMA_LOW_01, 3, Color.LightPink);
				drawHorizontallyLine(Consts.DMA_LOW_02, 5, Color.LightPink);
				drawHorizontallyLine(Consts.DMA_LOW_03, 7, Color.LightPink);
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
				ca.AxisY.Minimum = dmaYMin;
				ca.AxisY.Maximum = dmaYMax;

				{
					double q = (ca.AxisX.Maximum - ca.AxisX.Minimum) / 6.0;

					double x1 = ca.AxisX.Minimum;
					double x2 = ca.AxisX.Minimum + q * 2.0;
					double x3 = ca.AxisX.Minimum + q * 4.0;
					double x4 = ca.AxisX.Maximum;

					double xVal1 = ca.AxisX.Minimum + q * 1.0;
					double xVal2 = ca.AxisX.Minimum + q * 3.0;
					double xVal3 = ca.AxisX.Minimum + q * 5.0;

					//ca.AxisX.CustomLabels.Add(new CustomLabel(x1, x2, xVal1.ToString("F3"), 0, LabelMarkStyle.None, GridTickTypes.All));
					//ca.AxisX.CustomLabels.Add(new CustomLabel(x2, x3, xVal2.ToString("F3"), 0, LabelMarkStyle.None, GridTickTypes.All));
					//ca.AxisX.CustomLabels.Add(new CustomLabel(x3, x4, xVal3.ToString("F3"), 0, LabelMarkStyle.None, GridTickTypes.All));

					string xL1 = DateTimeUnit.FromDateTime(TTCommon.TTSecToDateTime(DoubleTools.ToLong(xVal1 * 86400.0))).ToString();
					string xL2 = DateTimeUnit.FromDateTime(TTCommon.TTSecToDateTime(DoubleTools.ToLong(xVal2 * 86400.0))).ToString();
					string xL3 = DateTimeUnit.FromDateTime(TTCommon.TTSecToDateTime(DoubleTools.ToLong(xVal3 * 86400.0))).ToString();

					ca.AxisX.CustomLabels.Add(new CustomLabel(x1, x2, xL1, 0, LabelMarkStyle.None, GridTickTypes.All));
					ca.AxisX.CustomLabels.Add(new CustomLabel(x2, x3, xL2, 0, LabelMarkStyle.None, GridTickTypes.All));
					ca.AxisX.CustomLabels.Add(new CustomLabel(x3, x4, xL3, 0, LabelMarkStyle.None, GridTickTypes.All));
				}

				{
					double q = (ca.AxisY.Maximum - ca.AxisY.Minimum) / 6.0;

					double y1 = ca.AxisY.Minimum;
					double y2 = ca.AxisY.Minimum + q * 2.0;
					double y3 = ca.AxisY.Minimum + q * 4.0;
					double y4 = ca.AxisY.Maximum;

					double yVal1 = ca.AxisY.Minimum + q * 1.0;
					double yVal2 = ca.AxisY.Minimum + q * 3.0;
					double yVal3 = ca.AxisY.Minimum + q * 5.0;

					ca.AxisY.CustomLabels.Add(new CustomLabel(y1, y2, yVal1.ToString("F3"), 0, LabelMarkStyle.None, GridTickTypes.All));
					ca.AxisY.CustomLabels.Add(new CustomLabel(y2, y3, yVal2.ToString("F3"), 0, LabelMarkStyle.None, GridTickTypes.All));
					ca.AxisY.CustomLabels.Add(new CustomLabel(y3, y4, yVal3.ToString("F3"), 0, LabelMarkStyle.None, GridTickTypes.All));
				}

				this.DmaChart.ChartAreas.Add(ca);
			}

			{
				string text =
					 DateTimeUnit.FromDateTime(TTCommon.TTSecToDateTime(ttSecStart)).ToString() +
					 " ～ " +
					 DateTimeUnit.FromDateTime(TTCommon.TTSecToDateTime(this.TTSecEnd)).ToString() +
					 ", Range: " +
					 (this.TTSecEnd - ttSecStart) +
					 " sec (" +
					 ((this.TTSecEnd - ttSecStart) / 86400.0) +
					 " days), Step: " +
					 this.TTSecStep +
					 " sec, MA: { " +
					 string.Join(", ", ChartSrvc.I.Macs.Select(v => v.SecSpan + "s_" + (v.SecSpan / 86400.0).ToString("F3") + "d")) +
					 " } MA_Step: " +
					 this.MaStep +
					 " sec";

				if (this.South.Text != text)
					this.South.Text = text;
			}
		}

		private void MaChart_MouseMove(object sender, MouseEventArgs e)
		{
			this.UIEvent_Go(() =>
			{
				this.ChartMoveMove(this.MaChart, e.X, e.Y);
			});
		}

		private void DmaChart_MouseMove(object sender, MouseEventArgs e)
		{
			this.UIEvent_Go(() =>
			{
				this.ChartMoveMove(this.DmaChart, e.X, e.Y);
			});
		}

		private void ChartMoveMove(Chart chart, int x, int y)
		{
			try
			{
				double aX = chart.ChartAreas[0].AxisX.PixelPositionToValue(x);
				double aY = chart.ChartAreas[0].AxisY.PixelPositionToValue(y);

				long ttSec = DoubleTools.ToLong(aX * 86400.0);
				long dt = TTCommon.TTSecToDateTime(ttSec);

				this.TTip.SetToolTip(
					chart,
					DateTimeUnit.FromDateTime(dt).ToString() + "\n" + aY.ToString("F9")
					);
			}
			catch
			{ }
		}
	}
}
