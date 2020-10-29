using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace ClProject.SerialPortUtility
{
    public partial class SeriaPortSetting : Form
    {
        public SeriaPortSetting()
        {
            InitializeComponent();
        }

        private void SeriaPortSetting_Load(object sender, EventArgs e)
        {
            String[] ports = SerialPort.GetPortNames();
            Array.Sort(ports);
            cbPorts.Items.AddRange(ports);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MessageBox.Show("保存成功.");
        }
    }
}