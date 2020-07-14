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

			this.CurrPair.SelectedIndex = 22; // USDJPY

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

			// -- 9999
		}

		private void CloseWindow()
		{
			using (this.MTBusy.Section())
			{
				try
				{
					// -- 9000

					// ----
				}
				catch (Exception e)
				{
					MessageBox.Show("" + e, "Error @ CloseWindow()", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
				ProcMain.WriteLog(ex);
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

		private void MainChart_Click(object sender, EventArgs e)
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
				// TODO
			});
		}

		private void DateTimeSt_TextChanged(object sender, EventArgs e)
		{
			this.UIEvent_Go(() =>
			{
				// TODO
			});
		}

		private void DateTimeEd_TextChanged(object sender, EventArgs e)
		{
			this.UIEvent_Go(() =>
			{
				// TODO
			});
		}

		private void MaDay_XX_CheckedChanged(object sender, EventArgs e)
		{
			this.UIEvent_Go(() =>
			{
				// TODO
			});
		}

		private void BtnBack_Click(object sender, EventArgs e)
		{
			this.UIEvent_Go(() =>
			{
				// TODO
			});
		}

		private void BtnForward_Click(object sender, EventArgs e)
		{
			this.UIEvent_Go(() =>
			{
				// TODO
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
			});
		}
	}
}
