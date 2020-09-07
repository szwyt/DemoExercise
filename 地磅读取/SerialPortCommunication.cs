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
    
    public partial class SerialPortCommunication : Form
    {
        private string tag;
        private double Weight;
        private returnWeight myDelegate = null;
        private SerialPortProtocol spp = null;
        
        
        public SerialPortCommunication()
        {
            InitializeComponent();
        }

        public SerialPortCommunication(returnWeight rwDelegate,string tag,string title)
        {
            InitializeComponent();
            this.myDelegate = rwDelegate;
            this.tag = tag;
            this.gbHint.Text = title;
            this.tbGetValue.ReadOnly = true;
            this.tbGetValue.SelectionStart = 0;
            this.tbGetValue.SelectionLength = 0;
            spp = SerialPortProtocol.getInstance(new returnWeight(receiveWeight), tag);
        }

        private void SerialPort_Load(object sender, EventArgs e)
        {
            this.btnSave.Enabled = false;
            
        }

        private void receiveWeight(double weight, String tag)
        {
            
            this.Weight = weight;
            this.tag = tag;
            this.Invoke((EventHandler)(delegate
            {
                this.btnSave.Enabled = true;
                this.tbGetValue.Text = this.Weight.ToString();
            }));  
        }
     
        private void btnSave_Click(object sender, EventArgs e)
        {
            myDelegate(this.Weight, this.tag);
        }

        private void SerialPortCommunication_FormClosed(object sender, FormClosedEventArgs e)
        {
            spp.closeSerialPort();
        }
    }
}
