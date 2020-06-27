using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Configuration;
using System.Collections.Specialized;
using System.IO.Ports;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace pci_server
{
    enum SERIAL_TYPE
    {
        DEVICE_DATA = 0,
        ADV_CONFIG_DATA = 1,
        SERVO_SET_DATA = 2,
        SERIAL_NONE = 100
    }

    class AdvConfig
    {
        public byte min_hall_diam1;    //1
        public int min_hall_value1;    //2
        public byte max_hall_diam1;    //1
        public int max_hall_value1;    //2
        public byte min_hall_diam2;    //1
        public int min_hall_value2;    //2
        public byte max_hall_diam2;    //1
        public int max_hall_value2;    //2
        public byte min_hall_diam3;    //1
        public int min_hall_value3;    //2
        public byte max_hall_diam3;    //1
        public int max_hall_value3;    //2

        public byte min_servo_angle1;  //1
        public byte max_servo_angle1;  //1
        public byte min_servo_angle2;  //1
        public byte max_servo_angle2;  //1
        public byte min_servo_angle3;  //1
        public byte max_servo_angle3;  //1

        public int contrast_threshold; //2
    };

    class DeviceData
    {
        public int hall_value1;        //2
        public int hall_value2;        //2
        public int hall_value3;        //2
        public int pressure;           //2
        public int contrast;           //2
        public byte switch1;           //1
        public byte switch2;           //1
        public byte servo_angle1;      //1
        public byte servo_angle2;      //1  
        public byte servo_angle3;      //1
    }

    public static class SMPos
    {
        // writable
        public static int force1_set = 0;   // int32
        public static int force2_set = 4;   // int32
        public static int force3_set = 8;   // int32
        // readonly
        public static int diam1 = 12;       // float
        public static int diam2 = 16;       // float
        public static int diam3 = 20;       // float
        public static int travel1 = 24;     // float
        public static int travel2 = 28;     // float
        public static int travel3 = 32;     // float
        public static int angle1 = 36;      // float
        public static int angle2 = 40;      // float
        public static int angle3 = 44;      // float
        public static int pressure = 48;    // int32
        public static int contrast = 52;    // int32
        public static int force1 = 56;      // int32
        public static int force2 = 60;      // int32
        public static int force3 = 64;      // int32

        public static int switch1 = 68;     // boolean
        public static int switch2 = 72;     // boolean

    }

    static class USBSerial
    {
        private static byte[] RecvBuffer = new byte[30];
        private static byte[] SendBuffer = new byte[30];
        private static int RecvBufferIndex = 0;
        private static SERIAL_TYPE SerialType;

        private static SerialPort _comDevice = new SerialPort();

        private static DeviceData _deviceData = new DeviceData();
        private static AdvConfig _advConfig = new AdvConfig();

        public delegate void DeviceDataReceived(DeviceData deviceData);
        public static event DeviceDataReceived deviceDataReceived;

        public delegate void AdvConfigDataReceived(AdvConfig advConfigData);
        public static event AdvConfigDataReceived advConfigDataReceived;

        public delegate void SendDataError(string errMessage);
        public static event SendDataError sendDataError;

        public static double map(double value, double fromLow, double fromHigh, double toLow, double toHigh)
        {
            return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
        }

        static USBSerial(){
            _comDevice.DataReceived += new SerialDataReceivedEventHandler(Com_DataReceived);//绑定事件
        }

        public static SerialPort ComDevice
        {
            get { return _comDevice; }
            set { _comDevice = value; }
        }

        public static DeviceData deviceData
        {
            get { return _deviceData; }
            set { _deviceData = value; }
        }

        public static AdvConfig advConfig
        {
            get { return _advConfig; }
            set { _advConfig = value; }
        }

        private static bool SendData(byte[] data, int length)
        {
            if (ComDevice.IsOpen)
            {
                try
                {
                    ComDevice.Write(data, 0, length);//发送数据
                    return true;
                }
                catch (Exception ex)
                {
                    if (sendDataError != null)
                    {
                        sendDataError(ex.Message);
                    }
                }
            }
            return false;
        }

        public static void RequestAdvConfig()
        {
            SendBuffer[0] = (byte)SERIAL_TYPE.ADV_CONFIG_DATA;
            SendBuffer[1] = (byte)'+';
            SendBuffer[2] = (byte)'+';
            SendBuffer[3] = (byte)'+';
            SendData(SendBuffer, 4);
        }

        public static void SetServoAngle(int num, int angle)
        {
            SendBuffer[0] = (byte)SERIAL_TYPE.SERVO_SET_DATA;
            SendBuffer[1] = (byte)num;
            SendBuffer[2] = (byte)angle;
            SendBuffer[3] = (byte)'+';
            SendBuffer[4] = (byte)'+';
            SendBuffer[5] = (byte)'+';
            SendData(SendBuffer, 6);
        }

        public static void UpdateAdvConfig()
        {
            SendBuffer[0] = (byte)SERIAL_TYPE.ADV_CONFIG_DATA;

            SendBuffer[1] = _advConfig.min_hall_diam1;
            SendBuffer[2] = BitConverter.GetBytes((Int16)_advConfig.min_hall_value1)[0];
            SendBuffer[3] = BitConverter.GetBytes((Int16)_advConfig.min_hall_value1)[1];
            SendBuffer[4] = _advConfig.max_hall_diam1;
            SendBuffer[5] = BitConverter.GetBytes((Int16)_advConfig.max_hall_value1)[0];
            SendBuffer[6] = BitConverter.GetBytes((Int16)_advConfig.max_hall_value1)[1];

            SendBuffer[7] = _advConfig.min_hall_diam2;
            SendBuffer[8] = BitConverter.GetBytes((Int16)_advConfig.min_hall_value2)[0];
            SendBuffer[9] = BitConverter.GetBytes((Int16)_advConfig.min_hall_value2)[1];
            SendBuffer[10] = _advConfig.max_hall_diam2;
            SendBuffer[11] = BitConverter.GetBytes((Int16)_advConfig.max_hall_value2)[0];
            SendBuffer[12] = BitConverter.GetBytes((Int16)_advConfig.max_hall_value2)[1];

            SendBuffer[13] = _advConfig.min_hall_diam3;
            SendBuffer[14] = BitConverter.GetBytes((Int16)_advConfig.min_hall_value3)[0];
            SendBuffer[15] = BitConverter.GetBytes((Int16)_advConfig.min_hall_value3)[1];
            SendBuffer[16] = _advConfig.max_hall_diam3;
            SendBuffer[17] = BitConverter.GetBytes((Int16)_advConfig.max_hall_value3)[0];
            SendBuffer[18] = BitConverter.GetBytes((Int16)_advConfig.max_hall_value3)[1];

            SendBuffer[19] = _advConfig.min_servo_angle1;
            SendBuffer[20] = _advConfig.max_servo_angle1;
            SendBuffer[21] = _advConfig.min_servo_angle2;
            SendBuffer[22] = _advConfig.max_servo_angle2;
            SendBuffer[23] = _advConfig.min_servo_angle3;
            SendBuffer[24] = _advConfig.max_servo_angle3;

            SendBuffer[25] = BitConverter.GetBytes((Int16)_advConfig.contrast_threshold)[0];
            SendBuffer[26] = BitConverter.GetBytes((Int16)_advConfig.contrast_threshold)[1];

            SendBuffer[27] = (byte)'+';
            SendBuffer[28] = (byte)'+';
            SendBuffer[29] = (byte)'+';
            SendData(SendBuffer, 30);
        }

        private static void Com_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] ReDatas = new byte[ComDevice.BytesToRead];
            ComDevice.Read(ReDatas, 0, ReDatas.Length);//读取数据
            foreach (byte item in ReDatas)
            {
                RecvBuffer[RecvBufferIndex] = item;
                // check packet complete
                if (RecvBufferIndex > 2 && item == '+' && RecvBuffer[RecvBufferIndex - 1] == '+' && RecvBuffer[RecvBufferIndex - 2] == '+')
                {
                    SerialType = (SERIAL_TYPE)RecvBuffer[0];
                    ParseContent();
                    SerialType = SERIAL_TYPE.SERIAL_NONE;
                    RecvBufferIndex = 0;
                }
                else
                {
                    RecvBufferIndex++;
                }
                if (RecvBufferIndex == 30)
                {
                    RecvBufferIndex = 0;
                }
            }
        }

        private static void ParseContent()
        {
            if (SerialType == SERIAL_TYPE.ADV_CONFIG_DATA && RecvBufferIndex == 29)
            {
                _advConfig.min_hall_diam1 = RecvBuffer[1];
                _advConfig.min_hall_value1 = BitConverter.ToInt16(new byte[] { RecvBuffer[2], RecvBuffer[3] }, 0);
                _advConfig.max_hall_diam1 = RecvBuffer[4];
                _advConfig.max_hall_value1 = BitConverter.ToInt16(new byte[] { RecvBuffer[5], RecvBuffer[6] }, 0);
                _advConfig.min_hall_diam2 = RecvBuffer[7];
                _advConfig.min_hall_value2 = BitConverter.ToInt16(new byte[] { RecvBuffer[8], RecvBuffer[9] }, 0);
                _advConfig.max_hall_diam2 = RecvBuffer[10];
                _advConfig.max_hall_value2 = BitConverter.ToInt16(new byte[] { RecvBuffer[11], RecvBuffer[12] }, 0);
                _advConfig.min_hall_diam3 = RecvBuffer[13];
                _advConfig.min_hall_value3 = BitConverter.ToInt16(new byte[] { RecvBuffer[14], RecvBuffer[15] }, 0);
                _advConfig.max_hall_diam3 = RecvBuffer[16];
                _advConfig.max_hall_value3 = BitConverter.ToInt16(new byte[] { RecvBuffer[17], RecvBuffer[18] }, 0);

                _advConfig.min_servo_angle1 = RecvBuffer[19];
                _advConfig.max_servo_angle1 = RecvBuffer[20];
                _advConfig.min_servo_angle2 = RecvBuffer[21];
                _advConfig.max_servo_angle2 = RecvBuffer[22];
                _advConfig.min_servo_angle3 = RecvBuffer[23];
                _advConfig.max_servo_angle3 = RecvBuffer[24];

                _advConfig.contrast_threshold = BitConverter.ToInt16(new byte[] { RecvBuffer[25], RecvBuffer[26] }, 0);

                // trigger adv config data received event
                if (advConfigDataReceived != null)
                {
                    advConfigDataReceived(_advConfig);
                }
            }
            else if (SerialType == SERIAL_TYPE.DEVICE_DATA && RecvBufferIndex == 18)
            {
                _deviceData.hall_value1 = BitConverter.ToInt16(new byte[] { RecvBuffer[1], RecvBuffer[2] }, 0);
                _deviceData.hall_value2 = BitConverter.ToInt16(new byte[] { RecvBuffer[3], RecvBuffer[4] }, 0);
                _deviceData.hall_value3 = BitConverter.ToInt16(new byte[] { RecvBuffer[5], RecvBuffer[6] }, 0);

                _deviceData.pressure = BitConverter.ToInt16(new byte[] { RecvBuffer[7], RecvBuffer[8] }, 0);
                _deviceData.contrast = BitConverter.ToInt16(new byte[] { RecvBuffer[9], RecvBuffer[10] }, 0);

                _deviceData.switch1 = RecvBuffer[11];
                _deviceData.switch2 = RecvBuffer[12];
                _deviceData.servo_angle1 = RecvBuffer[13];
                _deviceData.servo_angle2 = RecvBuffer[14];
                _deviceData.servo_angle3 = RecvBuffer[15];

                RecvBufferIndex = 0;
                SerialType = SERIAL_TYPE.SERIAL_NONE;

                // trigger device data received event
                if (deviceDataReceived != null)
                {
                    deviceDataReceived(_deviceData);
                }
            }
        }


    }

    static class PCIConfig
    {
        private static Dictionary<string, string> _baseConfig = new Dictionary<string, string>();

        public static Dictionary<string, string> BaseConfig
        {
            get { return _baseConfig; }
            set { _baseConfig = value; }
        }

        private static Dictionary<string, string> _newBaseConfig = new Dictionary<string, string>();

        public static Dictionary<string, string> NewBaseConfig
        {
            get { return _newBaseConfig; }
            set { _newBaseConfig = value; }
        }

        public static void LoadBaseConfig()
        {
            _baseConfig.Clear();
            _newBaseConfig.Clear();
            foreach (var key in ConfigurationManager.AppSettings.AllKeys)
            {
                _baseConfig.Add(key, ConfigurationManager.AppSettings[key]);
                _newBaseConfig.Add(key, ConfigurationManager.AppSettings[key]);
            }
        }

        public static void SaveBaseConfig()
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                foreach (KeyValuePair<string, string> entry in _newBaseConfig)
                {
                    if (settings[entry.Key] == null)
                    {
                        settings.Add(entry.Key, entry.Value);
                    }
                    else
                    {
                        settings[entry.Key].Value = entry.Value;
                    }
                }
                configFile.AppSettings.SectionInformation.ForceSave = true;
                configFile.Save(ConfigurationSaveMode.Full);
                // reload config
                LoadBaseConfig();
            }
            catch (ConfigurationErrorsException)
            {
                MessageBox.Show("保存基础设置错误！");
            }
        }

        public static bool CompareBaseConfig()
        {
            return _baseConfig.Count == _newBaseConfig.Count && !_baseConfig.Except(_newBaseConfig).Any(); ;
        }

        public static bool CompareAndSaveBaseConfig()
        {
            if (!CompareBaseConfig())
            {
                SaveBaseConfig();
                return false;
            }
            return true;
        }

    }

    static class Global
    {
        private static RawInput _mouseInput;

        public static RawInput MouseInput
        {
            get { return _mouseInput; }
            set { _mouseInput = value; }
        }
    }

    class RawInput
    {
        #region const definitions

        private const int RIDEV_INPUTSINK = 0x00000100;
        private const int RID_INPUT = 0x10000003;

        private const int FAPPCOMMAND_MASK = 0xF000;
        private const int FAPPCOMMAND_MOUSE = 0x8000;
        private const int FAPPCOMMAND_OEM = 0x1000;

        private const int RIM_TYPEMOUSE = 0;
        private const int RIM_TYPEKEYBOARD = 1;
        private const int RIM_TYPEHID = 2;

        private const int RIDI_DEVICENAME = 0x20000007;

        private const int WM_KEYDOWN = 0x0100;
        private const int WM_SYSKEYDOWN = 0x0104;
        private const int WM_INPUT = 0x00FF;
        private const int VK_OEM_CLEAR = 0xFE;
        private const int VK_LAST_KEY = VK_OEM_CLEAR; // this is a made up value used as a sentinal

        public Point CurrentCursorPoint;

        #endregion const definitions

        #region structs & enums

        public enum DeviceType
        {
            Key,
            Mouse,
            OEM
        }

        #region Windows.h structure declarations
        [StructLayout(LayoutKind.Sequential)]
        internal struct RAWINPUTDEVICELIST
        {
            public IntPtr hDevice;
            [MarshalAs(UnmanagedType.U4)]
            public int dwType;
        }

#if VISTA_64
        [StructLayout(LayoutKind.Explicit)]
        internal struct RAWINPUT
        {
            [FieldOffset(0)]
            public RAWINPUTHEADER header;
            [FieldOffset(24)]
            public RAWMOUSE mouse;
            [FieldOffset(24)]
            public RAWKEYBOARD keyboard;
            [FieldOffset(24)]
            public RAWHID hid;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct RAWINPUTHEADER
        {
            [MarshalAs(UnmanagedType.U4)]
            public int dwType;
            [MarshalAs(UnmanagedType.U4)]
            public int dwSize;
            public IntPtr hDevice;
            public IntPtr wParam;
        }
#else
        [StructLayout(LayoutKind.Explicit)]
        internal struct RAWINPUT
        {
            [FieldOffset(0)]
            public RAWINPUTHEADER header;
            [FieldOffset(16)]
            public RAWMOUSE mouse;
            [FieldOffset(16)]
            public RAWKEYBOARD keyboard;
            [FieldOffset(16)]
            public RAWHID hid;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct RAWINPUTHEADER
        {
            [MarshalAs(UnmanagedType.U4)]
            public int dwType;
            [MarshalAs(UnmanagedType.U4)]
            public int dwSize;
            public IntPtr hDevice;
            [MarshalAs(UnmanagedType.U4)]
            public int wParam;
        }
#endif

        [StructLayout(LayoutKind.Sequential)]
        internal struct RAWHID
        {
            [MarshalAs(UnmanagedType.U4)]
            public int dwSizHid;
            [MarshalAs(UnmanagedType.U4)]
            public int dwCount;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct BUTTONSSTR
        {
            [MarshalAs(UnmanagedType.U2)]
            public ushort usButtonFlags;
            [MarshalAs(UnmanagedType.U2)]
            public ushort usButtonData;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct RAWMOUSE
        {
            [MarshalAs(UnmanagedType.U2)]
            [FieldOffset(0)]
            public ushort usFlags;
            [MarshalAs(UnmanagedType.U4)]
            [FieldOffset(4)]
            public uint ulButtons;
            [FieldOffset(4)]
            public BUTTONSSTR buttonsStr;
            [MarshalAs(UnmanagedType.U4)]
            [FieldOffset(8)]
            public uint ulRawButtons;
            [FieldOffset(12)]
            public int lLastX;
            [FieldOffset(16)]
            public int lLastY;
            [MarshalAs(UnmanagedType.U4)]
            [FieldOffset(20)]
            public uint ulExtraInformation;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct RAWKEYBOARD
        {
            [MarshalAs(UnmanagedType.U2)]
            public ushort MakeCode;
            [MarshalAs(UnmanagedType.U2)]
            public ushort Flags;
            [MarshalAs(UnmanagedType.U2)]
            public ushort Reserved;
            [MarshalAs(UnmanagedType.U2)]
            public ushort VKey;
            [MarshalAs(UnmanagedType.U4)]
            public uint Message;
            [MarshalAs(UnmanagedType.U4)]
            public uint ExtraInformation;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct RAWINPUTDEVICE
        {
            [MarshalAs(UnmanagedType.U2)]
            public ushort usUsagePage;
            [MarshalAs(UnmanagedType.U2)]
            public ushort usUsage;
            [MarshalAs(UnmanagedType.U4)]
            public int dwFlags;
            public IntPtr hwndTarget;
        }
        #endregion Windows.h structure declarations

        /// <summary>
        /// Class encapsulating the information about a
        /// keyboard event, including the device it
        /// originated with and what key was pressed
        /// </summary>
        public class DeviceInfo
        {
            public string deviceName;
            public string deviceType;
            public IntPtr deviceHandle;
            public string identity;
            public long lastX;
            public long lastY;
            public long cumulativeX;
            public long cumulativeY;
            public int index;
            public int probeIndex;
        }

        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        };

        #endregion structs & enums

        #region DllImports

        [DllImport("User32.dll")]
        extern static uint GetRawInputDeviceList(IntPtr pRawInputDeviceList, ref uint uiNumDevices, uint cbSize);

        [DllImport("User32.dll")]
        extern static uint GetRawInputDeviceInfo(IntPtr hDevice, uint uiCommand, IntPtr pData, ref uint pcbSize);

        [DllImport("User32.dll")]
        extern static bool RegisterRawInputDevices(RAWINPUTDEVICE[] pRawInputDevice, uint uiNumDevices, uint cbSize);

        [DllImport("User32.dll")]
        extern static uint GetRawInputData(IntPtr hRawInput, uint uiCommand, IntPtr pData, ref uint pcbSize, uint cbSizeHeader);

        [DllImport("User32.dll")]
        extern static bool ClipCursor(ref RECT lpRect);

        [DllImport("User32.dll")]
        extern static bool ClipCursor([In()]IntPtr lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Point(POINT point)
            {
                return new Point(point.X, point.Y);
            }
        }

        /// <summary>
        /// Retrieves the cursor's position, in screen coordinates.
        /// </summary>
        /// <see>See MSDN documentation for further information.</see>
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        public static Point GetCursorPosition()
        {
            POINT lpPoint;
            GetCursorPos(out lpPoint);
            return lpPoint;
        }

        #endregion DllImports

        #region Variables and event handling

        /// <summary>
        /// List of keyboard devices
        /// Key: the device handle
        /// Value: the device info class
        /// </summary>
        private Hashtable deviceList = new Hashtable();
        private List<string> deviceIdentityList = new List<string>(); 

        public List<string> DeviceIdentityList
        {
            get { return deviceIdentityList; }
        }

        //Event and delegate
        public delegate void DeviceEventHandler(object sender, MouseMoveEventArgs e);
        public event DeviceEventHandler MouseMoved;

        public int EventHandlerCount()
        {
            return MouseMoved.GetInvocationList().Length;
        }

        /// <summary>
        /// Arguments provided by the handler for the KeyPressed
        /// event.
        /// </summary>
        public class MouseMoveEventArgs : EventArgs
        {
            private DeviceInfo m_deviceInfo;
            private DeviceType m_device;

            public MouseMoveEventArgs(DeviceInfo dInfo, DeviceType device)
            {
                m_deviceInfo = dInfo;
                m_device = device;
            }

            public MouseMoveEventArgs()
            {
            }

            public DeviceInfo Mouse
            {
                get { return m_deviceInfo; }
                set { m_deviceInfo = value; }
            }

            public DeviceType Device
            {
                get { return m_device; }
                set { m_device = value; }
            }
        }

        #endregion Variables and event handling

        #region RawInput( IntPtr hwnd )

        /// <summary>
        /// InputDevice constructor; registers the raw input devices
        /// for the calling window.
        /// </summary>
        /// <param name="hwnd">Handle of the window listening for key presses</param>
        public RawInput(IntPtr hwnd)
        {
            RAWINPUTDEVICE[] rid = new RAWINPUTDEVICE[1];

            rid[0].usUsagePage = 0x01;
            rid[0].usUsage = 0x02;
            rid[0].dwFlags = RIDEV_INPUTSINK;
            rid[0].hwndTarget = hwnd;

            if (!RegisterRawInputDevices(rid, (uint)rid.Length, (uint)Marshal.SizeOf(rid[0])))
            {
                throw new ApplicationException("Failed to register raw input device(s).");
            }
        }

        #endregion RawInput( IntPtr hwnd )

        #region ReadIdentity( string item )

        /// <summary>
        /// Reads the Registry to retrieve a friendly description
        /// of the device, and whether it is a keyboard.
        /// </summary>
        /// <param name="item">The device name to search for, as provided by GetRawInputDeviceInfo.</param>
        /// <param name="isKeyboard">Determines whether the device's class is "Keyboard". By reference.</param>
        /// <returns>The device description stored in the Registry entry's DeviceDesc value.</returns>
        private static string ReadIdentity(string item)
        {
            // Example Device Identification string
            //\\?\HID#VID_203A&PID_FFFC&MI_00#7&37099d97&0&0000#{378de44c-56ef-11d1-bc8c-00a0c91405dd}
            // @"\??\ACPI#PNP0303#3&13c0b0c5&0#{884b96c3-56ef-11d1-bc8c-00a0c91405dd}";

            // remove the \??\
            item = item.Substring(4);

            string[] split = item.Split('#');

            string id_01 = split[0];    // HID (Class code)
            string id_02 = split[1];    // VID_203A&PID_FFFC&MI_00 (SubClass code)
            string id_03 = split[2];    // 7&37099d97&0&0000 (Protocol code)

            return id_03.Replace("&", "");
        }

        #endregion ReadIIdentity( string item )

        #region int EnumerateDevices()
        /// <summary>
        /// Iterates through the list provided by GetRawInputDeviceList,
        /// counting keyboard devices and adding them to deviceList.
        /// </summary>
        /// <returns>The number of keyboard devices found.</returns>
        public int EnumerateDevices()
        {

            int NumberOfDevices = 0;
            uint deviceCount = 0;
            int dwSize = (Marshal.SizeOf(typeof(RAWINPUTDEVICELIST)));

            if (GetRawInputDeviceList(IntPtr.Zero, ref deviceCount, (uint)dwSize) == 0)
            {
                IntPtr pRawInputDeviceList = Marshal.AllocHGlobal((int)(dwSize * deviceCount));
                GetRawInputDeviceList(pRawInputDeviceList, ref deviceCount, (uint)dwSize);

                for (int i = 0; i < deviceCount; i++)
                {
                    uint pcbSize = 0;

                    RAWINPUTDEVICELIST rid = (RAWINPUTDEVICELIST)Marshal.PtrToStructure(
                                               new IntPtr((pRawInputDeviceList.ToInt32() + (dwSize * i))),
                                               typeof(RAWINPUTDEVICELIST));

                    GetRawInputDeviceInfo(rid.hDevice, RIDI_DEVICENAME, IntPtr.Zero, ref pcbSize);

                    if (pcbSize > 0)
                    {
                        IntPtr pData = Marshal.AllocHGlobal((int)pcbSize);
                        GetRawInputDeviceInfo(rid.hDevice, RIDI_DEVICENAME, pData, ref pcbSize);
                        string deviceName = Marshal.PtrToStringAnsi(pData);

                        //The list will include the "root" keyboard and mouse devices
                        //which appear to be the remote access devices used by Terminal
                        //Services or the Remote Desktop - we're not interested in these
                        //so the following code with drop into the next loop iteration
                        if (deviceName.ToUpper().Contains("ROOT"))
                        {
                            continue;
                        }

                        //If the device is identified as a keyboard or HID device,
                        //create a DeviceInfo object to store information about it
                        if (rid.dwType == RIM_TYPEMOUSE)
                        {
                            DeviceInfo dInfo = new DeviceInfo();

                            dInfo.deviceName = deviceName;
                            dInfo.deviceHandle = rid.hDevice;
                            dInfo.deviceType = GetDeviceType(rid.dwType);

                            dInfo.identity = ReadIdentity(deviceName);

                            //NumberOfDevices count
                            if (!deviceList.Contains(rid.hDevice))
                            {
                                dInfo.index = NumberOfDevices;
                                deviceList.Add(rid.hDevice, dInfo);
                                deviceIdentityList.Add(dInfo.identity);
                                NumberOfDevices++;
                            }
                        }
                        Marshal.FreeHGlobal(pData);
                    }
                }

                Marshal.FreeHGlobal(pRawInputDeviceList);
                return NumberOfDevices;
            }
            else
            {
                throw new ApplicationException("Error!");
            }
        }

        #endregion EnumerateDevices()

        #region ProcessInputCommand( Message message )

        /// <summary>
        /// Processes WM_INPUT messages to retrieve information about any
        /// keyboard events that occur.
        /// </summary>
        /// <param name="message">The WM_INPUT message to process.</param>
        public bool ProcessInputCommand(Message message)
        {
            bool is_probe = false;
            uint dwSize = 0;

            // First call to GetRawInputData sets the value of dwSize
            // dwSize can then be used to allocate the appropriate amount of memore,
            // storing the pointer in "buffer".
            GetRawInputData(message.LParam,
                             RID_INPUT, IntPtr.Zero,
                             ref dwSize,
                             (uint)Marshal.SizeOf(typeof(RAWINPUTHEADER)));

            IntPtr buffer = Marshal.AllocHGlobal((int)dwSize);
            try
            {
                // Check that buffer points to something, and if so,
                // call GetRawInputData again to fill the allocated memory
                // with information about the input
                if (buffer != IntPtr.Zero &&
                    GetRawInputData(message.LParam,
                                     RID_INPUT,
                                     buffer,
                                     ref dwSize,
                                     (uint)Marshal.SizeOf(typeof(RAWINPUTHEADER))) == dwSize)
                {
                    // Store the message information in "raw", then check
                    // that the input comes from a keyboard device before
                    // processing it to raise an appropriate KeyPressed event.

                    RAWINPUT raw = (RAWINPUT)Marshal.PtrToStructure(buffer, typeof(RAWINPUT));

                    if (raw.header.dwType == RIM_TYPEMOUSE)
                    {
                        // Retrieve information about the device
                        DeviceInfo dInfo = null;

                        if (deviceList.Contains(raw.header.hDevice))
                        {
                            dInfo = (DeviceInfo)deviceList[raw.header.hDevice];

                            dInfo.lastX = raw.mouse.lLastX;
                            dInfo.lastY = raw.mouse.lLastY;
                            dInfo.cumulativeX += dInfo.lastX;
                            dInfo.cumulativeY += dInfo.lastY;
                            if (dInfo.probeIndex > 0 && dInfo.probeIndex < 4)
                            {
                                is_probe = true;
                            }
                        }

                        if (MouseMoved != null && dInfo != null)
                        {
                            MouseMoved(this, new MouseMoveEventArgs(dInfo, GetDevice(message.LParam.ToInt32())));
                        }
                    }
                }
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
            if (is_probe)
            {
                DisableMouse();
            }
            else
            {
                EnableMouse();
                CurrentCursorPoint = GetCursorPosition();
            }

            return is_probe;
        }

        #endregion ProcessInputCommand( Message message )

        #region DeviceType GetDevice( int param )

        /// <summary>
        /// Determines what type of device triggered a WM_INPUT message.
        /// (Used in the ProcessInputCommand).
        /// </summary>
        /// <param name="param">The LParam from a WM_INPUT message.</param>
        /// <returns>A DeviceType enum value.</returns>
        private static DeviceType GetDevice(int param)
        {
            DeviceType deviceType;

            switch ((((ushort)(param >> 16)) & FAPPCOMMAND_MASK))
            {
                case FAPPCOMMAND_OEM:
                    deviceType = DeviceType.OEM;
                    break;
                case FAPPCOMMAND_MOUSE:
                    deviceType = DeviceType.Mouse;
                    break;
                default:
                    deviceType = DeviceType.Key;
                    break;
            }
            return deviceType;
        }

        #endregion DeviceType GetDevice( int param )

        #region ProcessMessage(Message message)

        /// <summary>
        /// Filters Windows messages for WM_INPUT messages and calls
        /// ProcessInputCommand if necessary.
        /// </summary>
        /// <param name="message">The Windows message.</param>
        public bool ProcessMessage(Message message)
        {
            switch (message.Msg)
            {
                case WM_INPUT:
                {
                    return ProcessInputCommand(message);
                }
            }
            return false;
        }

        #endregion ProcessMessage( Message message )

        #region GetDeviceType( int device )

        /// <summary>
        /// Converts a RAWINPUTDEVICELIST dwType value to a string
        /// describing the device type.
        /// </summary>
        /// <param name="device">A dwType value (RIM_TYPEMOUSE, 
        /// RIM_TYPEKEYBOARD or RIM_TYPEHID).</param>
        /// <returns>A string representation of the input value.</returns>
        private static string GetDeviceType(int device)
        {
            string deviceType;
            switch (device)
            {
                case RIM_TYPEMOUSE:    deviceType = "MOUSE";    break;
                case RIM_TYPEKEYBOARD: deviceType = "KEYBOARD"; break;
                case RIM_TYPEHID:      deviceType = "HID";      break;
                default:               deviceType = "UNKNOWN";  break;
            }
            return deviceType;
        }

        #endregion GetDeviceType( int device )

        public void ZeroCumulativeX(int probeIndex)
        {
            foreach (DictionaryEntry entry in deviceList)
            {
                DeviceInfo dInfo = (DeviceInfo)entry.Value;
                if (dInfo.probeIndex == probeIndex)
                {
                    dInfo.cumulativeX = 0;
                }
            }
        }

        public void ZeroCumulativeY(int probeIndex)
        {
            foreach (DictionaryEntry entry in deviceList)
            {
                DeviceInfo dInfo = (DeviceInfo)entry.Value;
                if (dInfo.probeIndex == probeIndex)
                {
                    dInfo.cumulativeY = 0;
                }
            }
        }

        public void UpdateProbeIndexes()
        {
            foreach (DictionaryEntry entry in deviceList)
            {
                DeviceInfo dInfo = (DeviceInfo)entry.Value;
                if (PCIConfig.BaseConfig.ContainsKey("Probe1") && PCIConfig.BaseConfig["Probe1"] == dInfo.identity)
                {
                    dInfo.probeIndex = 1;
                }
                else if (PCIConfig.BaseConfig.ContainsKey("Probe2") && PCIConfig.BaseConfig["Probe2"] == dInfo.identity)
                {
                    dInfo.probeIndex = 2;
                }
                else if (PCIConfig.BaseConfig.ContainsKey("Probe3") && PCIConfig.BaseConfig["Probe3"] == dInfo.identity)
                {
                    dInfo.probeIndex = 3;
                }
            }
        }
        
        public void DisableMouse()
        {
            RECT r;
            r.left = CurrentCursorPoint.X;
            r.top = CurrentCursorPoint.Y;
            r.right = CurrentCursorPoint.X + 1;
            r.bottom = CurrentCursorPoint.Y + 1;
            ClipCursor(ref r);
        }

        public void EnableMouse()
        {
            ClipCursor(IntPtr.Zero);
        }
    }
}
