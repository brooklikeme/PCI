namespace pci_server
{
    partial class MainForm
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
            this.btnClose = new System.Windows.Forms.Button();
            this.pbrPressure = new System.Windows.Forms.ProgressBar();
            this.tbrTravel1 = new System.Windows.Forms.TrackBar();
            this.lbProbe1 = new System.Windows.Forms.Label();
            this.lbTravel1 = new System.Windows.Forms.Label();
            this.lbRotation1 = new System.Windows.Forms.Label();
            this.lbForce1 = new System.Windows.Forms.Label();
            this.lbContrast = new System.Windows.Forms.Label();
            this.lbSwitch1 = new System.Windows.Forms.Label();
            this.lbSwitch2 = new System.Windows.Forms.Label();
            this.pbxSerialStatus = new System.Windows.Forms.PictureBox();
            this.lbSerial = new System.Windows.Forms.Label();
            this.btnConfig = new System.Windows.Forms.Button();
            this.lbSerialStatus = new System.Windows.Forms.Label();
            this.lbThickness1 = new System.Windows.Forms.Label();
            this.cbxForce1 = new System.Windows.Forms.ComboBox();
            this.btnSetForce1 = new System.Windows.Forms.Button();
            this.pbxThickness1 = new System.Windows.Forms.PictureBox();
            this.pbxThickness2 = new System.Windows.Forms.PictureBox();
            this.btnSetForce2 = new System.Windows.Forms.Button();
            this.cbxForce2 = new System.Windows.Forms.ComboBox();
            this.lbThickness2 = new System.Windows.Forms.Label();
            this.lbForce2 = new System.Windows.Forms.Label();
            this.lbRotation2 = new System.Windows.Forms.Label();
            this.lbTravel2 = new System.Windows.Forms.Label();
            this.lbProbe2 = new System.Windows.Forms.Label();
            this.tbrTravel2 = new System.Windows.Forms.TrackBar();
            this.pbxThickness3 = new System.Windows.Forms.PictureBox();
            this.btnSetForce3 = new System.Windows.Forms.Button();
            this.cbxForce3 = new System.Windows.Forms.ComboBox();
            this.lbThickness3 = new System.Windows.Forms.Label();
            this.lbForce3 = new System.Windows.Forms.Label();
            this.lbRotation3 = new System.Windows.Forms.Label();
            this.lbTravel3 = new System.Windows.Forms.Label();
            this.lbProbe3 = new System.Windows.Forms.Label();
            this.tbrTravel3 = new System.Windows.Forms.TrackBar();
            this.pbxContrast = new System.Windows.Forms.PictureBox();
            this.pbxSwitch1 = new System.Windows.Forms.PictureBox();
            this.pbxSwitch2 = new System.Windows.Forms.PictureBox();
            this.lbPressure = new System.Windows.Forms.Label();
            this.lbPressureValue = new System.Windows.Forms.Label();
            this.lbPressureUnit = new System.Windows.Forms.Label();
            this.btnZeroTravel1 = new System.Windows.Forms.Button();
            this.btnZeroRotation1 = new System.Windows.Forms.Button();
            this.btnZeroRotation2 = new System.Windows.Forms.Button();
            this.btnZeroTravel2 = new System.Windows.Forms.Button();
            this.btnZeroRotation3 = new System.Windows.Forms.Button();
            this.btnZeroTravel3 = new System.Windows.Forms.Button();
            this.tmrCheckSerial = new System.Windows.Forms.Timer(this.components);
            this.lbTravelValue1 = new System.Windows.Forms.Label();
            this.lbRotationValue1 = new System.Windows.Forms.Label();
            this.lbTravelValue2 = new System.Windows.Forms.Label();
            this.lbRotationValue2 = new System.Windows.Forms.Label();
            this.lbRotationValue3 = new System.Windows.Forms.Label();
            this.lbTravelValue3 = new System.Windows.Forms.Label();
            this.lbForceValue1 = new System.Windows.Forms.Label();
            this.lbForceValue2 = new System.Windows.Forms.Label();
            this.lbForceValue3 = new System.Windows.Forms.Label();
            this.lbThicknessValue1 = new System.Windows.Forms.Label();
            this.lbThicknessValue2 = new System.Windows.Forms.Label();
            this.lbThicknessValue3 = new System.Windows.Forms.Label();
            this.cpbRotation1 = new CircularProgressBar.CircularProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.cpbRotation2 = new CircularProgressBar.CircularProgressBar();
            this.cpbRotation3 = new CircularProgressBar.CircularProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.tbrTravel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSerialStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxThickness1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxThickness2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrTravel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxThickness3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrTravel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxContrast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSwitch1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSwitch2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(1512, 862);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(175, 61);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pbrPressure
            // 
            this.pbrPressure.Location = new System.Drawing.Point(153, 675);
            this.pbrPressure.Maximum = 1000;
            this.pbrPressure.Name = "pbrPressure";
            this.pbrPressure.Size = new System.Drawing.Size(890, 20);
            this.pbrPressure.Step = 1;
            this.pbrPressure.TabIndex = 11;
            // 
            // tbrTravel1
            // 
            this.tbrTravel1.Enabled = false;
            this.tbrTravel1.Location = new System.Drawing.Point(1200, 371);
            this.tbrTravel1.Maximum = 3000;
            this.tbrTravel1.Name = "tbrTravel1";
            this.tbrTravel1.Size = new System.Drawing.Size(505, 90);
            this.tbrTravel1.TabIndex = 10;
            // 
            // lbProbe1
            // 
            this.lbProbe1.AutoSize = true;
            this.lbProbe1.Location = new System.Drawing.Point(1419, 121);
            this.lbProbe1.Name = "lbProbe1";
            this.lbProbe1.Size = new System.Drawing.Size(68, 26);
            this.lbProbe1.TabIndex = 12;
            this.lbProbe1.Text = "探点1";
            // 
            // lbTravel1
            // 
            this.lbTravel1.AutoSize = true;
            this.lbTravel1.Location = new System.Drawing.Point(1208, 315);
            this.lbTravel1.Name = "lbTravel1";
            this.lbTravel1.Size = new System.Drawing.Size(56, 26);
            this.lbTravel1.TabIndex = 14;
            this.lbTravel1.Text = "位置";
            // 
            // lbRotation1
            // 
            this.lbRotation1.AutoSize = true;
            this.lbRotation1.Location = new System.Drawing.Point(1563, 473);
            this.lbRotation1.Name = "lbRotation1";
            this.lbRotation1.Size = new System.Drawing.Size(56, 26);
            this.lbRotation1.TabIndex = 15;
            this.lbRotation1.Text = "角度";
            // 
            // lbForce1
            // 
            this.lbForce1.AutoSize = true;
            this.lbForce1.Location = new System.Drawing.Point(1467, 175);
            this.lbForce1.Name = "lbForce1";
            this.lbForce1.Size = new System.Drawing.Size(78, 26);
            this.lbForce1.TabIndex = 28;
            this.lbForce1.Text = "力反馈";
            this.lbForce1.Click += new System.EventHandler(this.lbForce1_Click);
            // 
            // lbContrast
            // 
            this.lbContrast.AutoSize = true;
            this.lbContrast.Location = new System.Drawing.Point(60, 761);
            this.lbContrast.Name = "lbContrast";
            this.lbContrast.Size = new System.Drawing.Size(78, 26);
            this.lbContrast.TabIndex = 30;
            this.lbContrast.Text = "造影剂";
            // 
            // lbSwitch1
            // 
            this.lbSwitch1.AutoSize = true;
            this.lbSwitch1.Location = new System.Drawing.Point(540, 761);
            this.lbSwitch1.Name = "lbSwitch1";
            this.lbSwitch1.Size = new System.Drawing.Size(112, 26);
            this.lbSwitch1.TabIndex = 31;
            this.lbSwitch1.Text = "脚踏开关1";
            // 
            // lbSwitch2
            // 
            this.lbSwitch2.AutoSize = true;
            this.lbSwitch2.Location = new System.Drawing.Point(1113, 761);
            this.lbSwitch2.Name = "lbSwitch2";
            this.lbSwitch2.Size = new System.Drawing.Size(112, 26);
            this.lbSwitch2.TabIndex = 32;
            this.lbSwitch2.Text = "脚踏开关2";
            // 
            // pbxSerialStatus
            // 
            this.pbxSerialStatus.BackColor = System.Drawing.Color.Gray;
            this.pbxSerialStatus.Location = new System.Drawing.Point(161, 35);
            this.pbxSerialStatus.Name = "pbxSerialStatus";
            this.pbxSerialStatus.Size = new System.Drawing.Size(76, 36);
            this.pbxSerialStatus.TabIndex = 33;
            this.pbxSerialStatus.TabStop = false;
            // 
            // lbSerial
            // 
            this.lbSerial.AutoSize = true;
            this.lbSerial.Location = new System.Drawing.Point(33, 40);
            this.lbSerial.Name = "lbSerial";
            this.lbSerial.Size = new System.Drawing.Size(100, 26);
            this.lbSerial.TabIndex = 34;
            this.lbSerial.Text = "设备状态";
            // 
            // btnConfig
            // 
            this.btnConfig.Location = new System.Drawing.Point(64, 862);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(175, 61);
            this.btnConfig.TabIndex = 35;
            this.btnConfig.Text = "设置";
            this.btnConfig.UseVisualStyleBackColor = true;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // lbSerialStatus
            // 
            this.lbSerialStatus.AutoSize = true;
            this.lbSerialStatus.Location = new System.Drawing.Point(267, 40);
            this.lbSerialStatus.Name = "lbSerialStatus";
            this.lbSerialStatus.Size = new System.Drawing.Size(78, 26);
            this.lbSerialStatus.TabIndex = 36;
            this.lbSerialStatus.Text = "未连接";
            // 
            // lbThickness1
            // 
            this.lbThickness1.AutoSize = true;
            this.lbThickness1.Location = new System.Drawing.Point(1208, 175);
            this.lbThickness1.Name = "lbThickness1";
            this.lbThickness1.Size = new System.Drawing.Size(56, 26);
            this.lbThickness1.TabIndex = 37;
            this.lbThickness1.Text = "直径";
            // 
            // cbxForce1
            // 
            this.cbxForce1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxForce1.FormattingEnabled = true;
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
            this.cbxForce1.Location = new System.Drawing.Point(1438, 225);
            this.cbxForce1.Name = "cbxForce1";
            this.cbxForce1.Size = new System.Drawing.Size(121, 33);
            this.cbxForce1.TabIndex = 38;
            // 
            // btnSetForce1
            // 
            this.btnSetForce1.Location = new System.Drawing.Point(1565, 219);
            this.btnSetForce1.Name = "btnSetForce1";
            this.btnSetForce1.Size = new System.Drawing.Size(103, 45);
            this.btnSetForce1.TabIndex = 39;
            this.btnSetForce1.Text = "设置";
            this.btnSetForce1.UseVisualStyleBackColor = true;
            this.btnSetForce1.Click += new System.EventHandler(this.btnSetForce1_Click);
            // 
            // pbxThickness1
            // 
            this.pbxThickness1.BackColor = System.Drawing.Color.Gray;
            this.pbxThickness1.Location = new System.Drawing.Point(1213, 231);
            this.pbxThickness1.Name = "pbxThickness1";
            this.pbxThickness1.Size = new System.Drawing.Size(160, 20);
            this.pbxThickness1.TabIndex = 40;
            this.pbxThickness1.TabStop = false;
            // 
            // pbxThickness2
            // 
            this.pbxThickness2.BackColor = System.Drawing.Color.Gray;
            this.pbxThickness2.Location = new System.Drawing.Point(651, 240);
            this.pbxThickness2.Name = "pbxThickness2";
            this.pbxThickness2.Size = new System.Drawing.Size(160, 6);
            this.pbxThickness2.TabIndex = 50;
            this.pbxThickness2.TabStop = false;
            // 
            // btnSetForce2
            // 
            this.btnSetForce2.Location = new System.Drawing.Point(1013, 219);
            this.btnSetForce2.Name = "btnSetForce2";
            this.btnSetForce2.Size = new System.Drawing.Size(103, 45);
            this.btnSetForce2.TabIndex = 49;
            this.btnSetForce2.Text = "设置";
            this.btnSetForce2.UseVisualStyleBackColor = true;
            this.btnSetForce2.Click += new System.EventHandler(this.btnSetForce2_Click);
            // 
            // cbxForce2
            // 
            this.cbxForce2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxForce2.FormattingEnabled = true;
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
            this.cbxForce2.Location = new System.Drawing.Point(886, 225);
            this.cbxForce2.Name = "cbxForce2";
            this.cbxForce2.Size = new System.Drawing.Size(121, 33);
            this.cbxForce2.TabIndex = 48;
            // 
            // lbThickness2
            // 
            this.lbThickness2.AutoSize = true;
            this.lbThickness2.Location = new System.Drawing.Point(646, 175);
            this.lbThickness2.Name = "lbThickness2";
            this.lbThickness2.Size = new System.Drawing.Size(56, 26);
            this.lbThickness2.TabIndex = 47;
            this.lbThickness2.Text = "直径";
            // 
            // lbForce2
            // 
            this.lbForce2.AutoSize = true;
            this.lbForce2.Location = new System.Drawing.Point(900, 175);
            this.lbForce2.Name = "lbForce2";
            this.lbForce2.Size = new System.Drawing.Size(78, 26);
            this.lbForce2.TabIndex = 46;
            this.lbForce2.Text = "力反馈";
            // 
            // lbRotation2
            // 
            this.lbRotation2.AutoSize = true;
            this.lbRotation2.Location = new System.Drawing.Point(1006, 473);
            this.lbRotation2.Name = "lbRotation2";
            this.lbRotation2.Size = new System.Drawing.Size(56, 26);
            this.lbRotation2.TabIndex = 45;
            this.lbRotation2.Text = "角度";
            // 
            // lbTravel2
            // 
            this.lbTravel2.AutoSize = true;
            this.lbTravel2.Location = new System.Drawing.Point(653, 315);
            this.lbTravel2.Name = "lbTravel2";
            this.lbTravel2.Size = new System.Drawing.Size(56, 26);
            this.lbTravel2.TabIndex = 44;
            this.lbTravel2.Text = "位置";
            // 
            // lbProbe2
            // 
            this.lbProbe2.AutoSize = true;
            this.lbProbe2.Location = new System.Drawing.Point(800, 121);
            this.lbProbe2.Name = "lbProbe2";
            this.lbProbe2.Size = new System.Drawing.Size(68, 26);
            this.lbProbe2.TabIndex = 42;
            this.lbProbe2.Text = "探点2";
            // 
            // tbrTravel2
            // 
            this.tbrTravel2.Enabled = false;
            this.tbrTravel2.Location = new System.Drawing.Point(645, 371);
            this.tbrTravel2.Maximum = 3000;
            this.tbrTravel2.Name = "tbrTravel2";
            this.tbrTravel2.Size = new System.Drawing.Size(505, 90);
            this.tbrTravel2.TabIndex = 41;
            // 
            // pbxThickness3
            // 
            this.pbxThickness3.BackColor = System.Drawing.Color.Gray;
            this.pbxThickness3.Location = new System.Drawing.Point(53, 237);
            this.pbxThickness3.Name = "pbxThickness3";
            this.pbxThickness3.Size = new System.Drawing.Size(160, 10);
            this.pbxThickness3.TabIndex = 60;
            this.pbxThickness3.TabStop = false;
            // 
            // btnSetForce3
            // 
            this.btnSetForce3.Location = new System.Drawing.Point(431, 219);
            this.btnSetForce3.Name = "btnSetForce3";
            this.btnSetForce3.Size = new System.Drawing.Size(103, 45);
            this.btnSetForce3.TabIndex = 59;
            this.btnSetForce3.Text = "设置";
            this.btnSetForce3.UseVisualStyleBackColor = true;
            this.btnSetForce3.Click += new System.EventHandler(this.btnSetForce3_Click);
            // 
            // cbxForce3
            // 
            this.cbxForce3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxForce3.FormattingEnabled = true;
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
            this.cbxForce3.Location = new System.Drawing.Point(304, 225);
            this.cbxForce3.Name = "cbxForce3";
            this.cbxForce3.Size = new System.Drawing.Size(121, 33);
            this.cbxForce3.TabIndex = 58;
            // 
            // lbThickness3
            // 
            this.lbThickness3.AutoSize = true;
            this.lbThickness3.Location = new System.Drawing.Point(48, 175);
            this.lbThickness3.Name = "lbThickness3";
            this.lbThickness3.Size = new System.Drawing.Size(56, 26);
            this.lbThickness3.TabIndex = 57;
            this.lbThickness3.Text = "直径";
            // 
            // lbForce3
            // 
            this.lbForce3.AutoSize = true;
            this.lbForce3.Location = new System.Drawing.Point(339, 175);
            this.lbForce3.Name = "lbForce3";
            this.lbForce3.Size = new System.Drawing.Size(78, 26);
            this.lbForce3.TabIndex = 56;
            this.lbForce3.Text = "力反馈";
            // 
            // lbRotation3
            // 
            this.lbRotation3.AutoSize = true;
            this.lbRotation3.Location = new System.Drawing.Point(411, 473);
            this.lbRotation3.Name = "lbRotation3";
            this.lbRotation3.Size = new System.Drawing.Size(56, 26);
            this.lbRotation3.TabIndex = 55;
            this.lbRotation3.Text = "角度";
            // 
            // lbTravel3
            // 
            this.lbTravel3.AutoSize = true;
            this.lbTravel3.Location = new System.Drawing.Point(46, 315);
            this.lbTravel3.Name = "lbTravel3";
            this.lbTravel3.Size = new System.Drawing.Size(56, 26);
            this.lbTravel3.TabIndex = 54;
            this.lbTravel3.Text = "位置";
            // 
            // lbProbe3
            // 
            this.lbProbe3.AutoSize = true;
            this.lbProbe3.Location = new System.Drawing.Point(283, 121);
            this.lbProbe3.Name = "lbProbe3";
            this.lbProbe3.Size = new System.Drawing.Size(68, 26);
            this.lbProbe3.TabIndex = 52;
            this.lbProbe3.Text = "探点3";
            // 
            // tbrTravel3
            // 
            this.tbrTravel3.Enabled = false;
            this.tbrTravel3.Location = new System.Drawing.Point(38, 371);
            this.tbrTravel3.Maximum = 3000;
            this.tbrTravel3.Name = "tbrTravel3";
            this.tbrTravel3.Size = new System.Drawing.Size(505, 90);
            this.tbrTravel3.TabIndex = 51;
            // 
            // pbxContrast
            // 
            this.pbxContrast.BackColor = System.Drawing.Color.Gray;
            this.pbxContrast.Location = new System.Drawing.Point(153, 755);
            this.pbxContrast.Name = "pbxContrast";
            this.pbxContrast.Size = new System.Drawing.Size(76, 36);
            this.pbxContrast.TabIndex = 61;
            this.pbxContrast.TabStop = false;
            // 
            // pbxSwitch1
            // 
            this.pbxSwitch1.BackColor = System.Drawing.Color.Gray;
            this.pbxSwitch1.Location = new System.Drawing.Point(671, 755);
            this.pbxSwitch1.Name = "pbxSwitch1";
            this.pbxSwitch1.Size = new System.Drawing.Size(76, 36);
            this.pbxSwitch1.TabIndex = 62;
            this.pbxSwitch1.TabStop = false;
            // 
            // pbxSwitch2
            // 
            this.pbxSwitch2.BackColor = System.Drawing.Color.Gray;
            this.pbxSwitch2.Location = new System.Drawing.Point(1245, 755);
            this.pbxSwitch2.Name = "pbxSwitch2";
            this.pbxSwitch2.Size = new System.Drawing.Size(76, 36);
            this.pbxSwitch2.TabIndex = 63;
            this.pbxSwitch2.TabStop = false;
            // 
            // lbPressure
            // 
            this.lbPressure.AutoSize = true;
            this.lbPressure.Location = new System.Drawing.Point(59, 671);
            this.lbPressure.Name = "lbPressure";
            this.lbPressure.Size = new System.Drawing.Size(78, 26);
            this.lbPressure.TabIndex = 64;
            this.lbPressure.Text = "加压泵";
            // 
            // lbPressureValue
            // 
            this.lbPressureValue.AutoSize = true;
            this.lbPressureValue.Location = new System.Drawing.Point(1072, 672);
            this.lbPressureValue.Name = "lbPressureValue";
            this.lbPressureValue.Size = new System.Drawing.Size(49, 26);
            this.lbPressureValue.TabIndex = 65;
            this.lbPressureValue.Text = "N/A";
            // 
            // lbPressureUnit
            // 
            this.lbPressureUnit.AutoSize = true;
            this.lbPressureUnit.Location = new System.Drawing.Point(1218, 672);
            this.lbPressureUnit.Name = "lbPressureUnit";
            this.lbPressureUnit.Size = new System.Drawing.Size(126, 26);
            this.lbPressureUnit.TabIndex = 66;
            this.lbPressureUnit.Text = "kP（千帕）";
            // 
            // btnZeroTravel1
            // 
            this.btnZeroTravel1.Location = new System.Drawing.Point(1584, 306);
            this.btnZeroTravel1.Name = "btnZeroTravel1";
            this.btnZeroTravel1.Size = new System.Drawing.Size(103, 45);
            this.btnZeroTravel1.TabIndex = 68;
            this.btnZeroTravel1.Text = "清零";
            this.btnZeroTravel1.UseVisualStyleBackColor = true;
            this.btnZeroTravel1.Click += new System.EventHandler(this.btnZeroTravel1_Click);
            // 
            // btnZeroRotation1
            // 
            this.btnZeroRotation1.Location = new System.Drawing.Point(1565, 540);
            this.btnZeroRotation1.Name = "btnZeroRotation1";
            this.btnZeroRotation1.Size = new System.Drawing.Size(103, 45);
            this.btnZeroRotation1.TabIndex = 69;
            this.btnZeroRotation1.Text = "清零";
            this.btnZeroRotation1.UseVisualStyleBackColor = true;
            this.btnZeroRotation1.Click += new System.EventHandler(this.btnZeroRotation1_Click);
            // 
            // btnZeroRotation2
            // 
            this.btnZeroRotation2.Location = new System.Drawing.Point(1013, 540);
            this.btnZeroRotation2.Name = "btnZeroRotation2";
            this.btnZeroRotation2.Size = new System.Drawing.Size(103, 45);
            this.btnZeroRotation2.TabIndex = 71;
            this.btnZeroRotation2.Text = "清零";
            this.btnZeroRotation2.UseVisualStyleBackColor = true;
            this.btnZeroRotation2.Click += new System.EventHandler(this.btnZeroRotation2_Click);
            // 
            // btnZeroTravel2
            // 
            this.btnZeroTravel2.Location = new System.Drawing.Point(1029, 306);
            this.btnZeroTravel2.Name = "btnZeroTravel2";
            this.btnZeroTravel2.Size = new System.Drawing.Size(103, 45);
            this.btnZeroTravel2.TabIndex = 70;
            this.btnZeroTravel2.Text = "清零";
            this.btnZeroTravel2.UseVisualStyleBackColor = true;
            this.btnZeroTravel2.Click += new System.EventHandler(this.btnZeroTravel2_Click);
            // 
            // btnZeroRotation3
            // 
            this.btnZeroRotation3.Location = new System.Drawing.Point(418, 540);
            this.btnZeroRotation3.Name = "btnZeroRotation3";
            this.btnZeroRotation3.Size = new System.Drawing.Size(103, 45);
            this.btnZeroRotation3.TabIndex = 73;
            this.btnZeroRotation3.Text = "清零";
            this.btnZeroRotation3.UseVisualStyleBackColor = true;
            this.btnZeroRotation3.Click += new System.EventHandler(this.btnZeroRotation3_Click);
            // 
            // btnZeroTravel3
            // 
            this.btnZeroTravel3.Location = new System.Drawing.Point(431, 306);
            this.btnZeroTravel3.Name = "btnZeroTravel3";
            this.btnZeroTravel3.Size = new System.Drawing.Size(103, 45);
            this.btnZeroTravel3.TabIndex = 72;
            this.btnZeroTravel3.Text = "清零";
            this.btnZeroTravel3.UseVisualStyleBackColor = true;
            this.btnZeroTravel3.Click += new System.EventHandler(this.btnZeroTravel3_Click);
            // 
            // tmrCheckSerial
            // 
            this.tmrCheckSerial.Enabled = true;
            this.tmrCheckSerial.Interval = 1000;
            this.tmrCheckSerial.Tick += new System.EventHandler(this.tmrCheckSerial_Tick);
            // 
            // lbTravelValue1
            // 
            this.lbTravelValue1.AutoSize = true;
            this.lbTravelValue1.Location = new System.Drawing.Point(1272, 315);
            this.lbTravelValue1.Name = "lbTravelValue1";
            this.lbTravelValue1.Size = new System.Drawing.Size(49, 26);
            this.lbTravelValue1.TabIndex = 74;
            this.lbTravelValue1.Text = "N/A";
            // 
            // lbRotationValue1
            // 
            this.lbRotationValue1.AutoSize = true;
            this.lbRotationValue1.Location = new System.Drawing.Point(1626, 473);
            this.lbRotationValue1.Name = "lbRotationValue1";
            this.lbRotationValue1.Size = new System.Drawing.Size(49, 26);
            this.lbRotationValue1.TabIndex = 75;
            this.lbRotationValue1.Text = "N/A";
            // 
            // lbTravelValue2
            // 
            this.lbTravelValue2.AutoSize = true;
            this.lbTravelValue2.Location = new System.Drawing.Point(717, 315);
            this.lbTravelValue2.Name = "lbTravelValue2";
            this.lbTravelValue2.Size = new System.Drawing.Size(49, 26);
            this.lbTravelValue2.TabIndex = 76;
            this.lbTravelValue2.Text = "N/A";
            // 
            // lbRotationValue2
            // 
            this.lbRotationValue2.AutoSize = true;
            this.lbRotationValue2.Location = new System.Drawing.Point(1067, 473);
            this.lbRotationValue2.Name = "lbRotationValue2";
            this.lbRotationValue2.Size = new System.Drawing.Size(49, 26);
            this.lbRotationValue2.TabIndex = 77;
            this.lbRotationValue2.Text = "N/A";
            // 
            // lbRotationValue3
            // 
            this.lbRotationValue3.AutoSize = true;
            this.lbRotationValue3.Location = new System.Drawing.Point(472, 473);
            this.lbRotationValue3.Name = "lbRotationValue3";
            this.lbRotationValue3.Size = new System.Drawing.Size(49, 26);
            this.lbRotationValue3.TabIndex = 78;
            this.lbRotationValue3.Text = "N/A";
            // 
            // lbTravelValue3
            // 
            this.lbTravelValue3.AutoSize = true;
            this.lbTravelValue3.Location = new System.Drawing.Point(110, 315);
            this.lbTravelValue3.Name = "lbTravelValue3";
            this.lbTravelValue3.Size = new System.Drawing.Size(49, 26);
            this.lbTravelValue3.TabIndex = 79;
            this.lbTravelValue3.Text = "N/A";
            // 
            // lbForceValue1
            // 
            this.lbForceValue1.AutoSize = true;
            this.lbForceValue1.Location = new System.Drawing.Point(1561, 175);
            this.lbForceValue1.Name = "lbForceValue1";
            this.lbForceValue1.Size = new System.Drawing.Size(49, 26);
            this.lbForceValue1.TabIndex = 80;
            this.lbForceValue1.Text = "N/A";
            this.lbForceValue1.Click += new System.EventHandler(this.lbForceValue1_Click);
            // 
            // lbForceValue2
            // 
            this.lbForceValue2.AutoSize = true;
            this.lbForceValue2.Location = new System.Drawing.Point(1008, 175);
            this.lbForceValue2.Name = "lbForceValue2";
            this.lbForceValue2.Size = new System.Drawing.Size(49, 26);
            this.lbForceValue2.TabIndex = 81;
            this.lbForceValue2.Text = "N/A";
            // 
            // lbForceValue3
            // 
            this.lbForceValue3.AutoSize = true;
            this.lbForceValue3.Location = new System.Drawing.Point(426, 175);
            this.lbForceValue3.Name = "lbForceValue3";
            this.lbForceValue3.Size = new System.Drawing.Size(49, 26);
            this.lbForceValue3.TabIndex = 82;
            this.lbForceValue3.Text = "N/A";
            // 
            // lbThicknessValue1
            // 
            this.lbThicknessValue1.AutoSize = true;
            this.lbThicknessValue1.Location = new System.Drawing.Point(1296, 175);
            this.lbThicknessValue1.Name = "lbThicknessValue1";
            this.lbThicknessValue1.Size = new System.Drawing.Size(49, 26);
            this.lbThicknessValue1.TabIndex = 83;
            this.lbThicknessValue1.Text = "N/A";
            // 
            // lbThicknessValue2
            // 
            this.lbThicknessValue2.AutoSize = true;
            this.lbThicknessValue2.Location = new System.Drawing.Point(734, 175);
            this.lbThicknessValue2.Name = "lbThicknessValue2";
            this.lbThicknessValue2.Size = new System.Drawing.Size(49, 26);
            this.lbThicknessValue2.TabIndex = 84;
            this.lbThicknessValue2.Text = "N/A";
            // 
            // lbThicknessValue3
            // 
            this.lbThicknessValue3.AutoSize = true;
            this.lbThicknessValue3.Location = new System.Drawing.Point(136, 175);
            this.lbThicknessValue3.Name = "lbThicknessValue3";
            this.lbThicknessValue3.Size = new System.Drawing.Size(49, 26);
            this.lbThicknessValue3.TabIndex = 85;
            this.lbThicknessValue3.Text = "N/A";
            // 
            // cpbRotation1
            // 
            this.cpbRotation1.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner;
            this.cpbRotation1.AnimationSpeed = 0;
            this.cpbRotation1.BackColor = System.Drawing.Color.Transparent;
            this.cpbRotation1.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Bold);
            this.cpbRotation1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cpbRotation1.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cpbRotation1.InnerMargin = 2;
            this.cpbRotation1.InnerWidth = -1;
            this.cpbRotation1.Location = new System.Drawing.Point(1213, 455);
            this.cpbRotation1.MarqueeAnimationSpeed = 2000;
            this.cpbRotation1.Maximum = 360;
            this.cpbRotation1.Name = "cpbRotation1";
            this.cpbRotation1.OuterColor = System.Drawing.Color.DarkGray;
            this.cpbRotation1.OuterMargin = -25;
            this.cpbRotation1.OuterWidth = 26;
            this.cpbRotation1.ProgressColor = System.Drawing.Color.Green;
            this.cpbRotation1.ProgressWidth = 10;
            this.cpbRotation1.SecondaryFont = new System.Drawing.Font("Microsoft Sans Serif", 36F);
            this.cpbRotation1.Size = new System.Drawing.Size(150, 150);
            this.cpbRotation1.StartAngle = 270;
            this.cpbRotation1.SubscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.cpbRotation1.SubscriptMargin = new System.Windows.Forms.Padding(10, -35, 0, 0);
            this.cpbRotation1.SubscriptText = ".23";
            this.cpbRotation1.SuperscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.cpbRotation1.SuperscriptMargin = new System.Windows.Forms.Padding(10, 35, 0, 0);
            this.cpbRotation1.SuperscriptText = "°C";
            this.cpbRotation1.TabIndex = 86;
            this.cpbRotation1.TextMargin = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this.cpbRotation1.Value = 68;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // cpbRotation2
            // 
            this.cpbRotation2.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner;
            this.cpbRotation2.AnimationSpeed = 0;
            this.cpbRotation2.BackColor = System.Drawing.Color.Transparent;
            this.cpbRotation2.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Bold);
            this.cpbRotation2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cpbRotation2.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cpbRotation2.InnerMargin = 2;
            this.cpbRotation2.InnerWidth = -1;
            this.cpbRotation2.Location = new System.Drawing.Point(655, 455);
            this.cpbRotation2.MarqueeAnimationSpeed = 2000;
            this.cpbRotation2.Maximum = 360;
            this.cpbRotation2.Name = "cpbRotation2";
            this.cpbRotation2.OuterColor = System.Drawing.Color.DarkGray;
            this.cpbRotation2.OuterMargin = -25;
            this.cpbRotation2.OuterWidth = 26;
            this.cpbRotation2.ProgressColor = System.Drawing.Color.Green;
            this.cpbRotation2.ProgressWidth = 10;
            this.cpbRotation2.SecondaryFont = new System.Drawing.Font("Microsoft Sans Serif", 36F);
            this.cpbRotation2.Size = new System.Drawing.Size(150, 150);
            this.cpbRotation2.StartAngle = 270;
            this.cpbRotation2.SubscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.cpbRotation2.SubscriptMargin = new System.Windows.Forms.Padding(10, -35, 0, 0);
            this.cpbRotation2.SubscriptText = ".23";
            this.cpbRotation2.SuperscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.cpbRotation2.SuperscriptMargin = new System.Windows.Forms.Padding(10, 35, 0, 0);
            this.cpbRotation2.SuperscriptText = "°C";
            this.cpbRotation2.TabIndex = 88;
            this.cpbRotation2.TextMargin = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this.cpbRotation2.Value = 68;
            // 
            // cpbRotation3
            // 
            this.cpbRotation3.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner;
            this.cpbRotation3.AnimationSpeed = 0;
            this.cpbRotation3.BackColor = System.Drawing.Color.Transparent;
            this.cpbRotation3.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Bold);
            this.cpbRotation3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cpbRotation3.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cpbRotation3.InnerMargin = 2;
            this.cpbRotation3.InnerWidth = -1;
            this.cpbRotation3.Location = new System.Drawing.Point(38, 455);
            this.cpbRotation3.MarqueeAnimationSpeed = 2000;
            this.cpbRotation3.Maximum = 360;
            this.cpbRotation3.Name = "cpbRotation3";
            this.cpbRotation3.OuterColor = System.Drawing.Color.DarkGray;
            this.cpbRotation3.OuterMargin = -25;
            this.cpbRotation3.OuterWidth = 26;
            this.cpbRotation3.ProgressColor = System.Drawing.Color.Green;
            this.cpbRotation3.ProgressWidth = 10;
            this.cpbRotation3.SecondaryFont = new System.Drawing.Font("Microsoft Sans Serif", 36F);
            this.cpbRotation3.Size = new System.Drawing.Size(150, 150);
            this.cpbRotation3.StartAngle = 270;
            this.cpbRotation3.SubscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.cpbRotation3.SubscriptMargin = new System.Windows.Forms.Padding(10, -35, 0, 0);
            this.cpbRotation3.SubscriptText = ".23";
            this.cpbRotation3.SuperscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.cpbRotation3.SuperscriptMargin = new System.Windows.Forms.Padding(10, 35, 0, 0);
            this.cpbRotation3.SuperscriptText = "°C";
            this.cpbRotation3.TabIndex = 89;
            this.cpbRotation3.TextMargin = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this.cpbRotation3.Value = 68;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1744, 973);
            this.Controls.Add(this.cpbRotation3);
            this.Controls.Add(this.cpbRotation2);
            this.Controls.Add(this.cpbRotation1);
            this.Controls.Add(this.lbThicknessValue3);
            this.Controls.Add(this.lbThicknessValue2);
            this.Controls.Add(this.lbThicknessValue1);
            this.Controls.Add(this.lbForceValue3);
            this.Controls.Add(this.lbForceValue2);
            this.Controls.Add(this.lbForceValue1);
            this.Controls.Add(this.lbTravelValue3);
            this.Controls.Add(this.lbRotationValue3);
            this.Controls.Add(this.lbRotationValue2);
            this.Controls.Add(this.lbTravelValue2);
            this.Controls.Add(this.lbRotationValue1);
            this.Controls.Add(this.lbTravelValue1);
            this.Controls.Add(this.btnZeroRotation3);
            this.Controls.Add(this.btnZeroTravel3);
            this.Controls.Add(this.btnZeroRotation2);
            this.Controls.Add(this.btnZeroTravel2);
            this.Controls.Add(this.btnZeroRotation1);
            this.Controls.Add(this.btnZeroTravel1);
            this.Controls.Add(this.lbPressureUnit);
            this.Controls.Add(this.lbPressureValue);
            this.Controls.Add(this.lbPressure);
            this.Controls.Add(this.pbxSwitch2);
            this.Controls.Add(this.pbxSwitch1);
            this.Controls.Add(this.pbxContrast);
            this.Controls.Add(this.pbxThickness3);
            this.Controls.Add(this.btnSetForce3);
            this.Controls.Add(this.cbxForce3);
            this.Controls.Add(this.lbThickness3);
            this.Controls.Add(this.lbForce3);
            this.Controls.Add(this.lbRotation3);
            this.Controls.Add(this.lbTravel3);
            this.Controls.Add(this.lbProbe3);
            this.Controls.Add(this.tbrTravel3);
            this.Controls.Add(this.pbxThickness2);
            this.Controls.Add(this.btnSetForce2);
            this.Controls.Add(this.cbxForce2);
            this.Controls.Add(this.lbThickness2);
            this.Controls.Add(this.lbForce2);
            this.Controls.Add(this.lbRotation2);
            this.Controls.Add(this.lbTravel2);
            this.Controls.Add(this.lbProbe2);
            this.Controls.Add(this.tbrTravel2);
            this.Controls.Add(this.pbxThickness1);
            this.Controls.Add(this.btnSetForce1);
            this.Controls.Add(this.cbxForce1);
            this.Controls.Add(this.lbThickness1);
            this.Controls.Add(this.lbSerialStatus);
            this.Controls.Add(this.btnConfig);
            this.Controls.Add(this.lbSerial);
            this.Controls.Add(this.pbxSerialStatus);
            this.Controls.Add(this.lbSwitch2);
            this.Controls.Add(this.lbSwitch1);
            this.Controls.Add(this.lbContrast);
            this.Controls.Add(this.lbForce1);
            this.Controls.Add(this.lbRotation1);
            this.Controls.Add(this.lbTravel1);
            this.Controls.Add(this.lbProbe1);
            this.Controls.Add(this.pbrPressure);
            this.Controls.Add(this.tbrTravel1);
            this.Controls.Add(this.btnClose);
            this.Name = "MainForm";
            this.Text = "PCI服务端";
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.Deactivate += new System.EventHandler(this.MainForm_Deactivate);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tbrTravel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSerialStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxThickness1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxThickness2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrTravel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxThickness3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbrTravel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxContrast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSwitch1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSwitch2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ProgressBar pbrPressure;
        private System.Windows.Forms.TrackBar tbrTravel1;
        private System.Windows.Forms.Label lbProbe1;
        private System.Windows.Forms.Label lbTravel1;
        private System.Windows.Forms.Label lbRotation1;
        private System.Windows.Forms.Label lbForce1;
        private System.Windows.Forms.Label lbContrast;
        private System.Windows.Forms.Label lbSwitch1;
        private System.Windows.Forms.Label lbSwitch2;
        private System.Windows.Forms.PictureBox pbxSerialStatus;
        private System.Windows.Forms.Label lbSerial;
        private System.Windows.Forms.Button btnConfig;
        private System.Windows.Forms.Label lbSerialStatus;
        private System.Windows.Forms.Label lbThickness1;
        private System.Windows.Forms.ComboBox cbxForce1;
        private System.Windows.Forms.Button btnSetForce1;
        private System.Windows.Forms.PictureBox pbxThickness1;
        private System.Windows.Forms.PictureBox pbxThickness2;
        private System.Windows.Forms.Button btnSetForce2;
        private System.Windows.Forms.ComboBox cbxForce2;
        private System.Windows.Forms.Label lbThickness2;
        private System.Windows.Forms.Label lbForce2;
        private System.Windows.Forms.Label lbRotation2;
        private System.Windows.Forms.Label lbTravel2;
        private System.Windows.Forms.Label lbProbe2;
        private System.Windows.Forms.TrackBar tbrTravel2;
        private System.Windows.Forms.PictureBox pbxThickness3;
        private System.Windows.Forms.Button btnSetForce3;
        private System.Windows.Forms.ComboBox cbxForce3;
        private System.Windows.Forms.Label lbThickness3;
        private System.Windows.Forms.Label lbForce3;
        private System.Windows.Forms.Label lbRotation3;
        private System.Windows.Forms.Label lbTravel3;
        private System.Windows.Forms.Label lbProbe3;
        private System.Windows.Forms.TrackBar tbrTravel3;
        private System.Windows.Forms.PictureBox pbxContrast;
        private System.Windows.Forms.PictureBox pbxSwitch1;
        private System.Windows.Forms.PictureBox pbxSwitch2;
        private System.Windows.Forms.Label lbPressure;
        private System.Windows.Forms.Label lbPressureValue;
        private System.Windows.Forms.Label lbPressureUnit;
        private System.Windows.Forms.Button btnZeroTravel1;
        private System.Windows.Forms.Button btnZeroRotation1;
        private System.Windows.Forms.Button btnZeroRotation2;
        private System.Windows.Forms.Button btnZeroTravel2;
        private System.Windows.Forms.Button btnZeroRotation3;
        private System.Windows.Forms.Button btnZeroTravel3;
        private System.Windows.Forms.Timer tmrCheckSerial;
        private System.Windows.Forms.Label lbTravelValue1;
        private System.Windows.Forms.Label lbRotationValue1;
        private System.Windows.Forms.Label lbTravelValue2;
        private System.Windows.Forms.Label lbRotationValue2;
        private System.Windows.Forms.Label lbRotationValue3;
        private System.Windows.Forms.Label lbTravelValue3;
        private System.Windows.Forms.Label lbForceValue1;
        private System.Windows.Forms.Label lbForceValue2;
        private System.Windows.Forms.Label lbForceValue3;
        private System.Windows.Forms.Label lbThicknessValue1;
        private System.Windows.Forms.Label lbThicknessValue2;
        private System.Windows.Forms.Label lbThicknessValue3;
        private CircularProgressBar.CircularProgressBar cpbRotation1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private CircularProgressBar.CircularProgressBar cpbRotation2;
        private CircularProgressBar.CircularProgressBar cpbRotation3;
    }
}

