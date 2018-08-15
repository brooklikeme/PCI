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
using System.Threading; 

namespace pcitest
{
    public partial class Form1 : Form
    {
        private SerialPort ComDevice = new SerialPort();
        byte[] ReadBuffer = new byte[17];
        byte[] WriteBuffer = new byte[6];
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
            pbxStatus.BackColor = Color.Red;

            ComDevice.DataReceived += new SerialDataReceivedEventHandler(Com_DataReceived);//绑定事件

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
                byte status = ReadBuffer[1];
                double masterPos = BitConverter.ToInt16(new byte[] {ReadBuffer[2], ReadBuffer[3]}, 0) * 1.0 / 10;
                int masterAngle = BitConverter.ToInt16(new byte[] {ReadBuffer[4], ReadBuffer[5]}, 0);
                double slavePos = BitConverter.ToInt16(new byte[] {ReadBuffer[6], ReadBuffer[7]}, 0) * 1.0 / 10;
                int slaveAngle = BitConverter.ToInt16(new byte[] {ReadBuffer[8], ReadBuffer[9]}, 0);
                int contrast = BitConverter.ToInt16(new byte[] {ReadBuffer[10], ReadBuffer[11]}, 0);
                int pressure = BitConverter.ToInt16(new byte[] {ReadBuffer[12], ReadBuffer[13]}, 0);
                byte switchStatus1 = ReadBuffer[14];
                byte switchStatus2 = ReadBuffer[15];

                if (status == 0)
                {
                    pbxStatus.BackColor = Color.Orange;
                    lbStatus.Text = "未就绪";
                }
                else if (status == 1) {
                    pbxStatus.BackColor = Color.Yellow;
                    lbStatus.Text = "未初始化";
                }
                else if (status == 2)
                {
                    pbxStatus.BackColor = Color.Blue;
                    lbStatus.Text = "初始化中...";
                }
                else if (status == 3)
                {
                    pbxStatus.BackColor = Color.Green;
                    lbStatus.Text = "运行中";
                }
                else if (status == 255)
                {
                    pbxStatus.BackColor = Color.Red;
                    lbStatus.Text = "参数错误";
                }

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

                pbxSwitchStatus1.BackColor = switchStatus1 == 1 ? Color.Green : Color.Gray;
                lbSwitchStatus1.Text = switchStatus1 == 1 ? "开" : "关";

                pbxSwitchStatus2.BackColor = switchStatus2 == 1 ? Color.Green : Color.Gray;
                lbSwitchStatus2.Text = switchStatus2 == 1 ? "开" : "关";

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
                else if (item == '\n' && ReadBufferIndex == 16)
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
                pbxStatus.BackColor = Color.Green;
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
                pbxStatus.BackColor = Color.Red;
                lbStatus.Text = "未连接";
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
            WriteBuffer[2] = 1;
            WriteBuffer[3] = 0;
            WriteBuffer[4] = 0;
            WriteBuffer[5] = (byte) '\n';
            SendData(WriteBuffer);
        }

        private void btnInitSlave_Click(object sender, EventArgs e)
        {
            WriteBuffer[0] = (byte)'^';
            WriteBuffer[1] = 1;
            WriteBuffer[2] = 2;
            WriteBuffer[3] = 0;
            WriteBuffer[4] = 0;
            WriteBuffer[5] = (byte)'\n';
            SendData(WriteBuffer);
        }

        private void btnInitAll_Click(object sender, EventArgs e)
        {
            // Set all params
            SetForce2Pos();
            SetForce3Pos();
            SetForce1Strength();
            SetForce2Strength();
            SetForce3Strength();
            // Sleep for setting
            Thread.Sleep(1000);

            // Send command
            WriteBuffer[0] = (byte)'^';
            WriteBuffer[1] = 1;
            WriteBuffer[2] = 0;
            WriteBuffer[3] = 0;
            WriteBuffer[4] = 0;
            WriteBuffer[5] = (byte)'\n';
            SendData(WriteBuffer);
        }

