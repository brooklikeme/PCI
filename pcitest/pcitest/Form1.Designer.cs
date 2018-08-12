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
            this.components = new System.ComponentModel.Container();
            this.btnConnect = new System.Windows.Forms.Button();
            this.cbxSerialPorts = new System.Windows.Forms.ComboBox();
            this.pbxStatus = new System.Windows.Forms.PictureBox();
            this.tbxShow = new System.Windows.Forms.TextBox();
            this.tbrMasterPos = new System.Windows.Forms.TrackBar();
            this.pbrContrast = new System.Windows.Forms.ProgressBar();
            this.pbxSwitchStatus1 = new System.Windows.Forms.PictureBox();
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
            this.lbSwitchStatus1 = new System.Windows.Forms.Label();
            this.btnInitMaster = new System.Windows.Forms.Button();
            this.btnInitSlave = new System.Windows.Forms.Button();
            this.btnInitAll = new System.Windows.Forms.Button();
            this.btnInitForce = new System.Windows.Forms.Button();
            this.btnForcePos = new System.Windows.Forms.Button();
            this.lbSwitchStatus2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pbxSwitchStatus2 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.udForce2Strength = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.udForce2Pos = new System.Windows.Forms.NumericUpDown();
            this.udForce3Pos = new System.Windows.Forms.NumericUpDown();
            this.label19 = new System.Windows.Forms.Label();
            this.udForce1Strength = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.udForce3Strength = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnInitForceStrength = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.lbStatus = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbxStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrMasterPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSwitchStatus1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrMasterAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrSlaveAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrSlavePos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSwitchStatus2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udForce2Strength)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udForce2Pos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udForce3Pos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udForce1Strength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udForce3Strength)).BeginInit();
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
            // pbxStatus
            // 
            this.pbxStatus.BackColor = System.Drawing.Color.Red;
            this.pbxStatus.Location = new System.Drawing.Point(573, 32);
            this.pbxStatus.Name = "pbxStatus";
            this.pbxStatus.Size = new System.Drawing.Size(41, 37);
            this.pbxStatus.TabIndex = 3;
            this.pbxStatus.TabStop = false;
            // 
            // tbxShow
            // 
            this.tbxShow.Location = new System.Drawing.Point(1302, 944);
            this.tbxShow.Multiline = true;
            this.tbxShow.Name = "tbxShow";
            this.tbxShow.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbxShow.Size = new System.Drawing.Size(408, 74);
            this.tbxShow.TabIndex = 4;
            this.tbxShow.Visible = false;
            // 
            // tbrMasterPos
            // 
            this.tbrMasterPos.BackColor = System.Drawing.SystemColors.Control;
            this.tbrMasterPos.Location = new System.Drawing.Point(187, 463);
            this.tbrMasterPos.Maximum = 1000;
            this.tbrMasterPos.Name = "tbrMasterPos";
            this.tbrMasterPos.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbrMasterPos.Size = new System.Drawing.Size(950, 90);
            this.tbrMasterPos.TabIndex = 8;
            this.tbrMasterPos.Value = 10;
            // 
            // pbrContrast
            // 
            this.pbrContrast.Location = new System.Drawing.Point(207, 688);
            this.pbrContrast.Maximum = 12000;
            this.pbrContrast.Name = "pbrContrast";
            this.pbrContrast.Size = new System.Drawing.Size(930, 59);
            this.pbrContrast.TabIndex = 9;
            // 
            // pbxSwitchStatus1
            // 
            this.pbxSwitchStatus1.BackColor = System.Drawing.Color.Gray;
            this.pbxSwitchStatus1.Location = new System.Drawing.Point(207, 910);
            this.pbxSwitchStatus1.Name = "pbxSwitchStatus1";
            this.pbxSwitchStatus1.Size = new System.Drawing.Size(228, 59);
            this.pbxSwitchStatus1.TabIndex = 12;
            this.pbxSwitchStatus1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(26, 463);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 31);
            this.label1.TabIndex = 13;
            this.label1.Text = "主跟踪模块";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // tbrMasterAngle
            // 
            this.tbrMasterAngle.BackColor = System.Drawing.SystemColors.Control;
            this.tbrMasterAngle.Location = new System.Drawing.Point(1151, 463);
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
            this.label2.Location = new System.Drawing.Point(486, 429);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 31);
            this.label2.TabIndex = 16;
            this.label2.Text = "位置(mm) -- ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1246, 429);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 31);
            this.label3.TabIndex = 17;
            this.label3.Text = "角度 -- ";
            // 
            // lbMasterPos
            // 
            this.lbMasterPos.AutoSize = true;
            this.lbMasterPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMasterPos.Location = new System.Drawing.Point(659, 429);
            this.lbMasterPos.Name = "lbMasterPos";
            this.lbMasterPos.Size = new System.Drawing.Size(60, 31);
            this.lbMasterPos.TabIndex = 18;
            this.lbMasterPos.Text = "N/A";
            // 
            // lbMasterAngle
            // 
            this.lbMasterAngle.AutoSize = true;
            this.lbMasterAngle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMasterAngle.Location = new System.Drawing.Point(1345, 429);
            this.lbMasterAngle.Name = "lbMasterAngle";
            this.lbMasterAngle.Size = new System.Drawing.Size(60, 31);
            this.lbMasterAngle.TabIndex = 19;
            this.lbMasterAngle.Text = "N/A";
            // 
            // lbSlaveAngle
            // 
            this.lbSlaveAngle.AutoSize = true;
            this.lbSlaveAngle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSlaveAngle.Location = new System.Drawing.Point(1345, 558);
            this.lbSlaveAngle.Name = "lbSlaveAngle";
            this.lbSlaveAngle.Size = new System.Drawing.Size(60, 31);
            this.lbSlaveAngle.TabIndex = 26;
            this.lbSlaveAngle.Text = "N/A";
            // 
            // lbSlavePos
            // 
            this.lbSlavePos.AutoSize = true;
            this.lbSlavePos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSlavePos.Location = new System.Drawing.Point(659, 558);
            this.lbSlavePos.Name = "lbSlavePos";
            this.lbSlavePos.Size = new System.Drawing.Size(60, 31);
            this.lbSlavePos.TabIndex = 25;
            this.lbSlavePos.Text = "N/A";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(1246, 558);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 31);
            this.label8.TabIndex = 24;
            this.label8.Text = "角度 -- ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(486, 558);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(162, 31);
            this.label9.TabIndex = 23;
            this.label9.Text = "位置(mm) -- ";
            // 
            // tbrSlaveAngle
            // 
            this.tbrSlaveAngle.BackColor = System.Drawing.SystemColors.Control;
            this.tbrSlaveAngle.Location = new System.Drawing.Point(1151, 593);
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
            this.label10.Location = new System.Drawing.Point(26, 593);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(149, 31);
            this.label10.TabIndex = 21;
            this.label10.Text = "辅跟踪模块";
            // 
            // tbrSlavePos
            // 
            this.tbrSlavePos.BackColor = System.Drawing.SystemColors.Control;
            this.tbrSlavePos.Location = new System.Drawing.Point(187, 593);
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
            this.label11.Location = new System.Drawing.Point(26, 702);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(149, 31);
            this.label11.TabIndex = 27;
            this.label11.Text = "造影剂剂量";
            // 
            // lbContrast
            // 
            this.lbContrast.AutoSize = true;
            this.lbContrast.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbContrast.Location = new System.Drawing.Point(1166, 702);
            this.lbContrast.Name = "lbContrast";
            this.lbContrast.Size = new System.Drawing.Size(60, 31);
            this.lbContrast.TabIndex = 29;
            this.lbContrast.Text = "N/A";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(1296, 702);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(116, 31);
            this.label13.TabIndex = 28;
            this.label13.Text = "uL(微升)";
            // 
            // lbPressure
            // 
            this.lbPressure.AutoSize = true;
            this.lbPressure.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPressure.Location = new System.Drawing.Point(1166, 810);
            this.lbPressure.Name = "lbPressure";
            this.lbPressure.Size = new System.Drawing.Size(60, 31);
            this.lbPressure.TabIndex = 33;
            this.lbPressure.Text = "N/A";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(1296, 810);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(137, 31);
            this.label15.TabIndex = 32;
            this.label15.Text = "KPa(千帕)";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(26, 810);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(149, 31);
            this.label16.TabIndex = 31;
            this.label16.Text = "加压泵压力";
            // 
            // pbrPressure
            // 
            this.pbrPressure.Location = new System.Drawing.Point(207, 797);
            this.pbrPressure.Maximum = 400;
            this.pbrPressure.Name = "pbrPressure";
            this.pbrPressure.Size = new System.Drawing.Size(930, 59);
            this.pbrPressure.TabIndex = 30;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(26, 924);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(182, 31);
            this.label17.TabIndex = 34;
            this.label17.Text = "X光脚踏开关1";
            // 
            // lbSwitchStatus1
            // 
            this.lbSwitchStatus1.AutoSize = true;
            this.lbSwitchStatus1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSwitchStatus1.Location = new System.Drawing.Point(464, 924);
            this.lbSwitchStatus1.Name = "lbSwitchStatus1";
            this.lbSwitchStatus1.Size = new System.Drawing.Size(41, 31);
            this.lbSwitchStatus1.TabIndex = 35;
            this.lbSwitchStatus1.Text = "关";
            // 
            // btnInitMaster
            // 
            this.btnInitMaster.Location = new System.Drawing.Point(345, 311);
            this.btnInitMaster.Name = "btnInitMaster";
            this.btnInitMaster.Size = new System.Drawing.Size(160, 43);
            this.btnInitMaster.TabIndex = 36;
            this.btnInitMaster.Text = "主模块归位";
            this.btnInitMaster.UseVisualStyleBackColor = true;
            this.btnInitMaster.Click += new System.EventHandler(this.btnInitMaster_Click);
            // 
            // btnInitSlave
            // 
            this.btnInitSlave.Location = new System.Drawing.Point(525, 311);
            this.btnInitSlave.Name = "btnInitSlave";
            this.btnInitSlave.Size = new System.Drawing.Size(160, 43);
            this.btnInitSlave.TabIndex = 38;
            this.btnInitSlave.Text = "辅模块归位";
            this.btnInitSlave.UseVisualStyleBackColor = true;
            this.btnInitSlave.Click += new System.EventHandler(this.btnInitSlave_Click);
            // 
            // btnInitAll
            // 
            this.btnInitAll.Location = new System.Drawing.Point(26, 311);
            this.btnInitAll.Name = "btnInitAll";
            this.btnInitAll.Size = new System.Drawing.Size(160, 43);
            this.btnInitAll.TabIndex = 44;
            this.btnInitAll.Text = "系统初始化";
            this.btnInitAll.UseVisualStyleBackColor = true;
            this.btnInitAll.Click += new System.EventHandler(this.btnInitAll_Click);
            // 
            // btnInitForce
            // 
            this.btnInitForce.Location = new System.Drawing.Point(704, 311);
            this.btnInitForce.Name = "btnInitForce";
            this.btnInitForce.Size = new System.Drawing.Size(182, 43);
            this.btnInitForce.TabIndex = 45;
            this.btnInitForce.Text = "加紧机构归位";
            this.btnInitForce.UseVisualStyleBackColor = true;
            this.btnInitForce.Click += new System.EventHandler(this.btnInitForce_Click);
            // 
            // btnForcePos
            // 
            this.btnForcePos.Location = new System.Drawing.Point(901, 311);
            this.btnForcePos.Name = "btnForcePos";
            this.btnForcePos.Size = new System.Drawing.Size(224, 43);
            this.btnForcePos.TabIndex = 56;
            this.btnForcePos.Text = "加紧机构位置设置";
            this.btnForcePos.UseVisualStyleBackColor = true;
            this.btnForcePos.Click += new System.EventHandler(this.btnForcePos_Click);
            // 
            // lbSwitchStatus2
            // 
            this.lbSwitchStatus2.AutoSize = true;
            this.lbSwitchStatus2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSwitchStatus2.Location = new System.Drawing.Point(1071, 924);
            this.lbSwitchStatus2.Name = "lbSwitchStatus2";
            this.lbSwitchStatus2.Size = new System.Drawing.Size(41, 31);
            this.lbSwitchStatus2.TabIndex = 59;
            this.lbSwitchStatus2.Text = "关";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(633, 924);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(182, 31);
            this.label5.TabIndex = 58;
            this.label5.Text = "X光脚踏开关2";
            // 
            // pbxSwitchStatus2
            // 
            this.pbxSwitchStatus2.BackColor = System.Drawing.Color.Gray;
            this.pbxSwitchStatus2.Location = new System.Drawing.Point(814, 910);
            this.pbxSwitchStatus2.Name = "pbxSwitchStatus2";
            this.pbxSwitchStatus2.Size = new System.Drawing.Size(228, 59);
            this.pbxSwitchStatus2.TabIndex = 57;
            this.pbxSwitchStatus2.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(35, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 31);
            this.label4.TabIndex = 60;
            // 
            // udForce2Strength
            // 
            this.udForce2Strength.Location = new System.Drawing.Point(509, 112);
            this.udForce2Strength.Name = "udForce2Strength";
            this.udForce2Strength.Size = new System.Drawing.Size(100, 38);
            this.udForce2Strength.TabIndex = 61;
            this.udForce2Strength.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.udForce2Pos);
            this.groupBox1.Controls.Add(this.udForce3Pos);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.udForce1Strength);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.udForce3Strength);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.udForce2Strength);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(32, 89);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1036, 188);
            this.groupBox1.TabIndex = 62;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "初始化参数";
            // 
            // udForce2Pos
            // 
            this.udForce2Pos.Location = new System.Drawing.Point(509, 49);
            this.udForce2Pos.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.udForce2Pos.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.udForce2Pos.Name = "udForce2Pos";
            this.udForce2Pos.Size = new System.Drawing.Size(100, 38);
            this.udForce2Pos.TabIndex = 74;
            this.udForce2Pos.Value = new decimal(new int[] {
            6000,
            0,
            0,
            0});
            // 
            // udForce3Pos
            // 
            this.udForce3Pos.Location = new System.Drawing.Point(214, 48);
            this.udForce3Pos.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.udForce3Pos.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.udForce3Pos.Name = "udForce3Pos";
            this.udForce3Pos.Size = new System.Drawing.Size(100, 38);
            this.udForce3Pos.TabIndex = 73;
            this.udForce3Pos.Value = new decimal(new int[] {
            7000,
            0,
            0,
            0});
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(657, 115);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(137, 31);
            this.label19.TabIndex = 72;
            this.label19.Text = "加紧1力量";
            // 
            // udForce1Strength
            // 
            this.udForce1Strength.Location = new System.Drawing.Point(820, 113);
            this.udForce1Strength.Name = "udForce1Strength";
            this.udForce1Strength.Size = new System.Drawing.Size(100, 38);
            this.udForce1Strength.TabIndex = 71;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(51, 114);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(137, 31);
            this.label14.TabIndex = 70;
            this.label14.Text = "加紧3力量";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(51, 51);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(137, 31);
            this.label18.TabIndex = 69;
            this.label18.Text = "加紧3位置";
            // 
            // udForce3Strength
            // 
            this.udForce3Strength.Location = new System.Drawing.Point(214, 112);
            this.udForce3Strength.Name = "udForce3Strength";
            this.udForce3Strength.Size = new System.Drawing.Size(100, 38);
            this.udForce3Strength.TabIndex = 68;
            this.udForce3Strength.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(346, 114);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(137, 31);
            this.label12.TabIndex = 66;
            this.label12.Text = "加紧2力量";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(346, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(137, 31);
            this.label6.TabIndex = 63;
            this.label6.Text = "加紧2位置";
            // 
            // btnInitForceStrength
            // 
            this.btnInitForceStrength.Location = new System.Drawing.Point(1140, 311);
            this.btnInitForceStrength.Name = "btnInitForceStrength";
            this.btnInitForceStrength.Size = new System.Drawing.Size(224, 43);
            this.btnInitForceStrength.TabIndex = 63;
            this.btnInitForceStrength.Text = "加紧机构力量设置";
            this.btnInitForceStrength.UseVisualStyleBackColor = true;
            this.btnInitForceStrength.Click += new System.EventHandler(this.btnInitForceStrength_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(392, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(149, 31);
            this.label7.TabIndex = 64;
            this.label7.Text = "硬件状态：";
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStatus.Location = new System.Drawing.Point(633, 35);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(95, 31);
            this.lbStatus.TabIndex = 73;
            this.lbStatus.Text = "未连接";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(191F, 191F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1809, 1060);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnInitForceStrength);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbSwitchStatus2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pbxSwitchStatus2);
            this.Controls.Add(this.btnForcePos);
            this.Controls.Add(this.btnInitForce);
            this.Controls.Add(this.btnInitAll);
            this.Controls.Add(this.btnInitSlave);
            this.Controls.Add(this.btnInitMaster);
            this.Controls.Add(this.lbSwitchStatus1);
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
            this.Controls.Add(this.pbxSwitchStatus1);
            this.Controls.Add(this.pbrContrast);
            this.Controls.Add(this.tbrMasterPos);
            this.Controls.Add(this.tbxShow);
            this.Controls.Add(this.pbxStatus);
            this.Controls.Add(this.cbxSerialPorts);
            this.Controls.Add(this.btnConnect);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.916231F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PCI测试";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Leave += new System.EventHandler(this.Form1_Leave);
            ((System.ComponentModel.ISupportInitialize)(this.pbxStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrMasterPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSwitchStatus1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrMasterAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrSlaveAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrSlavePos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSwitchStatus2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udForce2Strength)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udForce2Pos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udForce3Pos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udForce1Strength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udForce3Strength)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ComboBox cbxSerialPorts;
        private System.Windows.Forms.PictureBox pbxStatus;
        private System.Windows.Forms.TextBox tbxShow;
        private System.Windows.Forms.TrackBar tbrMasterPos;
        private System.Windows.Forms.ProgressBar pbrContrast;
        private System.Windows.Forms.PictureBox pbxSwitchStatus1;
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
        private System.Windows.Forms.Label lbSwitchStatus1;
        private System.Windows.Forms.Button btnInitMaster;
        private System.Windows.Forms.Button btnInitSlave;
        private System.Windows.Forms.Button btnInitAll;
        private System.Windows.Forms.Button btnInitForce;
        private System.Windows.Forms.Button btnForcePos;
        private System.Windows.Forms.Label lbSwitchStatus2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pbxSwitchStatus2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown udForce2Strength;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnInitForceStrength;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.NumericUpDown udForce1Strength;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.NumericUpDown udForce3Strength;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.NumericUpDown udForce2Pos;
        private System.Windows.Forms.NumericUpDown udForce3Pos;
        private System.Windows.Forms.Timer timer1;
    }
}

