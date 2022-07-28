using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace NET调用其它程序包
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ProcessStartInfo processStartInfo = new System.Diagnostics.ProcessStartInfo();

            //processStartInfo.FileName = @"D:\电脑软件\QQ\Tencent\QQ\Bin\QQ.exe";  //资源管理器

            //Process.Start(processStartInfo);
            //Thread.Sleep(5000);
            //Process[] ps = Process.GetProcesses();
            //foreach (Process p in ps)
            //{
            //    if (p.ProcessName.ToLower().Contains("chrome")|| p.ProcessName.ToLower().Contains("Chromium"))//判断进程名称
            //    {
            //        p.Kill();//停止进程
            //    }
            //}

            Thread thread = new Thread(() =>
              {
                  for (int i = 0; i < 100000; i++)
                  {
                      Thread.Sleep(10);
                      Console.WriteLine($"第{i}条数据");
                  }
              });
            thread.IsBackground = true;
            thread.Start();
            Console.ReadKey();
        }
    }
}