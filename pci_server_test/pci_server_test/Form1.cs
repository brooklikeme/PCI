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

namespace pci_server_test
{
    public partial class MainForm : Form
    {

        List<bool> detectList = new List<bool>();
        List<double> diameterList = new List<double>();

        const int INVALID_HANDLE_VALUE = -1;
        const int PAGE_READWRITE = 0x04;
        private IntPtr smHandle;     //文件句柄
        private IntPtr smAddr;       //共享内存地址
        private SMData smData;
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
            detectList.Add(false);
            detectList.Add(false);
            detectList.Add(false);

            diameterList.Add(0.8F);
            diameterList.Add(0.3F);
            diameterList.Add(2.0F);

            cbxForce1.SelectedIndex = 0;
            cbxForce2.SelectedIndex = 0;
            cbxForce3.SelectedIndex = 0;
        }

        private void initSharedMemory()
        {

            smData = new SMData();

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

            timer1.Enabled = true;
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
                smMutex.WaitOne();
                Marshal.Copy(smData.data, 0, smAddr, (int)SMPos.sm_set_length);
            }
            catch (Exception ex)
            {
                // MessageBox.Show("更新共享内存出错：" + ex.Message);
            }
            finally
            {
                smMutex.ReleaseMutex();
            }
        }

        private void loadSharedMemory()
        {
            try
            {
                smMutex.WaitOne();
                Marshal.Copy(smAddr, smData.data, 0, (int)SMPos.sm_length);
                smData.getData();
            }
            catch (Exception ex)
            {
                // MessageBox.Show("获取共享内存出错：" + ex.Message);
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
            loadSharedMemory();
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

                lbThicknessValue1.Text = smData.diam1.ToString();
                lbThicknessValue2.Text = smData.diam2.ToString();
                lbThicknessValue3.Text = smData.diam3.ToString();

                lbTravelValue1.Text = smData.travel1.ToString();
                lbTravelValue2.Text = smData.travel2.ToString();
                lbTravelValue3.Text = smData.travel3.ToString();

                lbRotationValue1.Text = smData.angle1.ToString();
                lbRotationValue2.Text = smData.angle2.ToString();
                lbRotationValue3.Text = smData.angle3.ToString();

                tbrTravel1.Value = smData.travel1 < 0 ? (int)smData.travel1 + 3000 : (int)smData.travel1 % 3000;
                tbrTravel2.Value = smData.travel2 < 0 ? (int)smData.travel2 + 3000 : (int)smData.travel2 % 3000;
                tbrTravel3.Value = smData.travel3 < 0 ? (int)smData.travel3 + 3000 : (int)smData.travel3 % 3000;

                cpbRotation1.Value = (int)smData.angle1;
                cpbRotation2.Value = (int)smData.angle2;
                cpbRotation3.Value = (int)smData.angle3;
            }));
        }

        private void btnSetForce3_Click(object sender, EventArgs e)
        {
            smData.setAimForce3(Int32.Parse(cbxForce3.GetItemText(cbxForce3.SelectedItem)));
            updateSharedMemory();
        }

        private void btnSetForce2_Click(object sender, EventArgs e)
        {
            smData.setAimForce2(Int32.Parse(cbxForce2.GetItemText(cbxForce2.SelectedItem)));
            updateSharedMemory();
        }

        private void btnSetForce1_Click(object sender, EventArgs e)
        {
            smData.setAimForce1(Int32.Parse(cbxForce1.GetItemText(cbxForce1.SelectedItem)));
            updateSharedMemory();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
        public static int force1 = 52;      // int32
        public static int force2 = 56;      // int32
        public static int force3 = 60;      // int32

        public static int contrast = 64;    // boolean
        public static int switch1 = 65;     // boolean
        public static int switch2 = 66;     // boolean

        public static int sm_length = 100;        // shared memory total size
        public static int sm_set_length = 12;     // shared memory settnig size
    }

    public class SMData
    {
        public float diam1;             // Unit: mm
        public float diam2;             // Unit: mm
        public float diam3;             // Unit: mm
        public float travel1;           // Unit: mm
        public float travel2;           // Unit: mm
        public float travel3;           // Unit: mm
        public float angle1;            // Unit: degree
        public float angle2;            // Unit: degree
        public float angle3;            // Unit: degree
        public int pressure;            // kPa
        public int force1;              // Force Percentage
        public int force2;              // Force Percentage
        public int force3;              // Force Percentage

        public bool contrast;           // true: action, false: no action
        public bool switch1;            // true: on, false: off
        public bool switch2;            // true: on, false: off

        public byte[] data;             // shared memory data 

        public SMData(){
            data = new byte[100];
        }

        public int getAimForce1()
        {
            return BitConverter.ToInt32(data, SMPos.force1_set);
        }
        public int getAimForce2()
        {
            return BitConverter.ToInt32(data, SMPos.force2_set);
        }
        public int getAimForce3()
        {
            return BitConverter.ToInt32(data, SMPos.force3_set);
        }

        public void setAimForce1(int value)
        {
            Array.Copy(BitConverter.GetBytes(value), 0, data, SMPos.force1_set, 4);
        }

        public void setAimForce2(int value)
        {
            Array.Copy(BitConverter.GetBytes(value), 0, data, SMPos.force2_set, 4);
        }
        public void setAimForce3(int value)
        {
            Array.Copy(BitConverter.GetBytes(value), 0, data, SMPos.force3_set, 4);
        }

        public void getData()
        {
            this.diam1 = BitConverter.ToSingle(data, SMPos.diam1);
            this.diam2 = BitConverter.ToSingle(data, SMPos.diam2);
            this.diam3 = BitConverter.ToSingle(data, SMPos.diam3);

            this.travel1 = BitConverter.ToSingle(data, SMPos.travel1);
            this.travel2 = BitConverter.ToSingle(data, SMPos.travel2);
            this.travel3 = BitConverter.ToSingle(data, SMPos.travel3);

            this.angle1 = BitConverter.ToSingle(data, SMPos.angle1);
            this.angle2 = BitConverter.ToSingle(data, SMPos.angle2);
            this.angle3 = BitConverter.ToSingle(data, SMPos.angle3);

            this.pressure = BitConverter.ToInt32(data, SMPos.pressure);

            this.force1 = BitConverter.ToInt32(data, SMPos.force1);
            this.force2 = BitConverter.ToInt32(data, SMPos.force2);
            this.force3 = BitConverter.ToInt32(data, SMPos.force3);

            this.contrast = BitConverter.ToBoolean(data, SMPos.contrast);

            this.switch1 = BitConverter.ToBoolean(data, SMPos.switch1);
            this.switch2 = BitConverter.ToBoolean(data, SMPos.switch2);

        }
        
        public void setData()
        {
            // readonly
            Array.Copy(BitConverter.GetBytes(this.diam1), 0, data, SMPos.diam1, 4);
            Array.Copy(BitConverter.GetBytes(this.diam2), 0, data, SMPos.diam2, 4);
            Array.Copy(BitConverter.GetBytes(this.diam3), 0, data, SMPos.diam3, 4);

            Array.Copy(BitConverter.GetBytes(this.travel1), 0, data, SMPos.travel1, 4);
            Array.Copy(BitConverter.GetBytes(this.travel2), 0, data, SMPos.travel2, 4);
            Array.Copy(BitConverter.GetBytes(this.travel3), 0, data, SMPos.travel3, 4);

            Array.Copy(BitConverter.GetBytes(this.angle1), 0, data, SMPos.angle1, 4);
            Array.Copy(BitConverter.GetBytes(this.angle2), 0, data, SMPos.angle2, 4);
            Array.Copy(BitConverter.GetBytes(this.angle3), 0, data, SMPos.angle3, 4);

            Array.Copy(BitConverter.GetBytes(this.pressure), 0, data, SMPos.pressure, 4);

            Array.Copy(BitConverter.GetBytes(this.force1), 0, data, SMPos.force1, 4);
            Array.Copy(BitConverter.GetBytes(this.force2), 0, data, SMPos.force2, 4);
            Array.Copy(BitConverter.GetBytes(this.force3), 0, data, SMPos.force3, 4);

            Array.Copy(BitConverter.GetBytes(this.contrast), 0, data, SMPos.contrast, 1);

            Array.Copy(BitConverter.GetBytes(this.switch1), 0, data, SMPos.switch1, 1);
            Array.Copy(BitConverter.GetBytes(this.switch2), 0, data, SMPos.switch2, 1);
        }
    } 
}
