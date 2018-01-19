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

namespace pcitest
{
    public partial class Form1 : Form
    {
        private SerialPort ComDevice = new SerialPort();
        byte[] ReadBuffer = new byte[15];
        byte[] WriteBuffer = new byte[4];
        int ReadBufferIndex = 0;

        public Form1()
        {
            InitializeComponent();
        }

        public void initSerialStatus()
        {
            cbxSerialPorts.Items.AddRange(SerialPort.GetPortNames());
            if (cbxSerialPorts.Items.Count > 0)
            {
                cbxSerialPorts.SelectedIndex = 0;
            }
            pbxSerialStatus.BackColor = Color.Red;

            ComDevice.DataReceived += new SerialDataReceivedEventHandler(Com_DataReceived);//绑定事件

            //
            cbxMasterColor.SelectedIndex = 0;
            cbxSlaveColor.SelectedIndex = 1;
            cbxClampStrength.SelectedIndex = 0;
            cbxBlockStrength.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            initSerialStatus();    
        }

        private void ParseContent()
        {
            this.BeginInvoke(new MethodInvoker(delegate
            {
                //
                double masterPos = BitConverter.ToInt16(new byte[] {ReadBuffer[1], ReadBuffer[2]}, 0) * 1.0 / 10;
                int masterAngle = BitConverter.ToInt16(new byte[] {ReadBuffer[3], ReadBuffer[4]}, 0);
                double slavePos = BitConverter.ToInt16(new byte[] {ReadBuffer[5], ReadBuffer[6]}, 0) * 1.0 / 10;
                int slaveAngle = BitConverter.ToInt16(new byte[] {ReadBuffer[7], ReadBuffer[8]}, 0);
                int contrast = BitConverter.ToInt16(new byte[] {ReadBuffer[9], ReadBuffer[10]}, 0);
                int pressure = BitConverter.ToInt16(new byte[] {ReadBuffer[11], ReadBuffer[12]}, 0);
                byte switchStatus = ReadBuffer[13];

                if (masterPos >= 0 && (int)masterPos <= tbrMasterPos.Maximum)
                    tbrMasterPos.Value = (int)masterPos;
                lbMasterPos.Text = masterPos.ToString();
                if (masterAngle >= 0 && masterAngle <= tbrMasterAngle.Maximum)
                    tbrMasterAngle.Value = masterAngle;
                lbMasterAngle.Text = masterAngle.ToString();

                if (slavePos >= 0 && (int)slavePos <= tbrSlavePos.Maximum)
                    tbrSlavePos.Value = (int)slavePos;
                lbSlavePos.Text = slavePos.ToString();
                if (slaveAngle >= 0 && slaveAngle <= tbrSlaveAngle.Maximum)
                    tbrSlaveAngle.Value = slaveAngle;
                lbSlaveAngle.Text = slaveAngle.ToString();

                if (contrast >=0 && contrast <= pbrContrast.Maximum)
                    pbrContrast.Value = contrast;
                lbContrast.Text = contrast.ToString();
                if (pressure >= 0 && pressure <= pbrPressure.Maximum)
                    pbrPressure.Value = pressure;
                lbPressure.Text = pressure.ToString();

                pbxSwitchStatus.BackColor = switchStatus == 1 ? Color.Green : Color.Gray;
                lbSwitchStatus.Text = switchStatus == 1 ? "开" : "关";

            }));
        }

        // send data
        public bool SendData(byte[] data)
        {
            if (ComDevice.IsOpen)
            {
                try
                {
                    ComDevice.Write(data, 0, data.Length);//发送数据
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("串口未打开", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        private void Com_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] ReDatas = new byte[ComDevice.BytesToRead];
            ComDevice.Read(ReDatas, 0, ReDatas.Length);//读取数据
            foreach (byte item in ReDatas)
            {
                if (item == '^')
                {
                    // start byte
                    ReadBufferIndex = 0;
                }
                else if (item == '\n' && ReadBufferIndex == 14)
                {
                    // append text
                    ParseContent();
                }
                if (ReadBufferIndex < ReadBuffer.Length) {
                    ReadBuffer[ReadBufferIndex] = item;
                }
                ReadBufferIndex ++;
            }  
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            // open
            if (cbxSerialPorts.Items.Count <= 0)
            {
                MessageBox.Show("没有发现串口, 请检查线路！");
                return;
            }

            if (ComDevice.IsOpen == false)
            {
                ComDevice.PortName = cbxSerialPorts.SelectedItem.ToString();
                ComDevice.BaudRate = 115200;
                try
                {
                    ComDevice.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "连接错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                btnConnect.Text = "断开连接";
                pbxSerialStatus.BackColor = Color.Green;
            }
            else
            {
                try
                {
                    ComDevice.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "断开错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                btnConnect.Text = "连接";
                pbxSerialStatus.BackColor = Color.Red;
            }
        }

        private void Form1_Leave(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnInitMaster_Click(object sender, EventArgs e)
        {
            WriteBuffer[0] = (byte) '^';
            WriteBuffer[1] = 1;
            WriteBuffer[2] = (byte)cbxMasterColor.SelectedIndex;
            WriteBuffer[3] = (byte) '\n';
            SendData(WriteBuffer);
        }

        private void btnInitSlave_Click(object sender, EventArgs e)
        {
            WriteBuffer[0] = (byte)'^';
            WriteBuffer[1] = 2;
            WriteBuffer[2] = (byte)cbxSlaveColor.SelectedIndex;
            WriteBuffer[3] = (byte)'\n';
            SendData(WriteBuffer);
        }

        private void btnClamp_Click(object sender, EventArgs e)
        {
            WriteBuffer[0] = (byte)'^';
            WriteBuffer[1] = 11;
            WriteBuffer[2] = (byte)cbxClampStrength.SelectedIndex;
            WriteBuffer[3] = (byte)'\n';
            SendData(WriteBuffer);
        }

        private void btnBlock_Click(object sender, EventArgs e)
        {
            WriteBuffer[0] = (byte)'^';
            WriteBuffer[1] = 12;
            WriteBuffer[2] = (byte)cbxBlockStrength.SelectedIndex;
            WriteBuffer[3] = (byte)'\n';
            SendData(WriteBuffer);
        }
    }
}
