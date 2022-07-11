using Newtonsoft.Json;
using System;
using System.Linq;

namespace 基本语法
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
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
                //IConsoleTool consoleTool = new 迭代器();
               
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
                //IConsoleTool consoleTool = new LambdaTree();
                //consoleTool.ConsoleWriteLine();
            }

            #endregion LambdaTree

            //dynamic obj2 = new System.Dynamic.ExpandoObject();
            //obj2.Name = "金朝钱";
            //obj2.Age = 31;
            //obj2.Birthday = DateTime.Now;
            var data = new
            {
                action = "GetProperties",
                ip = "192.168.1.150",
                encryp = 0,
                date = DateTime.Now,
                model = "cash",
                sn = "2222",
                vs = ""
            };
            var d = data.GetType().GetProperties()//这一步获取匿名类的公共属性，返回一个数组
                .OrderBy(q => q.Name)//这一步排序，需要引入System.Linq，当然可以省略
                //.Where(q => q.Name != "vs")//这一步筛选，也可以省略
                .ToDictionary(q => q.Name, q => q.GetValue(data));//这一步将数组转换为字典
            var a = d.GetType().GetProperties();
            Console.WriteLine(string.Format("json:{0}", JsonConvert.SerializeObject(data)));
            Console.ReadKey();
        }
    }
}