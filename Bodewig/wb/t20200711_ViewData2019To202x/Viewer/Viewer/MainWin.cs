﻿using System;
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

					if (cond != null && (this.LastGrphCond == null || this.LastGrphCond.IsSame(cond)))
					{
						this.LastGrphCond = cond; // DrawGrph()が例外を投げたとき何度もDrawGrph()しないように、先に更新する。
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
				// TODO

				CondChanged_Fast();
			});
		}

		private void BtnForward_Click(object sender, EventArgs e)
		{
			this.UIEvent_Go(() =>
			{
				// TODO

				CondChanged_Fast();
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
				// TODO

				CondChanged_Fast();
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
				// TODO

				CondChanged_Fast();
			});
		}

		private void CondChanged()
		{
			this.CondChangedCount = Consts.REFRESH_DELAY;
		}

		private void CondChanged_Fast()
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
			this.MChart.Series.Clear();
			this.MChart.ChartAreas.Clear();

			// TODO
		}
	}
}
