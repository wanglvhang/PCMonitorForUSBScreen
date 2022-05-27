
namespace PCMonitor.UI
{
    partial class Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.cmbThemes = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labWidgetCount = new System.Windows.Forms.Label();
            this.labDevice = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labRenderTime = new System.Windows.Forms.Label();
            this.labFrameCount = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ckbAutoStart = new System.Windows.Forms.CheckBox();
            this.ckbScreenProtect = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cmbFrameTime = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.cmbCPUFans = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbNetInterfaces = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lnkGitHub = new System.Windows.Forms.LinkLabel();
            this.label12 = new System.Windows.Forms.Label();
            this.tbarBrightness = new System.Windows.Forms.TrackBar();
            this.label7 = new System.Windows.Forms.Label();
            this.labBrightness = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.numScreenprotectInterval = new System.Windows.Forms.NumericUpDown();
            this.ContextMenuStrip.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbarBrightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numScreenprotectInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // NotifyIcon
            // 
            this.NotifyIcon.ContextMenuStrip = this.ContextMenuStrip;
            this.NotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyIcon.Icon")));
            this.NotifyIcon.Text = "PCMonitor";
            this.NotifyIcon.Visible = true;
            this.NotifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseClick);
            // 
            // ContextMenuStrip
            // 
            this.ContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.ContextMenuStrip.Name = "ContextMenuStrip";
            this.ContextMenuStrip.Size = new System.Drawing.Size(97, 26);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStart.ForeColor = System.Drawing.Color.Teal;
            this.btnStart.Location = new System.Drawing.Point(412, 321);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(93, 38);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStop.ForeColor = System.Drawing.Color.OrangeRed;
            this.btnStop.Location = new System.Drawing.Point(312, 321);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(93, 38);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // cmbThemes
            // 
            this.cmbThemes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbThemes.FormattingEnabled = true;
            this.cmbThemes.Location = new System.Drawing.Point(161, 20);
            this.cmbThemes.Name = "cmbThemes";
            this.cmbThemes.Size = new System.Drawing.Size(121, 20);
            this.cmbThemes.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "所选主题/Selected Theme";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(86, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "设备/Device";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labWidgetCount);
            this.groupBox1.Controls.Add(this.labDevice);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cmbThemes);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(223, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(291, 110);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "主题信息/Theme Info";
            // 
            // labWidgetCount
            // 
            this.labWidgetCount.AutoSize = true;
            this.labWidgetCount.Location = new System.Drawing.Point(159, 82);
            this.labWidgetCount.Name = "labWidgetCount";
            this.labWidgetCount.Size = new System.Drawing.Size(23, 12);
            this.labWidgetCount.TabIndex = 13;
            this.labWidgetCount.Text = "N/A";
            // 
            // labDevice
            // 
            this.labDevice.AutoSize = true;
            this.labDevice.Location = new System.Drawing.Point(159, 54);
            this.labDevice.Name = "labDevice";
            this.labDevice.Size = new System.Drawing.Size(23, 12);
            this.labDevice.TabIndex = 12;
            this.labDevice.Text = "N/A";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(44, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "Widgets 数量/Count";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "单帧时间/Frame Time";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labRenderTime);
            this.groupBox2.Controls.Add(this.labFrameCount);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(223, 128);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(291, 122);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "运行信息/Running Info";
            // 
            // labRenderTime
            // 
            this.labRenderTime.AutoSize = true;
            this.labRenderTime.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.labRenderTime.Location = new System.Drawing.Point(61, 89);
            this.labRenderTime.Name = "labRenderTime";
            this.labRenderTime.Size = new System.Drawing.Size(73, 22);
            this.labRenderTime.TabIndex = 4;
            this.labRenderTime.Text = "N/A ms";
            // 
            // labFrameCount
            // 
            this.labFrameCount.AutoSize = true;
            this.labFrameCount.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labFrameCount.Location = new System.Drawing.Point(62, 40);
            this.labFrameCount.Name = "labFrameCount";
            this.labFrameCount.Size = new System.Drawing.Size(44, 22);
            this.labFrameCount.TabIndex = 3;
            this.labFrameCount.Text = "N/A";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "渲染耗时/Frame Cost";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "渲染次数/Frames";
            // 
            // ckbAutoStart
            // 
            this.ckbAutoStart.AutoSize = true;
            this.ckbAutoStart.Location = new System.Drawing.Point(37, 266);
            this.ckbAutoStart.Name = "ckbAutoStart";
            this.ckbAutoStart.Size = new System.Drawing.Size(72, 16);
            this.ckbAutoStart.TabIndex = 12;
            this.ckbAutoStart.Text = "自动启动";
            this.ckbAutoStart.UseVisualStyleBackColor = true;
            // 
            // ckbScreenProtect
            // 
            this.ckbScreenProtect.AutoSize = true;
            this.ckbScreenProtect.Location = new System.Drawing.Point(150, 266);
            this.ckbScreenProtect.Name = "ckbScreenProtect";
            this.ckbScreenProtect.Size = new System.Drawing.Size(48, 16);
            this.ckbScreenProtect.TabIndex = 13;
            this.ckbScreenProtect.Text = "屏保";
            this.ckbScreenProtect.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.cmbFrameTime);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.dtpStartDate);
            this.groupBox3.Controls.Add(this.cmbCPUFans);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.cmbNetInterfaces);
            this.groupBox3.Location = new System.Drawing.Point(21, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(196, 238);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "配置信息/Configuration Info";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(167, 48);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(17, 12);
            this.label15.TabIndex = 13;
            this.label15.Text = "ms";
            // 
            // cmbFrameTime
            // 
            this.cmbFrameTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFrameTime.FormattingEnabled = true;
            this.cmbFrameTime.Items.AddRange(new object[] {
            "500",
            "800",
            "1000",
            "1200"});
            this.cmbFrameTime.Location = new System.Drawing.Point(16, 40);
            this.cmbFrameTime.Name = "cmbFrameTime";
            this.cmbFrameTime.Size = new System.Drawing.Size(146, 20);
            this.cmbFrameTime.TabIndex = 12;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(15, 175);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(119, 12);
            this.label11.TabIndex = 5;
            this.label11.Text = "开始日期/Start Date";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(16, 193);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(148, 21);
            this.dtpStartDate.TabIndex = 4;
            this.dtpStartDate.Value = new System.DateTime(2021, 8, 7, 0, 0, 0, 0);
            // 
            // cmbCPUFans
            // 
            this.cmbCPUFans.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCPUFans.FormattingEnabled = true;
            this.cmbCPUFans.Location = new System.Drawing.Point(16, 90);
            this.cmbCPUFans.Name = "cmbCPUFans";
            this.cmbCPUFans.Size = new System.Drawing.Size(148, 20);
            this.cmbCPUFans.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 73);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 12);
            this.label10.TabIndex = 2;
            this.label10.Text = "CPU 风扇/Fan";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 123);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(137, 12);
            this.label9.TabIndex = 1;
            this.label9.Text = "监控网卡/Net Interface";
            // 
            // cmbNetInterfaces
            // 
            this.cmbNetInterfaces.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNetInterfaces.FormattingEnabled = true;
            this.cmbNetInterfaces.Location = new System.Drawing.Point(16, 141);
            this.cmbNetInterfaces.Name = "cmbNetInterfaces";
            this.cmbNetInterfaces.Size = new System.Drawing.Size(148, 20);
            this.cmbNetInterfaces.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lnkGitHub);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Location = new System.Drawing.Point(21, 375);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(496, 130);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "说明/Caption";
            // 
            // lnkGitHub
            // 
            this.lnkGitHub.AutoSize = true;
            this.lnkGitHub.Location = new System.Drawing.Point(9, 104);
            this.lnkGitHub.Name = "lnkGitHub";
            this.lnkGitHub.Size = new System.Drawing.Size(89, 12);
            this.lnkGitHub.TabIndex = 1;
            this.lnkGitHub.TabStop = true;
            this.lnkGitHub.Text = "项目GitHub主页";
            this.lnkGitHub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkGitHub_LinkClicked);
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(7, 17);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(483, 94);
            this.label12.TabIndex = 0;
            this.label12.Text = "渲染耗时指的是从获取硬件信息到信息呈现到屏幕所耗费的时间，实际上的每帧时间控制为设置的单帧时间。\r\n\r\n渲染次数既总共渲染过的帧总数。\r\n\r\n屏保运行时将以红、黄" +
    "、蓝三种颜色分别刷新一次屏幕。";
            // 
            // tbarBrightness
            // 
            this.tbarBrightness.Location = new System.Drawing.Point(37, 324);
            this.tbarBrightness.Maximum = 100;
            this.tbarBrightness.Name = "tbarBrightness";
            this.tbarBrightness.Size = new System.Drawing.Size(225, 45);
            this.tbarBrightness.TabIndex = 14;
            this.tbarBrightness.Value = 50;
            this.tbarBrightness.ValueChanged += new System.EventHandler(this.tbarBrightness_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(35, 305);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 12);
            this.label7.TabIndex = 16;
            this.label7.Text = "亮度/Brightness";
            // 
            // labBrightness
            // 
            this.labBrightness.AutoSize = true;
            this.labBrightness.Location = new System.Drawing.Point(260, 328);
            this.labBrightness.Name = "labBrightness";
            this.labBrightness.Size = new System.Drawing.Size(17, 12);
            this.labBrightness.TabIndex = 17;
            this.labBrightness.Text = "50";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(354, 267);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 12);
            this.label8.TabIndex = 19;
            this.label8.Text = "分钟（60~720）";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(226, 266);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 12);
            this.label13.TabIndex = 20;
            this.label13.Text = "屏保运行间隔";
            // 
            // numScreenprotectInterval
            // 
            this.numScreenprotectInterval.Location = new System.Drawing.Point(304, 263);
            this.numScreenprotectInterval.Maximum = new decimal(new int[] {
            720,
            0,
            0,
            0});
            this.numScreenprotectInterval.Minimum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numScreenprotectInterval.Name = "numScreenprotectInterval";
            this.numScreenprotectInterval.Size = new System.Drawing.Size(48, 21);
            this.numScreenprotectInterval.TabIndex = 21;
            this.numScreenprotectInterval.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 517);
            this.Controls.Add(this.numScreenprotectInterval);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.labBrightness);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbarBrightness);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.ckbScreenProtect);
            this.Controls.Add(this.ckbAutoStart);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PCMonitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.SizeChanged += new System.EventHandler(this.Main_SizeChanged);
            this.ContextMenuStrip.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbarBrightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numScreenprotectInterval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon NotifyIcon;
        private System.Windows.Forms.ContextMenuStrip ContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.ComboBox cmbThemes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labRenderTime;
        private System.Windows.Forms.Label labFrameCount;
        private System.Windows.Forms.CheckBox ckbAutoStart;
        private System.Windows.Forms.CheckBox ckbScreenProtect;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbNetInterfaces;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.ComboBox cmbCPUFans;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmbFrameTime;
        private System.Windows.Forms.Label labWidgetCount;
        private System.Windows.Forms.Label labDevice;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.LinkLabel lnkGitHub;
        private System.Windows.Forms.TrackBar tbarBrightness;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labBrightness;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown numScreenprotectInterval;
    }
}

