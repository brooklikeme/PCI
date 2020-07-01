using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;


namespace pci_server
{

    public partial class MainForm : Form
    {
        int NumberOfMouses;
        bool IsActive = false;
        List<bool> detectList = new List<bool>();
        List<double> diameterList = new List<double>();
        List<Label> travelLabelList = new List<Label>();
        List<Label> rotationLabelList = new List<Label>();
        List<TrackBar> travelBarList = new List<TrackBar>();
        List<CircularProgressBar.CircularProgressBar> rotationBarList = new List<CircularProgressBar.CircularProgressBar>();

        private int portCountDown = 0;
        private string portErrorMessage = "";

        const int INVALID_HANDLE_VALUE = -1;
        const int PAGE_READWRITE = 0x04;
        private IntPtr smHandle;     //文件句柄
        private IntPtr smAddr;       //共享内存地址
        private SMData smData;
        private int sharedMemoryInterval = 100;
        private long sharedMemoryLastTime = DateTime.Now.Ticks / 10000; 
        private uint smLength = 100;
        private Mutex smMutex;

        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        //共享内存
        [DllImport("Kernel32.dll", EntryPoint = "CreateFileMapping")]
        private static extern IntPtr CreateFileMapping(IntPtr hFile, //HANDLE hFile,
            UInt32 lpAttributes,//LPSECURITY_ATTRIBUTES lpAttributes,  //0
            UInt32 flProtect,//DWORD flProtect
            Int32 dwMaximumSizeHigh,//DWORD dwMaximumSizeHigh,
            UInt32 dwMaximumSizeLow,//DWORD dwMaximumSizeLow,
            string lpName//LPCTSTR lpName
         );

        [DllImport("Kernel32.dll", EntryPoint = "OpenFileMapping")]
        private static extern IntPtr OpenFileMapping(
         UInt32 dwDesiredAccess,//DWORD dwDesiredAccess,
         int bInheritHandle,//BOOL bInheritHandle,
         string lpName//LPCTSTR lpName
         );

        const int FILE_MAP_ALL_ACCESS = 0x0002;
        const int FILE_MAP_WRITE = 0x0002;

        [DllImport("Kernel32.dll", EntryPoint = "MapViewOfFile")]
        private static extern IntPtr MapViewOfFile(
            IntPtr hFileMappingObject,//HANDLE hFileMappingObject,
            UInt32 dwDesiredAccess,//DWORD dwDesiredAccess
            UInt32 dwFileOffsetHight,//DWORD dwFileOffsetHigh,
            UInt32 dwFileOffsetLow,//DWORD dwFileOffsetLow,
            UInt32 dwNumberOfBytesToMap//SIZE_T dwNumberOfBytesToMap
         );

        [DllImport("Kernel32.dll", EntryPoint = "UnmapViewOfFile")]
        private static extern int UnmapViewOfFile(IntPtr lpBaseAddress);

        [DllImport("Kernel32.dll", EntryPoint = "CloseHandle")]
        private static extern int CloseHandle(IntPtr hObject);

        public MainForm()
        {
            InitializeComponent();
        }

