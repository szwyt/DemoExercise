using System;
using System.Threading;
using System.Threading.Tasks;

namespace 热加载
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("执行前Main.....线程ID:" + Thread.CurrentThread.ManagedThreadId.ToString());//步骤①
            Task<int> res = GetResultAsync();
            Console.WriteLine("执行结束Main....线程ID:" + Thread.CurrentThread.ManagedThreadId.ToString());//步骤②
            Console.WriteLine("执行结果：" + res.Result + "....线程ID:" + Thread.CurrentThread.ManagedThreadId.ToString());//步骤③
            Console.ReadKey();
        }
        async static Task<int> GetResultAsync()
        {
            Console.WriteLine("执行前GetResult.....线程ID:" + Thread.CurrentThread.ManagedThreadId.ToString());//步骤④
            await Task.Delay(10000);
            Console.WriteLine("执行结束GetResult.....线程ID:" + Thread.CurrentThread.ManagedThreadId.ToString());//步骤⑤
            return 10;
        }
    }


}
