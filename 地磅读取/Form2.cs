using PuppeteerSharp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 地磅读取
{
    public partial class Form2 : Form
    {
        /// <summary>
        /// 线程总数
        /// </summary>
        private int threadNum = 3;

        /// <summary>
        /// 总数
        /// </summary>
        private int totalCount = 0;

        /// <summary>
        /// 已处理
        /// </summary>
        private int index = 0;

        /// <summary>
        /// 异常数
        /// </summary>
        private int error = 0;

        /// <summary>
        /// 成功数
        /// </summary>
        private int countSum = 0;

        private static object lockobj = new object();//创建一个对象

        /// <summary>
        /// 队列
        /// </summary>
        private ConcurrentQueue<string> queues = new ConcurrentQueue<string>();

        private Thread thread;
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            thread?.Abort();

            thread = new Thread(() =>
            {
                var list = File.ReadAllLines($"{Path.Combine(AppContext.BaseDirectory, "siteurl.txt")}");
                foreach (var item in list)
                {
                    queues.Enqueue(item);
                }
                totalCount = list.Count();

                Console.WriteLine("可执行的数据:" + list.Count() + "条");

                List<Task> tasks = new List<Task>();
                for (int i = 0; i < threadNum; i++)
                {
                    var task = Task.Run(async () =>
                    {
                        await TaskAsync();
                    });
                    tasks.Add(task);
                }
                var taskList = Task.Factory.ContinueWhenAll(tasks.ToArray(), (ts) =>
                {

                });
                taskList.Wait();
            });

            thread.IsBackground = true;
            thread.Start();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            //Process[] ps = Process.GetProcesses();
            //foreach (Process p in ps)
            //{
            //    if (p.ProcessName.ToLower().Contains($"chrome2") || p.ProcessName.ToLower().Contains($"chrome2"))//判断进程名称
            //    {
            //        p.Kill();//停止进程
            //    }
            //}

            string chromePath = Path.Combine(AppContext.BaseDirectory, ".local-chromium", "Win64-970485", "chrome-win");
            // 如果不存在chrome就下载一个
            if (!Directory.Exists(chromePath))
            {
                using (var browserFetcher = new BrowserFetcher())
                {
                    await browserFetcher.DownloadAsync();
                };

            }

            var launch = new LaunchOptions
            {
                Headless = true,
                ExecutablePath = Path.Combine(chromePath, "chrome.exe"),
            };

            try
            {
                using (var browser = await Puppeteer.LaunchAsync(launch))
                {
                    var browserContext = await browser.CreateIncognitoBrowserContextAsync();

                    using (var page = await browserContext.NewPageAsync())
                    {
                        await page.SetViewportAsync(new ViewPortOptions
                        {
                            Width = 1920,
                            Height = 1080,
                        });

                        Stopwatch stopwatch = new Stopwatch();
                        stopwatch.Start();//在下一个中间价处理前，启动计时器
                        var result = await page.GoToAsync($"{this.textURL.Text}", new NavigationOptions
                        {
                            WaitUntil = new[]
                               {
                                    WaitUntilNavigation.DOMContentLoaded,
                                    WaitUntilNavigation.Load,
                                    WaitUntilNavigation.Networkidle0,
                                    WaitUntilNavigation.Networkidle2
                                }
                        });

                        if (!string.IsNullOrWhiteSpace(this.textScripts.Text))
                        {
                            await page.EvaluateExpressionAsync($"{this.textScripts.Text}");
                        }

                        await page.WaitForTimeoutAsync(1500);

                        stopwatch.Stop();//所有的中间件处理完后，停止秒表。
                        Console.WriteLine($@"耗时{stopwatch.ElapsedMilliseconds}ms");

                        //var buffer = result.BufferAsync();
                        if (result != null && result.Status == System.Net.HttpStatusCode.OK)
                        {
                            string fileName = $"Files/{DateTime.Now.ToString("yyyyMMddHHmmss")}.Png";
                            string outputFile = $"{AppContext.BaseDirectory}/{fileName}";

                            await page.ScreenshotAsync($"{outputFile}", new ScreenshotOptions()
                            {
                                Type = ScreenshotType.Png,
                                FullPage = true,
                            });

                            PreviewForm previewForm = new PreviewForm();
                            previewForm.LoadImage?.Invoke(outputFile);
                            previewForm.Show();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private async Task TaskAsync()
        {
            string chromePath = Path.Combine(AppContext.BaseDirectory, ".local-chromium", "Win64-970485", "chrome-win");
            // 如果不存在chrome就下载一个
            if (!Directory.Exists(chromePath))
            {
                using (var browserFetcher = new BrowserFetcher())
                {
                    await browserFetcher.DownloadAsync();
                };
            }

            var launch = new LaunchOptions
            {
                Headless = true,
                ExecutablePath = Path.Combine(chromePath, "chrome.exe"),
                Timeout = 15000
            };

            while (true)
            {
                try
                {
                    var count = Interlocked.Increment(ref countSum);
                    if (count == totalCount)
                    {
                        Console.WriteLine($"执行完成");
                    }
                    var isExit = queues.TryDequeue(out string url);
                    if (!isExit)
                    {
                        continue;
                    }

                    using (var browser = await Puppeteer.LaunchAsync(launch))
                    {
                        var browserContext = await browser.CreateIncognitoBrowserContextAsync();
                        using (var page = await browserContext.NewPageAsync())
                        {
                            await page.SetViewportAsync(new ViewPortOptions
                            {
                                Width = 1920,
                                Height = 1080,
                            });

                            var result = await page.GoToAsync($"{url}", new NavigationOptions
                            {
                                WaitUntil = new[]
                                {
                                        WaitUntilNavigation.DOMContentLoaded,
                                        WaitUntilNavigation.Load,
                                        WaitUntilNavigation.Networkidle0,
                                        WaitUntilNavigation.Networkidle2
                                    }
                            });

                            await page.WaitForTimeoutAsync(1500);

                            if (result != null && result.Status == System.Net.HttpStatusCode.OK)
                            {
                                var currentIndex = Interlocked.Increment(ref index);
                                string fileName = $"Files/{currentIndex}.Png";
                                string outputFile = $"{AppContext.BaseDirectory}/{fileName}";

                                await page.ScreenshotAsync($"{outputFile}", new ScreenshotOptions()
                                {
                                    Type = ScreenshotType.Png,
                                    FullPage = true,
                                });

                                Console.WriteLine(string.Format("时间{2}、 共{0}条、 已处理{1}", totalCount, currentIndex, DateTime.Now));
                            }
                            else
                            {
                                throw new Exception("异常");
                            }
                        }

                        await browserContext.CloseAsync();
                    }
                }
                catch (Exception ex)
                {
                    var errorIndex = Interlocked.Increment(ref error);
                    Console.WriteLine(string.Format("时间{1}、 异常数{0}、异常消息：{2}", errorIndex, DateTime.Now, ex.Message));
                }
            }
        }
    }
}
