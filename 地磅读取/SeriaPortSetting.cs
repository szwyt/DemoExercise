using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

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
