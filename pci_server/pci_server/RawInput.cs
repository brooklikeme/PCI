﻿using System;
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

namespace pci_server
{
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
        public void ProcessInputCommand(Message message)
        {
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
        public void ProcessMessage(Message message)
        {
            switch (message.Msg)
            {
                case WM_INPUT:
                {
                    ProcessInputCommand(message);
                }
                break;
            }
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


    }
}
