using System;

namespace 基本语法
{
    public class GenericsDemo : IConsoleTool
    {
        private string hello;

        public void ConsoleWriteLine()
        {
            Show<int>(1);
            Show<string>("你好啊");
            Show<DateTime>(DateTime.Now.Date);

            Console.WriteLine(nameof(hello));
            object o = Show<object>("你好啊");
            System.Diagnostics.Debug.Assert(o == null);
            Console.WriteLine(Show<int>(1));
            Console.WriteLine(o == null);
        }

        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tParameter"></param>
        public T Show<T>(T tParameter)
        {
            T t = default(T);
            Console.WriteLine("This is {0},parameter={1},type={2}",
               typeof(GenericsDemo), tParameter.GetType().Name, tParameter);
            return t;
        }
    }
}