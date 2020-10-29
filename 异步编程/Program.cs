﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace 异步编程
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine($"MainBegin:{Thread.CurrentThread.ManagedThreadId.ToString()}");
            await PagePaint();
            Console.WriteLine($"MainEnd");
            Console.ReadKey();
        }

        public async static Task GetIdAsync()
        {
            Console.WriteLine($"GetIdAsyncBegin:");
            await Task.Run(() =>
              {
                  Thread.Sleep(5000);
                  Console.WriteLine($"当前线程id:{Thread.CurrentThread.ManagedThreadId.ToString()}");
              });
            Console.WriteLine($"GetIdAsyncEnd:");
        }

        private static async Task PagePaint()
        {
            Console.WriteLine("Paint Start");
            await Paint();
            Console.WriteLine("Paint End");
        }

        //static async void Paint()
        //{
        //    Rendering("Header");
        //    Rendering(await RequestBody());
        //    Rendering("Footer");
        //}

        private static async Task Paint()
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

        private static async Task PaintAds()
        {
            string ads = await Task.Run(() =>
            {
                Thread.Sleep(5000);
                return "Ads";
            });
            Rendering(ads);
        }

        private static async Task<string> RequestBody()
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
        private static void Rendering(string v)
        {
            Console.WriteLine(v);
        }
    }
}