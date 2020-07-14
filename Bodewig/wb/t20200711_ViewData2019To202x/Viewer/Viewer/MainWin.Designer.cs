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
			System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWin));
			this.MainTimer = new System.Windows.Forms.Timer(this.components);
			this.MChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.DateTimeSt = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.DateTimeEd = new System.Windows.Forms.TextBox();
			this.MaGroup = new System.Windows.Forms.GroupBox();
			this.MaDay_80 = new System.Windows.Forms.CheckBox();
			this.MaDay_75 = new System.Windows.Forms.CheckBox();
			this.MaDay_70 = new System.Windows.Forms.CheckBox();
			this.MaDay_65 = new System.Windows.Forms.CheckBox();
			this.MaDay_60 = new System.Windows.Forms.CheckBox();
			this.MaDay_55 = new System.Windows.Forms.CheckBox();
			this.MaDay_50 = new System.Windows.Forms.CheckBox();
			this.MaDay_45 = new System.Windows.Forms.CheckBox();
			this.MaDay_40 = new System.Windows.Forms.CheckBox();
			this.MaDay_35 = new System.Windows.Forms.CheckBox();
			this.MaDay_30 = new System.Windows.Forms.CheckBox();
			this.MaDay_29 = new System.Windows.Forms.CheckBox();
			this.MaDay_28 = new System.Windows.Forms.CheckBox();
			this.MaDay_27 = new System.Windows.Forms.CheckBox();
			this.MaDay_26 = new System.Windows.Forms.CheckBox();
			this.MaDay_25 = new System.Windows.Forms.CheckBox();
			this.MaDay_24 = new System.Windows.Forms.CheckBox();
			this.MaDay_23 = new System.Windows.Forms.CheckBox();
			this.MaDay_22 = new System.Windows.Forms.CheckBox();
			this.MaDay_21 = new System.Windows.Forms.CheckBox();
			this.MaDay_20 = new System.Windows.Forms.CheckBox();
			this.MaDay_19 = new System.Windows.Forms.CheckBox();
			this.MaDay_18 = new System.Windows.Forms.CheckBox();
			this.MaDay_17 = new System.Windows.Forms.CheckBox();
			this.MaDay_16 = new System.Windows.Forms.CheckBox();
			this.MaDay_15 = new System.Windows.Forms.CheckBox();
			this.MaDay_14 = new System.Windows.Forms.CheckBox();
			this.MaDay_13 = new System.Windows.Forms.CheckBox();
			this.MaDay_12 = new System.Windows.Forms.CheckBox();
			this.MaDay_11 = new System.Windows.Forms.CheckBox();
			this.MaDay_10 = new System.Windows.Forms.CheckBox();
			this.MaDay_09 = new System.Windows.Forms.CheckBox();
			this.MaDay_08 = new System.Windows.Forms.CheckBox();
			this.MaDay_07 = new System.Windows.Forms.CheckBox();
			this.MaDay_06 = new System.Windows.Forms.CheckBox();
			this.MaDay_05 = new System.Windows.Forms.CheckBox();
			this.MaDay_04 = new System.Windows.Forms.CheckBox();
			this.MaDay_03 = new System.Windows.Forms.CheckBox();
			this.MaDay_02 = new System.Windows.Forms.CheckBox();
			this.MaDay_01 = new System.Windows.Forms.CheckBox();
			this.BtnBack = new System.Windows.Forms.Button();
			this.BtnForward = new System.Windows.Forms.Button();
			this.BtnUnexpand = new System.Windows.Forms.Button();
			this.BtnExpand = new System.Windows.Forms.Button();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.Status = new System.Windows.Forms.ToolStripStatusLabel();
			this.SubStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.CurrPair = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.MChart)).BeginInit();
			this.MaGroup.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// MainTimer
			// 
			this.MainTimer.Enabled = true;
			this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
			// 
			// MChart
			// 
			this.MChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			chartArea1.Name = "ChartArea1";
			this.MChart.ChartAreas.Add(chartArea1);
			this.MChart.Location = new System.Drawing.Point(12, 176);
			this.MChart.Name = "MChart";
			series1.ChartArea = "ChartArea1";
			series1.Name = "Series1";
			this.MChart.Series.Add(series1);
			this.MChart.Size = new System.Drawing.Size(740, 380);
			this.MChart.TabIndex = 11;
			this.MChart.Text = "chart1";
			this.MChart.Click += new System.EventHandler(this.MChart_Click);
			// 
			// DateTimeSt
			// 
			this.DateTimeSt.Location = new System.Drawing.Point(79, 46);
			this.DateTimeSt.MaxLength = 14;
			this.DateTimeSt.Name = "DateTimeSt";
			this.DateTimeSt.Size = new System.Drawing.Size(135, 27);
			this.DateTimeSt.TabIndex = 3;
			this.DateTimeSt.Text = "19991231235959";
			this.DateTimeSt.TextChanged += new System.EventHandler(this.DateTimeSt_TextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 49);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(61, 20);
			this.label1.TabIndex = 2;
			this.label1.Text = "開始日時";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 82);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(61, 20);
			this.label2.TabIndex = 4;
			this.label2.Text = "終了日時";
			// 
			// DateTimeEd
			// 
			this.DateTimeEd.Location = new System.Drawing.Point(79, 79);
			this.DateTimeEd.MaxLength = 14;
			this.DateTimeEd.Name = "DateTimeEd";
			this.DateTimeEd.Size = new System.Drawing.Size(135, 27);
			this.DateTimeEd.TabIndex = 5;
			this.DateTimeEd.Text = "19991231235959";
			this.DateTimeEd.TextChanged += new System.EventHandler(this.DateTimeEd_TextChanged);
			// 
			// MaGroup
			// 
			this.MaGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MaGroup.Controls.Add(this.MaDay_80);
			this.MaGroup.Controls.Add(this.MaDay_75);
			this.MaGroup.Controls.Add(this.MaDay_70);
			this.MaGroup.Controls.Add(this.MaDay_65);
			this.MaGroup.Controls.Add(this.MaDay_60);
			this.MaGroup.Controls.Add(this.MaDay_55);
			this.MaGroup.Controls.Add(this.MaDay_50);
			this.MaGroup.Controls.Add(this.MaDay_45);
			this.MaGroup.Controls.Add(this.MaDay_40);
			this.MaGroup.Controls.Add(this.MaDay_35);
			this.MaGroup.Controls.Add(this.MaDay_30);
			this.MaGroup.Controls.Add(this.MaDay_29);
			this.MaGroup.Controls.Add(this.MaDay_28);
			this.MaGroup.Controls.Add(this.MaDay_27);
			this.MaGroup.Controls.Add(this.MaDay_26);
			this.MaGroup.Controls.Add(this.MaDay_25);
			this.MaGroup.Controls.Add(this.MaDay_24);
			this.MaGroup.Controls.Add(this.MaDay_23);
			this.MaGroup.Controls.Add(this.MaDay_22);
			this.MaGroup.Controls.Add(this.MaDay_21);
			this.MaGroup.Controls.Add(this.MaDay_20);
			this.MaGroup.Controls.Add(this.MaDay_19);
			this.MaGroup.Controls.Add(this.MaDay_18);
			this.MaGroup.Controls.Add(this.MaDay_17);
			this.MaGroup.Controls.Add(this.MaDay_16);
			this.MaGroup.Controls.Add(this.MaDay_15);
			this.MaGroup.Controls.Add(this.MaDay_14);
			this.MaGroup.Controls.Add(this.MaDay_13);
			this.MaGroup.Controls.Add(this.MaDay_12);
			this.MaGroup.Controls.Add(this.MaDay_11);
			this.MaGroup.Controls.Add(this.MaDay_10);
			this.MaGroup.Controls.Add(this.MaDay_09);
			this.MaGroup.Controls.Add(this.MaDay_08);
			this.MaGroup.Controls.Add(this.MaDay_07);
			this.MaGroup.Controls.Add(this.MaDay_06);
			this.MaGroup.Controls.Add(this.MaDay_05);
			this.MaGroup.Controls.Add(this.MaDay_04);
			this.MaGroup.Controls.Add(this.MaDay_03);
			this.MaGroup.Controls.Add(this.MaDay_02);
			this.MaGroup.Controls.Add(this.MaDay_01);
			this.MaGroup.Location = new System.Drawing.Point(220, 12);
			this.MaGroup.Name = "MaGroup";
			this.MaGroup.Size = new System.Drawing.Size(532, 158);
			this.MaGroup.TabIndex = 10;
			this.MaGroup.TabStop = false;
			this.MaGroup.Text = "移動平均";
			this.MaGroup.Enter += new System.EventHandler(this.MaGroup_Enter);
			// 
			// MaDay_80
			// 
			this.MaDay_80.AutoSize = true;
			this.MaDay_80.Location = new System.Drawing.Point(439, 122);
			this.MaDay_80.Name = "MaDay_80";
			this.MaDay_80.Size = new System.Drawing.Size(57, 24);
			this.MaDay_80.TabIndex = 39;
			this.MaDay_80.Tag = "80";
			this.MaDay_80.Text = "80日";
			this.MaDay_80.UseVisualStyleBackColor = true;
			this.MaDay_80.CheckedChanged += new System.EventHandler(this.MaDay_XX_CheckedChanged);
			// 
			// MaDay_75
			// 
			this.MaDay_75.AutoSize = true;
			this.MaDay_75.Checked = true;
			this.MaDay_75.CheckState = System.Windows.Forms.CheckState.Checked;
			this.MaDay_75.Location = new System.Drawing.Point(439, 98);
			this.MaDay_75.Name = "MaDay_75";
			this.MaDay_75.Size = new System.Drawing.Size(57, 24);
			this.MaDay_75.TabIndex = 38;
			this.MaDay_75.Tag = "75";
			this.MaDay_75.Text = "75日";
			this.MaDay_75.UseVisualStyleBackColor = true;
			this.MaDay_75.CheckedChanged += new System.EventHandler(this.MaDay_XX_CheckedChanged);
			// 
			// MaDay_70
			// 
			this.MaDay_70.AutoSize = true;
			this.MaDay_70.Location = new System.Drawing.Point(439, 74);
			this.MaDay_70.Name = "MaDay_70";
			this.MaDay_70.Size = new System.Drawing.Size(57, 24);
			this.MaDay_70.TabIndex = 37;
			this.MaDay_70.Tag = "70";
			this.MaDay_70.Text = "70日";
			this.MaDay_70.UseVisualStyleBackColor = true;
			this.MaDay_70.CheckedChanged += new System.EventHandler(this.MaDay_XX_CheckedChanged);
			// 
			// MaDay_65
			// 
			this.MaDay_65.AutoSize = true;
			this.MaDay_65.Location = new System.Drawing.Point(439, 50);
			this.MaDay_65.Name = "MaDay_65";
			this.MaDay_65.Size = new System.Drawing.Size(57, 24);
			this.MaDay_65.TabIndex = 36;
			this.MaDay_65.Tag = "65";
			this.MaDay_65.Text = "65日";
			this.MaDay_65.UseVisualStyleBackColor = true;
			this.MaDay_65.CheckedChanged += new System.EventHandler(this.MaDay_XX_CheckedChanged);
			// 
			// MaDay_60
			// 
			this.MaDay_60.AutoSize = true;
			this.MaDay_60.Location = new System.Drawing.Point(439, 26);
			this.MaDay_60.Name = "MaDay_60";
			this.MaDay_60.Size = new System.Drawing.Size(57, 24);
			this.MaDay_60.TabIndex = 35;
			this.MaDay_60.Tag = "60";
			this.MaDay_60.Text = "60日";
			this.MaDay_60.UseVisualStyleBackColor = true;
			this.MaDay_60.CheckedChanged += new System.EventHandler(this.MaDay_XX_CheckedChanged);
			// 
			// MaDay_55
			// 
			this.MaDay_55.AutoSize = true;
			this.MaDay_55.Location = new System.Drawing.Point(376, 122);
			this.MaDay_55.Name = "MaDay_55";
			this.MaDay_55.Size = new System.Drawing.Size(57, 24);
			this.MaDay_55.TabIndex = 34;
			this.MaDay_55.Tag = "55";
			this.MaDay_55.Text = "55日";
			this.MaDay_55.UseVisualStyleBackColor = true;
			this.MaDay_55.CheckedChanged += new System.EventHandler(this.MaDay_XX_CheckedChanged);
			// 
			// MaDay_50
			// 
			this.MaDay_50.AutoSize = true;
			this.MaDay_50.Location = new System.Drawing.Point(376, 98);
			this.MaDay_50.Name = "MaDay_50";
			this.MaDay_50.Size = new System.Drawing.Size(57, 24);
			this.MaDay_50.TabIndex = 33;
			this.MaDay_50.Tag = "50";
			this.MaDay_50.Text = "50日";
			this.MaDay_50.UseVisualStyleBackColor = true;
			this.MaDay_50.CheckedChanged += new System.EventHandler(this.MaDay_XX_CheckedChanged);
			// 
			// MaDay_45
			// 
			this.MaDay_45.AutoSize = true;
			this.MaDay_45.Location = new System.Drawing.Point(376, 74);
			this.MaDay_45.Name = "MaDay_45";
			this.MaDay_45.Size = new System.Drawing.Size(57, 24);
			this.MaDay_45.TabIndex = 32;
			this.MaDay_45.Tag = "45";
			this.MaDay_45.Text = "45日";
			this.MaDay_45.UseVisualStyleBackColor = true;
			this.MaDay_45.CheckedChanged += new System.EventHandler(this.MaDay_XX_CheckedChanged);
			// 
			// MaDay_40
			// 
			this.MaDay_40.AutoSize = true;
			this.MaDay_40.Location = new System.Drawing.Point(376, 50);
			this.MaDay_40.Name = "MaDay_40";
			this.MaDay_40.Size = new System.Drawing.Size(57, 24);
			this.MaDay_40.TabIndex = 31;
			this.MaDay_40.Tag = "40";
			this.MaDay_40.Text = "40日";
			this.MaDay_40.UseVisualStyleBackColor = true;
			this.MaDay_40.CheckedChanged += new System.EventHandler(this.MaDay_XX_CheckedChanged);
			// 
			// MaDay_35
			// 
			this.MaDay_35.AutoSize = true;
			this.MaDay_35.Location = new System.Drawing.Point(376, 26);
			this.MaDay_35.Name = "MaDay_35";
			this.MaDay_35.Size = new System.Drawing.Size(57, 24);
			this.MaDay_35.TabIndex = 30;
			this.MaDay_35.Tag = "35";
			this.MaDay_35.Text = "35日";
			this.MaDay_35.UseVisualStyleBackColor = true;
			this.MaDay_35.CheckedChanged += new System.EventHandler(this.MaDay_XX_CheckedChanged);
			// 
			// MaDay_30
			// 
			this.MaDay_30.AutoSize = true;
			this.MaDay_30.Location = new System.Drawing.Point(313, 122);
			this.MaDay_30.Name = "MaDay_30";
			this.MaDay_30.Size = new System.Drawing.Size(57, 24);
			this.MaDay_30.TabIndex = 29;
			this.MaDay_30.Tag = "30";
			this.MaDay_30.Text = "30日";
			this.MaDay_30.UseVisualStyleBackColor = true;
			this.MaDay_30.CheckedChanged += new System.EventHandler(this.MaDay_XX_CheckedChanged);
			// 
			// MaDay_29
			// 
			this.MaDay_29.AutoSize = true;
			this.MaDay_29.Location = new System.Drawing.Point(313, 98);
			this.MaDay_29.Name = "MaDay_29";
			this.MaDay_29.Size = new System.Drawing.Size(57, 24);
			this.MaDay_29.TabIndex = 28;
			this.MaDay_29.Tag = "29";
			this.MaDay_29.Text = "29日";
			this.MaDay_29.UseVisualStyleBackColor = true;
			this.MaDay_29.CheckedChanged += new System.EventHandler(this.MaDay_XX_CheckedChanged);
			// 
			// MaDay_28
			// 
			this.MaDay_28.AutoSize = true;
			this.MaDay_28.Location = new System.Drawing.Point(313, 74);
			this.MaDay_28.Name = "MaDay_28";
			this.MaDay_28.Size = new System.Drawing.Size(57, 24);
			this.MaDay_28.TabIndex = 27;
			this.MaDay_28.Tag = "28";
			this.MaDay_28.Text = "28日";
			this.MaDay_28.UseVisualStyleBackColor = true;
			this.MaDay_28.CheckedChanged += new System.EventHandler(this.MaDay_XX_CheckedChanged);
			// 
			// MaDay_27
			// 
			this.MaDay_27.AutoSize = true;
			this.MaDay_27.Location = new System.Drawing.Point(313, 50);
			this.MaDay_27.Name = "MaDay_27";
			this.MaDay_27.Size = new System.Drawing.Size(57, 24);
			this.MaDay_27.TabIndex = 26;
			this.MaDay_27.Tag = "27";
			this.MaDay_27.Text = "27日";
			this.MaDay_27.UseVisualStyleBackColor = true;
			this.MaDay_27.CheckedChanged += new System.EventHandler(this.MaDay_XX_CheckedChanged);
			// 
			// MaDay_26
			// 
			this.MaDay_26.AutoSize = true;
			this.MaDay_26.Location = new System.Drawing.Point(313, 26);
			this.MaDay_26.Name = "MaDay_26";
			this.MaDay_26.Size = new System.Drawing.Size(57, 24);
			this.MaDay_26.TabIndex = 25;
			this.MaDay_26.Tag = "26";
			this.MaDay_26.Text = "26日";
			this.MaDay_26.UseVisualStyleBackColor = true;
			this.MaDay_26.CheckedChanged += new System.EventHandler(this.MaDay_XX_CheckedChanged);
			// 
			// MaDay_25
			// 
			this.MaDay_25.AutoSize = true;
			this.MaDay_25.Checked = true;
			this.MaDay_25.CheckState = System.Windows.Forms.CheckState.Checked;
			this.MaDay_25.Location = new System.Drawing.Point(250, 122);
			this.MaDay_25.Name = "MaDay_25";
			this.MaDay_25.Size = new System.Drawing.Size(57, 24);
			this.MaDay_25.TabIndex = 24;
			this.MaDay_25.Tag = "25";
			this.MaDay_25.Text = "25日";
			this.MaDay_25.UseVisualStyleBackColor = true;
			this.MaDay_25.CheckedChanged += new System.EventHandler(this.MaDay_XX_CheckedChanged);
			// 
			// MaDay_24
			// 
			this.MaDay_24.AutoSize = true;
			this.MaDay_24.Location = new System.Drawing.Point(250, 98);
			this.MaDay_24.Name = "MaDay_24";
			this.MaDay_24.Size = new System.Drawing.Size(57, 24);
			this.MaDay_24.TabIndex = 23;
			this.MaDay_24.Tag = "24";
			this.MaDay_24.Text = "24日";
			this.MaDay_24.UseVisualStyleBackColor = true;
			this.MaDay_24.CheckedChanged += new System.EventHandler(this.MaDay_XX_CheckedChanged);
			// 
			// MaDay_23
			// 
			this.MaDay_23.AutoSize = true;
			this.MaDay_23.Location = new System.Drawing.Point(250, 74);
			this.MaDay_23.Name = "MaDay_23";
			this.MaDay_23.Size = new System.Drawing.Size(57, 24);
			this.MaDay_23.TabIndex = 22;
			this.MaDay_23.Tag = "23";
			this.MaDay_23.Text = "23日";
			this.MaDay_23.UseVisualStyleBackColor = true;
			this.MaDay_23.CheckedChanged += new System.EventHandler(this.MaDay_XX_CheckedChanged);
			// 
			// MaDay_22
			// 
			this.MaDay_22.AutoSize = true;
			this.MaDay_22.Location = new System.Drawing.Point(250, 50);
			this.MaDay_22.Name = "MaDay_22";
			this.MaDay_22.Size = new System.Drawing.Size(57, 24);
			this.MaDay_22.TabIndex = 21;
			this.MaDay_22.Tag = "22";
			this.MaDay_22.Text = "22日";
			this.MaDay_22.UseVisualStyleBackColor = true;
			this.MaDay_22.CheckedChanged += new System.EventHandler(this.MaDay_XX_CheckedChanged);
			// 
			// MaDay_21
			// 
			this.MaDay_21.AutoSize = true;
			this.MaDay_21.Location = new System.Drawing.Point(250, 26);
			this.MaDay_21.Name = "MaDay_21";
			this.MaDay_21.Size = new System.Drawing.Size(57, 24);
			this.MaDay_21.TabIndex = 20;
			this.MaDay_21.Tag = "21";
			this.MaDay_21.Text = "21日";
			this.MaDay_21.UseVisualStyleBackColor = true;
			this.MaDay_21.CheckedChanged += new System.EventHandler(this.MaDay_XX_CheckedChanged);
			// 
			// MaDay_20
			// 
			this.MaDay_20.AutoSize = true;
			this.MaDay_20.Location = new System.Drawing.Point(187, 122);
			this.MaDay_20.Name = "MaDay_20";
			this.MaDay_20.Size = new System.Drawing.Size(57, 24);
			this.MaDay_20.TabIndex = 19;
			this.MaDay_20.Tag = "20";
			this.MaDay_20.Text = "20日";
			this.MaDay_20.UseVisualStyleBackColor = true;
			this.MaDay_20.CheckedChanged += new System.EventHandler(this.MaDay_XX_CheckedChanged);
			// 
			// MaDay_19
			// 
			this.MaDay_19.AutoSize = true;
			this.MaDay_19.Location = new System.Drawing.Point(187, 98);
			this.MaDay_19.Name = "MaDay_19";
			this.MaDay_19.Size = new System.Drawing.Size(57, 24);
			this.MaDay_19.TabIndex = 18;
			this.MaDay_19.Tag = "19";
			this.MaDay_19.Text = "19日";
			this.MaDay_19.UseVisualStyleBackColor = true;
			this.MaDay_19.CheckedChanged += new System.EventHandler(this.MaDay_XX_CheckedChanged);
			// 
			// MaDay_18
			// 
			this.MaDay_18.AutoSize = true;
			this.MaDay_18.Location = new System.Drawing.Point(187, 74);
			this.MaDay_18.Name = "MaDay_18";
			this.MaDay_18.Size = new System.Drawing.Size(57, 24);
			this.MaDay_18.TabIndex = 17;
			this.MaDay_18.Tag = "18";
			this.MaDay_18.Text = "18日";
			this.MaDay_18.UseVisualStyleBackColor = true;
			this.MaDay_18.CheckedChanged += new System.EventHandler(this.MaDay_XX_CheckedChanged);
			// 
			// MaDay_17
			// 
			this.MaDay_17.AutoSize = true;
			this.MaDay_17.Location = new System.Drawing.Point(187, 50);
			this.MaDay_17.Name = "MaDay_17";
			this.MaDay_17.Size = new System.Drawing.Size(57, 24);
			this.MaDay_17.TabIndex = 16;
			this.MaDay_17.Tag = "17";
			this.MaDay_17.Text = "17日";
			this.MaDay_17.UseVisualStyleBackColor = true;
			this.MaDay_17.CheckedChanged += new System.EventHandler(this.MaDay_XX_CheckedChanged);
			// 
			// MaDay_16
			// 
			this.MaDay_16.AutoSize = true;
			this.MaDay_16.Location = new System.Drawing.Point(187, 26);
			this.MaDay_16.Name = "MaDay_16";
			this.MaDay_16.Size = new System.Drawing.Size(57, 24);
			this.MaDay_16.TabIndex = 15;
			this.MaDay_16.Tag = "16";
			this.MaDay_16.Text = "16日";
			this.MaDay_16.UseVisualStyleBackColor = true;
			this.MaDay_16.CheckedChanged += new System.EventHandler(this.MaDay_XX_CheckedChanged);
			// 
			// MaDay_15
			// 
			this.MaDay_15.AutoSize = true;
			this.MaDay_15.Location = new System.Drawing.Point(124, 122);
			this.MaDay_15.Name = "MaDay_15";
			this.MaDay_15.Size = new System.Drawing.Size(57, 24);
			this.MaDay_15.TabIndex = 14;
			this.MaDay_15.Tag = "15";
			this.MaDay_15.Text = "15日";
			this.MaDay_15.UseVisualStyleBackColor = true;
			this.MaDay_15.CheckedChanged += new System.EventHandler(this.MaDay_XX_CheckedChanged);
			// 
			// MaDay_14
			// 
			this.MaDay_14.AutoSize = true;
			this.MaDay_14.Location = new System.Drawing.Point(124, 98);
			this.MaDay_14.Name = "MaDay_14";
			this.MaDay_14.Size = new System.Drawing.Size(57, 24);
			this.MaDay_14.TabIndex = 13;
			this.MaDay_14.Tag = "14";
			this.MaDay_14.Text = "14日";
			this.MaDay_14.UseVisualStyleBackColor = true;
			this.MaDay_14.CheckedChanged += new System.EventHandler(this.MaDay_XX_CheckedChanged);
			// 
			// MaDay_13
			// 
			this.MaDay_13.AutoSize = true;
			this.MaDay_13.Location = new System.Drawing.Point(124, 74);
			this.MaDay_13.Name = "MaDay_13";
			this.MaDay_13.Size = new System.Drawing.Size(57, 24);
			this.MaDay_13.TabIndex = 12;
			this.MaDay_13.Tag = "13";
			this.MaDay_13.Text = "13日";
			this.MaDay_13.UseVisualStyleBackColor = true;
			this.MaDay_13.CheckedChanged += new System.EventHandler(this.MaDay_XX_CheckedChanged);
			// 
			// MaDay_12
			// 
			this.MaDay_12.AutoSize = true;
			this.MaDay_12.Location = new System.Drawing.Point(124, 50);
			this.MaDay_12.Name = "MaDay_12";
			this.MaDay_12.Size = new System.Drawing.Size(57, 24);
			this.MaDay_12.TabIndex = 11;
			this.MaDay_12.Tag = "12";
			this.MaDay_12.Text = "12日";
			this.MaDay_12.UseVisualStyleBackColor = true;
			this.MaDay_12.CheckedChanged += new System.EventHandler(this.MaDay_XX_CheckedChanged);
			// 
			// MaDay_11
			// 
			this.MaDay_11.AutoSize = true;
			this.MaDay_11.Location = new System.Drawing.Point(124, 26);
			this.MaDay_11.Name = "MaDay_11";
			this.MaDay_11.Size = new System.Drawing.Size(57, 24);
			this.MaDay_11.TabIndex = 10;
			this.MaDay_11.Tag = "11";
			this.MaDay_11.Text = "11日";
			this.MaDay_11.UseVisualStyleBackColor = true;
			this.MaDay_11.CheckedChanged += new System.EventHandler(this.MaDay_XX_CheckedChanged);
			// 
			// MaDay_10
			// 
			this.MaDay_10.AutoSize = true;
			this.MaDay_10.Location = new System.Drawing.Point(61, 122);
			this.MaDay_10.Name = "MaDay_10";
			this.MaDay_10.Size = new System.Drawing.Size(57, 24);
			this.MaDay_10.TabIndex = 9;
			this.MaDay_10.Tag = "10";
			this.MaDay_10.Text = "10日";
			this.MaDay_10.UseVisualStyleBackColor = true;
			this.MaDay_10.CheckedChanged += new System.EventHandler(this.MaDay_XX_CheckedChanged);
			// 
			// MaDay_09
			// 
			this.MaDay_09.AutoSize = true;
			this.MaDay_09.Location = new System.Drawing.Point(61, 98);
			this.MaDay_09.Name = "MaDay_09";
			this.MaDay_09.Size = new System.Drawing.Size(49, 24);
			this.MaDay_09.TabIndex = 8;
			this.MaDay_09.Tag = "09";
			this.MaDay_09.Text = "9日";
			this.MaDay_09.UseVisualStyleBackColor = true;
			this.MaDay_09.CheckedChanged += new System.EventHandler(this.MaDay_XX_CheckedChanged);
			// 
			// MaDay_08
			// 
			this.MaDay_08.AutoSize = true;
			this.MaDay_08.Location = new System.Drawing.Point(61, 74);
			this.MaDay_08.Name = "MaDay_08";
			this.MaDay_08.Size = new System.Drawing.Size(49, 24);
			this.MaDay_08.TabIndex = 7;
			this.MaDay_08.Tag = "08";
			this.MaDay_08.Text = "8日";
			this.MaDay_08.UseVisualStyleBackColor = true;
			this.MaDay_08.CheckedChanged += new System.EventHandler(this.MaDay_XX_CheckedChanged);
			// 
			// MaDay_07
			// 
			this.MaDay_07.AutoSize = true;
			this.MaDay_07.Location = new System.Drawing.Point(61, 50);
			this.MaDay_07.Name = "MaDay_07";
			this.MaDay_07.Size = new System.Drawing.Size(49, 24);
			this.MaDay_07.TabIndex = 6;
			this.MaDay_07.Tag = "07";
			this.MaDay_07.Text = "7日";
			this.MaDay_07.UseVisualStyleBackColor = true;
			this.MaDay_07.CheckedChanged += new System.EventHandler(this.MaDay_XX_CheckedChanged);
			// 
			// MaDay_06
			// 
			this.MaDay_06.AutoSize = true;
			this.MaDay_06.Location = new System.Drawing.Point(61, 26);
			this.MaDay_06.Name = "MaDay_06";
			this.MaDay_06.Size = new System.Drawing.Size(49, 24);
			this.MaDay_06.TabIndex = 5;
			this.MaDay_06.Tag = "06";
			this.MaDay_06.Text = "6日";
			this.MaDay_06.UseVisualStyleBackColor = true;
			this.MaDay_06.CheckedChanged += new System.EventHandler(this.MaDay_XX_CheckedChanged);
			// 
			// MaDay_05
			// 
			this.MaDay_05.AutoSize = true;
			this.MaDay_05.Checked = true;
			this.MaDay_05.CheckState = System.Windows.Forms.CheckState.Checked;
			this.MaDay_05.Location = new System.Drawing.Point(6, 122);
			this.MaDay_05.Name = "MaDay_05";
			this.MaDay_05.Size = new System.Drawing.Size(49, 24);
			this.MaDay_05.TabIndex = 4;
			this.MaDay_05.Tag = "05";
			this.MaDay_05.Text = "5日";
			this.MaDay_05.UseVisualStyleBackColor = true;
			this.MaDay_05.CheckedChanged += new System.EventHandler(this.MaDay_XX_CheckedChanged);
			// 
			// MaDay_04
			// 
			this.MaDay_04.AutoSize = true;
			this.MaDay_04.Location = new System.Drawing.Point(6, 98);
			this.MaDay_04.Name = "MaDay_04";
			this.MaDay_04.Size = new System.Drawing.Size(49, 24);
			this.MaDay_04.TabIndex = 3;
			this.MaDay_04.Tag = "04";
			this.MaDay_04.Text = "4日";
			this.MaDay_04.UseVisualStyleBackColor = true;
			this.MaDay_04.CheckedChanged += new System.EventHandler(this.MaDay_XX_CheckedChanged);
			// 
			// MaDay_03
			// 
			this.MaDay_03.AutoSize = true;
			this.MaDay_03.Location = new System.Drawing.Point(6, 74);
			this.MaDay_03.Name = "MaDay_03";
			this.MaDay_03.Size = new System.Drawing.Size(49, 24);
			this.MaDay_03.TabIndex = 2;
			this.MaDay_03.Tag = "03";
			this.MaDay_03.Text = "3日";
			this.MaDay_03.UseVisualStyleBackColor = true;
			this.MaDay_03.CheckedChanged += new System.EventHandler(this.MaDay_XX_CheckedChanged);
			// 
			// MaDay_02
			// 
			this.MaDay_02.AutoSize = true;
			this.MaDay_02.Location = new System.Drawing.Point(6, 50);
			this.MaDay_02.Name = "MaDay_02";
			this.MaDay_02.Size = new System.Drawing.Size(49, 24);
			this.MaDay_02.TabIndex = 1;
			this.MaDay_02.Tag = "02";
			this.MaDay_02.Text = "2日";
			this.MaDay_02.UseVisualStyleBackColor = true;
			this.MaDay_02.CheckedChanged += new System.EventHandler(this.MaDay_XX_CheckedChanged);
			// 
			// MaDay_01
			// 
			this.MaDay_01.AutoSize = true;
			this.MaDay_01.Location = new System.Drawing.Point(6, 26);
			this.MaDay_01.Name = "MaDay_01";
			this.MaDay_01.Size = new System.Drawing.Size(49, 24);
			this.MaDay_01.TabIndex = 0;
			this.MaDay_01.Tag = "01";
			this.MaDay_01.Text = "1日";
			this.MaDay_01.UseVisualStyleBackColor = true;
			this.MaDay_01.CheckedChanged += new System.EventHandler(this.MaDay_XX_CheckedChanged);
			// 
			// BtnBack
			// 
			this.BtnBack.Location = new System.Drawing.Point(12, 112);
			this.BtnBack.Name = "BtnBack";
			this.BtnBack.Size = new System.Drawing.Size(98, 26);
			this.BtnBack.TabIndex = 6;
			this.BtnBack.Text = "過去へ";
			this.BtnBack.UseVisualStyleBackColor = true;
			this.BtnBack.Click += new System.EventHandler(this.BtnBack_Click);
			// 
			// BtnForward
			// 
			this.BtnForward.Location = new System.Drawing.Point(116, 112);
			this.BtnForward.Name = "BtnForward";
			this.BtnForward.Size = new System.Drawing.Size(98, 26);
			this.BtnForward.TabIndex = 7;
			this.BtnForward.Text = "未来へ";
			this.BtnForward.UseVisualStyleBackColor = true;
			this.BtnForward.Click += new System.EventHandler(this.BtnForward_Click);
			// 
			// BtnUnexpand
			// 
			this.BtnUnexpand.Location = new System.Drawing.Point(12, 144);
			this.BtnUnexpand.Name = "BtnUnexpand";
			this.BtnUnexpand.Size = new System.Drawing.Size(98, 26);
			this.BtnUnexpand.TabIndex = 8;
			this.BtnUnexpand.Text = "拡大";
			this.BtnUnexpand.UseVisualStyleBackColor = true;
			this.BtnUnexpand.Click += new System.EventHandler(this.BtnUnexpand_Click);
			// 
			// BtnExpand
			// 
			this.BtnExpand.Location = new System.Drawing.Point(116, 144);
			this.BtnExpand.Name = "BtnExpand";
			this.BtnExpand.Size = new System.Drawing.Size(98, 26);
			this.BtnExpand.TabIndex = 9;
			this.BtnExpand.Text = "縮小";
			this.BtnExpand.UseVisualStyleBackColor = true;
			this.BtnExpand.Click += new System.EventHandler(this.BtnExpand_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Status,
            this.SubStatus});
			this.statusStrip1.Location = new System.Drawing.Point(0, 559);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(764, 22);
			this.statusStrip1.TabIndex = 12;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// Status
			// 
			this.Status.Name = "Status";
			this.Status.Size = new System.Drawing.Size(690, 17);
			this.Status.Spring = true;
			this.Status.Text = "Status";
			this.Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// SubStatus
			// 
			this.SubStatus.Name = "SubStatus";
			this.SubStatus.Size = new System.Drawing.Size(59, 17);
			this.SubStatus.Text = "SubStatus";
			// 
			// CurrPair
			// 
			this.CurrPair.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CurrPair.FormattingEnabled = true;
			this.CurrPair.Items.AddRange(new object[] {
            "AUDCHF",
            "AUDJPY",
            "AUDNZD",
            "AUDUSD",
            "CADJPY",
            "CHFJPY",
            "EURAUD",
            "EURCAD",
            "EURCHF",
            "EURGBP",
            "EURJPY",
            "EURNZD",
            "EURUSD",
            "GBPAUD",
            "GBPCHF",
            "GBPJPY",
            "GBPNZD",
            "GBPUSD",
            "NZDJPY",
            "NZDUSD",
            "USDCAD",
            "USDCHF",
            "USDJPY",
            "ZARJPY"});
			this.CurrPair.Location = new System.Drawing.Point(79, 12);
			this.CurrPair.Name = "CurrPair";
			this.CurrPair.Size = new System.Drawing.Size(135, 28);
			this.CurrPair.TabIndex = 1;
			this.CurrPair.SelectedIndexChanged += new System.EventHandler(this.CurrPair_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 15);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(61, 20);
			this.label3.TabIndex = 0;
			this.label3.Text = "通貨ペア";
			// 
			// MainWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(764, 581);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.CurrPair);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.BtnExpand);
			this.Controls.Add(this.BtnUnexpand);
			this.Controls.Add(this.BtnForward);
			this.Controls.Add(this.BtnBack);
			this.Controls.Add(this.MaGroup);
			this.Controls.Add(this.DateTimeEd);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.DateTimeSt);
			this.Controls.Add(this.MChart);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "MainWin";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "Viewer";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWin_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWin_FormClosed);
			this.Load += new System.EventHandler(this.MainWin_Load);
			this.Shown += new System.EventHandler(this.MainWin_Shown);
			((System.ComponentModel.ISupportInitialize)(this.MChart)).EndInit();
			this.MaGroup.ResumeLayout(false);
			this.MaGroup.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Timer MainTimer;
		private System.Windows.Forms.DataVisualization.Charting.Chart MChart;
		private System.Windows.Forms.TextBox DateTimeSt;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox DateTimeEd;
		private System.Windows.Forms.GroupBox MaGroup;
		private System.Windows.Forms.CheckBox MaDay_05;
		private System.Windows.Forms.CheckBox MaDay_04;
		private System.Windows.Forms.CheckBox MaDay_03;
		private System.Windows.Forms.CheckBox MaDay_02;
		private System.Windows.Forms.CheckBox MaDay_01;
		private System.Windows.Forms.CheckBox MaDay_80;
		private System.Windows.Forms.CheckBox MaDay_75;
		private System.Windows.Forms.CheckBox MaDay_70;
		private System.Windows.Forms.CheckBox MaDay_65;
		private System.Windows.Forms.CheckBox MaDay_60;
		private System.Windows.Forms.CheckBox MaDay_55;
		private System.Windows.Forms.CheckBox MaDay_50;
		private System.Windows.Forms.CheckBox MaDay_45;
		private System.Windows.Forms.CheckBox MaDay_40;
		private System.Windows.Forms.CheckBox MaDay_35;
		private System.Windows.Forms.CheckBox MaDay_30;
		private System.Windows.Forms.CheckBox MaDay_29;
		private System.Windows.Forms.CheckBox MaDay_28;
		private System.Windows.Forms.CheckBox MaDay_27;
		private System.Windows.Forms.CheckBox MaDay_26;
		private System.Windows.Forms.CheckBox MaDay_25;
		private System.Windows.Forms.CheckBox MaDay_24;
		private System.Windows.Forms.CheckBox MaDay_23;
		private System.Windows.Forms.CheckBox MaDay_22;
		private System.Windows.Forms.CheckBox MaDay_21;
		private System.Windows.Forms.CheckBox MaDay_20;
		private System.Windows.Forms.CheckBox MaDay_19;
		private System.Windows.Forms.CheckBox MaDay_18;
		private System.Windows.Forms.CheckBox MaDay_17;
		private System.Windows.Forms.CheckBox MaDay_16;
		private System.Windows.Forms.CheckBox MaDay_15;
		private System.Windows.Forms.CheckBox MaDay_14;
		private System.Windows.Forms.CheckBox MaDay_13;
		private System.Windows.Forms.CheckBox MaDay_12;
		private System.Windows.Forms.CheckBox MaDay_11;
		private System.Windows.Forms.CheckBox MaDay_10;
		private System.Windows.Forms.CheckBox MaDay_09;
		private System.Windows.Forms.CheckBox MaDay_08;
		private System.Windows.Forms.CheckBox MaDay_07;
		private System.Windows.Forms.CheckBox MaDay_06;
		private System.Windows.Forms.Button BtnBack;
		private System.Windows.Forms.Button BtnForward;
		private System.Windows.Forms.Button BtnUnexpand;
		private System.Windows.Forms.Button BtnExpand;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel Status;
		private System.Windows.Forms.ToolStripStatusLabel SubStatus;
		private System.Windows.Forms.ComboBox CurrPair;
		private System.Windows.Forms.Label label3;
	}
}

