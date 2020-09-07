using SpeechLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Speech.Synthesis;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 地磅读取
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            //Task.Run(() =>
            //{
            //    while (true)
            //    {
            //        if (this.label1.Text == "") { }
            //        //WebClient webClient = new WebClient();
            //        //string html = webClient.DownloadString("http://www.baidu.com");
            //        //this.label1.Text = html;
            //        //Thread.Sleep(10000);
            //    }
            //});
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            PlayDingDong($"负差偏高,重量{NumberToChinese("3520")}");
        }

        public Task GetIdAsync()
        {
            return Task.Run(() =>
            {
                WebClient webClient = new WebClient();
                string html = webClient.DownloadString("http://www.baidu.com");
                this.BeginInvoke(new Action(() =>
                {
                    this.label1.Text = html;
                }));
            });
        }

        /// <summary>
        /// 播放音频文件
        /// </summary>
        /// <param name="inData"></param>
        /// <returns></returns>
        public static void PlayDingDong(string inData)
        {
            SpVoice sp = new SpVoice();
            //播放音频文件
            SpFileStream ss = new SpFileStream();
            ss.Open($"{ AppDomain.CurrentDomain.BaseDirectory + "Resources/901095.wav"}", SpeechStreamFileMode.SSFMOpenForRead, true);
            var stream = ss as ISpeechBaseStream;
            sp.SpeakStream(stream, SpeechVoiceSpeakFlags.SVSFIsFilename);
            ss.Close();

            SpeechSynthesizer speak = new SpeechSynthesizer();
            speak.Volume = 100;//设置音量 0~100
            speak.Rate = 1;//设置语速 
            //speak.SelectVoice("Microsoft Huihui Desktop");
            //speak.SelectVoice("Microsoft Kangkang-Chinese(Simplifined,PRC)");
            speak.SpeakAsync(inData.Replace(" ", "").Replace("   ", "").Replace("\r\n", ""));
        }

        public static string NumberToChinese(string numberStr)
        {
            string numStr = "0123456789";
            string chineseStr = "零一二三四五六七八九";
            char[] c = numberStr.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                int index = numStr.IndexOf(c[i]);
                if (index != -1)
                    c[i] = chineseStr.ToCharArray()[index];
            }
            numStr = null;
            chineseStr = null;
            return new string(c);
        }

        private string param1 = string.Empty;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            Match m = Regex.Match(textBox.Text, @"^[0-9]*$");   // 匹配正则表达式

            if (!m.Success)   // 输入的不是数字
            {
                textBox.Text = param1;   // textBox内容不变

                // 将光标定位到文本框的最后
                textBox.SelectionStart = textBox.Text.Length;
            }
            else   // 输入的是数字
            {
                param1 = textBox.Text;   // 将现在textBox的值保存下来
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = textBox1.Text;
            label2.Text = textBox2.Text;
        }
    }
}
