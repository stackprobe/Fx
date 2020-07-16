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

		private void MainWin_Shown(object sender, EventArgs e)
		{
			// -- 0001

			try
			{
				this.MainWin_Resize(null, null);

				// TODO
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

		private void 過去へToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UIEvent_Go(() =>
			{
				// TODO
			});
		}

		private void 未来へToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UIEvent_Go(() =>
			{
				// TODO
			});
		}

		private void 表示期間を拡大ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UIEvent_Go(() =>
			{
				// TODO
			});
		}

		private void 表示期間を縮小ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UIEvent_Go(() =>
			{
				// TODO
			});
		}
	}
}
