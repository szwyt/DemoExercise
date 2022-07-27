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
            thread = new Thread(new ThreadStart(async () =>
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
                    ExecutablePath = Path.Combine(chromePath, "chrome2.exe"),
                    Timeout = 15000
                };
               
                for (int i = 0; i < list.Count(); i++)
                {
                    await Task.Delay(100);
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

                                    Process[] ps = Process.GetProcesses();
                                    foreach (Process p in ps)
                                    {
                                        if (p.ProcessName.ToLower().Contains("chrome2") || p.ProcessName.ToLower().Contains("Chromium"))//判断进程名称
                                        {
                                            p.Kill();//停止进程
                                        }
                                    }

                                    Console.WriteLine($"第{j}条数据------->{DateTime.Now}------->{outputFile}------->成功数：{success++}");

                                    ////第2种
                                    //using (var stream = await page.ScreenshotStreamAsync(new ScreenshotOptions { FullPage = false }))
                                    //{

                                    //    byte[] srcBuf = new Byte[stream.Length];
                                    //    stream.Read(srcBuf, 0, srcBuf.Length);
                                    //    stream.Seek(0, SeekOrigin.Begin);
                                    //    using (FileStream fs = new FileStream($"{outputFile}", FileMode.Create, FileAccess.Write))
                                    //    {
                                    //        fs.Write(srcBuf, 0, srcBuf.Length);
                                    //    }
                                    //}

                                    //第3种
                                    //var buffer = await page.ScreenshotDataAsync(new ScreenshotOptions { Type = ScreenshotType.Png, FullPage = true });
                                    //if (buffer.Length < 20 * 1024)
                                    //{
                                    //    using (FileStream fs = new FileStream($"{outputFile}", FileMode.Create, FileAccess.Write))
                                    //    {
                                    //        fs.Write(buffer, 0, buffer.Length);
                                    //    }
                                    //    Console.WriteLine($"{j}----------------->{DateTime.Now}----------------->" + outputFile);
                                    //}
                                    //else
                                    //    Console.WriteLine($"{j}----------------->{DateTime.Now}----------------->" + "buffer is big data");
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

            }));
            thread.IsBackground = true;
            thread.Start();
        }

        private async void button2_Click(object sender, EventArgs e)
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

                    var result = await page.GoToAsync($"{this.textBox1.Text}");
                    //li#ch_xxgg
                    if (!string.IsNullOrWhiteSpace(this.textBox2.Text))
                    {
                        //await page.ClickAsync($"{this.textBox2.Text}");

                        //var clickReviews = "document.querySelectorAll('span.title-s')[2].click();";
                        await page.EvaluateExpressionAsync($"{this.textBox2.Text}");

                        //var reviews = "Array.from(document.querySelectorAll('.comments-content'));";
                        //var review = await page.EvaluateExpressionAsync(reviews);
                        //Console.WriteLine(review);
                    }
                    //var a= await page.EvaluateFunctionAsync<int>($@"() => {{
                    //    return $('#{this.textBox2.Text}').length;
                    // }}");

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
                    }
                }
            }
        }
    }
}
