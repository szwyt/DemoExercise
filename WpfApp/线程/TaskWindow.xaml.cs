using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// TaskWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TaskWindow : Window
    {
        public TaskWindow()
        {
            InitializeComponent();

          
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("BeforeInvoke1");
                Dispatcher.Invoke((Action)(() => {
                    Thread.Sleep(1000);
                    Console.WriteLine("Invoke1");
                }));
                Console.WriteLine("EndInvoke1");
            });
        }
    }
}
