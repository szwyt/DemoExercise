using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 地磅读取
{
    public partial class PreviewForm : Form
    {
        public Action<string> LoadImage = null;
        public PreviewForm()
        {
            LoadImage = RefreshImage;
            InitializeComponent();
        }

        private void PreviewForm_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            Rectangle ret = Screen.GetWorkingArea(this);
            this.pictureBox1.ClientSize = new Size(ret.Width, ret.Height);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            this.pictureBox1.Dock = DockStyle.Fill;
            this.pictureBox1.BringToFront();
        }

        private void RefreshImage(string path = "")
        {
            this.pictureBox1.Image = Image.FromFile(path);
        }

        private void PreviewForm_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point p = new Point();
                p.X = this.Location.X + e.X + 7;//窗体在屏幕中的坐标+鼠标点击位置在窗体中的坐标+一定的偏移量
                p.Y = this.Location.Y + e.Y + 30;
                contextMenuStrip1.Show(p);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
