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
        private SharedMemeryData smData;
        private int sharedMemoryInterval = 100;
        private int sharedMemoryLastTime = DateTime.Now.Millisecond; 
        private uint smLength = 1000;
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
            if (IsActive && e.Mouse.probeIndex > 0 && e.Mouse.probeIndex < 4 && detectList[e.Mouse.probeIndex - 1])
            {
                double yTravel = e.Mouse.cumulativeY * 1.0 / 24;
                int yTravelInt = (int)(yTravel * 10) % 3000;
                double xTravel = e.Mouse.cumulativeX * 1.0 / 24;
                int xAngle = (int)((xTravel * 360) / (6.28 * diameterList[e.Mouse.probeIndex - 1])) % 360;
                xAngle = xAngle < 0 ? xAngle + 360 : xAngle;
                yTravelInt = yTravelInt < 0 ? yTravelInt + 3000 : yTravelInt;
                travelLabelList[e.Mouse.probeIndex - 1].Text = Math.Round(yTravel, 1).ToString() + " / " + e.Mouse.cumulativeY.ToString();
                rotationLabelList[e.Mouse.probeIndex - 1].Text = xAngle.ToString() + " / " + e.Mouse.cumulativeX.ToString();
                travelBarList[e.Mouse.probeIndex - 1].Value = yTravelInt % 3000;
                rotationBarList[e.Mouse.probeIndex - 1].Value = xAngle;

                // update shared memory
                if (DateTime.Now.Millisecond - sharedMemoryLastTime > sharedMemoryInterval)
                {
                    // update information
                    updateSharedMemory();
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
            // init data
            smData = new SharedMemeryData();

            // byte[] smBytes = SharedMemeryData.ObjectToByteArray(smData);
            // smLength = (uint)smBytes.Length;
            IntPtr hFile = new IntPtr(INVALID_HANDLE_VALUE);
            smHandle = CreateFileMapping(hFile, 0, PAGE_READWRITE, 0, smLength, "PCI-SERVER-SM");
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
            try
            {
                byte[] sendData = BitConverter.GetBytes(DateTime.Now.Millisecond);
                smMutex.WaitOne();
                // byte[] sendData = SharedMemeryData.ObjectToByteArray(smData);
                Marshal.Copy(sendData, 0, smAddr, sendData.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show("更新共享内存出错：" + ex.Message); 
            }
            finally
            {
                smMutex.ReleaseMutex();
            }
        }

        private void sendDataError(string errMessage)
        {
            lbSerialStatus.Text = "发送数据错误：" + errMessage;
        }

        private void deviceDataReceived(DeviceData deviceData)
        {
            this.BeginInvoke(new MethodInvoker(delegate
            {
                if (DateTime.Now.Millisecond - sharedMemoryLastTime > sharedMemoryInterval)
                {
                    // update information
                    updateSharedMemory();
                }

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

                    detectList[0] = diameterList[0] > 1.3;
                    detectList[1] = diameterList[1] > 0.2 && diameterList[1] < 0.6;
                    detectList[2] = diameterList[2] > 0.6;

                    // pbxThickness1.BackColor = detectList[0] ? Color.Green : Color.Gray;
                    // pbxThickness2.BackColor = detectList[1] ? Color.Green : Color.Gray;
                    // pbxThickness3.BackColor = detectList[2] ? Color.Green : Color.Gray;

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
                }

            }));
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
            updateSharedMemory();

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
    }
}
