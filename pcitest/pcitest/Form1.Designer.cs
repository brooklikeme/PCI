namespace pcitest
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnConnect = new System.Windows.Forms.Button();
            this.cbxSerialPorts = new System.Windows.Forms.ComboBox();
            this.pbxSerialStatus = new System.Windows.Forms.PictureBox();
            this.tbxShow = new System.Windows.Forms.TextBox();
            this.tbrMasterPos = new System.Windows.Forms.TrackBar();
            this.pbrContrast = new System.Windows.Forms.ProgressBar();
            this.pbxSwitchStatus = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbrMasterAngle = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbMasterPos = new System.Windows.Forms.Label();
            this.lbMasterAngle = new System.Windows.Forms.Label();
            this.lbSlaveAngle = new System.Windows.Forms.Label();
            this.lbSlavePos = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbrSlaveAngle = new System.Windows.Forms.TrackBar();
            this.label10 = new System.Windows.Forms.Label();
            this.tbrSlavePos = new System.Windows.Forms.TrackBar();
            this.label11 = new System.Windows.Forms.Label();
            this.lbContrast = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lbPressure = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.pbrPressure = new System.Windows.Forms.ProgressBar();
            this.label17 = new System.Windows.Forms.Label();
            this.lbSwitchStatus = new System.Windows.Forms.Label();
            this.btnInitMaster = new System.Windows.Forms.Button();
            this.cbxMasterColor = new System.Windows.Forms.ComboBox();
            this.cbxSlaveColor = new System.Windows.Forms.ComboBox();
            this.btnInitSlave = new System.Windows.Forms.Button();
            this.cbxForce1 = new System.Windows.Forms.ComboBox();
            this.btnForce1 = new System.Windows.Forms.Button();
            this.cbxForce2 = new System.Windows.Forms.ComboBox();
            this.btnForce2 = new System.Windows.Forms.Button();
            this.btnInitAll = new System.Windows.Forms.Button();
            this.btnInitForce = new System.Windows.Forms.Button();
            this.cbxForce3 = new System.Windows.Forms.ComboBox();
            this.btnForce3 = new System.Windows.Forms.Button();
            this.btnForce2Pos = new System.Windows.Forms.Button();
            this.txtForce2Pos = new System.Windows.Forms.TextBox();
            this.txtForce3Pos = new System.Windows.Forms.TextBox();
            this.btnForce3Pos = new System.Windows.Forms.Button();
            this.cbxMasterFine = new System.Windows.Forms.ComboBox();
            this.btnMasterFine = new System.Windows.Forms.Button();
            this.cbxSlaveFine = new System.Windows.Forms.ComboBox();
            this.btnSlaveFine = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSerialStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrMasterPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSwitchStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrMasterAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrSlaveAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrSlavePos)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(182, 30);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(160, 43);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // cbxSerialPorts
            // 
            this.cbxSerialPorts.FormattingEnabled = true;
            this.cbxSerialPorts.ItemHeight = 25;
            this.cbxSerialPorts.Location = new System.Drawing.Point(26, 35);
            this.cbxSerialPorts.Name = "cbxSerialPorts";
            this.cbxSerialPorts.Size = new System.Drawing.Size(139, 33);
            this.cbxSerialPorts.TabIndex = 2;
            // 
            // pbxSerialStatus
            // 
            this.pbxSerialStatus.BackColor = System.Drawing.Color.Red;
            this.pbxSerialStatus.Location = new System.Drawing.Point(364, 32);
            this.pbxSerialStatus.Name = "pbxSerialStatus";
            this.pbxSerialStatus.Size = new System.Drawing.Size(41, 37);
            this.pbxSerialStatus.TabIndex = 3;
            this.pbxSerialStatus.TabStop = false;
            // 
            // tbxShow
            // 
            this.tbxShow.Location = new System.Drawing.Point(1298, 796);
            this.tbxShow.Multiline = true;
            this.tbxShow.Name = "tbxShow";
            this.tbxShow.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbxShow.Size = new System.Drawing.Size(408, 73);
            this.tbxShow.TabIndex = 4;
            this.tbxShow.Visible = false;
            // 
            // tbrMasterPos
            // 
            this.tbrMasterPos.BackColor = System.Drawing.SystemColors.Control;
            this.tbrMasterPos.Location = new System.Drawing.Point(183, 315);
            this.tbrMasterPos.Maximum = 1000;
            this.tbrMasterPos.Name = "tbrMasterPos";
            this.tbrMasterPos.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbrMasterPos.Size = new System.Drawing.Size(950, 90);
            this.tbrMasterPos.TabIndex = 8;
            this.tbrMasterPos.Value = 10;
            // 
            // pbrContrast
            // 
            this.pbrContrast.Location = new System.Drawing.Point(203, 540);
            this.pbrContrast.Maximum = 12000;
            this.pbrContrast.Name = "pbrContrast";
            this.pbrContrast.Size = new System.Drawing.Size(930, 58);
            this.pbrContrast.TabIndex = 9;
            // 
            // pbxSwitchStatus
            // 
            this.pbxSwitchStatus.BackColor = System.Drawing.Color.Gray;
            this.pbxSwitchStatus.Location = new System.Drawing.Point(203, 762);
            this.pbxSwitchStatus.Name = "pbxSwitchStatus";
            this.pbxSwitchStatus.Size = new System.Drawing.Size(228, 58);
            this.pbxSwitchStatus.TabIndex = 12;
            this.pbxSwitchStatus.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 315);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 31);
            this.label1.TabIndex = 13;
            this.label1.Text = "主跟踪模块";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // tbrMasterAngle
            // 
            this.tbrMasterAngle.BackColor = System.Drawing.SystemColors.Control;
            this.tbrMasterAngle.Location = new System.Drawing.Point(1147, 315);
            this.tbrMasterAngle.Maximum = 360;
            this.tbrMasterAngle.Name = "tbrMasterAngle";
            this.tbrMasterAngle.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbrMasterAngle.Size = new System.Drawing.Size(436, 90);
            this.tbrMasterAngle.TabIndex = 15;
            this.tbrMasterAngle.Value = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(482, 281);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 31);
            this.label2.TabIndex = 16;
            this.label2.Text = "位置(mm) -- ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1242, 281);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 31);
            this.label3.TabIndex = 17;
            this.label3.Text = "角度 -- ";
            // 
            // lbMasterPos
            // 
            this.lbMasterPos.AutoSize = true;
            this.lbMasterPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMasterPos.Location = new System.Drawing.Point(655, 281);
            this.lbMasterPos.Name = "lbMasterPos";
            this.lbMasterPos.Size = new System.Drawing.Size(60, 31);
            this.lbMasterPos.TabIndex = 18;
            this.lbMasterPos.Text = "N/A";
            // 
            // lbMasterAngle
            // 
            this.lbMasterAngle.AutoSize = true;
            this.lbMasterAngle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMasterAngle.Location = new System.Drawing.Point(1341, 281);
            this.lbMasterAngle.Name = "lbMasterAngle";
            this.lbMasterAngle.Size = new System.Drawing.Size(60, 31);
            this.lbMasterAngle.TabIndex = 19;
            this.lbMasterAngle.Text = "N/A";
            // 
            // lbSlaveAngle
            // 
            this.lbSlaveAngle.AutoSize = true;
            this.lbSlaveAngle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSlaveAngle.Location = new System.Drawing.Point(1341, 410);
            this.lbSlaveAngle.Name = "lbSlaveAngle";
            this.lbSlaveAngle.Size = new System.Drawing.Size(60, 31);
            this.lbSlaveAngle.TabIndex = 26;
            this.lbSlaveAngle.Text = "N/A";
            // 
            // lbSlavePos
            // 
            this.lbSlavePos.AutoSize = true;
            this.lbSlavePos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSlavePos.Location = new System.Drawing.Point(655, 410);
            this.lbSlavePos.Name = "lbSlavePos";
            this.lbSlavePos.Size = new System.Drawing.Size(60, 31);
            this.lbSlavePos.TabIndex = 25;
            this.lbSlavePos.Text = "N/A";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(1242, 410);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 31);
            this.label8.TabIndex = 24;
            this.label8.Text = "角度 -- ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(482, 410);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(162, 31);
            this.label9.TabIndex = 23;
            this.label9.Text = "位置(mm) -- ";
            // 
            // tbrSlaveAngle
            // 
            this.tbrSlaveAngle.BackColor = System.Drawing.SystemColors.Control;
            this.tbrSlaveAngle.Location = new System.Drawing.Point(1147, 445);
            this.tbrSlaveAngle.Maximum = 3600;
            this.tbrSlaveAngle.Name = "tbrSlaveAngle";
            this.tbrSlaveAngle.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbrSlaveAngle.Size = new System.Drawing.Size(436, 90);
            this.tbrSlaveAngle.TabIndex = 22;
            this.tbrSlaveAngle.Value = 10;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(22, 445);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(149, 31);
            this.label10.TabIndex = 21;
            this.label10.Text = "辅跟踪模块";
            // 
            // tbrSlavePos
            // 
            this.tbrSlavePos.BackColor = System.Drawing.SystemColors.Control;
            this.tbrSlavePos.Location = new System.Drawing.Point(183, 445);
            this.tbrSlavePos.Maximum = 1000;
            this.tbrSlavePos.Name = "tbrSlavePos";
            this.tbrSlavePos.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbrSlavePos.Size = new System.Drawing.Size(950, 90);
            this.tbrSlavePos.TabIndex = 20;
            this.tbrSlavePos.Value = 10;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(22, 554);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(149, 31);
            this.label11.TabIndex = 27;
            this.label11.Text = "造影剂剂量";
            // 
            // lbContrast
            // 
            this.lbContrast.AutoSize = true;
            this.lbContrast.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbContrast.Location = new System.Drawing.Point(1162, 554);
            this.lbContrast.Name = "lbContrast";
            this.lbContrast.Size = new System.Drawing.Size(60, 31);
            this.lbContrast.TabIndex = 29;
            this.lbContrast.Text = "N/A";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(1292, 554);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(116, 31);
            this.label13.TabIndex = 28;
            this.label13.Text = "uL(微升)";
            // 
            // lbPressure
            // 
            this.lbPressure.AutoSize = true;
            this.lbPressure.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPressure.Location = new System.Drawing.Point(1162, 662);
            this.lbPressure.Name = "lbPressure";
            this.lbPressure.Size = new System.Drawing.Size(60, 31);
            this.lbPressure.TabIndex = 33;
            this.lbPressure.Text = "N/A";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(1292, 662);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(137, 31);
            this.label15.TabIndex = 32;
            this.label15.Text = "KPa(千帕)";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(22, 662);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(149, 31);
            this.label16.TabIndex = 31;
            this.label16.Text = "加压泵压力";
            // 
            // pbrPressure
            // 
            this.pbrPressure.Location = new System.Drawing.Point(203, 649);
            this.pbrPressure.Maximum = 400;
            this.pbrPressure.Name = "pbrPressure";
            this.pbrPressure.Size = new System.Drawing.Size(930, 58);
            this.pbrPressure.TabIndex = 30;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(22, 776);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(167, 31);
            this.label17.TabIndex = 34;
            this.label17.Text = "X光脚踏开关";
            // 
            // lbSwitchStatus
            // 
            this.lbSwitchStatus.AutoSize = true;
            this.lbSwitchStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSwitchStatus.Location = new System.Drawing.Point(460, 776);
            this.lbSwitchStatus.Name = "lbSwitchStatus";
            this.lbSwitchStatus.Size = new System.Drawing.Size(41, 31);
            this.lbSwitchStatus.TabIndex = 35;
            this.lbSwitchStatus.Text = "关";
            // 
            // btnInitMaster
            // 
            this.btnInitMaster.Location = new System.Drawing.Point(768, 30);
            this.btnInitMaster.Name = "btnInitMaster";
            this.btnInitMaster.Size = new System.Drawing.Size(160, 43);
            this.btnInitMaster.TabIndex = 36;
            this.btnInitMaster.Text = "主模块初始化";
            this.btnInitMaster.UseVisualStyleBackColor = true;
            this.btnInitMaster.Click += new System.EventHandler(this.btnInitMaster_Click);
            // 
            // cbxMasterColor
            // 
            this.cbxMasterColor.FormattingEnabled = true;
            this.cbxMasterColor.ItemHeight = 25;
            this.cbxMasterColor.Items.AddRange(new object[] {
            "红色",
            "黄色",
            "蓝色"});
            this.cbxMasterColor.Location = new System.Drawing.Point(672, 35);
            this.cbxMasterColor.Name = "cbxMasterColor";
            this.cbxMasterColor.Size = new System.Drawing.Size(85, 33);
            this.cbxMasterColor.TabIndex = 37;
            // 
            // cbxSlaveColor
            // 
            this.cbxSlaveColor.FormattingEnabled = true;
            this.cbxSlaveColor.ItemHeight = 25;
            this.cbxSlaveColor.Items.AddRange(new object[] {
            "红色",
            "黄色",
            "蓝色"});
            this.cbxSlaveColor.Location = new System.Drawing.Point(941, 34);
            this.cbxSlaveColor.Name = "cbxSlaveColor";
            this.cbxSlaveColor.Size = new System.Drawing.Size(85, 33);
            this.cbxSlaveColor.TabIndex = 39;
            // 
            // btnInitSlave
            // 
            this.btnInitSlave.Location = new System.Drawing.Point(1037, 29);
            this.btnInitSlave.Name = "btnInitSlave";
            this.btnInitSlave.Size = new System.Drawing.Size(160, 43);
            this.btnInitSlave.TabIndex = 38;
            this.btnInitSlave.Text = "辅模块初始化";
            this.btnInitSlave.UseVisualStyleBackColor = true;
            this.btnInitSlave.Click += new System.EventHandler(this.btnInitSlave_Click);
            // 
            // cbxForce1
            // 
            this.cbxForce1.FormattingEnabled = true;
            this.cbxForce1.ItemHeight = 25;
            this.cbxForce1.Items.AddRange(new object[] {
            "0",
            "10",
            "20",
            "30",
            "40",
            "50",
            "60",
            "70",
            "80",
            "90",
            "100"});
            this.cbxForce1.Location = new System.Drawing.Point(1048, 112);
            this.cbxForce1.Name = "cbxForce1";
            this.cbxForce1.Size = new System.Drawing.Size(85, 33);
            this.cbxForce1.TabIndex = 41;
            // 
            // btnForce1
            // 
            this.btnForce1.Location = new System.Drawing.Point(1138, 107);
            this.btnForce1.Name = "btnForce1";
            this.btnForce1.Size = new System.Drawing.Size(95, 43);
            this.btnForce1.TabIndex = 40;
            this.btnForce1.Text = "加紧1";
            this.btnForce1.UseVisualStyleBackColor = true;
            this.btnForce1.Click += new System.EventHandler(this.btnForce1_Click);
            // 
            // cbxForce2
            // 
            this.cbxForce2.FormattingEnabled = true;
            this.cbxForce2.ItemHeight = 25;
            this.cbxForce2.Items.AddRange(new object[] {
            "0",
            "10",
            "20",
            "30",
            "40",
            "50",
            "60",
            "70",
            "80",
            "90",
            "100"});
            this.cbxForce2.Location = new System.Drawing.Point(1245, 111);
            this.cbxForce2.Name = "cbxForce2";
            this.cbxForce2.Size = new System.Drawing.Size(85, 33);
            this.cbxForce2.TabIndex = 43;
            // 
            // btnForce2
            // 
            this.btnForce2.Location = new System.Drawing.Point(1341, 106);
            this.btnForce2.Name = "btnForce2";
            this.btnForce2.Size = new System.Drawing.Size(97, 43);
            this.btnForce2.TabIndex = 42;
            this.btnForce2.Text = "加紧2";
            this.btnForce2.UseVisualStyleBackColor = true;
            this.btnForce2.Click += new System.EventHandler(this.btnForce2_Click);
            // 
            // btnInitAll
            // 
            this.btnInitAll.Location = new System.Drawing.Point(474, 29);
            this.btnInitAll.Name = "btnInitAll";
            this.btnInitAll.Size = new System.Drawing.Size(160, 43);
            this.btnInitAll.TabIndex = 44;
            this.btnInitAll.Text = "全部初始化";
            this.btnInitAll.UseVisualStyleBackColor = true;
            this.btnInitAll.Click += new System.EventHandler(this.btnInitAll_Click);
            // 
            // btnInitForce
            // 
            this.btnInitForce.Location = new System.Drawing.Point(1256, 29);
            this.btnInitForce.Name = "btnInitForce";
            this.btnInitForce.Size = new System.Drawing.Size(182, 43);
            this.btnInitForce.TabIndex = 45;
            this.btnInitForce.Text = "加紧机构初始化";
            this.btnInitForce.UseVisualStyleBackColor = true;
            this.btnInitForce.Click += new System.EventHandler(this.btnInitForce_Click);
            // 
            // cbxForce3
            // 
            this.cbxForce3.FormattingEnabled = true;
            this.cbxForce3.ItemHeight = 25;
            this.cbxForce3.Items.AddRange(new object[] {
            "0",
            "10",
            "20",
            "30",
            "40",
            "50",
            "60",
            "70",
            "80",
            "90",
            "100"});
            this.cbxForce3.Location = new System.Drawing.Point(1452, 110);
            this.cbxForce3.Name = "cbxForce3";
            this.cbxForce3.Size = new System.Drawing.Size(85, 33);
            this.cbxForce3.TabIndex = 47;
            // 
            // btnForce3
            // 
            this.btnForce3.Location = new System.Drawing.Point(1548, 105);
            this.btnForce3.Name = "btnForce3";
            this.btnForce3.Size = new System.Drawing.Size(97, 43);
            this.btnForce3.TabIndex = 46;
            this.btnForce3.Text = "加紧3";
            this.btnForce3.UseVisualStyleBackColor = true;
            this.btnForce3.Click += new System.EventHandler(this.btnForce3_Click);
            // 
            // btnForce2Pos
            // 
            this.btnForce2Pos.Location = new System.Drawing.Point(492, 105);
            this.btnForce2Pos.Name = "btnForce2Pos";
            this.btnForce2Pos.Size = new System.Drawing.Size(160, 43);
            this.btnForce2Pos.TabIndex = 48;
            this.btnForce2Pos.Text = "加紧2位置";
            this.btnForce2Pos.UseVisualStyleBackColor = true;
            this.btnForce2Pos.Click += new System.EventHandler(this.btnForce2Pos_Click);
            // 
            // txtForce2Pos
            // 
            this.txtForce2Pos.Location = new System.Drawing.Point(364, 114);
            this.txtForce2Pos.Name = "txtForce2Pos";
            this.txtForce2Pos.Size = new System.Drawing.Size(100, 31);
            this.txtForce2Pos.TabIndex = 49;
            this.txtForce2Pos.Text = "6000";
            // 
            // txtForce3Pos
            // 
            this.txtForce3Pos.Location = new System.Drawing.Point(682, 113);
            this.txtForce3Pos.Name = "txtForce3Pos";
            this.txtForce3Pos.Size = new System.Drawing.Size(100, 31);
            this.txtForce3Pos.TabIndex = 51;
            this.txtForce3Pos.Text = "7000";
            // 
            // btnForce3Pos
            // 
            this.btnForce3Pos.Location = new System.Drawing.Point(810, 104);
            this.btnForce3Pos.Name = "btnForce3Pos";
            this.btnForce3Pos.Size = new System.Drawing.Size(160, 43);
            this.btnForce3Pos.TabIndex = 50;
            this.btnForce3Pos.Text = "加紧3位置";
            this.btnForce3Pos.UseVisualStyleBackColor = true;
            this.btnForce3Pos.Click += new System.EventHandler(this.btnForce3Pos_Click);
            // 
            // cbxMasterFine
            // 
            this.cbxMasterFine.FormattingEnabled = true;
            this.cbxMasterFine.ItemHeight = 25;
            this.cbxMasterFine.Items.AddRange(new object[] {
            "退出",
            "进入"});
            this.cbxMasterFine.Location = new System.Drawing.Point(365, 183);
            this.cbxMasterFine.Name = "cbxMasterFine";
            this.cbxMasterFine.Size = new System.Drawing.Size(85, 33);
            this.cbxMasterFine.TabIndex = 53;
            // 
            // btnMasterFine
            // 
            this.btnMasterFine.Location = new System.Drawing.Point(455, 178);
            this.btnMasterFine.Name = "btnMasterFine";
            this.btnMasterFine.Size = new System.Drawing.Size(302, 43);
            this.btnMasterFine.TabIndex = 52;
            this.btnMasterFine.Text = "主模块精细模式";
            this.btnMasterFine.UseVisualStyleBackColor = true;
            this.btnMasterFine.Click += new System.EventHandler(this.btnMasterFine_Click);
            // 
            // cbxSlaveFine
            // 
            this.cbxSlaveFine.FormattingEnabled = true;
            this.cbxSlaveFine.ItemHeight = 25;
            this.cbxSlaveFine.Items.AddRange(new object[] {
            "退出",
            "进入"});
            this.cbxSlaveFine.Location = new System.Drawing.Point(779, 183);
            this.cbxSlaveFine.Name = "cbxSlaveFine";
            this.cbxSlaveFine.Size = new System.Drawing.Size(85, 33);
            this.cbxSlaveFine.TabIndex = 55;
            // 
            // btnSlaveFine
            // 
            this.btnSlaveFine.Location = new System.Drawing.Point(869, 178);
            this.btnSlaveFine.Name = "btnSlaveFine";
            this.btnSlaveFine.Size = new System.Drawing.Size(302, 43);
            this.btnSlaveFine.TabIndex = 54;
            this.btnSlaveFine.Text = "辅模块精细模式";
            this.btnSlaveFine.UseVisualStyleBackColor = true;
            this.btnSlaveFine.Click += new System.EventHandler(this.btnSlaveFine_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(191F, 191F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1803, 860);
            this.Controls.Add(this.cbxSlaveFine);
            this.Controls.Add(this.btnSlaveFine);
            this.Controls.Add(this.cbxMasterFine);
            this.Controls.Add(this.btnMasterFine);
            this.Controls.Add(this.txtForce3Pos);
            this.Controls.Add(this.btnForce3Pos);
            this.Controls.Add(this.txtForce2Pos);
            this.Controls.Add(this.btnForce2Pos);
            this.Controls.Add(this.cbxForce3);
            this.Controls.Add(this.btnForce3);
            this.Controls.Add(this.btnInitForce);
            this.Controls.Add(this.btnInitAll);
            this.Controls.Add(this.cbxForce2);
            this.Controls.Add(this.btnForce2);
            this.Controls.Add(this.cbxForce1);
            this.Controls.Add(this.btnForce1);
            this.Controls.Add(this.cbxSlaveColor);
            this.Controls.Add(this.btnInitSlave);
            this.Controls.Add(this.cbxMasterColor);
            this.Controls.Add(this.btnInitMaster);
            this.Controls.Add(this.lbSwitchStatus);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.lbPressure);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.pbrPressure);
            this.Controls.Add(this.lbContrast);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lbSlaveAngle);
            this.Controls.Add(this.lbSlavePos);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tbrSlaveAngle);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tbrSlavePos);
            this.Controls.Add(this.lbMasterAngle);
            this.Controls.Add(this.lbMasterPos);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbrMasterAngle);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbxSwitchStatus);
            this.Controls.Add(this.pbrContrast);
            this.Controls.Add(this.tbrMasterPos);
            this.Controls.Add(this.tbxShow);
            this.Controls.Add(this.pbxSerialStatus);
            this.Controls.Add(this.cbxSerialPorts);
            this.Controls.Add(this.btnConnect);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.916231F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PCI测试";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Leave += new System.EventHandler(this.Form1_Leave);
            ((System.ComponentModel.ISupportInitialize)(this.pbxSerialStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrMasterPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSwitchStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrMasterAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrSlaveAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrSlavePos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ComboBox cbxSerialPorts;
        private System.Windows.Forms.PictureBox pbxSerialStatus;
        private System.Windows.Forms.TextBox tbxShow;
        private System.Windows.Forms.TrackBar tbrMasterPos;
        private System.Windows.Forms.ProgressBar pbrContrast;
        private System.Windows.Forms.PictureBox pbxSwitchStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar tbrMasterAngle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbMasterPos;
        private System.Windows.Forms.Label lbMasterAngle;
        private System.Windows.Forms.Label lbSlaveAngle;
        private System.Windows.Forms.Label lbSlavePos;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TrackBar tbrSlaveAngle;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TrackBar tbrSlavePos;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lbContrast;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lbPressure;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ProgressBar pbrPressure;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lbSwitchStatus;
        private System.Windows.Forms.Button btnInitMaster;
        private System.Windows.Forms.ComboBox cbxMasterColor;
        private System.Windows.Forms.ComboBox cbxSlaveColor;
        private System.Windows.Forms.Button btnInitSlave;
        private System.Windows.Forms.ComboBox cbxForce1;
        private System.Windows.Forms.Button btnForce1;
        private System.Windows.Forms.ComboBox cbxForce2;
        private System.Windows.Forms.Button btnForce2;
        private System.Windows.Forms.Button btnInitAll;
        private System.Windows.Forms.Button btnInitForce;
        private System.Windows.Forms.ComboBox cbxForce3;
        private System.Windows.Forms.Button btnForce3;
        private System.Windows.Forms.Button btnForce2Pos;
        private System.Windows.Forms.TextBox txtForce2Pos;
        private System.Windows.Forms.TextBox txtForce3Pos;
        private System.Windows.Forms.Button btnForce3Pos;
        private System.Windows.Forms.ComboBox cbxMasterFine;
        private System.Windows.Forms.Button btnMasterFine;
        private System.Windows.Forms.ComboBox cbxSlaveFine;
        private System.Windows.Forms.Button btnSlaveFine;
    }
}