        private void btnInitForce_Click(object sender, EventArgs e)
        {
            WriteBuffer[0] = (byte)'^';
            WriteBuffer[1] = 1;
            WriteBuffer[2] = 3;
            WriteBuffer[3] = 0;
            WriteBuffer[4] = 0;
            WriteBuffer[5] = (byte)'\n';
            SendData(WriteBuffer);
        }

        private void SetForce2Pos(){
            WriteBuffer[0] = (byte)'^';
            WriteBuffer[1] = 2;
            WriteBuffer[2] = 1;
            WriteBuffer[3] = BitConverter.GetBytes((Int16)6000)[0];
            WriteBuffer[4] = BitConverter.GetBytes((Int16)6000)[1];
            WriteBuffer[5] = (byte)'\n';
            SendData(WriteBuffer);
        }

        private void SetForce3Pos(){
            WriteBuffer[0] = (byte)'^';
            WriteBuffer[1] = 2;
            WriteBuffer[2] = 2;
            WriteBuffer[3] = BitConverter.GetBytes((Int16)7000)[0];
            WriteBuffer[4] = BitConverter.GetBytes((Int16)7000)[1];
            WriteBuffer[5] = (byte)'\n';
            SendData(WriteBuffer);
        }

        private void SetForce1Strength()
        {
            WriteBuffer[0] = (byte)'^';
            WriteBuffer[1] = 2;
            WriteBuffer[2] = 3;
            WriteBuffer[3] = BitConverter.GetBytes((Int16)udForce1Strength.Value)[0];
            WriteBuffer[4] = BitConverter.GetBytes((Int16)udForce1Strength.Value)[1];
            WriteBuffer[5] = (byte)'\n';
            SendData(WriteBuffer);
        }

        private void SetForce2Strength()
        {
            WriteBuffer[0] = (byte)'^';
            WriteBuffer[1] = 2;
            WriteBuffer[2] = 4;
            WriteBuffer[3] = BitConverter.GetBytes((Int16)udForce2Strength.Value)[0];
            WriteBuffer[4] = BitConverter.GetBytes((Int16)udForce2Strength.Value)[1];
            WriteBuffer[5] = (byte)'\n';
            SendData(WriteBuffer);
        }

        private void SetForce3Strength()
        {
            WriteBuffer[0] = (byte)'^';
            WriteBuffer[1] = 2;
            WriteBuffer[2] = 5;
            WriteBuffer[3] = BitConverter.GetBytes((Int16)udForce3Strength.Value)[0];
            WriteBuffer[4] = BitConverter.GetBytes((Int16)udForce3Strength.Value)[1];
            WriteBuffer[5] = (byte)'\n';
            SendData(WriteBuffer);
        }

        private void btnInitForceStrength_Click(object sender, EventArgs e)
        {
            // Set Strength params
            SetForce1Strength();
            SetForce2Strength();
            SetForce3Strength(); 
            // Sleep for setting
            Thread.Sleep(1000);

            WriteBuffer[0] = (byte)'^';
            WriteBuffer[1] = 1;
            WriteBuffer[2] = 5;
            WriteBuffer[3] = 0;
            WriteBuffer[4] = 0;
            WriteBuffer[5] = (byte)'\n';
            SendData(WriteBuffer);
        }

        private void btnForcePos_Click(object sender, EventArgs e)
        {
            // Set Pos params
            // SetForce2Pos();
            // SetForce3Pos();
            // Sleep for setting
            Thread.Sleep(1000);

            WriteBuffer[0] = (byte)'^';
            WriteBuffer[1] = 1;
            WriteBuffer[2] = 4;
            WriteBuffer[3] = 0;
            WriteBuffer[4] = 0;
            WriteBuffer[5] = (byte)'\n';
            SendData(WriteBuffer);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // send heartbeat signal to hardware
            WriteBuffer[0] = (byte)'^';
            WriteBuffer[1] = 3;
            WriteBuffer[2] = 1;
            WriteBuffer[3] = 0;
            WriteBuffer[4] = 0;
            WriteBuffer[5] = (byte)'\n';
            SendData(WriteBuffer);
        }

    }
}
