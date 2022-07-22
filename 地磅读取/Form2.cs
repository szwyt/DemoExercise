using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 地磅读取
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
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
            await Task.Run(async () =>
            {
                int success = 0;
                int error = 0;
                int bigdata = 0;
                int noimage = 0;
                for (int i = 0; i < list.Count(); i++)
                {

                    try
                    {
                        var launch = new LaunchOptions
                        {
                            Headless = true,
                            ExecutablePath = Path.Combine(chromePath, "chrome.exe")
                        };
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
                                await page.WaitForTimeoutAsync(3000);
                                int j = i + 1;
                                if (result != null && result.Status == System.Net.HttpStatusCode.OK)
                                {
                                    string fileName = $"Files/{j}.Png";
                                    string outputFile = $"{AppContext.BaseDirectory}/{fileName}";
                                    //第1种
                                    var buffer = await result.BufferAsync();
                                    if (buffer.Length < 30 * 1024)
                                    {
                                        await page.ScreenshotAsync($"{outputFile}", new ScreenshotOptions()
                                        {
                                            Type = ScreenshotType.Png,
                                            FullPage = true,
                                        });
                                        Console.WriteLine($"{j}------->{DateTime.Now}------->{outputFile}------->成功数：{success++}");
                                    }
                                    else
                                        Console.WriteLine($"{j}------->{DateTime.Now}------->buffer is big data------->bigdata数：{bigdata}");

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
                                    Console.WriteLine($"{j}------->{DateTime.Now}------->no generate image------->noimages数：{noimage}");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        var k = i + 1;
                        Console.WriteLine($"{k}------->{DateTime.Now}------->error:{ex.Message}------->异常数：{error}");
                    }
                }
            });
        }
    }
}
