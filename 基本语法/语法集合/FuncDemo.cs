using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 基本语法
{
    public class FuncDemo : IConsoleTool
    {
        public void ConsoleWriteLine()
        {
            //string 是参数,int是返回值
            Func<string, int> func = param => Convert.ToInt32(param);
            string str = returnStr(() =>
            {
                return "你好啊";
            });

            returnStr(s =>
            {

            });
            Console.WriteLine(str);
        }

        /// <summary>
        /// 把方法当参数传递
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        private static string returnStr(Func<string> func)
        {
            return func();
        }

        /// <summary>
        /// 把方法当参数传递
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        private static void returnStr(Action<string> func)
        {
        }
    }
}
