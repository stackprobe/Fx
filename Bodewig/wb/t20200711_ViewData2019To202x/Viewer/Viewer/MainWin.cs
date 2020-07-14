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
using Charlotte.Utils;
using System.Windows.Forms.DataVisualization.Charting;
using Charlotte.TradingTimeChart;

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

		public MainWin()
		{
			InitializeComponent();

			this.MinimumSize = this.Size;

			this.CurrPair.SelectedIndex = 0;

			this.DateTimeSt.Text = "";
			this.DateTimeEd.Text = "";

			this.Status.Text = "";
			this.SubStatus.Text = "";
		}

		private void MainWin_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void MainWin_Shown(object sender, EventArgs e)
		{
			// -- 0001

			try
			{
				Ground.I = new Ground();
				Ground.I.LoadDataDir();

				this.CurrPair.SelectedIndex = 22; // USDJPY

				{
					long ed = Ground.I.Period_DateTimeEd;
					long st = DateTimeToSec.ToDateTime(DateTimeToSec.ToSec(ed) - 86400 * 5);

					this.DateTimeSt.Text = st.ToString();
					this.DateTimeEd.Text = ed.ToString();
				}

				this.CondChanged();
			}
			catch (Exception ex)
			{
				MessageDlgTools.Error(Program.APP_TITLE + " - Error @ Shown", ex);

				Environment.Exit(1);
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
					MessageDlgTools.Error(Program.APP_TITLE + " - Error @ CloseWindow()", e);
				}

				this.MTBusy.Enter();
				this.Close();
			}
		}

		private VisitorCounter MTBusy = new VisitorCounter(1);
		private long MTCount;

		private int CondChangedCount = -1;

		private GrphCond LastGrphCond = null;

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

				// ---- CondChanged

				if (this.CondChangedCount == 1) // Do Refresh
				{
					GrphCond cond = null;
					string statusText = "";

					try
					{
						cond = this.GetGrphCond();
					}
					catch (Exception ex)
					{
						statusText = ex.Message;
					}

					if (cond != null && (this.LastGrphCond == null || this.LastGrphCond.IsSame(cond) == false)) // ? グラフ更新の必要あり
					{
						this.LastGrphCond = cond; // DrawGrph()が例外を投げたとき何度もDrawGrph()しないよう、先に更新する。
						this.DrawGrph(cond);
					}

					if (this.Status.Text != statusText)
						this.Status.Text = statusText;
				}

				{
					string text = StringTools.Repeat("/", this.CondChangedCount);

					if (this.SubStatus.Text != text)
						this.SubStatus.Text = text;
				}

				if (0 < this.CondChangedCount)
					this.CondChangedCount--;

				// ----
			}
			catch (Exception ex)
			{
				MessageDlgTools.Error(Program.APP_TITLE + " - Error @ Timer", ex);
			}
			finally
			{
				this.MTBusy.Leave();
				this.MTCount++;
			}
		}

		private void MaGroup_Enter(object sender, EventArgs e)
		{
			// noop
		}

		private void MChart_Click(object sender, EventArgs e)
		{
			// noop
		}

		private void UIEvent_Go(Action routine)
		{
			using (this.MTBusy.Section())
			{
				try
				{
					routine();
				}
				catch (Exception ex)
				{
					MessageDlgTools.Error(Program.APP_TITLE + " - Error", ex);
				}
			}
		}

		private void CurrPair_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.UIEvent_Go(() =>
			{
				CondChanged();
			});
		}

		private void DateTimeSt_TextChanged(object sender, EventArgs e)
		{
			this.UIEvent_Go(() =>
			{
				CondChanged();
			});
		}

		private void DateTimeEd_TextChanged(object sender, EventArgs e)
		{
			this.UIEvent_Go(() =>
			{
				CondChanged();
			});
		}

		private void MaDay_XX_CheckedChanged(object sender, EventArgs e)
		{
			this.UIEvent_Go(() =>
			{
				CondChanged();
			});
		}

		private void BtnBack_Click(object sender, EventArgs e)
		{
			this.UIEvent_Go(() =>
			{
				{
					long sec1 = DateTimeToSec.ToSec(long.Parse(this.DateTimeSt.Text));
					long sec2 = DateTimeToSec.ToSec(long.Parse(this.DateTimeEd.Text));

					long span = sec2 - sec1;
					span = Math.Max(span, 0);
					sec1 -= span / 2;

					long st = DateTimeToSec.ToDateTime(sec1);
					st = Math.Max(st, Ground.I.Period_DateTimeSt);

					sec1 = DateTimeToSec.ToSec(st);
					sec2 = sec1 + span;

					long ed = DateTimeToSec.ToDateTime(sec2);
					ed = Math.Min(ed, Ground.I.Period_DateTimeEd);

					this.DateTimeSt.Text = st.ToString();
					this.DateTimeEd.Text = ed.ToString();
				}

				CondChanged_ByBtn();
			});
		}

		private void BtnForward_Click(object sender, EventArgs e)
		{
			this.UIEvent_Go(() =>
			{
				{
					long sec1 = DateTimeToSec.ToSec(long.Parse(this.DateTimeSt.Text));
					long sec2 = DateTimeToSec.ToSec(long.Parse(this.DateTimeEd.Text));

					long span = sec2 - sec1;
					span = Math.Max(span, 0);
					sec2 += span / 2;

					long ed = DateTimeToSec.ToDateTime(sec2);
					ed = Math.Min(ed, Ground.I.Period_DateTimeEd);

					sec2 = DateTimeToSec.ToSec(ed);
					sec1 = sec2 - span;

					long st = DateTimeToSec.ToDateTime(sec1);
					st = Math.Max(st, Ground.I.Period_DateTimeSt);

					this.DateTimeSt.Text = st.ToString();
					this.DateTimeEd.Text = ed.ToString();
				}

				CondChanged_ByBtn();
			});
		}

		/// <summary>
		/// 表示する期間を半分に
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnUnexpand_Click(object sender, EventArgs e)
		{
			this.UIEvent_Go(() =>
			{
				this.ExpandSpan(0.5);

				CondChanged_ByBtn();
			});
		}

		/// <summary>
		/// 表示する期間を倍に
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnExpand_Click(object sender, EventArgs e)
		{
			this.UIEvent_Go(() =>
			{
				this.ExpandSpan(2.0);

				CondChanged_ByBtn();
			});
		}

		private void ExpandSpan(double rate)
		{
			long sec1 = DateTimeToSec.ToSec(long.Parse(this.DateTimeSt.Text));
			long sec2 = DateTimeToSec.ToSec(long.Parse(this.DateTimeEd.Text));

			long span = sec2 - sec1;
			span = Math.Max(span, 120); // 2 min <=
			span = DoubleTools.ToLong(span * rate);
			sec1 = sec2 - span;

			long st = DateTimeToSec.ToDateTime(sec1);
			st = Math.Max(st, Ground.I.Period_DateTimeSt);

			sec1 = DateTimeToSec.ToSec(st);
			sec2 = sec1 + span;

			long ed = DateTimeToSec.ToDateTime(sec2);
			ed = Math.Min(ed, Ground.I.Period_DateTimeEd);

			this.DateTimeSt.Text = st.ToString();
			this.DateTimeEd.Text = ed.ToString();
		}

		private void CondChanged()
		{
			this.CondChangedCount = Consts.REFRESH_DELAY;
		}

		private void CondChanged_ByBtn()
		{
			this.CondChangedCount = Consts.REFRESH_DELAY_FAST;
		}

		private GrphCond GetGrphCond()
		{
			GrphCond cond = new GrphCond();

			{
				string currPair = this.CurrPair.SelectedItem.ToString();

				if (StringTools.LiteValidate(currPair, StringTools.ALPHA, 6, 6) == false)
					throw new Exception("通貨ペアが選択されていません。");

				cond.CurrPair = currPair;
			}

			{
				long st = long.Parse(this.DateTimeSt.Text);
				long ed = long.Parse(this.DateTimeEd.Text);

				if (DateTimeToSecUtils.IsFairDateTime(st) == false)
					throw new Exception("開始日時の書式エラー");

				if (DateTimeToSecUtils.IsFairDateTime(ed) == false)
					throw new Exception("終了日時の書式エラー");

				if (st < Ground.I.Period_DateTimeSt)
					throw new Exception("開始日時が古すぎる。");

				if (Ground.I.Period_DateTimeEd < ed)
					throw new Exception("終了日時が先すぎる。");

				if (ed <= st)
					throw new Exception("終了日時 <= 開始日時");

				cond.DateTimeSt = st;
				cond.DateTimeEd = ed;
			}

			CheckBox[] maDayCBs = GetMaDayCheckBoxes();

			{
				List<GrphCond.MaDayInfo> dest = new List<GrphCond.MaDayInfo>();

				for (int index = 0; index < maDayCBs.Length; index++)
				{
					CheckBox maDayCB = maDayCBs[index];

					if (maDayCB.Checked)
					{
						dest.Add(new GrphCond.MaDayInfo()
						{
							Index = index,
							Day = int.Parse(maDayCB.Tag.ToString()),
						});
					}
				}
				cond.MaDays = dest.ToArray();
			}

			return cond;
		}

		private CheckBox[] GetMaDayCheckBoxes()
		{
			return new CheckBox[]
			{
				this.MaDay_01, this.MaDay_02, this.MaDay_03, this.MaDay_04, this.MaDay_05,
				this.MaDay_06, this.MaDay_07, this.MaDay_08, this.MaDay_09, this.MaDay_10,
				this.MaDay_11, this.MaDay_12, this.MaDay_13, this.MaDay_14, this.MaDay_15,
				this.MaDay_16, this.MaDay_17, this.MaDay_18, this.MaDay_19, this.MaDay_20,
				this.MaDay_21, this.MaDay_22, this.MaDay_23, this.MaDay_24, this.MaDay_25,
				this.MaDay_26, this.MaDay_27, this.MaDay_28, this.MaDay_29, this.MaDay_30,
				this.MaDay_35, this.MaDay_40, this.MaDay_45, this.MaDay_50, this.MaDay_55,
				this.MaDay_60, this.MaDay_65, this.MaDay_70, this.MaDay_75, this.MaDay_80,
			};
		}

		private void DrawGrph(GrphCond cond)
		{
			if (Ground.I.GrphData == null || cond.CurrPair != Ground.I.GrphData.CurrPair) // ? グラフデータ更新の必要あり
			{
				Ground.I.GrphData = null;
				GC.Collect();

				BusyDlgTools.Show(Program.APP_TITLE, "グラフデータをロードしています。(通貨ペア：" + cond.CurrPair + ")", () =>
				{
					Ground.I.GrphData = new GrphData(cond.CurrPair);
				});

				GC.Collect();
			}

			Ground.I.GrphData.SetRange(cond.DateTimeSt, cond.DateTimeEd);

			// ---- グラフの描画ここから

			this.MChart.Series.Clear();
			this.MChart.ChartAreas.Clear();

			double yMin = double.MaxValue;
			double yMax = double.MinValue;

			// Low
			{
				Series srs = new Series();
				srs.ChartType = SeriesChartType.Line;
				srs.Color = Color.LightGray;
				srs.BorderWidth = 2;

				for (int index = Ground.I.GrphData.Start; index <= Ground.I.GrphData.End; index += Ground.I.GrphData.Step)
				{
					GrphData.PriceInfo price = Ground.I.GrphData.GetPrice(index);
					double low = price.Low;

					yMin = Math.Min(yMin, low);
					yMax = Math.Max(yMax, low);

					srs.Points.AddXY(price.TTSec / 86400.0, low);
				}
				this.MChart.Series.Add(srs);
			}

			// Hig
			{
				Series srs = new Series();
				srs.ChartType = SeriesChartType.Line;
				srs.Color = Color.LightGray;
				srs.BorderWidth = 2;

				for (int index = Ground.I.GrphData.Start; index <= Ground.I.GrphData.End; index += Ground.I.GrphData.Step)
				{
					GrphData.PriceInfo price = Ground.I.GrphData.GetPrice(index);
					double hig = price.Hig;

					yMin = Math.Min(yMin, hig);
					yMax = Math.Max(yMax, hig);

					srs.Points.AddXY(price.TTSec / 86400.0, hig);
				}
				this.MChart.Series.Add(srs);
			}

			// Mid
			{
				Series srs = new Series();
				srs.ChartType = SeriesChartType.Line;
				srs.Color = Color.Green;

				for (int index = Ground.I.GrphData.Start; index <= Ground.I.GrphData.End; index += Ground.I.GrphData.Step)
				{
					GrphData.PriceInfo price = Ground.I.GrphData.GetPrice(index);
					double mid = price.Mid;

					yMin = Math.Min(yMin, mid);
					yMax = Math.Max(yMax, mid);

					srs.Points.AddXY(price.TTSec / 86400.0, mid);
				}
				this.MChart.Series.Add(srs);
			}

			Color[] maColors = new Color[]
			{
				Color.Red,
				Color.Blue,
				Color.Purple,
				Color.DarkCyan,
				Color.DarkOrange,
			};

			for (int maIndex = 0; maIndex < cond.MaDays.Length; maIndex++)
			{
				GrphCond.MaDayInfo maDay = cond.MaDays[maIndex];
				int maColorIndex = Math.Min(maIndex, maColors.Length - 1);
				Color maColor = maColors[maColorIndex];

				// ma
				{
					Series srs = new Series();
					srs.ChartType = SeriesChartType.Line;
					srs.Color = maColor;

					for (int index = Ground.I.GrphData.Start; index <= Ground.I.GrphData.End; index += Ground.I.GrphData.Step)
					{
						GrphData.PriceInfo price = Ground.I.GrphData.GetPrice(index);
						double maVal = price.MaVals[maDay.Index];

						yMin = Math.Min(yMin, maVal);
						yMax = Math.Max(yMax, maVal);

						srs.Points.AddXY(price.TTSec / 86400.0, maVal);
					}
					this.MChart.Series.Add(srs);
				}
			}

			// ca
			{
				ChartArea ca = new ChartArea();

				ca.AxisX.Minimum = Ground.I.GrphData.GetPrice(Ground.I.GrphData.Start).TTSec / 86400.0;
				ca.AxisX.Maximum = Ground.I.GrphData.GetPrice(Ground.I.GrphData.End).TTSec / 86400.0;
				ca.AxisX.Interval = 1.0;
				ca.AxisY.Minimum = yMin;
				ca.AxisY.Maximum = yMax;

				this.MChart.ChartAreas.Add(ca);
			}

			// ---- グラフの描画ここまで
		}

		private int MCMM_LastX = int.MinValue;
		private int MCMM_LastY = int.MinValue;

		private void MChart_MouseMove(object sender, MouseEventArgs e)
		{
			int x = e.X;
			int y = e.Y;

			if (this.MCMM_LastX == x && this.MCMM_LastY == y)
				return;

			this.MCMM_LastX = x;
			this.MCMM_LastY = y;

			if (this.MChart.ChartAreas.Count == 0) // ? 未表示
				return;

			if (Ground.I.GrphData == null) // ? 未表示
				return;

			if (Ground.I.GrphData.Start == -1) // ? 未表示
				return;

			try
			{
				double aX = this.MChart.ChartAreas[0].AxisX.PixelPositionToValue(x);
				double aY = this.MChart.ChartAreas[0].AxisY.PixelPositionToValue(y);

				long ttSec = DoubleTools.ToLong(aX * 86400.0);
				long dt = TTCommon.TTSecToDateTime(ttSec);

				this.TTip.SetToolTip(
					this.MChart,
					DateTimeUnit.FromDateTime(dt).ToString() + "\n" + aY.ToString("F9")
					);
			}
			catch
			{ }
		}
	}
}
