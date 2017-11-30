using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Charlotte.Tools;

namespace Charlotte
{
	public partial class MainWin : Form
	{
		public MainWin()
		{
			InitializeComponent();
		}

		private void MainWin_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void MainWin_Shown(object sender, EventArgs e)
		{
			//this.MChart.Legends.Clear(); // 凡例を消す。-- プロパティから消せる。

			foreach (CurrencyPair cPair in CurrencyPairs.All)
			{
				CurrencyPair cPairTmp = cPair;

				this.通貨ペアToolStripMenuItem.DropDownItems.Add(cPair.Code, null, delegate
				{
					LoadGraph(cPairTmp);
				});
			}

			//CurrencyPairs.FxCollect(); // 時間が掛かるようになったので、勝手に呼ばないようにした。@ 2017.7.2

			//LoadGraph(CurrencyPairs.USDJPY);
		}

		private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			// noop
		}

		private void MChart_Click(object sender, EventArgs e)
		{
			// noop
		}

		private void LoadGraph(CurrencyPair cPair)
		{
			//CurrencyPairs.FxCollect();

			this.MChart.Series.Clear();
			this.MChart.Legends.Clear();
			this.MChart.ChartAreas.Clear();

			double minMid = double.MaxValue;
			double maxMid = double.MinValue;

			long currSec = DateTimeToSec.Now.getSec();

			Series srs = new Series();
			srs.ChartType = SeriesChartType.Line;
			srs.Color = Color.OrangeRed;

			for (long sec = currSec - 86400L * 7L; sec <= currSec; sec += 30L)
			{
				double mid = cPair.GetPrice(DateTimeToSec.toDateTime(sec)).Mid;

				minMid = Math.Min(minMid, mid);
				maxMid = Math.Max(maxMid, mid);

				srs.Points.AddXY((sec - currSec) / 86400.0, mid);
			}
			this.MChart.Series.Add(srs);

			ChartArea ca = new ChartArea();

			{
				double METER_SCALE = 1000.0;

				minMid = (long)(minMid * METER_SCALE) / METER_SCALE;
				maxMid = ((long)(maxMid * METER_SCALE) + 1) / METER_SCALE;
			}

			ca.AxisX.Minimum = -7.0;
			ca.AxisX.Maximum = 0.0;
			ca.AxisX.Interval = 1.0;
			ca.AxisY.Minimum = minMid;
			ca.AxisY.Maximum = maxMid;

			//ca.BorderWidth = 0;

			this.MChart.ChartAreas.Add(ca);

			//this.MChart.Margin = new Padding(0);
			//this.MChart.BorderlineDashStyle = ChartDashStyle.NotSet;
			//this.MChart.BorderlineWidth = 0;

			this.Text = Program.APP_TITLE + " - " + cPair.Code;
		}

		private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			// noop
		}

		private void 単純移動平均ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new Win単純移動平均().Show();
		}

		private void fxCollectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CurrencyPairs.FxCollect();
		}

		private void debugWinWeSecToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new DebugWin_WeSec().Show();
		}
	}
}
