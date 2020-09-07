using SpeechLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;

namespace 语音播报
{
    public class SpVoiceUtil
    {
        /// <summary>
        /// 汉字转数字
        /// </summary>
        /// <param name="numberStr"></param>
        /// <returns></returns>
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
    }
}
