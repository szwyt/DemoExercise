using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 基本语法
{
    class Program
    {
        private static string hello;
        static void Main(string[] args)
        {
            #region 字典排序Demo练习
            //{
            //    IConsoleTool consoleTool = new DictionarySortDemo();
            //    consoleTool.ConsoleWriteLine();
            //}
            #endregion

            #region 引用类型当参数传递Demo
            //{
            //    IConsoleTool consoleTool = new ReferenceTypePassing();
            //    consoleTool.ConsoleWriteLine();
            //}
            #endregion

            #region FuncDemo
            //{
            //    IConsoleTool consoleTool = new FuncDemo();
            //    consoleTool.ConsoleWriteLine();
            //}
            #endregion

            #region SelectMnayDemo
            //{
            //    IConsoleTool consoleTool = new SelectManyDemo();
            //    consoleTool.ConsoleWriteLine();
            //}
            #endregion

            #region Switch新语法Demo
            //{
            //    IConsoleTool consoleTool = new Switch新语法Demo();
            //    consoleTool.ConsoleWriteLine();
            //}
            #endregion

            #region 泛型Demo
            {
                IConsoleTool consoleTool = new GenericsDemo();
                consoleTool.ConsoleWriteLine();
            }
            #endregion

            #region 反射
            //{
            //    IConsoleTool consoleTool = new ReflectDemo();
            //    consoleTool.ConsoleWriteLine();
            //}
            #endregion

            Console.ReadKey();
        }
    }
}
