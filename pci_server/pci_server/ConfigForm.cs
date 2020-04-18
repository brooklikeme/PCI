using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace pci_server
{
    public partial class ConfigForm : Form
    {
        List<TextBox> deviceIdentityTextList = new List<TextBox>();
        List<TextBox> travelSignalTextList = new List<TextBox>();
        List<TextBox> rotationSignalTextList = new List<TextBox>();
        List<ComboBox> setProbeComboBoxList = new List<ComboBox>();


        public ConfigForm()
        {
            InitializeComponent();
        }

        private void m_MouseMoved(object sender, RawInput.MouseMoveEventArgs e)
        {
            int index = e.Mouse.index;
            if (index < 5)
            {
                travelSignalTextList[index].Text = e.Mouse.lastY.ToString();
                rotationSignalTextList[index].Text = e.Mouse.lastX.ToString();
            }
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            // init serial ports
            cbxSerialPorts.Items.AddRange(SerialPort.GetPortNames());
            string configured_port = PCIConfig.BaseConfig.ContainsKey("SerialPort") ? PCIConfig.BaseConfig["SerialPort"] : "";
            int selectedIndex = cbxSerialPorts.Items.IndexOf(configured_port);
            if (selectedIndex < 0)
            {
                if (configured_port != "")
                {
                    cbxSerialPorts.Items.Add(configured_port);
                    cbxSerialPorts.SelectedIndex = cbxSerialPorts.Items.Count - 1;
                }
                else
                {
                    cbxSerialPorts.SelectedIndex = 0;
                }
            }
            else
            {
                cbxSerialPorts.SelectedIndex = selectedIndex;
            }

            // init widget lists
            deviceIdentityTextList.Add(txtDeviceIdentity1);
            deviceIdentityTextList.Add(txtDeviceIdentity2);
            deviceIdentityTextList.Add(txtDeviceIdentity3);
            deviceIdentityTextList.Add(txtDeviceIdentity4);
            deviceIdentityTextList.Add(txtDeviceIdentity5);

            travelSignalTextList.Add(txtTravelSignal1);
            travelSignalTextList.Add(txtTravelSignal2);
            travelSignalTextList.Add(txtTravelSignal3);
            travelSignalTextList.Add(txtTravelSignal4);
            travelSignalTextList.Add(txtTravelSignal5);

            rotationSignalTextList.Add(txtRotationSignal1);
            rotationSignalTextList.Add(txtRotationSignal2);
            rotationSignalTextList.Add(txtRotationSignal3);
            rotationSignalTextList.Add(txtRotationSignal4);
            rotationSignalTextList.Add(txtRotationSignal5);

            setProbeComboBoxList.Add(cbxSetProbe1);
            setProbeComboBoxList.Add(cbxSetProbe2);
            setProbeComboBoxList.Add(cbxSetProbe3);
            setProbeComboBoxList.Add(cbxSetProbe4);
            setProbeComboBoxList.Add(cbxSetProbe5);
            
            // init list
            for (var i = 0; i < Global.MouseInput.DeviceIdentityList.Count && i < 5; i++)
            {
                deviceIdentityTextList[i].Text = Global.MouseInput.DeviceIdentityList[i];
            }

            // load probe config
            for (int i = 0; i < 5; i++)
            {
                setProbeComboBoxList[i].SelectedIndex = 0;
                if (deviceIdentityTextList[i].Text != "")
                {
                    foreach (KeyValuePair<string, string> entry in PCIConfig.BaseConfig)
                    {
                        if (entry.Value == deviceIdentityTextList[i].Text)
                        {
                            setProbeComboBoxList[i].SelectedIndex = (int)Char.GetNumericValue(entry.Key[entry.Key.Length - 1]);

                        }
                    }

                }
            }

            // add event handler
            Global.MouseInput.MouseMoved += new RawInput.DeviceEventHandler(m_MouseMoved);
        }

        private void ConfigForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Global.MouseInput.MouseMoved -= new RawInput.DeviceEventHandler(m_MouseMoved);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            PCIConfig.SaveBaseConfig();
            Global.MouseInput.UpdateProbeIndexes();
            btnApply.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            PCIConfig.CompareAndSaveBaseConfig();
            Global.MouseInput.UpdateProbeIndexes();
            this.Close();
        }

        private void CheckBaseConfigChanged()
        {
            // modify serial port info
            string selectedSerialPort = cbxSerialPorts.GetItemText(cbxSerialPorts.SelectedItem);
            selectedSerialPort = selectedSerialPort == "<未设置>" ? "" : selectedSerialPort;
            if (PCIConfig.NewBaseConfig.ContainsKey("SerialPort")) {
                PCIConfig.NewBaseConfig["SerialPort"] = selectedSerialPort;
            } else{
                PCIConfig.NewBaseConfig.Add("SerialPort", selectedSerialPort);
            }

            // modify probe info
            for (int i = 1; i < 4; i ++) {
                if (PCIConfig.NewBaseConfig.ContainsKey("Probe" + i)) {
                    PCIConfig.NewBaseConfig["Probe" + i] = "";
                } else{
                    PCIConfig.NewBaseConfig.Add("Probe" + i, "");
             }
            }
            for (int i = 0; i < 5; i++)
            {
                if (deviceIdentityTextList[i].Text != "" && setProbeComboBoxList[i].SelectedIndex > 0)
                {
                    PCIConfig.NewBaseConfig["Probe" + setProbeComboBoxList[i].SelectedIndex] = deviceIdentityTextList[i].Text;
                }
            }
            // compare new config to old
            btnApply.Enabled = !PCIConfig.CompareBaseConfig();
        }

        private void cbxSerialPorts_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CheckBaseConfigChanged();
        }

        private void cbxSetProbe1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                if (i != 0 && setProbeComboBoxList[i].SelectedIndex == cbxSetProbe1.SelectedIndex)
                {
                    setProbeComboBoxList[i].SelectedIndex = 0;
                }
            }
            CheckBaseConfigChanged();
        }

        private void cbxSetProbe2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                if (i != 1 && setProbeComboBoxList[i].SelectedIndex == cbxSetProbe2.SelectedIndex)
                {
                    setProbeComboBoxList[i].SelectedIndex = 0;
                }
            }
            CheckBaseConfigChanged();
        }

        private void cbxSetProbe3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                if (i != 2 && setProbeComboBoxList[i].SelectedIndex == cbxSetProbe3.SelectedIndex)
                {
                    setProbeComboBoxList[i].SelectedIndex = 0;
                }
            }
            CheckBaseConfigChanged();
        }

        private void cbxSetProbe4_SelectionChangeCommitted(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                if (i != 3 && setProbeComboBoxList[i].SelectedIndex == cbxSetProbe4.SelectedIndex)
                {
                    setProbeComboBoxList[i].SelectedIndex = 0;
                }
            }
            CheckBaseConfigChanged();
        }

        private void cbxSetProbe5_SelectionChangeCommitted(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                if (i != 4 && setProbeComboBoxList[i].SelectedIndex == cbxSetProbe5.SelectedIndex)
                {
                    setProbeComboBoxList[i].SelectedIndex = 0;
                }
            }
            CheckBaseConfigChanged();
        }

        private void btnAdvancedConfig_Click(object sender, EventArgs e)
        {
            AdvConfgForm frmAdvConfig = new AdvConfgForm();
            frmAdvConfig.ShowDialog(this);
            frmAdvConfig.Dispose();
        }

    }
}
