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

        public MainForm()
        {
            InitializeComponent();
        }

        private void m_MouseMoved(object sender, RawInput.MouseMoveEventArgs e)
        {
            txt1.Text = e.Mouse.deviceName;
            txtX.Text = e.Mouse.lastX.ToString();
            txtY.Text = e.Mouse.lastY.ToString();
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

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            ConfigForm frmConfig = new ConfigForm();
            // mouseInput.MouseMoved += new RawInput.DeviceEventHandler(frmConfig.m_MouseMoved);
            // Show testDialog as a modal dialog and determine if DialogResult = OK.
            if (frmConfig.ShowDialog(this) == DialogResult.OK)
            {
                // Read the contents of testDialog's TextBox.
                // this.txtResult.Text = testDialog.TextBox1.Text;
            } else {
                // this.txtResult.Text = "Cancelled";
            }
            frmConfig.Dispose();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Create a new InputDevice object, get the number of
            // keyboards, and register the method which will handle the 
            // InputDevice KeyPressed event
            Global.MouseInput = new RawInput(Handle);
            NumberOfMouses = Global.MouseInput.EnumerateDevices();
            Global.MouseInput.MouseMoved += new RawInput.DeviceEventHandler(m_MouseMoved);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show(Global.MouseInput.EventHandlerCount().ToString());
        }
    }
}
