using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;

namespace ClProject.SerialPortUtility
{
    public delegate void returnWeight(double weight, string tag);

    public class SerialPortProtocol
    {
        private string tag;

        private returnWeight myDelegate = null;
        private SerialPort spCom = new SerialPort();
        private StringBuilder builder = new StringBuilder();//避免在事件处理方法中反复的创建，定义到外面。
        private long received_count = 0;//接收计数
        private bool Listening = false;//是否没有执行完invoke相关操作
        private bool Closing = false;//是否正在关闭串口，执行Application.DoEvents，并阻止再次invoke

        private List<byte> buffer = new List<byte>(4096);//默认分l配1页内存
        private byte[] binary_data_1 = new byte[12];//02 2B 30 30 30 30 33 30 30 31 38 03

        //单例
        private static SerialPortProtocol instance;

        public static SerialPortProtocol getInstance(returnWeight rwDelegate, string tag)
        {
            if (instance == null)
                instance = new SerialPortProtocol(rwDelegate, tag);
            else
            {
                instance.myDelegate = rwDelegate;
                instance.tag = tag;
            }
            if (instance.spCom.IsOpen)
            {
                instance.Closing = true;
                while (instance.Listening) Application.DoEvents();
                instance.spCom.Close();
                instance.Closing = false;
            }
            return instance;
        }

        private SerialPortProtocol(returnWeight rwDelegate, string tag)
        {
            spCom.NewLine = "/r/n";
            //添加事件注册
            spCom.DataReceived += spCom_DataReceived;
            spCom.Parity = Parity.None;
            spCom.Encoding = Encoding.ASCII;
            spCom.DataBits = 8;
            spCom.StopBits = StopBits.One;
            this.myDelegate = rwDelegate;
            this.tag = tag;
        }

        private void spCom_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (Closing) return;//如果正在关闭，忽略操作，直接返回，尽快的完成串口监听线程的一次循环

            try
            {
                Listening = true;
                int n = spCom.BytesToRead;//先记录下来，避免某种原因，人为的原因，操作几次之间时间长，缓存不一致
                byte[] buf = new byte[n];//声明一个临时数组存储当前来的串口数据
                //received_count += n;//增加接收计数
                spCom.Read(buf, 0, n);//读取缓冲数据

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////

                //解析>
                bool data_1_catched = false;//缓存记录数据是否捕获到
                //缓存数据
                buffer.AddRange(buf);
                //完整性判断
                while (buffer.Count >= 12)
                {
                    if (buffer[0].ToString("X2") == "02" && buffer[11].ToString("X2") == "03")
                    {
                        //异或校验，逐个字节异或得到校验码
                        byte checksum = 0;
                        for (int i = 1; i < 9; i++)
                        {
                            checksum ^= buffer[i];
                        }
                        //String ch = ((checksum >> 4) & 0x0F).ToString("X");  //高；
                        //String  cl = (checksum & 0x0F).ToString("X");  //低；
                        String verify = checksum.ToString("X2");
                        String verify1 = Encoding.ASCII.GetString(new byte[] { buffer[9], buffer[10] });
                        if (verify != verify1)
                        {
                            buffer.RemoveRange(0, 12);
                            continue;
                        }

                        buffer.CopyTo(0, binary_data_1, 0, 12);//复制一条完整数据到具体的数据缓存
                        data_1_catched = true;
                        buffer.RemoveRange(0, 12);//正确分析一条数据，从缓存中移除数据。
                    }
                    else
                    {
                        //这里是很重要的，如果数据开始不是头，则删除数据
                        buffer.RemoveAt(0);
                    }
                }

                //分析数据
                if (data_1_catched)
                {
                    string data = (Convert.ToInt32(binary_data_1[2].ToString("X2")) - 30) + ""
                        + (Convert.ToInt32(binary_data_1[3].ToString("X2")) - 30) + ""
                        + (Convert.ToInt32(binary_data_1[4].ToString("X2")) - 30) + ""
                        + (Convert.ToInt32(binary_data_1[5].ToString("X2")) - 30) + ""
                        + (Convert.ToInt32(binary_data_1[6].ToString("X2")) - 30) + ""
                        + (Convert.ToInt32(binary_data_1[7].ToString("X2")) - 30) + "";
                    //更新界面
                    int num = Convert.ToInt32(data);
                    myDelegate(num, tag);
                }
            }
            finally
            {
                Listening = false;//我用完了，ui可以关闭串口了。
            }
        }

        //关闭串口
        public void closeSerialPort()
        {
            if (spCom.IsOpen)
                spCom.Close();
        }
    }
}