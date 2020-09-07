using SpeechLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 语音播报
{
    class Program
    {
        static void Main(string[] args)
        {
            //Task.Run(()=> 
            //{
            //    for (int i = 0; i < 3; i++)
            //    {
            //        PlayDingDong($"负差偏高,重量{NumberToChinese("3520")}");
            //        Thread.Sleep(6000);
            //    }
            //});
            Thread th_autoPrint = null;

            if (th_autoPrint == null)
            {
                // 自动称重打牌线程
                th_autoPrint = new Thread(Thread_AutoPrint);
                th_autoPrint.IsBackground = true;
                th_autoPrint.Start();
            }
            Console.ReadKey();
        }


        private static void Thread_AutoPrint()
        {
            while (true)
            {
                AutoPrint();
                Thread.Sleep(500);
            }
        }

        private static void AutoPrint()
        {
            for (int i = 0; i < 1; i++)
            {
                PlayDingDong($"负差偏高,重量{NumberToChinese("3520")}");
                Thread.Sleep(6000);
            }
        }

        public static void PlayVoice(string inData)
        {
            SpeechSynthesizer speak = new SpeechSynthesizer();
            speak.Volume = 100;//设置音量 0~100
            speak.Rate = 1;//设置语速 
            //speak.SelectVoice("Microsoft Huihui Desktop");
            speak.SpeakAsync(inData.Replace(" ", "").Replace("   ", "").Replace("\r\n", ""));
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
            ss.Open($"{ AppDomain.CurrentDomain.BaseDirectory + "901095.wav"}", SpeechStreamFileMode.SSFMOpenForRead, true);
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

        //以下方法网上查询后 修改：
        public static string ChineseTONumber(string chineseStr1)
        {
            string numStr = "0123456789";
            string chineseStr = "零一二三四五六七八九";
            char[] c = chineseStr1.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                int index = chineseStr.IndexOf(c[i]);
                if (index != -1)
                    c[i] = numStr.ToCharArray()[index];
            }
            numStr = null;
            chineseStr = null;
            return new string(c);
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
    }
}
