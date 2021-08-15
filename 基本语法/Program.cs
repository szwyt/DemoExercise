using System;

namespace 基本语法
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            #region 字典排序Demo练习

            //{
            //    IConsoleTool consoleTool = new DictionarySortDemo();
            //    consoleTool.ConsoleWriteLine();
            //}

            #endregion 字典排序Demo练习

            #region 引用类型当参数传递Demo

            //{
            //    IConsoleTool consoleTool = new ReferenceTypePassing();
            //    consoleTool.ConsoleWriteLine();
            //}

            #endregion 引用类型当参数传递Demo

            #region FuncDemo

            //{
            //    IConsoleTool consoleTool = new FuncDemo();
            //    consoleTool.ConsoleWriteLine();
            //}

            #endregion FuncDemo

            #region SelectMnayDemo

            //{
            //    IConsoleTool consoleTool = new SelectManyDemo();
            //    consoleTool.ConsoleWriteLine();
            //}

            #endregion SelectMnayDemo

            #region Switch新语法Demo

            //{
            //    IConsoleTool consoleTool = new Switch新语法Demo();
            //    consoleTool.ConsoleWriteLine();
            //}

            #endregion Switch新语法Demo

            #region 泛型Demo

            //{
            //    IConsoleTool consoleTool = new GenericsDemo();
            //    consoleTool.ConsoleWriteLine();
            //}

            #endregion 泛型Demo

            #region 反射

            //{
            //    IConsoleTool consoleTool = new ReflectDemo();
            //    consoleTool.ConsoleWriteLine();
            //}

            #endregion 反射

            #region 获取文件夹下的所有文件的文件名

            //{
            //    IConsoleTool consoleTool = new DirectoryDemo();
            //    consoleTool.ConsoleWriteLine();
            //}

            #endregion 获取文件夹下的所有文件的文件名

            #region Lambda表达式

            {
                //IConsoleTool consoleTool = new LambdaDemo();
                //consoleTool.ConsoleWriteLine();
            }

            #endregion Lambda表达式

            #region LinqToSql

            {
                //IConsoleTool consoleTool = new LinqToSql();
                //consoleTool.ConsoleWriteLine();
            }

            #endregion LinqToSql

            #region LambdaTree

            {
                IConsoleTool consoleTool = new LambdaTree();
                consoleTool.ConsoleWriteLine();
            }

            #endregion LambdaTree
           
            Console.ReadKey();
        }
    }
}