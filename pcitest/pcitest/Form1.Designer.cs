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
            this.cbxClampStrength = new System.Windows.Forms.ComboBox();
            this.btnClamp = new System.Windows.Forms.Button();
            this.cbxBlockStrength = new System.Windows.Forms.ComboBox();
            this.btnBlock = new System.Windows.Forms.Button();
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
            this.tbxShow.Location = new System.Drawing.Point(1306, 643);
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
            this.tbrMasterPos.Location = new System.Drawing.Point(191, 162);
            this.tbrMasterPos.Maximum = 10000;
            this.tbrMasterPos.Name = "tbrMasterPos";
            this.tbrMasterPos.Size = new System.Drawing.Size(950, 90);
            this.tbrMasterPos.TabIndex = 8;
            this.tbrMasterPos.Value = 10;
            // 
            // pbrContrast
            // 
            this.pbrContrast.Location = new System.Drawing.Point(211, 387);
            this.pbrContrast.Maximum = 12000;
            this.pbrContrast.Name = "pbrContrast";
            this.pbrContrast.Size = new System.Drawing.Size(930, 58);
            this.pbrContrast.TabIndex = 9;
            // 
            // pbxSwitchStatus
            // 
            this.pbxSwitchStatus.BackColor = System.Drawing.Color.Gray;
            this.pbxSwitchStatus.Location = new System.Drawing.Point(211, 609);
            this.pbxSwitchStatus.Name = "pbxSwitchStatus";
            this.pbxSwitchStatus.Size = new System.Drawing.Size(228, 58);
            this.pbxSwitchStatus.TabIndex = 12;
            this.pbxSwitchStatus.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(30, 162);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 31);
            this.label1.TabIndex = 13;
            this.label1.Text = "主跟踪模块";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // tbrMasterAngle
            // 
            this.tbrMasterAngle.BackColor = System.Drawing.SystemColors.Control;
            this.tbrMasterAngle.Location = new System.Drawing.Point(1155, 162);
            this.tbrMasterAngle.Maximum = 360;
            this.tbrMasterAngle.Name = "tbrMasterAngle";
            this.tbrMasterAngle.Size = new System.Drawing.Size(436, 90);
            this.tbrMasterAngle.TabIndex = 15;
            this.tbrMasterAngle.Value = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(490, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 31);
            this.label2.TabIndex = 16;
            this.label2.Text = "位置(mm) -- ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1250, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 31);
            this.label3.TabIndex = 17;
            this.label3.Text = "角度 -- ";
            // 
            // lbMasterPos
            // 
            this.lbMasterPos.AutoSize = true;
            this.lbMasterPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMasterPos.Location = new System.Drawing.Point(663, 128);
            this.lbMasterPos.Name = "lbMasterPos";
            this.lbMasterPos.Size = new System.Drawing.Size(60, 31);
            this.lbMasterPos.TabIndex = 18;
            this.lbMasterPos.Text = "N/A";
            // 
            // lbMasterAngle
            // 
            this.lbMasterAngle.AutoSize = true;
            this.lbMasterAngle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMasterAngle.Location = new System.Drawing.Point(1349, 128);
            this.lbMasterAngle.Name = "lbMasterAngle";
            this.lbMasterAngle.Size = new System.Drawing.Size(60, 31);
            this.lbMasterAngle.TabIndex = 19;
            this.lbMasterAngle.Text = "N/A";
            // 
            // lbSlaveAngle
            // 
            this.lbSlaveAngle.AutoSize = true;
            this.lbSlaveAngle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSlaveAngle.Location = new System.Drawing.Point(1349, 257);
            this.lbSlaveAngle.Name = "lbSlaveAngle";
            this.lbSlaveAngle.Size = new System.Drawing.Size(60, 31);
            this.lbSlaveAngle.TabIndex = 26;
            this.lbSlaveAngle.Text = "N/A";
            // 
            // lbSlavePos
            // 
            this.lbSlavePos.AutoSize = true;
            this.lbSlavePos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSlavePos.Location = new System.Drawing.Point(663, 257);
            this.lbSlavePos.Name = "lbSlavePos";
            this.lbSlavePos.Size = new System.Drawing.Size(60, 31);
            this.lbSlavePos.TabIndex = 25;
            this.lbSlavePos.Text = "N/A";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(1250, 257);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 31);
            this.label8.TabIndex = 24;
            this.label8.Text = "角度 -- ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(490, 257);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(162, 31);
            this.label9.TabIndex = 23;
            this.label9.Text = "位置(mm) -- ";
            // 
            // tbrSlaveAngle
            // 
            this.tbrSlaveAngle.BackColor = System.Drawing.SystemColors.Control;
            this.tbrSlaveAngle.Location = new System.Drawing.Point(1155, 292);
            this.tbrSlaveAngle.Maximum = 3600;
            this.tbrSlaveAngle.Name = "tbrSlaveAngle";
            this.tbrSlaveAngle.Size = new System.Drawing.Size(436, 90);
            this.tbrSlaveAngle.TabIndex = 22;
            this.tbrSlaveAngle.Value = 10;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(30, 292);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(149, 31);
            this.label10.TabIndex = 21;
            this.label10.Text = "辅跟踪模块";
            // 
            // tbrSlavePos
            // 
            this.tbrSlavePos.BackColor = System.Drawing.SystemColors.Control;
            this.tbrSlavePos.Location = new System.Drawing.Point(191, 292);
            this.tbrSlavePos.Maximum = 1000;
            this.tbrSlavePos.Name = "tbrSlavePos";
            this.tbrSlavePos.Size = new System.Drawing.Size(950, 90);
            this.tbrSlavePos.TabIndex = 20;
            this.tbrSlavePos.Value = 10;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(30, 401);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(149, 31);
            this.label11.TabIndex = 27;
            this.label11.Text = "造影剂剂量";
            // 
            // lbContrast
            // 
            this.lbContrast.AutoSize = true;
            this.lbContrast.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbContrast.Location = new System.Drawing.Point(1170, 401);
            this.lbContrast.Name = "lbContrast";
            this.lbContrast.Size = new System.Drawing.Size(60, 31);
            this.lbContrast.TabIndex = 29;
            this.lbContrast.Text = "N/A";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(1300, 401);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(116, 31);
            this.label13.TabIndex = 28;
            this.label13.Text = "uL(微升)";
            // 
            // lbPressure
            // 
            this.lbPressure.AutoSize = true;
            this.lbPressure.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPressure.Location = new System.Drawing.Point(1170, 509);
            this.lbPressure.Name = "lbPressure";
            this.lbPressure.Size = new System.Drawing.Size(60, 31);
            this.lbPressure.TabIndex = 33;
            this.lbPressure.Text = "N/A";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(1300, 509);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(137, 31);
            this.label15.TabIndex = 32;
            this.label15.Text = "KPa(千帕)";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(30, 509);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(149, 31);
            this.label16.TabIndex = 31;
            this.label16.Text = "加压泵压力";
            // 
            // pbrPressure
            // 
            this.pbrPressure.Location = new System.Drawing.Point(211, 496);
            this.pbrPressure.Maximum = 400;
            this.pbrPressure.Name = "pbrPressure";
            this.pbrPressure.Size = new System.Drawing.Size(930, 58);
            this.pbrPressure.TabIndex = 30;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(30, 623);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(167, 31);
            this.label17.TabIndex = 34;
            this.label17.Text = "X光脚踏开关";
            // 
            // lbSwitchStatus
            // 
            this.lbSwitchStatus.AutoSize = true;
            this.lbSwitchStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.17801F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSwitchStatus.Location = new System.Drawing.Point(468, 623);
            this.lbSwitchStatus.Name = "lbSwitchStatus";
            this.lbSwitchStatus.Size = new System.Drawing.Size(41, 31);
            this.lbSwitchStatus.TabIndex = 35;
            this.lbSwitchStatus.Text = "关";
            // 
            // btnInitMaster
            // 
            this.btnInitMaster.Location = new System.Drawing.Point(607, 30);
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
            "绿色",
            "蓝色"});
            this.cbxMasterColor.Location = new System.Drawing.Point(511, 35);
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
            "绿色",
            "蓝色"});
            this.cbxSlaveColor.Location = new System.Drawing.Point(780, 34);
            this.cbxSlaveColor.Name = "cbxSlaveColor";
            this.cbxSlaveColor.Size = new System.Drawing.Size(85, 33);
            this.cbxSlaveColor.TabIndex = 39;
            // 
            // btnInitSlave
            // 
            this.btnInitSlave.Location = new System.Drawing.Point(876, 29);
            this.btnInitSlave.Name = "btnInitSlave";
            this.btnInitSlave.Size = new System.Drawing.Size(160, 43);
            this.btnInitSlave.TabIndex = 38;
            this.btnInitSlave.Text = "辅模块初始化";
            this.btnInitSlave.UseVisualStyleBackColor = true;
            this.btnInitSlave.Click += new System.EventHandler(this.btnInitSlave_Click);
            // 
            // cbxClampStrength
            // 
            this.cbxClampStrength.FormattingEnabled = true;
            this.cbxClampStrength.ItemHeight = 25;
            this.cbxClampStrength.Items.AddRange(new object[] {
            "<无>",
            "低",
            "中",
            "高"});
            this.cbxClampStrength.Location = new System.Drawing.Point(1052, 34);
            this.cbxClampStrength.Name = "cbxClampStrength";
            this.cbxClampStrength.Size = new System.Drawing.Size(85, 33);
            this.cbxClampStrength.TabIndex = 41;
            // 
            // btnClamp
            // 
            this.btnClamp.Location = new System.Drawing.Point(1148, 29);
            this.btnClamp.Name = "btnClamp";
            this.btnClamp.Size = new System.Drawing.Size(160, 43);
            this.btnClamp.TabIndex = 40;
            this.btnClamp.Text = "施加阻力";
            this.btnClamp.UseVisualStyleBackColor = true;
            this.btnClamp.Click += new System.EventHandler(this.btnClamp_Click);
            // 
            // cbxBlockStrength
            // 
            this.cbxBlockStrength.FormattingEnabled = true;
            this.cbxBlockStrength.ItemHeight = 25;
            this.cbxBlockStrength.Items.AddRange(new object[] {
            "<无>",
            "低",
            "中",
            "高"});
            this.cbxBlockStrength.Location = new System.Drawing.Point(1325, 34);
            this.cbxBlockStrength.Name = "cbxBlockStrength";
            this.cbxBlockStrength.Size = new System.Drawing.Size(85, 33);
            this.cbxBlockStrength.TabIndex = 43;
            // 
            // btnBlock
            // 
            this.btnBlock.Location = new System.Drawing.Point(1421, 29);
            this.btnBlock.Name = "btnBlock";
            this.btnBlock.Size = new System.Drawing.Size(160, 43);
            this.btnBlock.TabIndex = 42;
            this.btnBlock.Text = "触发阻塞";
            this.btnBlock.UseVisualStyleBackColor = true;
            this.btnBlock.Click += new System.EventHandler(this.btnBlock_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(191F, 191F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1803, 860);
            this.Controls.Add(this.cbxBlockStrength);
            this.Controls.Add(this.btnBlock);
            this.Controls.Add(this.cbxClampStrength);
            this.Controls.Add(this.btnClamp);
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
        private System.Windows.Forms.ComboBox cbxClampStrength;
        private System.Windows.Forms.Button btnClamp;
        private System.Windows.Forms.ComboBox cbxBlockStrength;
        private System.Windows.Forms.Button btnBlock;
    }
}

