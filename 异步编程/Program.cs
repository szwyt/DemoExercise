using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 异步编程
{
    class Program
    {
        static void Main(string[] args)
        {
            PagePaint();
            Console.ReadKey();
        }

        public static Task GetIdAsync()
        {
            return Task.Run(() =>
              {
                  Thread.Sleep(2000);
                  Console.WriteLine($"当前线程id:{Thread.CurrentThread.ManagedThreadId.ToString()}");
              });
        }
        static void PagePaint()
        {
            Console.WriteLine("Paint Start");
            Paint();
            Console.WriteLine("Paint End");
        }

        //static async void Paint()
        //{
        //    Rendering("Header");
        //    Rendering(await RequestBody());
        //    Rendering("Footer");
        //}

        static async void Paint()
        {
            await PaintAds();
            Rendering("Header");
            Rendering(await RequestBody());
            Rendering("Footer");
        }
        //static async void Paint()
        //{
        //    PaintAds();
        //    Rendering("Header");
        //    Rendering(await RequestBody());
        //    Rendering("Footer");
        //}

        static async Task PaintAds()
        {
            string ads = await Task.Run(() =>
            {
                Thread.Sleep(5000);
                return "Ads";
            });
            Rendering(ads);
        }
        static async Task<string> RequestBody()
        {
            return await Task.Run(() =>
            {
                Thread.Sleep(5000);
                return "Body";
            });
        }
        //static void Paint()
        //{
        //    Rendering("Header");
        //    Rendering(RequestBody());
        //    Rendering("Footer");
        //}

        //static string RequestBody()
        //{
        //    Thread.Sleep(5000);
        //    return "Body";
        //}
        static void Rendering(string v)
        {
            Console.WriteLine(v);
        }
    }
}
