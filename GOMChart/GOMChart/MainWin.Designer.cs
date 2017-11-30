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
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWin));
			this.MChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.通貨ペアToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.開くToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.単純移動平均ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ツールToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fxCollectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.テストToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.debugWinWeSecToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.MChart)).BeginInit();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// MChart
			// 
			chartArea1.Name = "ChartArea1";
			this.MChart.ChartAreas.Add(chartArea1);
			this.MChart.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MChart.Location = new System.Drawing.Point(0, 26);
			this.MChart.Name = "MChart";
			series1.ChartArea = "ChartArea1";
			series1.Name = "Series1";
			this.MChart.Series.Add(series1);
			this.MChart.Size = new System.Drawing.Size(695, 458);
			this.MChart.TabIndex = 0;
			this.MChart.Click += new System.EventHandler(this.MChart_Click);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.通貨ペアToolStripMenuItem,
            this.開くToolStripMenuItem,
            this.ツールToolStripMenuItem,
            this.テストToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(695, 26);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
			// 
			// 通貨ペアToolStripMenuItem
			// 
			this.通貨ペアToolStripMenuItem.Name = "通貨ペアToolStripMenuItem";
			this.通貨ペアToolStripMenuItem.Size = new System.Drawing.Size(68, 22);
			this.通貨ペアToolStripMenuItem.Text = "通貨ペア";
			// 
			// 開くToolStripMenuItem
			// 
			this.開くToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.単純移動平均ToolStripMenuItem});
			this.開くToolStripMenuItem.Name = "開くToolStripMenuItem";
			this.開くToolStripMenuItem.Size = new System.Drawing.Size(44, 22);
			this.開くToolStripMenuItem.Text = "開く";
			// 
			// 単純移動平均ToolStripMenuItem
			// 
			this.単純移動平均ToolStripMenuItem.Name = "単純移動平均ToolStripMenuItem";
			this.単純移動平均ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.単純移動平均ToolStripMenuItem.Text = "単純移動平均";
			this.単純移動平均ToolStripMenuItem.Click += new System.EventHandler(this.単純移動平均ToolStripMenuItem_Click);
			// 
			// ツールToolStripMenuItem
			// 
			this.ツールToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fxCollectToolStripMenuItem});
			this.ツールToolStripMenuItem.Name = "ツールToolStripMenuItem";
			this.ツールToolStripMenuItem.Size = new System.Drawing.Size(56, 22);
			this.ツールToolStripMenuItem.Text = "ツール";
			// 
			// fxCollectToolStripMenuItem
			// 
			this.fxCollectToolStripMenuItem.Name = "fxCollectToolStripMenuItem";
			this.fxCollectToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
			this.fxCollectToolStripMenuItem.Text = "FxCollect";
			this.fxCollectToolStripMenuItem.Click += new System.EventHandler(this.fxCollectToolStripMenuItem_Click);
			// 
			// テストToolStripMenuItem
			// 
			this.テストToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.debugWinWeSecToolStripMenuItem});
			this.テストToolStripMenuItem.Name = "テストToolStripMenuItem";
			this.テストToolStripMenuItem.Size = new System.Drawing.Size(56, 22);
			this.テストToolStripMenuItem.Text = "テスト";
			// 
			// debugWinWeSecToolStripMenuItem
			// 
			this.debugWinWeSecToolStripMenuItem.Name = "debugWinWeSecToolStripMenuItem";
			this.debugWinWeSecToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.debugWinWeSecToolStripMenuItem.Text = "DebugWin_WeSec";
			this.debugWinWeSecToolStripMenuItem.Click += new System.EventHandler(this.debugWinWeSecToolStripMenuItem_Click);
			// 
			// MainWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(695, 484);
			this.Controls.Add(this.MChart);
			this.Controls.Add(this.menuStrip1);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "MainWin";
			this.Text = "GOMChart";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWin_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWin_FormClosed);
			this.Load += new System.EventHandler(this.MainWin_Load);
			this.Shown += new System.EventHandler(this.MainWin_Shown);
			((System.ComponentModel.ISupportInitialize)(this.MChart)).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataVisualization.Charting.Chart MChart;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem 通貨ペアToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 開くToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 単純移動平均ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ツールToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fxCollectToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem テストToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem debugWinWeSecToolStripMenuItem;
	}
}

