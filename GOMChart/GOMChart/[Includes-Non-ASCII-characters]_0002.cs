namespace Charlotte
{
	partial class Win単純移動平均
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
			System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Win単純移動平均));
			this.MChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.Args = new System.Windows.Forms.TextBox();
			this.TTip = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.MChart)).BeginInit();
			this.SuspendLayout();
			// 
			// MChart
			// 
			chartArea1.Name = "ChartArea1";
			this.MChart.ChartAreas.Add(chartArea1);
			this.MChart.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MChart.Location = new System.Drawing.Point(0, 27);
			this.MChart.Name = "MChart";
			series1.ChartArea = "ChartArea1";
			series1.Name = "Series1";
			this.MChart.Series.Add(series1);
			this.MChart.Size = new System.Drawing.Size(621, 432);
			this.MChart.TabIndex = 1;
			this.MChart.Text = "MChart";
			this.TTip.SetToolTip(this.MChart, "Ready...");
			this.MChart.Click += new System.EventHandler(this.MChart_Click);
			this.MChart.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MChart_MouseMove);
			// 
			// Args
			// 
			this.Args.Dock = System.Windows.Forms.DockStyle.Top;
			this.Args.Location = new System.Drawing.Point(0, 0);
			this.Args.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Args.Name = "Args";
			this.Args.Size = new System.Drawing.Size(621, 27);
			this.Args.TabIndex = 0;
			this.Args.Text = "USDJPY:20171231235959:30:7d:1m:5d:10d";
			this.Args.TextChanged += new System.EventHandler(this.Args_TextChanged);
			this.Args.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Args_KeyPress);
			this.Args.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Args_KeyUp);
			// 
			// TTip
			// 
			this.TTip.AutoPopDelay = 20000;
			this.TTip.InitialDelay = 500;
			this.TTip.ReshowDelay = 100;
			// 
			// Win単純移動平均
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(621, 459);
			this.Controls.Add(this.MChart);
			this.Controls.Add(this.Args);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "Win単純移動平均";
			this.Text = "単純移動平均";
			this.Load += new System.EventHandler(this.Win単純移動平均_Load);
			this.Shown += new System.EventHandler(this.Win単純移動平均_Shown);
			((System.ComponentModel.ISupportInitialize)(this.MChart)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataVisualization.Charting.Chart MChart;
		private System.Windows.Forms.TextBox Args;
		private System.Windows.Forms.ToolTip TTip;
	}
}
