namespace pci_server
{
    partial class ConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnAdvancedConfig = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxSerialPorts = new System.Windows.Forms.ComboBox();
            this.cbxSetProbe1 = new System.Windows.Forms.ComboBox();
            this.lbDeviceIdentity = new System.Windows.Forms.Label();
            this.txtDeviceIdentity1 = new System.Windows.Forms.TextBox();
            this.lbSetProbe = new System.Windows.Forms.Label();
            this.txtTravelSignal1 = new System.Windows.Forms.TextBox();
            this.lbTravelSignal = new System.Windows.Forms.Label();
            this.lbRotationSignal = new System.Windows.Forms.Label();
            this.txtRotationSignal1 = new System.Windows.Forms.TextBox();
            this.txtRotationSignal2 = new System.Windows.Forms.TextBox();
            this.txtTravelSignal2 = new System.Windows.Forms.TextBox();
            this.txtDeviceIdentity2 = new System.Windows.Forms.TextBox();
            this.cbxSetProbe2 = new System.Windows.Forms.ComboBox();
            this.txtRotationSignal3 = new System.Windows.Forms.TextBox();
            this.txtTravelSignal3 = new System.Windows.Forms.TextBox();
            this.txtDeviceIdentity3 = new System.Windows.Forms.TextBox();
            this.cbxSetProbe3 = new System.Windows.Forms.ComboBox();
            this.txtRotationSignal4 = new System.Windows.Forms.TextBox();
            this.txtTravelSignal4 = new System.Windows.Forms.TextBox();
            this.txtDeviceIdentity4 = new System.Windows.Forms.TextBox();
            this.cbxSetProbe4 = new System.Windows.Forms.ComboBox();
            this.txtRotationSignal5 = new System.Windows.Forms.TextBox();
            this.txtTravelSignal5 = new System.Windows.Forms.TextBox();
            this.txtDeviceIdentity5 = new System.Windows.Forms.TextBox();
            this.cbxSetProbe5 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(876, 672);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(172, 55);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(664, 672);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(172, 55);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnApply
            // 
            this.btnApply.Enabled = false;
            this.btnApply.Location = new System.Drawing.Point(1090, 672);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(172, 55);
            this.btnApply.TabIndex = 4;
            this.btnApply.Text = "应用";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnAdvancedConfig
            // 
            this.btnAdvancedConfig.Location = new System.Drawing.Point(50, 672);
            this.btnAdvancedConfig.Name = "btnAdvancedConfig";
            this.btnAdvancedConfig.Size = new System.Drawing.Size(172, 55);
            this.btnAdvancedConfig.TabIndex = 5;
            this.btnAdvancedConfig.Text = "高级设置";
            this.btnAdvancedConfig.UseVisualStyleBackColor = true;
            this.btnAdvancedConfig.Click += new System.EventHandler(this.btnAdvancedConfig_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 26);
            this.label1.TabIndex = 6;
            this.label1.Text = "设备端口：";
            // 
            // cbxSerialPorts
            // 
            this.cbxSerialPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSerialPorts.FormattingEnabled = true;
            this.cbxSerialPorts.Items.AddRange(new object[] {
            "<未设置>"});
            this.cbxSerialPorts.Location = new System.Drawing.Point(174, 49);
            this.cbxSerialPorts.Name = "cbxSerialPorts";
            this.cbxSerialPorts.Size = new System.Drawing.Size(200, 33);
            this.cbxSerialPorts.TabIndex = 7;
            this.cbxSerialPorts.SelectionChangeCommitted += new System.EventHandler(this.cbxSerialPorts_SelectionChangeCommitted);
            // 
            // cbxSetProbe1
            // 
            this.cbxSetProbe1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSetProbe1.FormattingEnabled = true;
            this.cbxSetProbe1.Items.AddRange(new object[] {
            "<未设置>",
            "探测点1",
            "探测点2",
            "探测点3"});
            this.cbxSetProbe1.Location = new System.Drawing.Point(853, 192);
            this.cbxSetProbe1.Name = "cbxSetProbe1";
            this.cbxSetProbe1.Size = new System.Drawing.Size(222, 33);
            this.cbxSetProbe1.TabIndex = 9;
            this.cbxSetProbe1.SelectionChangeCommitted += new System.EventHandler(this.cbxSetProbe1_SelectionChangeCommitted);
            // 
            // lbDeviceIdentity
            // 
            this.lbDeviceIdentity.AutoSize = true;
            this.lbDeviceIdentity.Location = new System.Drawing.Point(179, 142);
            this.lbDeviceIdentity.Name = "lbDeviceIdentity";
            this.lbDeviceIdentity.Size = new System.Drawing.Size(100, 26);
            this.lbDeviceIdentity.TabIndex = 10;
            this.lbDeviceIdentity.Text = "设备标识";
            // 
            // txtDeviceIdentity1
            // 
            this.txtDeviceIdentity1.Location = new System.Drawing.Point(51, 193);
            this.txtDeviceIdentity1.Name = "txtDeviceIdentity1";
            this.txtDeviceIdentity1.ReadOnly = true;
            this.txtDeviceIdentity1.Size = new System.Drawing.Size(374, 31);
            this.txtDeviceIdentity1.TabIndex = 11;
            // 
            // lbSetProbe
            // 
            this.lbSetProbe.AutoSize = true;
            this.lbSetProbe.Location = new System.Drawing.Point(907, 142);
            this.lbSetProbe.Name = "lbSetProbe";
            this.lbSetProbe.Size = new System.Drawing.Size(100, 26);
            this.lbSetProbe.TabIndex = 12;
            this.lbSetProbe.Text = "设置探点";
            // 
            // txtTravelSignal1
            // 
            this.txtTravelSignal1.Location = new System.Drawing.Point(453, 193);
            this.txtTravelSignal1.Name = "txtTravelSignal1";
            this.txtTravelSignal1.ReadOnly = true;
            this.txtTravelSignal1.Size = new System.Drawing.Size(170, 31);
            this.txtTravelSignal1.TabIndex = 13;
            // 
            // lbTravelSignal
            // 
            this.lbTravelSignal.AutoSize = true;
            this.lbTravelSignal.Location = new System.Drawing.Point(485, 142);
            this.lbTravelSignal.Name = "lbTravelSignal";
            this.lbTravelSignal.Size = new System.Drawing.Size(100, 26);
            this.lbTravelSignal.TabIndex = 14;
            this.lbTravelSignal.Text = "前进信号";
            // 
            // lbRotationSignal
            // 
            this.lbRotationSignal.AutoSize = true;
            this.lbRotationSignal.Location = new System.Drawing.Point(685, 142);
            this.lbRotationSignal.Name = "lbRotationSignal";
            this.lbRotationSignal.Size = new System.Drawing.Size(100, 26);
            this.lbRotationSignal.TabIndex = 16;
            this.lbRotationSignal.Text = "旋转信号";
            // 
            // txtRotationSignal1
            // 
            this.txtRotationSignal1.Location = new System.Drawing.Point(653, 193);
            this.txtRotationSignal1.Name = "txtRotationSignal1";
            this.txtRotationSignal1.ReadOnly = true;
            this.txtRotationSignal1.Size = new System.Drawing.Size(170, 31);
            this.txtRotationSignal1.TabIndex = 15;
            // 
            // txtRotationSignal2
            // 
            this.txtRotationSignal2.Location = new System.Drawing.Point(653, 262);
            this.txtRotationSignal2.Name = "txtRotationSignal2";
            this.txtRotationSignal2.ReadOnly = true;
            this.txtRotationSignal2.Size = new System.Drawing.Size(170, 31);
            this.txtRotationSignal2.TabIndex = 20;
            // 
            // txtTravelSignal2
            // 
            this.txtTravelSignal2.Location = new System.Drawing.Point(453, 262);
            this.txtTravelSignal2.Name = "txtTravelSignal2";
            this.txtTravelSignal2.ReadOnly = true;
            this.txtTravelSignal2.Size = new System.Drawing.Size(170, 31);
            this.txtTravelSignal2.TabIndex = 19;
            // 
            // txtDeviceIdentity2
            // 
            this.txtDeviceIdentity2.Location = new System.Drawing.Point(51, 262);
            this.txtDeviceIdentity2.Name = "txtDeviceIdentity2";
            this.txtDeviceIdentity2.ReadOnly = true;
            this.txtDeviceIdentity2.Size = new System.Drawing.Size(374, 31);
            this.txtDeviceIdentity2.TabIndex = 18;
            // 
            // cbxSetProbe2
            // 
            this.cbxSetProbe2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSetProbe2.FormattingEnabled = true;
            this.cbxSetProbe2.Items.AddRange(new object[] {
            "<未设置>",
            "探测点1",
            "探测点2",
            "探测点3"});
            this.cbxSetProbe2.Location = new System.Drawing.Point(853, 261);
            this.cbxSetProbe2.Name = "cbxSetProbe2";
            this.cbxSetProbe2.Size = new System.Drawing.Size(222, 33);
            this.cbxSetProbe2.TabIndex = 17;
            this.cbxSetProbe2.SelectionChangeCommitted += new System.EventHandler(this.cbxSetProbe2_SelectionChangeCommitted);
            // 
            // txtRotationSignal3
            // 
            this.txtRotationSignal3.Location = new System.Drawing.Point(653, 333);
            this.txtRotationSignal3.Name = "txtRotationSignal3";
            this.txtRotationSignal3.ReadOnly = true;
            this.txtRotationSignal3.Size = new System.Drawing.Size(170, 31);
            this.txtRotationSignal3.TabIndex = 24;
            // 
            // txtTravelSignal3
            // 
            this.txtTravelSignal3.Location = new System.Drawing.Point(453, 333);
            this.txtTravelSignal3.Name = "txtTravelSignal3";
            this.txtTravelSignal3.ReadOnly = true;
            this.txtTravelSignal3.Size = new System.Drawing.Size(170, 31);
            this.txtTravelSignal3.TabIndex = 23;
            // 
            // txtDeviceIdentity3
            // 
            this.txtDeviceIdentity3.Location = new System.Drawing.Point(51, 333);
            this.txtDeviceIdentity3.Name = "txtDeviceIdentity3";
            this.txtDeviceIdentity3.ReadOnly = true;
            this.txtDeviceIdentity3.Size = new System.Drawing.Size(374, 31);
            this.txtDeviceIdentity3.TabIndex = 22;
            // 
            // cbxSetProbe3
            // 
            this.cbxSetProbe3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSetProbe3.FormattingEnabled = true;
            this.cbxSetProbe3.Items.AddRange(new object[] {
            "<未设置>",
            "探测点1",
            "探测点2",
            "探测点3"});
            this.cbxSetProbe3.Location = new System.Drawing.Point(853, 332);
            this.cbxSetProbe3.Name = "cbxSetProbe3";
            this.cbxSetProbe3.Size = new System.Drawing.Size(222, 33);
            this.cbxSetProbe3.TabIndex = 21;
            this.cbxSetProbe3.SelectionChangeCommitted += new System.EventHandler(this.cbxSetProbe3_SelectionChangeCommitted);
            // 
            // txtRotationSignal4
            // 
            this.txtRotationSignal4.Location = new System.Drawing.Point(653, 400);
            this.txtRotationSignal4.Name = "txtRotationSignal4";
            this.txtRotationSignal4.ReadOnly = true;
            this.txtRotationSignal4.Size = new System.Drawing.Size(170, 31);
            this.txtRotationSignal4.TabIndex = 28;
            // 
            // txtTravelSignal4
            // 
            this.txtTravelSignal4.Location = new System.Drawing.Point(453, 400);
            this.txtTravelSignal4.Name = "txtTravelSignal4";
            this.txtTravelSignal4.ReadOnly = true;
            this.txtTravelSignal4.Size = new System.Drawing.Size(170, 31);
            this.txtTravelSignal4.TabIndex = 27;
            // 
            // txtDeviceIdentity4
            // 
            this.txtDeviceIdentity4.Location = new System.Drawing.Point(51, 400);
            this.txtDeviceIdentity4.Name = "txtDeviceIdentity4";
            this.txtDeviceIdentity4.ReadOnly = true;
            this.txtDeviceIdentity4.Size = new System.Drawing.Size(374, 31);
            this.txtDeviceIdentity4.TabIndex = 26;
            // 
            // cbxSetProbe4
            // 
            this.cbxSetProbe4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSetProbe4.FormattingEnabled = true;
            this.cbxSetProbe4.Items.AddRange(new object[] {
            "<未设置>",
            "探测点1",
            "探测点2",
            "探测点3"});
            this.cbxSetProbe4.Location = new System.Drawing.Point(853, 399);
            this.cbxSetProbe4.Name = "cbxSetProbe4";
            this.cbxSetProbe4.Size = new System.Drawing.Size(222, 33);
            this.cbxSetProbe4.TabIndex = 25;
            this.cbxSetProbe4.SelectionChangeCommitted += new System.EventHandler(this.cbxSetProbe4_SelectionChangeCommitted);
            // 
            // txtRotationSignal5
            // 
            this.txtRotationSignal5.Location = new System.Drawing.Point(653, 472);
            this.txtRotationSignal5.Name = "txtRotationSignal5";
            this.txtRotationSignal5.ReadOnly = true;
            this.txtRotationSignal5.Size = new System.Drawing.Size(170, 31);
            this.txtRotationSignal5.TabIndex = 32;
            // 
            // txtTravelSignal5
            // 
            this.txtTravelSignal5.Location = new System.Drawing.Point(453, 472);
            this.txtTravelSignal5.Name = "txtTravelSignal5";
            this.txtTravelSignal5.ReadOnly = true;
            this.txtTravelSignal5.Size = new System.Drawing.Size(170, 31);
            this.txtTravelSignal5.TabIndex = 31;
            // 
            // txtDeviceIdentity5
            // 
            this.txtDeviceIdentity5.Location = new System.Drawing.Point(51, 472);
            this.txtDeviceIdentity5.Name = "txtDeviceIdentity5";
            this.txtDeviceIdentity5.ReadOnly = true;
            this.txtDeviceIdentity5.Size = new System.Drawing.Size(374, 31);
            this.txtDeviceIdentity5.TabIndex = 30;
            // 
            // cbxSetProbe5
            // 
            this.cbxSetProbe5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSetProbe5.FormattingEnabled = true;
            this.cbxSetProbe5.Items.AddRange(new object[] {
            "<未设置>",
            "探测点1",
            "探测点2",
            "探测点3"});
            this.cbxSetProbe5.Location = new System.Drawing.Point(853, 471);
            this.cbxSetProbe5.Name = "cbxSetProbe5";
            this.cbxSetProbe5.Size = new System.Drawing.Size(222, 33);
            this.cbxSetProbe5.TabIndex = 29;
            this.cbxSetProbe5.SelectionChangeCommitted += new System.EventHandler(this.cbxSetProbe5_SelectionChangeCommitted);
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1309, 759);
            this.Controls.Add(this.txtRotationSignal5);
            this.Controls.Add(this.txtTravelSignal5);
            this.Controls.Add(this.txtDeviceIdentity5);
            this.Controls.Add(this.cbxSetProbe5);
            this.Controls.Add(this.txtRotationSignal4);
            this.Controls.Add(this.txtTravelSignal4);
            this.Controls.Add(this.txtDeviceIdentity4);
            this.Controls.Add(this.cbxSetProbe4);
            this.Controls.Add(this.txtRotationSignal3);
            this.Controls.Add(this.txtTravelSignal3);
            this.Controls.Add(this.txtDeviceIdentity3);
            this.Controls.Add(this.cbxSetProbe3);
            this.Controls.Add(this.txtRotationSignal2);
            this.Controls.Add(this.txtTravelSignal2);
            this.Controls.Add(this.txtDeviceIdentity2);
            this.Controls.Add(this.cbxSetProbe2);
            this.Controls.Add(this.lbRotationSignal);
            this.Controls.Add(this.txtRotationSignal1);
            this.Controls.Add(this.lbTravelSignal);
            this.Controls.Add(this.txtTravelSignal1);
            this.Controls.Add(this.lbSetProbe);
            this.Controls.Add(this.txtDeviceIdentity1);
            this.Controls.Add(this.lbDeviceIdentity);
            this.Controls.Add(this.cbxSetProbe1);
            this.Controls.Add(this.cbxSerialPorts);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAdvancedConfig);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Name = "ConfigForm";
            this.Text = "基础设置";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ConfigForm_FormClosed);
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnAdvancedConfig;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxSerialPorts;
        private System.Windows.Forms.ComboBox cbxSetProbe1;
        private System.Windows.Forms.Label lbDeviceIdentity;
        private System.Windows.Forms.TextBox txtDeviceIdentity1;
        private System.Windows.Forms.Label lbSetProbe;
        private System.Windows.Forms.TextBox txtTravelSignal1;
        private System.Windows.Forms.Label lbTravelSignal;
        private System.Windows.Forms.Label lbRotationSignal;
        private System.Windows.Forms.TextBox txtRotationSignal1;
        private System.Windows.Forms.TextBox txtRotationSignal2;
        private System.Windows.Forms.TextBox txtTravelSignal2;
        private System.Windows.Forms.TextBox txtDeviceIdentity2;
        private System.Windows.Forms.ComboBox cbxSetProbe2;
        private System.Windows.Forms.TextBox txtRotationSignal3;
        private System.Windows.Forms.TextBox txtTravelSignal3;
        private System.Windows.Forms.TextBox txtDeviceIdentity3;
        private System.Windows.Forms.ComboBox cbxSetProbe3;
        private System.Windows.Forms.TextBox txtRotationSignal4;
        private System.Windows.Forms.TextBox txtTravelSignal4;
        private System.Windows.Forms.TextBox txtDeviceIdentity4;
        private System.Windows.Forms.ComboBox cbxSetProbe4;
        private System.Windows.Forms.TextBox txtRotationSignal5;
        private System.Windows.Forms.TextBox txtTravelSignal5;
        private System.Windows.Forms.TextBox txtDeviceIdentity5;
        private System.Windows.Forms.ComboBox cbxSetProbe5;
    }
}