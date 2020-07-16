namespace Charlotte
{
	partial class MainWin
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWin));
			this.MainTimer = new System.Windows.Forms.Timer(this.components);
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.South = new System.Windows.Forms.ToolStripStatusLabel();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.appToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.終了ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.commandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.過去へToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.未来へToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.表示期間を拡大ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.表示期間を縮小ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MaChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.DmaChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.statusStrip1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.MaChart)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DmaChart)).BeginInit();
			this.SuspendLayout();
			// 
			// MainTimer
			// 
			this.MainTimer.Enabled = true;
			this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.South});
			this.statusStrip1.Location = new System.Drawing.Point(0, 539);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(784, 22);
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// South
			// 
			this.South.Name = "South";
			this.South.Size = new System.Drawing.Size(38, 17);
			this.South.Text = "South";
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.appToolStripMenuItem,
            this.commandToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(784, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// appToolStripMenuItem
			// 
			this.appToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.終了ToolStripMenuItem});
			this.appToolStripMenuItem.Name = "appToolStripMenuItem";
			this.appToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
			this.appToolStripMenuItem.Text = "App";
			// 
			// 終了ToolStripMenuItem
			// 
			this.終了ToolStripMenuItem.Name = "終了ToolStripMenuItem";
			this.終了ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
			this.終了ToolStripMenuItem.Text = "終了";
			this.終了ToolStripMenuItem.Click += new System.EventHandler(this.終了ToolStripMenuItem_Click);
			// 
			// commandToolStripMenuItem
			// 
			this.commandToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.過去へToolStripMenuItem,
            this.未来へToolStripMenuItem,
            this.表示期間を拡大ToolStripMenuItem,
            this.表示期間を縮小ToolStripMenuItem});
			this.commandToolStripMenuItem.Name = "commandToolStripMenuItem";
			this.commandToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
			this.commandToolStripMenuItem.Text = "Command";
			// 
			// 過去へToolStripMenuItem
			// 
			this.過去へToolStripMenuItem.Name = "過去へToolStripMenuItem";
			this.過去へToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Left)));
			this.過去へToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
			this.過去へToolStripMenuItem.Text = "過去へ";
			this.過去へToolStripMenuItem.Click += new System.EventHandler(this.過去へToolStripMenuItem_Click);
			// 
			// 未来へToolStripMenuItem
			// 
			this.未来へToolStripMenuItem.Name = "未来へToolStripMenuItem";
			this.未来へToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Right)));
			this.未来へToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
			this.未来へToolStripMenuItem.Text = "未来へ";
			this.未来へToolStripMenuItem.Click += new System.EventHandler(this.未来へToolStripMenuItem_Click);
			// 
			// 表示期間を拡大ToolStripMenuItem
			// 
			this.表示期間を拡大ToolStripMenuItem.Name = "表示期間を拡大ToolStripMenuItem";
			this.表示期間を拡大ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Up)));
			this.表示期間を拡大ToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
			this.表示期間を拡大ToolStripMenuItem.Text = "表示期間を拡大";
			this.表示期間を拡大ToolStripMenuItem.Click += new System.EventHandler(this.表示期間を拡大ToolStripMenuItem_Click);
			// 
			// 表示期間を縮小ToolStripMenuItem
			// 
			this.表示期間を縮小ToolStripMenuItem.Name = "表示期間を縮小ToolStripMenuItem";
			this.表示期間を縮小ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Down)));
			this.表示期間を縮小ToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
			this.表示期間を縮小ToolStripMenuItem.Text = "表示期間を縮小";
			this.表示期間を縮小ToolStripMenuItem.Click += new System.EventHandler(this.表示期間を縮小ToolStripMenuItem_Click);
			// 
			// MaChart
			// 
			chartArea1.Name = "ChartArea1";
			this.MaChart.ChartAreas.Add(chartArea1);
			legend1.Name = "Legend1";
			this.MaChart.Legends.Add(legend1);
			this.MaChart.Location = new System.Drawing.Point(12, 27);
			this.MaChart.Name = "MaChart";
			series1.ChartArea = "ChartArea1";
			series1.Legend = "Legend1";
			series1.Name = "Series1";
			this.MaChart.Series.Add(series1);
			this.MaChart.Size = new System.Drawing.Size(760, 100);
			this.MaChart.TabIndex = 2;
			this.MaChart.Text = "MaChart";
			// 
			// DmaChart
			// 
			this.DmaChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			chartArea2.Name = "ChartArea1";
			this.DmaChart.ChartAreas.Add(chartArea2);
			legend2.Name = "Legend1";
			this.DmaChart.Legends.Add(legend2);
			this.DmaChart.Location = new System.Drawing.Point(12, 133);
			this.DmaChart.Name = "DmaChart";
			series2.ChartArea = "ChartArea1";
			series2.Legend = "Legend1";
			series2.Name = "Series1";
			this.DmaChart.Series.Add(series2);
			this.DmaChart.Size = new System.Drawing.Size(760, 403);
			this.DmaChart.TabIndex = 3;
			this.DmaChart.Text = "DmaChart";
			// 
			// MainWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 561);
			this.Controls.Add(this.DmaChart);
			this.Controls.Add(this.MaChart);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.menuStrip1);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "MainWin";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Viewer";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWin_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWin_FormClosed);
			this.Load += new System.EventHandler(this.MainWin_Load);
			this.Shown += new System.EventHandler(this.MainWin_Shown);
			this.Resize += new System.EventHandler(this.MainWin_Resize);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.MaChart)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DmaChart)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Timer MainTimer;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel South;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem appToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 終了ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem commandToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 過去へToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 未来へToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 表示期間を拡大ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 表示期間を縮小ToolStripMenuItem;
		private System.Windows.Forms.DataVisualization.Charting.Chart MaChart;
		private System.Windows.Forms.DataVisualization.Charting.Chart DmaChart;
	}
}

