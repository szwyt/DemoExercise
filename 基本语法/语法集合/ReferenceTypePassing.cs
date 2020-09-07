using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 基本语法
{
    public class ReferenceTypePassing : IConsoleTool
    {
        public void ConsoleWriteLine()
        {
            int[] intArry = new int[] { 1, 2, 3 };

            ChangeArray(intArry);

            foreach (var item in intArry)
            {
                Console.WriteLine(item);
            }
        }
        public static void ChangeArray(int[] intArray)
        {
            for (int i = 0; i < intArray.Length; i++)
            {
                intArray[i] *= 2;
            }
        }
    }
}
