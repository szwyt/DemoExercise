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

                                    var buffer = await result.BufferAsync();
                                    if (buffer.Length < 20 * 1024)
                                    {
                                        await page.ScreenshotAsync($"{outputFile}", new ScreenshotOptions()
                                        {
                                            Type = ScreenshotType.Png,
                                            FullPage = true,
                                        });
                                        Console.WriteLine($"{j}----------------->{DateTime.Now}----------------->" + outputFile);
                                    }
                                    else
                                        Console.WriteLine($"{j}----------------->{DateTime.Now}----------------->" + "buffer is big data");
                                }
                                else
                                {
                                    Console.WriteLine($"{j}----------------->{DateTime.Now}----------------->" + "没有生成图片");
                                }
                                await page.DisposeAsync();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        var k = i + 1;
                        Console.WriteLine($"{k}----------------->{DateTime.Now}----------------->" + $"error:{ex.Message}");
                    }
                }
            });
        }
    }
}
