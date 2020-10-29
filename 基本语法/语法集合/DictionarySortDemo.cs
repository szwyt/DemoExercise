using System;
using System.Collections.Generic;
using System.Linq;

namespace 基本语法
{
    public class DictionarySortDemo : IConsoleTool
    {
        public void ConsoleWriteLine()
        {
            Console.WriteLine("-----------------------------字典排序---------------------------");
            List<Dictionary<string, object>> keyValuePairs = new List<Dictionary<string, object>>()
            {
                new Dictionary<string, object>{ {"s","1.0" }, { "z", "3.0" } },
                new Dictionary<string, object>{ {"s","2.0" } , { "z", "1.0" }},
                new Dictionary<string, object>{ {"s","3.0" } , { "z", "2.0" }},
            };

            foreach (var item in keyValuePairs.OrderByDescending(o => o["z"]).ToList().Take(3))
            {
                Console.WriteLine(item["s"]);
            }

            Console.WriteLine("----------------------foreach里面的return含义--------------------");
            List<int> list = new List<int> { 1, 2, 3 };
            list.ForEach(o =>
            {
                if (o == 1)
                {
                    return;
                }
                Console.WriteLine(o.ToString());
            });
        }
    }
}