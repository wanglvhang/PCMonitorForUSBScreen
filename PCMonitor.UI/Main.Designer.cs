
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            NotifyIcon = new System.Windows.Forms.NotifyIcon(components);
            ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
            exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            btnStart = new System.Windows.Forms.Button();
            btnStop = new System.Windows.Forms.Button();
            cmbThemes = new System.Windows.Forms.ComboBox();
            label1 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            groupBox1 = new System.Windows.Forms.GroupBox();
            labRenderWH = new System.Windows.Forms.Label();
            label17 = new System.Windows.Forms.Label();
            labScreenWH = new System.Windows.Forms.Label();
            label20 = new System.Windows.Forms.Label();
            labDeviceStatus = new System.Windows.Forms.Label();
            label18 = new System.Windows.Forms.Label();
            labComName = new System.Windows.Forms.Label();
            label21 = new System.Windows.Forms.Label();
            labWidgetCount = new System.Windows.Forms.Label();
            labDevice = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            groupBox2 = new System.Windows.Forms.GroupBox();
            labRenderTime = new System.Windows.Forms.Label();
            labFrameCount = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            ckbAutoStart = new System.Windows.Forms.CheckBox();
            ckbScreenProtect = new System.Windows.Forms.CheckBox();
            groupBox3 = new System.Windows.Forms.GroupBox();
            cmbMainboardFan = new System.Windows.Forms.ComboBox();
            label14 = new System.Windows.Forms.Label();
            label15 = new System.Windows.Forms.Label();
            cmbFrameTime = new System.Windows.Forms.ComboBox();
            numScreenprotectInterval = new System.Windows.Forms.NumericUpDown();
            label11 = new System.Windows.Forms.Label();
            label13 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            dtpStartDate = new System.Windows.Forms.DateTimePicker();
            cmbCPUFans = new System.Windows.Forms.ComboBox();
            label10 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            cmbNetInterfaces = new System.Windows.Forms.ComboBox();
            groupBox4 = new System.Windows.Forms.GroupBox();
            linkAuthor = new System.Windows.Forms.LinkLabel();
            lnkGitHub = new System.Windows.Forms.LinkLabel();
            label12 = new System.Windows.Forms.Label();
            tbarBrightness = new System.Windows.Forms.TrackBar();
            label7 = new System.Windows.Forms.Label();
            labBrightness = new System.Windows.Forms.Label();
            groupBox5 = new System.Windows.Forms.GroupBox();
            label22 = new System.Windows.Forms.Label();
            cmbScreenInvert = new System.Windows.Forms.ComboBox();
            ContextMenuStrip.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numScreenprotectInterval).BeginInit();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tbarBrightness).BeginInit();
            groupBox5.SuspendLayout();
            SuspendLayout();
            // 
            // NotifyIcon
            // 
            NotifyIcon.ContextMenuStrip = ContextMenuStrip;
            NotifyIcon.Icon = (System.Drawing.Icon)resources.GetObject("NotifyIcon.Icon");
            NotifyIcon.Text = "PCMonitor";
            NotifyIcon.Visible = true;
            NotifyIcon.MouseClick += NotifyIcon_MouseClick;
            // 
            // ContextMenuStrip
            // 
            ContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { exitToolStripMenuItem });
            ContextMenuStrip.Name = "ContextMenuStrip";
            ContextMenuStrip.Size = new System.Drawing.Size(97, 26);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // btnStart
            // 
            btnStart.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
            btnStart.ForeColor = System.Drawing.Color.Teal;
            btnStart.Location = new System.Drawing.Point(443, 516);
            btnStart.Margin = new System.Windows.Forms.Padding(4);
            btnStart.Name = "btnStart";
            btnStart.Size = new System.Drawing.Size(108, 54);
            btnStart.TabIndex = 1;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // btnStop
            // 
            btnStop.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
            btnStop.ForeColor = System.Drawing.Color.OrangeRed;
            btnStop.Location = new System.Drawing.Point(287, 516);
            btnStop.Margin = new System.Windows.Forms.Padding(4);
            btnStop.Name = "btnStop";
            btnStop.Size = new System.Drawing.Size(108, 54);
            btnStop.TabIndex = 2;
            btnStop.Text = "Stop";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // cmbThemes
            // 
            cmbThemes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbThemes.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            cmbThemes.FormattingEnabled = true;
            cmbThemes.Location = new System.Drawing.Point(146, 33);
            cmbThemes.Margin = new System.Windows.Forms.Padding(4);
            cmbThemes.Name = "cmbThemes";
            cmbThemes.Size = new System.Drawing.Size(174, 24);
            cmbThemes.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(61, 38);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(76, 17);
            label1.TabIndex = 4;
            label1.Text = "主题/Theme";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(54, 81);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(75, 17);
            label4.TabIndex = 9;
            label4.Text = "设备/Device";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(labRenderWH);
            groupBox1.Controls.Add(label17);
            groupBox1.Controls.Add(labScreenWH);
            groupBox1.Controls.Add(label20);
            groupBox1.Controls.Add(labDeviceStatus);
            groupBox1.Controls.Add(label18);
            groupBox1.Controls.Add(labComName);
            groupBox1.Controls.Add(label21);
            groupBox1.Controls.Add(labWidgetCount);
            groupBox1.Controls.Add(labDevice);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(cmbThemes);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new System.Drawing.Point(258, 17);
            groupBox1.Margin = new System.Windows.Forms.Padding(4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(4);
            groupBox1.Size = new System.Drawing.Size(338, 276);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            groupBox1.Text = "主题与设备/Theme&&Device Info";
            // 
            // labRenderWH
            // 
            labRenderWH.AutoSize = true;
            labRenderWH.Location = new System.Drawing.Point(147, 244);
            labRenderWH.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labRenderWH.Name = "labRenderWH";
            labRenderWH.Size = new System.Drawing.Size(31, 17);
            labRenderWH.TabIndex = 21;
            labRenderWH.Text = "N/A";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new System.Drawing.Point(6, 244);
            label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label17.Name = "label17";
            label17.Size = new System.Drawing.Size(128, 17);
            label17.TabIndex = 20;
            label17.Text = "渲染像素/Render WH";
            // 
            // labScreenWH
            // 
            labScreenWH.AutoSize = true;
            labScreenWH.Location = new System.Drawing.Point(146, 212);
            labScreenWH.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labScreenWH.Name = "labScreenWH";
            labScreenWH.Size = new System.Drawing.Size(31, 17);
            labScreenWH.TabIndex = 19;
            labScreenWH.Text = "N/A";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new System.Drawing.Point(6, 211);
            label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label20.Name = "label20";
            label20.Size = new System.Drawing.Size(125, 17);
            label20.TabIndex = 18;
            label20.Text = "设备像素/Screen WH";
            // 
            // labDeviceStatus
            // 
            labDeviceStatus.AutoSize = true;
            labDeviceStatus.Location = new System.Drawing.Point(146, 147);
            labDeviceStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labDeviceStatus.Name = "labDeviceStatus";
            labDeviceStatus.Size = new System.Drawing.Size(31, 17);
            labDeviceStatus.TabIndex = 17;
            labDeviceStatus.Text = "N/A";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new System.Drawing.Point(54, 146);
            label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label18.Name = "label18";
            label18.Size = new System.Drawing.Size(72, 17);
            label18.TabIndex = 16;
            label18.Text = "状态/Status";
            // 
            // labComName
            // 
            labComName.AutoSize = true;
            labComName.Location = new System.Drawing.Point(146, 115);
            labComName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labComName.Name = "labComName";
            labComName.Size = new System.Drawing.Size(31, 17);
            labComName.TabIndex = 15;
            labComName.Text = "N/A";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new System.Drawing.Point(26, 113);
            label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label21.Name = "label21";
            label21.Size = new System.Drawing.Size(102, 17);
            label21.TabIndex = 14;
            label21.Text = "连接/Connection";
            // 
            // labWidgetCount
            // 
            labWidgetCount.AutoSize = true;
            labWidgetCount.Location = new System.Drawing.Point(146, 180);
            labWidgetCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labWidgetCount.Name = "labWidgetCount";
            labWidgetCount.Size = new System.Drawing.Size(31, 17);
            labWidgetCount.TabIndex = 13;
            labWidgetCount.Text = "N/A";
            // 
            // labDevice
            // 
            labDevice.AutoSize = true;
            labDevice.Location = new System.Drawing.Point(146, 82);
            labDevice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labDevice.Name = "labDevice";
            labDevice.Size = new System.Drawing.Size(31, 17);
            labDevice.TabIndex = 12;
            labDevice.Text = "N/A";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(5, 178);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(123, 17);
            label5.TabIndex = 11;
            label5.Text = "Widgets 数量/Count";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(19, 37);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(129, 17);
            label3.TabIndex = 10;
            label3.Text = "单帧时间/Frame Time";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(labRenderTime);
            groupBox2.Controls.Add(labFrameCount);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(label2);
            groupBox2.Location = new System.Drawing.Point(24, 604);
            groupBox2.Margin = new System.Windows.Forms.Padding(4);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new System.Windows.Forms.Padding(4);
            groupBox2.Size = new System.Drawing.Size(574, 118);
            groupBox2.TabIndex = 11;
            groupBox2.TabStop = false;
            groupBox2.Text = "运行信息/Running Info";
            // 
            // labRenderTime
            // 
            labRenderTime.AutoSize = true;
            labRenderTime.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            labRenderTime.Location = new System.Drawing.Point(331, 69);
            labRenderTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labRenderTime.Name = "labRenderTime";
            labRenderTime.Size = new System.Drawing.Size(73, 22);
            labRenderTime.TabIndex = 4;
            labRenderTime.Text = "N/A ms";
            // 
            // labFrameCount
            // 
            labFrameCount.AutoSize = true;
            labFrameCount.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
            labFrameCount.Location = new System.Drawing.Point(156, 69);
            labFrameCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labFrameCount.Name = "labFrameCount";
            labFrameCount.Size = new System.Drawing.Size(44, 22);
            labFrameCount.TabIndex = 3;
            labFrameCount.Text = "N/A";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(306, 40);
            label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(127, 17);
            label6.TabIndex = 1;
            label6.Text = "渲染耗时/Frame Cost";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(121, 40);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(103, 17);
            label2.TabIndex = 0;
            label2.Text = "渲染次数/Frames";
            // 
            // ckbAutoStart
            // 
            ckbAutoStart.AutoSize = true;
            ckbAutoStart.Location = new System.Drawing.Point(21, 521);
            ckbAutoStart.Margin = new System.Windows.Forms.Padding(4);
            ckbAutoStart.Name = "ckbAutoStart";
            ckbAutoStart.Size = new System.Drawing.Size(75, 21);
            ckbAutoStart.TabIndex = 12;
            ckbAutoStart.Text = "自动启动";
            ckbAutoStart.UseVisualStyleBackColor = true;
            // 
            // ckbScreenProtect
            // 
            ckbScreenProtect.AutoSize = true;
            ckbScreenProtect.Location = new System.Drawing.Point(21, 402);
            ckbScreenProtect.Margin = new System.Windows.Forms.Padding(4);
            ckbScreenProtect.Name = "ckbScreenProtect";
            ckbScreenProtect.Size = new System.Drawing.Size(51, 21);
            ckbScreenProtect.TabIndex = 13;
            ckbScreenProtect.Text = "屏保";
            ckbScreenProtect.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(cmbMainboardFan);
            groupBox3.Controls.Add(label14);
            groupBox3.Controls.Add(label15);
            groupBox3.Controls.Add(cmbFrameTime);
            groupBox3.Controls.Add(numScreenprotectInterval);
            groupBox3.Controls.Add(label11);
            groupBox3.Controls.Add(label13);
            groupBox3.Controls.Add(label3);
            groupBox3.Controls.Add(label8);
            groupBox3.Controls.Add(ckbAutoStart);
            groupBox3.Controls.Add(dtpStartDate);
            groupBox3.Controls.Add(cmbCPUFans);
            groupBox3.Controls.Add(label10);
            groupBox3.Controls.Add(label9);
            groupBox3.Controls.Add(cmbNetInterfaces);
            groupBox3.Controls.Add(ckbScreenProtect);
            groupBox3.Location = new System.Drawing.Point(16, 17);
            groupBox3.Margin = new System.Windows.Forms.Padding(4);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new System.Windows.Forms.Padding(4);
            groupBox3.Size = new System.Drawing.Size(229, 568);
            groupBox3.TabIndex = 14;
            groupBox3.TabStop = false;
            groupBox3.Text = "配置/Configuration";
            // 
            // cmbMainboardFan
            // 
            cmbMainboardFan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbMainboardFan.FormattingEnabled = true;
            cmbMainboardFan.Location = new System.Drawing.Point(19, 201);
            cmbMainboardFan.Margin = new System.Windows.Forms.Padding(4);
            cmbMainboardFan.Name = "cmbMainboardFan";
            cmbMainboardFan.Size = new System.Drawing.Size(172, 25);
            cmbMainboardFan.TabIndex = 23;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new System.Drawing.Point(19, 178);
            label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label14.Name = "label14";
            label14.Size = new System.Drawing.Size(150, 17);
            label14.TabIndex = 22;
            label14.Text = "主板风扇/Mainboard Fan";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new System.Drawing.Point(195, 62);
            label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label15.Name = "label15";
            label15.Size = new System.Drawing.Size(25, 17);
            label15.TabIndex = 13;
            label15.Text = "ms";
            // 
            // cmbFrameTime
            // 
            cmbFrameTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbFrameTime.FormattingEnabled = true;
            cmbFrameTime.Items.AddRange(new object[] { "0", "500", "800", "1000", "1200", "1600", "2000" });
            cmbFrameTime.Location = new System.Drawing.Point(19, 57);
            cmbFrameTime.Margin = new System.Windows.Forms.Padding(4);
            cmbFrameTime.Name = "cmbFrameTime";
            cmbFrameTime.Size = new System.Drawing.Size(170, 25);
            cmbFrameTime.TabIndex = 12;
            // 
            // numScreenprotectInterval
            // 
            numScreenprotectInterval.Location = new System.Drawing.Point(24, 468);
            numScreenprotectInterval.Margin = new System.Windows.Forms.Padding(4);
            numScreenprotectInterval.Maximum = new decimal(new int[] { 720, 0, 0, 0 });
            numScreenprotectInterval.Minimum = new decimal(new int[] { 60, 0, 0, 0 });
            numScreenprotectInterval.Name = "numScreenprotectInterval";
            numScreenprotectInterval.Size = new System.Drawing.Size(56, 23);
            numScreenprotectInterval.TabIndex = 21;
            numScreenprotectInterval.Tag = "";
            numScreenprotectInterval.Value = new decimal(new int[] { 60, 0, 0, 0 });
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(18, 327);
            label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(119, 17);
            label11.TabIndex = 5;
            label11.Text = "开始日期/Start Date";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            label13.Location = new System.Drawing.Point(18, 445);
            label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(77, 12);
            label13.TabIndex = 20;
            label13.Text = "屏保运行间隔";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            label8.Location = new System.Drawing.Point(80, 476);
            label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(89, 12);
            label8.TabIndex = 19;
            label8.Text = "分钟（60~720）";
            // 
            // dtpStartDate
            // 
            dtpStartDate.Location = new System.Drawing.Point(19, 353);
            dtpStartDate.Margin = new System.Windows.Forms.Padding(4);
            dtpStartDate.Name = "dtpStartDate";
            dtpStartDate.Size = new System.Drawing.Size(172, 23);
            dtpStartDate.TabIndex = 4;
            dtpStartDate.Value = new System.DateTime(2021, 8, 7, 0, 0, 0, 0);
            // 
            // cmbCPUFans
            // 
            cmbCPUFans.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbCPUFans.FormattingEnabled = true;
            cmbCPUFans.Location = new System.Drawing.Point(19, 128);
            cmbCPUFans.Margin = new System.Windows.Forms.Padding(4);
            cmbCPUFans.Name = "cmbCPUFans";
            cmbCPUFans.Size = new System.Drawing.Size(172, 25);
            cmbCPUFans.TabIndex = 3;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(19, 103);
            label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(85, 17);
            label10.TabIndex = 2;
            label10.Text = "CPU 风扇/Fan";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(16, 252);
            label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(137, 17);
            label9.TabIndex = 1;
            label9.Text = "监控网卡/Net Interface";
            // 
            // cmbNetInterfaces
            // 
            cmbNetInterfaces.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbNetInterfaces.FormattingEnabled = true;
            cmbNetInterfaces.Location = new System.Drawing.Point(19, 278);
            cmbNetInterfaces.Margin = new System.Windows.Forms.Padding(4);
            cmbNetInterfaces.Name = "cmbNetInterfaces";
            cmbNetInterfaces.Size = new System.Drawing.Size(172, 25);
            cmbNetInterfaces.TabIndex = 0;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(linkAuthor);
            groupBox4.Controls.Add(lnkGitHub);
            groupBox4.Controls.Add(label12);
            groupBox4.Location = new System.Drawing.Point(610, 17);
            groupBox4.Margin = new System.Windows.Forms.Padding(4);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new System.Windows.Forms.Padding(4);
            groupBox4.Size = new System.Drawing.Size(387, 704);
            groupBox4.TabIndex = 15;
            groupBox4.TabStop = false;
            groupBox4.Text = "说明/Caption";
            // 
            // linkAuthor
            // 
            linkAuthor.AutoSize = true;
            linkAuthor.Location = new System.Drawing.Point(290, 659);
            linkAuthor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            linkAuthor.Name = "linkAuthor";
            linkAuthor.Size = new System.Drawing.Size(68, 17);
            linkAuthor.TabIndex = 2;
            linkAuthor.TabStop = true;
            linkAuthor.Text = "开发者主页";
            linkAuthor.LinkClicked += linkAuthor_LinkClicked;
            // 
            // lnkGitHub
            // 
            lnkGitHub.AutoSize = true;
            lnkGitHub.Location = new System.Drawing.Point(170, 659);
            lnkGitHub.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lnkGitHub.Name = "lnkGitHub";
            lnkGitHub.Size = new System.Drawing.Size(96, 17);
            lnkGitHub.TabIndex = 1;
            lnkGitHub.TabStop = true;
            lnkGitHub.Text = "项目GitHub主页";
            lnkGitHub.LinkClicked += lnkGitHub_LinkClicked;
            // 
            // label12
            // 
            label12.FlatStyle = System.Windows.Forms.FlatStyle.System;
            label12.Location = new System.Drawing.Point(8, 24);
            label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(372, 619);
            label12.TabIndex = 0;
            label12.Text = resources.GetString("label12.Text");
            // 
            // tbarBrightness
            // 
            tbarBrightness.Location = new System.Drawing.Point(20, 112);
            tbarBrightness.Margin = new System.Windows.Forms.Padding(4);
            tbarBrightness.Maximum = 100;
            tbarBrightness.Name = "tbarBrightness";
            tbarBrightness.Size = new System.Drawing.Size(287, 45);
            tbarBrightness.TabIndex = 14;
            tbarBrightness.Value = 50;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(18, 86);
            label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(98, 17);
            label7.TabIndex = 16;
            label7.Text = "亮度/Brightness";
            // 
            // labBrightness
            // 
            labBrightness.AutoSize = true;
            labBrightness.Location = new System.Drawing.Point(273, 85);
            labBrightness.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labBrightness.Name = "labBrightness";
            labBrightness.Size = new System.Drawing.Size(22, 17);
            labBrightness.TabIndex = 17;
            labBrightness.Text = "50";
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(label22);
            groupBox5.Controls.Add(labBrightness);
            groupBox5.Controls.Add(cmbScreenInvert);
            groupBox5.Controls.Add(label7);
            groupBox5.Controls.Add(tbarBrightness);
            groupBox5.Location = new System.Drawing.Point(259, 307);
            groupBox5.Margin = new System.Windows.Forms.Padding(4);
            groupBox5.Name = "groupBox5";
            groupBox5.Padding = new System.Windows.Forms.Padding(4);
            groupBox5.Size = new System.Drawing.Size(337, 178);
            groupBox5.TabIndex = 22;
            groupBox5.TabStop = false;
            groupBox5.Text = "屏幕显示/Screen Ajust";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new System.Drawing.Point(74, 50);
            label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label22.Name = "label22";
            label22.Size = new System.Drawing.Size(56, 17);
            label22.TabIndex = 20;
            label22.Text = "屏幕翻转";
            // 
            // cmbScreenInvert
            // 
            cmbScreenInvert.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbScreenInvert.FormattingEnabled = true;
            cmbScreenInvert.Items.AddRange(new object[] { "不翻转", "翻转180°" });
            cmbScreenInvert.Location = new System.Drawing.Point(145, 45);
            cmbScreenInvert.Margin = new System.Windows.Forms.Padding(4);
            cmbScreenInvert.Name = "cmbScreenInvert";
            cmbScreenInvert.Size = new System.Drawing.Size(148, 25);
            cmbScreenInvert.TabIndex = 21;
            // 
            // Main
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1013, 734);
            Controls.Add(groupBox5);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(btnStart);
            Controls.Add(btnStop);
            Controls.Add(groupBox1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4);
            MaximizeBox = false;
            Name = "Main";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "PCMonitor";
            FormClosing += Main_FormClosing;
            Load += Main_Load;
            SizeChanged += Main_SizeChanged;
            ContextMenuStrip.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numScreenprotectInterval).EndInit();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)tbarBrightness).EndInit();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            ResumeLayout(false);
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
        private System.Windows.Forms.Label labComName;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label labDeviceStatus;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label labScreenWH;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.LinkLabel linkAuthor;
        private System.Windows.Forms.ComboBox cmbMainboardFan;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label labRenderWH;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cmbScreenInvert;
        private System.Windows.Forms.Label label22;
    }
}

