using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace pci_server
{

    public partial class MainForm : Form
    {
        int NumberOfMouses;
        bool IsActive = false;
        long test = 0;
        List<double> diameterList = new List<double>();
        List<Label> travelLabelList = new List<Label>();
        List<Label> rotationLabelList = new List<Label>();
        List<TrackBar> travelBarList = new List<TrackBar>();
        List<TrackBar> rotationBarList = new List<TrackBar>();

        private int portCountDown = 0;
        private string portErrorMessage = "";

        public MainForm()
        {
            InitializeComponent();
        }

        private void m_MouseMoved(object sender, RawInput.MouseMoveEventArgs e)
        {
            if (e.Mouse.probeIndex > 0 && e.Mouse.probeIndex < 4)
            {
                double yTravel = e.Mouse.cumulativeY * 1.0 / 24;
                int yTravelInt = (int)(yTravel * 10);
                double xTravel = e.Mouse.cumulativeX * 1.0 / 24;
                int xAngle = (int)((xTravel * 360) / (3.14 * diameterList[e.Mouse.probeIndex - 1])) % 360;
                xAngle = xAngle < 0 ? xAngle + 360 : xAngle;
                travelLabelList[e.Mouse.probeIndex - 1].Text = Math.Round(yTravel, 1).ToString() + " / " + e.Mouse.cumulativeY.ToString();
                rotationLabelList[e.Mouse.probeIndex - 1].Text = xAngle.ToString() + " / " + e.Mouse.cumulativeX.ToString();
                if (Math.Abs(yTravelInt) > travelBarList[e.Mouse.probeIndex - 1].Maximum) {
                    travelBarList[e.Mouse.probeIndex - 1].Maximum += travelBarList[e.Mouse.probeIndex - 1].Maximum;
                    travelBarList[e.Mouse.probeIndex - 1].Minimum = 0 - travelBarList[e.Mouse.probeIndex - 1].Maximum;
                }
                travelBarList[e.Mouse.probeIndex - 1].Value = yTravelInt;
                rotationBarList[e.Mouse.probeIndex - 1].Value = xAngle;
            }
        }

        // The WndProc is overridden to allow InputDevice to intercept
        // messages to the window and thus catch WM_INPUT messages
        protected override void WndProc(ref Message message)
        {
            if (Global.MouseInput != null)
            {
                Global.MouseInput.ProcessMessage(message);
            }
            base.WndProc(ref message);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            ConfigForm frmConfig = new ConfigForm();
            frmConfig.ShowDialog(this);
            frmConfig.Dispose();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // load config
            PCIConfig.LoadBaseConfig();

            // fill components
            travelLabelList.Add(lbTravelValue1);
            travelLabelList.Add(lbTravelValue2);
            travelLabelList.Add(lbTravelValue3);

            rotationLabelList.Add(lbRotationValue1);
            rotationLabelList.Add(lbRotationValue2);
            rotationLabelList.Add(lbRotationValue3);

            travelBarList.Add(tbrTravel1);
            travelBarList.Add(tbrTravel2);
            travelBarList.Add(tbrTravel3);

            rotationBarList.Add(tbrRotation1);
            rotationBarList.Add(tbrRotation2);
            rotationBarList.Add(tbrRotation3);

            cbxForce1.SelectedIndex = 0;
            cbxForce2.SelectedIndex = 0;
            cbxForce3.SelectedIndex = 0;

            diameterList.Add(0.8F);
            diameterList.Add(0.3F);
            diameterList.Add(2.0F);

            // Create a new InputDevice object, get the number of
            // keyboards, and register the method which will handle the 
            // InputDevice KeyPressed event
            Global.MouseInput = new RawInput(Handle);
            NumberOfMouses = Global.MouseInput.EnumerateDevices();
            Global.MouseInput.UpdateProbeIndexes();
            Global.MouseInput.MouseMoved += new RawInput.DeviceEventHandler(m_MouseMoved);

            // register serial data received event
            USBSerial.deviceDataReceived += new USBSerial.DeviceDataReceived(deviceDataReceived);
            USBSerial.advConfigDataReceived += new USBSerial.AdvConfigDataReceived(advConfigDataReceived);

            USBSerial.sendDataError += new USBSerial.SendDataError(sendDataError);
        }

        private void sendDataError(string errMessage)
        {
            lbSerialStatus.Text = "发送数据错误：" + errMessage;
        }

        private void deviceDataReceived(DeviceData deviceData)
        {
            this.BeginInvoke(new MethodInvoker(delegate
            {
                pbrPressure.Value = deviceData.pressure;
                lbPressureValue.Text = deviceData.pressure.ToString();
                pbxSwitch1.BackColor = deviceData.switch1 == 1 ? Color.Green : Color.Gray;
                pbxSwitch2.BackColor = deviceData.switch2 == 1 ? Color.Green : Color.Gray;

                // check if device adv config is available
                if (USBSerial.advConfig.max_hall_value1 > 100)
                {
                    pbxContrast.BackColor = deviceData.contrast > USBSerial.advConfig.contrast_threshold ? Color.Green : Color.Gray;

                    lbForceValue1.Text = Convert.ToInt32(USBSerial.map(deviceData.servo_angle1, USBSerial.advConfig.min_servo_angle1, USBSerial.advConfig.max_servo_angle1, 0, 100)).ToString();
                    lbForceValue2.Text = Convert.ToInt32(USBSerial.map(deviceData.servo_angle2, USBSerial.advConfig.min_servo_angle2, USBSerial.advConfig.max_servo_angle2, 0, 100)).ToString();
                    lbForceValue3.Text = Convert.ToInt32(USBSerial.map(deviceData.servo_angle3, USBSerial.advConfig.min_servo_angle3, USBSerial.advConfig.max_servo_angle3, 0, 100)).ToString();

                    diameterList[0] = USBSerial.map(deviceData.hall_value1, USBSerial.advConfig.min_hall_value1, USBSerial.advConfig.max_hall_value1, USBSerial.advConfig.min_hall_diam1, USBSerial.advConfig.max_hall_diam1) / 10.0;
                    diameterList[1] = USBSerial.map(deviceData.hall_value2, USBSerial.advConfig.min_hall_value2, USBSerial.advConfig.max_hall_value2, USBSerial.advConfig.min_hall_diam2, USBSerial.advConfig.max_hall_diam2) / 10.0;
                    diameterList[2] = USBSerial.map(deviceData.hall_value3, USBSerial.advConfig.min_hall_value3, USBSerial.advConfig.max_hall_value3, USBSerial.advConfig.min_hall_diam3, USBSerial.advConfig.max_hall_diam3) / 10.0;

                    pbxThickness1.BackColor = diameterList[0] > 1 ? Color.Green : Color.Gray;
                    pbxThickness2.BackColor = diameterList[1] > 0.2 ? Color.Green : Color.Gray;
                    pbxThickness3.BackColor = diameterList[2] > 0.5 ? Color.Green : Color.Gray;

                    lbThicknessValue1.Text = Math.Round(diameterList[0], 1).ToString();
                    lbThicknessValue2.Text = Math.Round(diameterList[1], 1).ToString();
                    lbThicknessValue3.Text = Math.Round(diameterList[2], 1).ToString();
                }

            }));
        }

        private void advConfigDataReceived(AdvConfig advConfig)
        {

        }

        private void btnZeroTravel1_Click(object sender, EventArgs e)
        {
            Global.MouseInput.ZeroCumulativeY(1);
        }

        private void btnZeroTravel2_Click(object sender, EventArgs e)
        {
            Global.MouseInput.ZeroCumulativeY(2);
        }

        private void btnZeroTravel3_Click(object sender, EventArgs e)
        {
            Global.MouseInput.ZeroCumulativeY(3);
        }

        private void btnZeroRotation1_Click(object sender, EventArgs e)
        {
            Global.MouseInput.ZeroCumulativeX(1);
        }

        private void btnZeroRotation2_Click(object sender, EventArgs e)
        {
            Global.MouseInput.ZeroCumulativeX(2);
        }

        private void btnZeroRotation3_Click(object sender, EventArgs e)
        {
            Global.MouseInput.ZeroCumulativeX(3);
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            IsActive = true;
        }

        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            IsActive = false;
        }

        private bool CloseSerialPort() 
        {
            if (!USBSerial.ComDevice.IsOpen)
                return true;
            try
            {
                USBSerial.ComDevice.Close();
            }
            catch (Exception ex)
            {
                lbSerialStatus.Text = "关闭端口出错：" + ex.Message;
                Application.DoEvents();
                return false;
            }
            pbxSerialStatus.BackColor = Color.Green;
            return true;
        }

        private bool OpenSerialPort(string portName, int baudRate) 
        {
            if (USBSerial.ComDevice.IsOpen)
                return true;
            USBSerial.ComDevice.PortName = portName;
            USBSerial.ComDevice.BaudRate = baudRate;
            try
            {
                USBSerial.ComDevice.Open();
            }
            catch (Exception ex)
            {
                portErrorMessage = "打开端口出错：" + ex.Message;
                return false;
            }
            pbxSerialStatus.BackColor = Color.Green;
            return true;
        }

        private void tmrCheckSerial_Tick(object sender, EventArgs e)
        {

            // task
            if (!PCIConfig.NewBaseConfig.ContainsKey("SerialPort") || PCIConfig.NewBaseConfig["SerialPort"] == "") {
                if (USBSerial.ComDevice.IsOpen) {
                    // close port
                    if (CloseSerialPort()) {
                        lbSerialStatus.Text = "未设置USB端口，无法连接设备，请打开设置页进行配置";
                    }
                }
                return;
            } else {
                if (!USBSerial.ComDevice.IsOpen) {
                    if (portCountDown == 0)
                    {
                        if (!OpenSerialPort(PCIConfig.NewBaseConfig["SerialPort"], 115200))
                        {
                            portCountDown = 5;
                            lbSerialStatus.Text = portErrorMessage + ", 5秒钟后重新连接..." + portCountDown.ToString();
                        }
                        else
                        {
                            lbSerialStatus.Text = "已连接";
                            // get adv config data
                            USBSerial.RequestAdvConfig();
                            USBSerial.RequestAdvConfig();
                        }
                    }
                    else
                    {
                        lbSerialStatus.Text = portErrorMessage + ", 5秒钟后重新连接..." + portCountDown.ToString();
                    }
                    portCountDown--;
                }
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseSerialPort();
        }

        private void btnSetForce1_Click(object sender, EventArgs e)
        {
            if (USBSerial.advConfig.max_hall_value1 > 100)
            {
                int force = Int32.Parse(cbxForce1.GetItemText(cbxForce1.SelectedItem));
                USBSerial.SetServoAngle(0, (int)USBSerial.map(force, 0, 100, USBSerial.advConfig.min_servo_angle1, USBSerial.advConfig.max_servo_angle1));
            }
        }

        private void btnSetForce2_Click(object sender, EventArgs e)
        {
            if (USBSerial.advConfig.max_hall_value1 > 100)
            {
                int force = Int32.Parse(cbxForce2.GetItemText(cbxForce2.SelectedItem));
                USBSerial.SetServoAngle(1, (int)USBSerial.map(force, 0, 100, USBSerial.advConfig.min_servo_angle2, USBSerial.advConfig.max_servo_angle2));
            }
        }

        private void btnSetForce3_Click(object sender, EventArgs e)
        {
            if (USBSerial.advConfig.max_hall_value1 > 100)
            {
                int force = Int32.Parse(cbxForce3.GetItemText(cbxForce3.SelectedItem));
                USBSerial.SetServoAngle(2, (int)USBSerial.map(force, 0, 100, USBSerial.advConfig.min_servo_angle3, USBSerial.advConfig.max_servo_angle3));
            }
        }
    }
}
