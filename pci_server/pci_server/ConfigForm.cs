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
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
        }

        private void m_MouseMoved(object sender, RawInput.MouseMoveEventArgs e)
        {
            txt1.Text = e.Mouse.deviceName;
            txtX.Text = e.Mouse.lastX.ToString();
            txtY.Text = e.Mouse.lastY.ToString();
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            Global.MouseInput.MouseMoved += new RawInput.DeviceEventHandler(m_MouseMoved);
        }

        private void ConfigForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Global.MouseInput.MouseMoved -= new RawInput.DeviceEventHandler(m_MouseMoved);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Global.MouseInput.EventHandlerCount().ToString());
        }
    }
}
