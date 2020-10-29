using System;
using System.IO;

namespace 基本语法
{
    public class DirectoryDemo : IConsoleTool
    {
        // 获取程序的基目录。
        private string path = System.AppDomain.CurrentDomain.BaseDirectory;

        // 获取模块的完整路径，包含文件名
        private string path1 = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;

        // 获取和设置当前目录(该进程从中启动的目录)的完全限定目录。
        private string path2 = System.Environment.CurrentDirectory;

        // 获取应用程序的当前工作目录，注意工作目录是可以改变的，而不限定在程序所在目录。
        private string path3 = System.IO.Directory.GetCurrentDirectory();

        // 获取和设置包括该应用程序的目录的名称。
        private string path4 = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

        public void ConsoleWriteLine()
        {
            DirectoryInfo folder = new DirectoryInfo(path);

            foreach (var file in folder.GetDirectories())
            {
                Console.WriteLine(file.FullName);
                foreach (var item in file.GetFiles("*.tt"))
                {
                    Console.WriteLine(item.Name);
                }
            }
        }
    }
}