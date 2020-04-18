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
        List<Label> travelLabelList = new List<Label>();
        List<Label> rotationLabelList = new List<Label>();
        List<TrackBar> travelBarList = new List<TrackBar>();
        List<TrackBar> rotationBarList = new List<TrackBar>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void m_MouseMoved(object sender, RawInput.MouseMoveEventArgs e)
        {
            if (IsActive && e.Mouse.probeIndex > 0 && e.Mouse.probeIndex < 4)
            {
                travelLabelList[e.Mouse.probeIndex - 1].Text = "位置 -- " + e.Mouse.cumulativeY.ToString();
                rotationLabelList[e.Mouse.probeIndex - 1].Text =  "角度 -- " + e.Mouse.cumulativeX.ToString();
                travelBarList[e.Mouse.probeIndex - 1].Value = 0;
                rotationBarList[e.Mouse.probeIndex - 1].Value = 0;
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
            travelLabelList.Add(lbTravel1);
            travelLabelList.Add(lbTravel2);
            travelLabelList.Add(lbTravel3);

            rotationLabelList.Add(lbRotation1);
            rotationLabelList.Add(lbRotation2);
            rotationLabelList.Add(lbRotation3);

            travelBarList.Add(tbrTravel1);
            travelBarList.Add(tbrTravel2);
            travelBarList.Add(tbrTravel3);

            rotationBarList.Add(tbrRotation1);
            rotationBarList.Add(tbrRotation2);
            rotationBarList.Add(tbrRotation3);

            // Create a new InputDevice object, get the number of
            // keyboards, and register the method which will handle the 
            // InputDevice KeyPressed event
            Global.MouseInput = new RawInput(Handle);
            NumberOfMouses = Global.MouseInput.EnumerateDevices();
            Global.MouseInput.UpdateProbeIndexes();
            Global.MouseInput.MouseMoved += new RawInput.DeviceEventHandler(m_MouseMoved);
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
    }
}
