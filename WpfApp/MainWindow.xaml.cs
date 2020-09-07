using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await AccessWebAsync();
        }

        // 使用C# 5.0中提供的async 和await关键字来定义异步方法
        // 从代码中可以看出C#5.0 中定义异步方法就像定义同步方法一样简单。
        // 使用async 和await定义异步方法不会创建新线程,
        // 它运行在现有线程上执行多个任务.
        // 此时不知道大家有没有一个疑问的？在现有线程上(即UI线程上)运行一个耗时的操作时，
        // 为什么不会堵塞UI线程的呢？
        // 这个问题的答案就是 当编译器看到await关键字时，线程会
        private async Task<long> AccessWebAsync()
        {
            MemoryStream content = new MemoryStream();

            // 对MSDN发起一个Web请求
            HttpWebRequest webRequest = WebRequest.Create("http://msdn.microsoft.com/zh-cn/") as HttpWebRequest;
            if (webRequest != null)
            {
                // 返回回复结果
                using (WebResponse response = await webRequest.GetResponseAsync())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        await responseStream.CopyToAsync(content);
                    }
                }
            }
            return content.Length;
        }
    }
}
