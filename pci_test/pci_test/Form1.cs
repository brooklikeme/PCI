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
        private SharedMemeryData smData;
        private int sharedMemoryInterval = 100;
        private int sharedMemoryLastTime = DateTime.Now.Millisecond;
        private int smLength = 1000;
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
            // init data
            smData = new SharedMemeryData();

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
                byte[] recvData = new byte[1000];
                Marshal.Copy(smAddr, recvData, 0, smLength);
                int i = BitConverter.ToInt32(recvData, 0);
                // smData = (SharedMemeryData)SharedMemeryData.ByteArrayToObject(recvData);
                this.BeginInvoke(new MethodInvoker(delegate
                {
                    label1.Text = i.ToString();
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
            try
            {
                smData.readonlyData.pressure = DateTime.Now.Millisecond;
                smMutex.WaitOne();
                byte[] sendData = SharedMemeryData.ObjectToByteArray(smData);
                if (sendData.Length <= smLength)
                    Marshal.Copy(sendData, 0, smAddr, (int)smLength);
            }
            catch (Exception ex)
            {
                //   
            }
            finally
            {
                smMutex.ReleaseMutex();
            }
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

    [Serializable]
    class ReadonlyData
    {
        public float diam1;
        public float diam2;
        public float diam3;
        public float travel1;
        public float travel2;
        public float travel3;
        public float angle1;
        public float angle2;
        public float angle3;
        public int pressure;
        public int contrast;
        public bool switch1;
        public bool switch2;
        public int force1;
        public int force2;
        public int force3;
    }

    [Serializable]
    class WritableData
    {
        public int force1_set;
        public int force2_set;
        public int force3_set;
    }

    [Serializable]
    class SharedMemeryData
    {
        public WritableData writableData;
        public ReadonlyData readonlyData;

        public SharedMemeryData()
        {
            writableData = new WritableData();
            readonlyData = new ReadonlyData();
        }
        ~SharedMemeryData()
        {
        }

        // Convert an object to a byte array
        public static byte[] ObjectToByteArray(Object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static T FromByteArray<T>(byte[] data)
        {
            if (data == null)
                return default(T);
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(data))
            {
                object obj = bf.Deserialize(ms);
                return (T)obj;
            }
        }

        // Convert a byte array to an Object
        public static Object ByteArrayToObject(byte[] arrBytes)
        {
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = binForm.Deserialize(memStream);
                return obj;
            }
        }
    }
}
