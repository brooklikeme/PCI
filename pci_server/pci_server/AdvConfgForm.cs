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
    public partial class AdvConfgForm : Form
    {
        public AdvConfgForm()
        {
            InitializeComponent();
        }
     
        private void deviceDataReceived(DeviceData deviceData)
        {
            this.BeginInvoke(new MethodInvoker(delegate
            {
                txtHall1.Text = deviceData.hall_value1.ToString();
                txtHall2.Text = deviceData.hall_value2.ToString();
                txtHall3.Text = deviceData.hall_value3.ToString();

                txtServoAngle1.Text = deviceData.servo_angle1.ToString();
                txtServoAngle2.Text = deviceData.servo_angle2.ToString();
                txtServoAngle3.Text = deviceData.servo_angle3.ToString();

                txtContrast.Text = deviceData.contrast.ToString();
            }));
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReloadAdvConfig_Click(object sender, EventArgs e)
        {
            ReloadAdvConfig();
        }

        private void ReloadAdvConfig()
        {
            //USBSerial.AdvConfig;
            txtMinHallDiam1.Text =  USBSerial.advConfig.min_hall_diam1.ToString();
            txtMinHallValue1.Text = USBSerial.advConfig.min_hall_value1.ToString();
            txtMaxHallDiam1.Text = USBSerial.advConfig.max_hall_diam1.ToString();
            txtMaxHallValue1.Text = USBSerial.advConfig.max_hall_value1.ToString();

            txtMinHallDiam2.Text = USBSerial.advConfig.min_hall_diam2.ToString();
            txtMinHallValue2.Text = USBSerial.advConfig.min_hall_value2.ToString();
            txtMaxHallDiam2.Text = USBSerial.advConfig.max_hall_diam2.ToString();
            txtMaxHallValue2.Text = USBSerial.advConfig.max_hall_value2.ToString();

            txtMinHallDiam3.Text = USBSerial.advConfig.min_hall_diam3.ToString();
            txtMinHallValue3.Text = USBSerial.advConfig.min_hall_value3.ToString();
            txtMaxHallDiam3.Text = USBSerial.advConfig.max_hall_diam3.ToString();
            txtMaxHallValue3.Text = USBSerial.advConfig.max_hall_value3.ToString();

            txtMinServoAngle1.Text = USBSerial.advConfig.min_servo_angle1.ToString();
            txtMaxServoAngle1.Text = USBSerial.advConfig.max_servo_angle1.ToString();
            txtMinServoAngle2.Text = USBSerial.advConfig.min_servo_angle2.ToString();
            txtMaxServoAngle2.Text = USBSerial.advConfig.max_servo_angle2.ToString();
            txtMinServoAngle3.Text = USBSerial.advConfig.min_servo_angle3.ToString();
            txtMaxServoAngle3.Text = USBSerial.advConfig.max_servo_angle3.ToString();

            txtContrastThreshold.Text = USBSerial.advConfig.contrast_threshold.ToString();
        }

        private void UpdateAdvConfig()
        {
            int value = 0;
            USBSerial.advConfig.min_hall_diam1 = Int32.TryParse(txtMinHallDiam1.Text, out value) ? (byte)value : USBSerial.advConfig.min_hall_diam1;
            USBSerial.advConfig.min_hall_value1 = Int32.TryParse(txtMinHallValue1.Text, out value) ? value : USBSerial.advConfig.min_hall_value1;
            USBSerial.advConfig.max_hall_diam1 = Int32.TryParse(txtMaxHallDiam1.Text, out value) ? (byte)value : USBSerial.advConfig.max_hall_diam1;
            USBSerial.advConfig.max_hall_value1 = Int32.TryParse(txtMaxHallValue1.Text, out value) ? value : USBSerial.advConfig.max_hall_value1;

            USBSerial.advConfig.min_hall_diam2 = Int32.TryParse(txtMinHallDiam2.Text, out value) ? (byte)value : USBSerial.advConfig.min_hall_diam2;
            USBSerial.advConfig.min_hall_value2 = Int32.TryParse(txtMinHallValue2.Text, out value) ? value : USBSerial.advConfig.min_hall_value2;
            USBSerial.advConfig.max_hall_diam2 = Int32.TryParse(txtMaxHallDiam2.Text, out value) ? (byte)value : USBSerial.advConfig.max_hall_diam2;
            USBSerial.advConfig.max_hall_value2 = Int32.TryParse(txtMaxHallValue2.Text, out value) ? value : USBSerial.advConfig.max_hall_value2;

            USBSerial.advConfig.min_hall_diam3 = Int32.TryParse(txtMinHallDiam3.Text, out value) ? (byte)value : USBSerial.advConfig.min_hall_diam3;
            USBSerial.advConfig.min_hall_value3 = Int32.TryParse(txtMinHallValue3.Text, out value) ? value : USBSerial.advConfig.min_hall_value3;
            USBSerial.advConfig.max_hall_diam3 = Int32.TryParse(txtMaxHallDiam3.Text, out value) ? (byte)value : USBSerial.advConfig.max_hall_diam3;
            USBSerial.advConfig.max_hall_value3 = Int32.TryParse(txtMaxHallValue3.Text, out value) ? value : USBSerial.advConfig.max_hall_value3;

            USBSerial.advConfig.min_servo_angle1 = Int32.TryParse(txtMinServoAngle1.Text, out value) ? (byte)value : USBSerial.advConfig.min_servo_angle1;
            USBSerial.advConfig.max_servo_angle1 = Int32.TryParse(txtMaxServoAngle1.Text, out value) ? (byte)value : USBSerial.advConfig.max_servo_angle1;
            USBSerial.advConfig.min_servo_angle2 = Int32.TryParse(txtMinServoAngle2.Text, out value) ? (byte)value : USBSerial.advConfig.min_servo_angle2;
            USBSerial.advConfig.max_servo_angle2 = Int32.TryParse(txtMaxServoAngle2.Text, out value) ? (byte)value : USBSerial.advConfig.max_servo_angle2;
            USBSerial.advConfig.min_servo_angle3 = Int32.TryParse(txtMinServoAngle3.Text, out value) ? (byte)value : USBSerial.advConfig.min_servo_angle3;
            USBSerial.advConfig.max_servo_angle3 = Int32.TryParse(txtMaxServoAngle3.Text, out value) ? (byte)value : USBSerial.advConfig.max_servo_angle3;

            USBSerial.advConfig.contrast_threshold = Int32.TryParse(txtContrastThreshold.Text, out value) ? value : USBSerial.advConfig.contrast_threshold;
        }

        private void AdvConfgForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            USBSerial.deviceDataReceived -= new USBSerial.DeviceDataReceived(deviceDataReceived);
        }

        private void AdvConfgForm_Load(object sender, EventArgs e)
        {
            USBSerial.deviceDataReceived += new USBSerial.DeviceDataReceived(deviceDataReceived);
            ReloadAdvConfig();
        }

        private void btnSetMinAngle1_Click(object sender, EventArgs e)
        {
            if (USBSerial.advConfig.max_hall_value1 > 100)
            {
                int angle = 0;
                if (Int32.TryParse(txtMinServoAngle1.Text, out angle) && angle <= 180 && angle >= 0)
                {
                    USBSerial.SetServoAngle(0, angle);
                }
            }
        }

        private void btnSetMaxAngle1_Click(object sender, EventArgs e)
        {
            if (USBSerial.advConfig.max_hall_value1 > 100)
            {
                int angle = 0;
                if (Int32.TryParse(txtMaxServoAngle1.Text, out angle) && angle <= 180 && angle >= 0)
                {
                    USBSerial.SetServoAngle(0, angle);
                }
            }
        }

        private void btnSetMinAngle2_Click(object sender, EventArgs e)
        {
            if (USBSerial.advConfig.max_hall_value1 > 100)
            {
                int angle = 0;
                if (Int32.TryParse(txtMinServoAngle2.Text, out angle) && angle <= 180 && angle >= 0)
                {
                    USBSerial.SetServoAngle(1, angle);
                }
            }
        }

        private void btnSetMaxAngle2_Click(object sender, EventArgs e)
        {
            if (USBSerial.advConfig.max_hall_value1 > 100)
            {
                int angle = 0;
                if (Int32.TryParse(txtMaxServoAngle2.Text, out angle) && angle <= 180 && angle >= 0)
                {
                    USBSerial.SetServoAngle(1, angle);
                }
            }
        }

        private void btnSetMinAngle3_Click(object sender, EventArgs e)
        {
            if (USBSerial.advConfig.max_hall_value1 > 100)
            {
                int angle = 0;
                if (Int32.TryParse(txtMinServoAngle3.Text, out angle) && angle <= 180 && angle >= 0)
                {
                    USBSerial.SetServoAngle(2, angle);
                }
            }
        }

        private void btnSetMaxAngle3_Click(object sender, EventArgs e)
        {
            if (USBSerial.advConfig.max_hall_value1 > 100)
            {
                int angle = 0;
                if (Int32.TryParse(txtMaxServoAngle3.Text, out angle) && angle <= 180 && angle >= 0)
                {
                    USBSerial.SetServoAngle(2, angle);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            UpdateAdvConfig();
            USBSerial.UpdateAdvConfig();
            this.Close();
        }
    }
}