        private void m_MouseMoved(object sender, RawInput.MouseMoveEventArgs e)
        {
            if (e.Mouse.probeIndex > 0 && e.Mouse.probeIndex < 4 && detectList[e.Mouse.probeIndex - 1])
            {
                double yTravel = e.Mouse.cumulativeY * 1.0 / 24;
                int yTravelInt = (int)(yTravel * 10) % 3000;
                double xTravel = e.Mouse.cumulativeX * 1.0 / 24;
                int xAngle = (int)((xTravel * 360) / (6.28 * diameterList[e.Mouse.probeIndex - 1])) % 360;
                xAngle = xAngle < 0 ? xAngle + 360 : xAngle;
                yTravelInt = yTravelInt < 0 ? yTravelInt + 3000 : yTravelInt;
                if (e.Mouse.probeIndex == 1)
                {
                    smData.angle1 = xAngle;
                    smData.travel1 = Convert.ToSingle(Math.Round(yTravel * 10));
                }
                else if (e.Mouse.probeIndex == 2)
                {
                    smData.angle2 = xAngle;
                    smData.travel2 = Convert.ToSingle(Math.Round(yTravel * 10));
                }
                else
                {
                    smData.angle3 = xAngle;
                    smData.travel3 = Convert.ToSingle(Math.Round(yTravel * 10));
                }
                // update shared memory
                if (DateTime.Now.Ticks / 10000 - sharedMemoryLastTime > sharedMemoryInterval)
                {
                    // update information
                    updateSharedMemory();
                }
                if (IsActive)
                {

                    travelLabelList[e.Mouse.probeIndex - 1].Text = Math.Round(yTravel, 1).ToString() + " / " + e.Mouse.cumulativeY.ToString();
                    rotationLabelList[e.Mouse.probeIndex - 1].Text = xAngle.ToString() + " / " + e.Mouse.cumulativeX.ToString();
                    travelBarList[e.Mouse.probeIndex - 1].Value = yTravelInt % 3000;
                    rotationBarList[e.Mouse.probeIndex - 1].Value = xAngle;
                }
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
            int value = 0;
            this.sharedMemoryInterval = Int32.TryParse(PCIConfig.NewBaseConfig["Interval"], out value) ? value : 100;

            detectList.Add(false);
            detectList.Add(false);
            detectList.Add(false);

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

            rotationBarList.Add(cpbRotation1);
            rotationBarList.Add(cpbRotation2);
            rotationBarList.Add(cpbRotation3);

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

            initSharedMemory();
            
        }

        private void initSharedMemory()
        {
            smData = new SMData();
            IntPtr hFile = new IntPtr(INVALID_HANDLE_VALUE);
            smHandle = CreateFileMapping(hFile, 0, PAGE_READWRITE, 0, (uint)SMPos.sm_length, "PCI-SERVER-SM");
            if (smHandle == IntPtr.Zero)
            {
                MessageBox.Show("创建共享内存对象失败!");
                return;
            }
            smAddr = MapViewOfFile(smHandle, FILE_MAP_ALL_ACCESS, 0, 0, 0);
            if (smAddr == IntPtr.Zero)
            {
                MessageBox.Show("创建共享内存映射文件失败!");
                return;
            }
            bool mutexCreated;
            smMutex = new Mutex(true, "PCI-SERVER-MUTEX", out mutexCreated);
            smMutex.ReleaseMutex();
        }

        private void freeSharedMemory()
        {
            UnmapViewOfFile(smAddr);
            CloseHandle(smHandle);
        }

        private void updateSharedMemory()
        {
            int force1 = -1;
            int force2 = -1;
            int force3 = -1;
            try
            {
                smMutex.WaitOne();
                // read force set data
                Marshal.Copy(smAddr, smData.data, 0, (int)SMPos.sm_set_length);
                force1 = smData.getAimForce1();
                force2 = smData.getAimForce2();
                force3 = smData.getAimForce3();
                // clear setting flag
                smData.setAimForce1(-1);
                smData.setAimForce2(-1);
                smData.setAimForce3(-1);
                smData.setData();
                Marshal.Copy(smData.data, 0, smAddr, (int)SMPos.sm_length);
                sharedMemoryLastTime = DateTime.Now.Ticks / 10000;
            }
            catch (Exception ex)
            {
                MessageBox.Show("更新共享内存出错：" + ex.Message); 
            }
            finally
            {
                smMutex.ReleaseMutex();
            }

            // check if aim forces are set
            if (force1 >= 0)
            {
                USBSerial.SetServoAngle(0, (int)USBSerial.map(force1, 0, 100, USBSerial.advConfig.min_servo_angle1, USBSerial.advConfig.max_servo_angle1));
            }

            if (force2 >= 0)
            {
                USBSerial.SetServoAngle(1, (int)USBSerial.map(force2, 0, 100, USBSerial.advConfig.min_servo_angle2, USBSerial.advConfig.max_servo_angle2));
            }

            if (force3 >= 0)
            {
                USBSerial.SetServoAngle(2, (int)USBSerial.map(force3, 0, 100, USBSerial.advConfig.min_servo_angle3, USBSerial.advConfig.max_servo_angle3));
            }

        }

        private void sendDataError(string errMessage)
        {
            lbSerialStatus.Text = "发送数据错误：" + errMessage;
        }

        private void deviceDataReceived(DeviceData deviceData)
        {
            // update sm data received from serial device
            smData.pressure = deviceData.pressure;
            smData.switch1 = deviceData.switch1 == 1;
            smData.switch2 = deviceData.switch2 == 1;

            // check if device adv config is available
            if (USBSerial.advConfig.max_hall_value1 > 100)
            {
                smData.contrast = deviceData.contrast > USBSerial.advConfig.contrast_threshold;

                smData.force1 = Convert.ToInt32(USBSerial.map(deviceData.servo_angle1, USBSerial.advConfig.min_servo_angle1, USBSerial.advConfig.max_servo_angle1, 0, 100));
                smData.force2 = Convert.ToInt32(USBSerial.map(deviceData.servo_angle2, USBSerial.advConfig.min_servo_angle2, USBSerial.advConfig.max_servo_angle2, 0, 100));
                smData.force3 = Convert.ToInt32(USBSerial.map(deviceData.servo_angle3, USBSerial.advConfig.min_servo_angle3, USBSerial.advConfig.max_servo_angle3, 0, 100));

                smData.diam1 = Convert.ToSingle(Math.Round(USBSerial.map(deviceData.hall_value1, USBSerial.advConfig.min_hall_value1, USBSerial.advConfig.max_hall_value1, USBSerial.advConfig.min_hall_diam1, USBSerial.advConfig.max_hall_diam1) / 10.0, 1));
                smData.diam2 = Convert.ToSingle(Math.Round(USBSerial.map(deviceData.hall_value2, USBSerial.advConfig.min_hall_value2, USBSerial.advConfig.max_hall_value2, USBSerial.advConfig.min_hall_diam2, USBSerial.advConfig.max_hall_diam2) / 10.0, 1));
                smData.diam3 = Convert.ToSingle(Math.Round(USBSerial.map(deviceData.hall_value3, USBSerial.advConfig.min_hall_value3, USBSerial.advConfig.max_hall_value3, USBSerial.advConfig.min_hall_diam3, USBSerial.advConfig.max_hall_diam3) / 10.0, 1));

                if (DateTime.Now.Ticks / 10000 - sharedMemoryLastTime > sharedMemoryInterval)
                {
                    // update information
                    updateSharedMemory();
                }

                this.BeginInvoke(new MethodInvoker(delegate
                {
                    pbrPressure.Value = smData.pressure;
                    lbPressureValue.Text = smData.pressure.ToString();
                    pbxSwitch1.BackColor = smData.switch1 ? Color.Green : Color.Gray;
                    pbxSwitch2.BackColor = smData.switch2 ? Color.Green : Color.Gray;

                    pbxContrast.BackColor = smData.contrast ? Color.Green : Color.Gray;

                    lbForceValue1.Text = smData.force1.ToString();
                    lbForceValue2.Text = smData.force2.ToString();
                    lbForceValue3.Text = smData.force3.ToString();

                    diameterList[0] = smData.diam1;
                    diameterList[1] = smData.diam2;
                    diameterList[2] = smData.diam3;

                    detectList[0] = diameterList[0] > 1.3;
                    detectList[1] = diameterList[1] > 0.2 && diameterList[1] < 0.6;
                    detectList[2] = diameterList[2] > 0.6;

                    if (detectList[0])
                    {
                        pbxThickness1.BackColor = Color.Green;
                    }
                    else
                    {
                        pbxThickness1.BackColor = Color.Gray;
                        tbrTravel1.Value = 0;
                        cpbRotation1.Value = 0;
                    }
                    if (detectList[1])
                    {
                        pbxThickness2.BackColor = Color.Green;
                    }
                    else
                    {
                        pbxThickness2.BackColor = Color.Gray;
                        tbrTravel2.Value = 0;
                        cpbRotation2.Value = 0;
                    }
                    if (detectList[2])
                    {
                        pbxThickness3.BackColor = Color.Green;
                    }
                    else
                    {
                        pbxThickness3.BackColor = Color.Gray;
                        tbrTravel3.Value = 0;
                        cpbRotation3.Value = 0;
                    }

                    lbThicknessValue1.Text = Math.Round(diameterList[0], 1).ToString();
                    lbThicknessValue2.Text = Math.Round(diameterList[1], 1).ToString();
                    lbThicknessValue3.Text = Math.Round(diameterList[2], 1).ToString();
                }));
            }
        }

        private void advConfigDataReceived(AdvConfig advConfig)
        {

        }

        private void btnZeroTravel1_Click(object sender, EventArgs e)
        {
            Global.MouseInput.ZeroCumulativeY(1);
            tbrTravel1.Value = 0;
        }

        private void btnZeroTravel2_Click(object sender, EventArgs e)
        {
            Global.MouseInput.ZeroCumulativeY(2);
            tbrTravel2.Value = 0;
        }

        private void btnZeroTravel3_Click(object sender, EventArgs e)
        {
            Global.MouseInput.ZeroCumulativeY(3);
            tbrTravel3.Value = 0;
        }

        private void btnZeroRotation1_Click(object sender, EventArgs e)
        {
            Global.MouseInput.ZeroCumulativeX(1);
            cpbRotation1.Value = 0;
        }

        private void btnZeroRotation2_Click(object sender, EventArgs e)
        {
            Global.MouseInput.ZeroCumulativeX(2);
            cpbRotation2.Value = 0;
        }

        private void btnZeroRotation3_Click(object sender, EventArgs e)
        {
            Global.MouseInput.ZeroCumulativeX(3);
            cpbRotation3.Value = 0;
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
            // also check share memory update interval
            int value = 0;
            this.sharedMemoryInterval = Int32.TryParse(PCIConfig.NewBaseConfig["Interval"], out value) ? value : 100;

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
            freeSharedMemory();
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

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void lbForce1_Click(object sender, EventArgs e)
        {

        }

        private void lbForceValue1_Click(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
              // 注意判断关闭事件reason来源于窗体按钮，否则用菜单退出时无法退出!
            if (e.CloseReason == CloseReason.UserClosing)
            {
                //取消"关闭窗口"事件
                e.Cancel = true; // 取消关闭窗体 

                //使关闭时窗口向右下角缩小的效果
                this.WindowState = FormWindowState.Minimized;
                this.mainNotifyIcon.Visible = true;
                this.Hide();
                return;
            }
        }

        private void mainNotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.Visible)
            {
                this.WindowState = FormWindowState.Minimized;
                this.mainNotifyIcon.Visible = true;
                this.Hide();
            }
            else
            {
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
                this.Activate();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.mainNotifyIcon.Visible = true;
            this.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("退出后PCI设备将无法正常运行，确定要退出？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {

                this.mainNotifyIcon.Visible = false;
                this.Close();
                this.Dispose();
                System.Environment.Exit(System.Environment.ExitCode);   

            }
        }
    }
}
