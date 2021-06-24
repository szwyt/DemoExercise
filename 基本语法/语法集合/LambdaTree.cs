using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 基本语法
{
    public class LambdaTree : IConsoleTool
    {
        public void ConsoleWriteLine()
        {
            System.Linq.Expressions.Expression<Func<int, bool>> expr = i => i < 5;
            Func<int, bool> deleg2 = expr.Compile();
            Console.WriteLine("deleg2(4) = {0}", deleg2(4));
        }
    }
}
