using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp
{
    /// <summary>
    /// ButtonWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ButtonWindow : Window
    {
        public ButtonWindow()
        {
            InitializeComponent();
            //double ScreenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;//WPF
            //this.Top = 0;
            //this.Left = ScreenWidth - this.Width;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            this.btn.IsEnabled = false;
            Console.WriteLine($"当前线程id:{Thread.CurrentThread.ManagedThreadId.ToString()}");
            this.txt.Text = await VsAsync();
            this.btn.IsEnabled = true;
        }

        public async Task<string> GetIdAsync()
        {
            return await Task.Run(() =>
            {
                WebClient webClient = new WebClient();
                Thread.Sleep(3000);
                Console.WriteLine($"当前线程id:{Thread.CurrentThread.ManagedThreadId.ToString()}");
                return webClient.DownloadString("https://www.google.com/");
            });
        }

        public async Task<string> VsAsync()
        {
            return await Task.Run(async () =>
             {
                 for (int i = 0; i < 1000; i++)
                 {
                     Console.WriteLine($"{i}");
                 }
                 await Task.Delay(10000);
                 return "123";
             });
        }
    }
}