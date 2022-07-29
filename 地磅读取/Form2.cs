using PuppeteerSharp;
using System;
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
        private Thread thread;
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            thread?.Abort();

            thread = new Thread(async () =>
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

                var list = File.ReadAllLines($"{Path.Combine(AppContext.BaseDirectory, "siteurl.txt")}");
                int success = 1;
                int error = 1;
                int noimage = 1;

                var launch = new LaunchOptions
                {
                    Headless = true,
                    ExecutablePath = Path.Combine(chromePath, "chrome.exe"),
                    Timeout = 15000
                };

                for (int i = 0; i < list.Count(); i++)
                {
                    Console.WriteLine($"线程运行状态：{Thread.CurrentThread.IsAlive}");
                    await Task.Delay(500);
                    int j = i + 1;
                    try
                    {
                        using (var browser = await Puppeteer.LaunchAsync(launch))
                        {
                            using (var page = await browser.NewPageAsync())
                            {
                                await page.SetViewportAsync(new ViewPortOptions
                                {
                                    Width = 1920,
                                    Height = 1080,
                                });
                                var item = list[i];
                                var result = await page.GoToAsync($"{item}");

                                await page.WaitForTimeoutAsync(1000);

                                if (result != null && result.Status == System.Net.HttpStatusCode.OK)
                                {
                                    string fileName = $"Files/{j}.Png";
                                    string outputFile = $"{AppContext.BaseDirectory}/{fileName}";

                                    await page.ScreenshotAsync($"{outputFile}", new ScreenshotOptions()
                                    {
                                        Type = ScreenshotType.Png,
                                        FullPage = true,
                                    });

                                    Console.WriteLine($"第{j}条数据------->{DateTime.Now}------->{outputFile}------->成功数：{success++}");
                                }
                                else
                                {
                                    Console.WriteLine($"第{j}条数据------->{DateTime.Now}------->no generate image------->noimages数：{noimage++}");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"第{j}条数据------->{DateTime.Now}------->error:{ex.Message}------->异常数：{error++}");
                    }
                }
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
                ExecutablePath = Path.Combine(chromePath, "chrome.exe")
            };

            using (var browser = await Puppeteer.LaunchAsync(launch))
            {
                using (var page = await browser.NewPageAsync())
                {
                    //await page.AddScriptTagAsync("https://code.jquery.com/jquery-3.2.1.min.Js")
                    //    .ContinueWith(_ => Task.CompletedTask);
                    await page.SetViewportAsync(new ViewPortOptions
                    {
                        Width = 1920,
                        Height = 1080,
                    });

                    var result = await page.GoToAsync($"{this.textURL.Text}");
                    //li#ch_xxgg
                    if (!string.IsNullOrWhiteSpace(this.textScripts.Text))
                    {
                        //await page.ClickAsync($"{this.textBox2.Text}");

                        //var clickReviews = "document.querySelectorAll('span.title-s')[2].click();";
                        await page.EvaluateExpressionAsync($"{this.textScripts.Text}");

                        //var reviews = "Array.from(document.querySelectorAll('.comments-content'));";
                        //var review = await page.EvaluateExpressionAsync(reviews);
                        //Console.WriteLine(review);
                    }

                    await page.WaitForTimeoutAsync(1500);
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
    }
}
