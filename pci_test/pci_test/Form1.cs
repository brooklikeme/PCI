using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace pci_test
{
    public partial class MainForm : Form
    {

        const int INVALID_HANDLE_VALUE = -1;
        const int PAGE_READWRITE = 0x04;
        private IntPtr smHandle;     //文件句柄
        private IntPtr smAddr;       //共享内存地址
        private int smLength = 100;
        private byte[] smData = new byte[100];
        private int sharedMemoryInterval = 100;
        private int sharedMemoryLastTime = DateTime.Now.Millisecond;
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

        private void MainForm_Load(object sender, EventArgs e)
        {
            initSharedMemory();
        }

        private void initSharedMemory()
        {
            // byte[] smBytes = SharedMemeryData.ObjectToByteArray(smData);
            // smLength = smBytes.Length;
            smHandle = OpenFileMapping(FILE_MAP_ALL_ACCESS, 0, "PCI-SERVER-SM");
            if (smHandle == IntPtr.Zero)
            {
                MessageBox.Show("打开共享内存对象失败!");
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
        }

        private void freeSharedMemory()
        {
            UnmapViewOfFile(smAddr);
            CloseHandle(smHandle);
        }

        private void loadShareMemory()
        {
            try
            {
                smMutex.WaitOne();
                Marshal.Copy(smAddr, smData, 0, smLength);
                float force1_set = BitConverter.ToSingle(smData, SMPos.angle1);
                float force2_set = BitConverter.ToSingle(smData, SMPos.angle2);
                float force3_set = BitConverter.ToSingle(smData, SMPos.angle3);
                // smData = (SharedMemeryData)SharedMemeryData.ByteArrayToObject(recvData);
                this.BeginInvoke(new MethodInvoker(delegate
                {
                    label1.Text = force1_set.ToString();
                    label2.Text = force2_set.ToString();
                    label3.Text = force3_set.ToString();
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取共享内存出错：" + ex.Message); 
            }
            finally
            {
                smMutex.ReleaseMutex();
            }
        }

        private void updateSharedMemory()
        {
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            freeSharedMemory();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            loadShareMemory();
        }
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
}
