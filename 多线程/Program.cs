using System;
using System.Threading;
using System.Threading.Tasks;

namespace 多线程
{
    internal class Program
    {
        //static void Main(string[] args)
        //{
        //    CancellationTokenSource source = new CancellationTokenSource();
        //    int index = 0;
        //    //开启一个task执行任务
        //    Task task1 = new Task(() =>
        //    {
        //        while (!source.IsCancellationRequested)
        //        {
        //            Thread.Sleep(1000);
        //            Console.WriteLine($"第{++index}次执行，线程运行中...");
        //        }
        //    });
        //    task1.Start();
        //    //五秒后取消任务执行
        //    Thread.Sleep(5000);
        //    //source.Cancel()方法请求取消任务，IsCancellationRequested会变成true
        //    source.Cancel();
        //    Console.ReadKey();
        //}

        static void Main(string[] args)
        {
            CancellationTokenSource source = new CancellationTokenSource();
            //注册任务取消的事件
            source.Token.Register(() =>
            {
                Console.WriteLine("任务被取消后执行xx操作！");
            });

            int index = 0;
            //开启一个task执行任务
            Task task1 = new Task(() =>
            {
                Console.WriteLine($"线程{source.IsCancellationRequested}");
                while (!source.IsCancellationRequested)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine($"第{++index}次执行，线程运行中...");
                }
            });
            task1.Start();
            //延时取消，效果等同于Thread.Sleep(5000);source.Cancel();
            source.CancelAfter(5000);
            Console.ReadKey();
        }
    }
}
