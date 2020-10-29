using System;

namespace 基本语法
{
    public class Switch新语法Demo : IConsoleTool
    {
        //元组
        public (string, string) tuple { get; set; } = ("rock", "paper");

        public void ConsoleWriteLine()
        {
            //Console.WriteLine(FromRainbow(Rainbow.Green));
            //Console.WriteLine(RockPaperScissors("rock", "paper"));
            //Console.WriteLine(RockPaperScissors(tuple.Item1, tuple.Item2));
            //Console.WriteLine(GetTupleResult(new Tuple<bool, int>(true, 1)).Item1 + "------" + GetTupleResult(new Tuple<bool, int>(true, 1)).Item2);
            //Console.WriteLine(GetTupleResult(s =>
            //{
            //    return (s.Item2 + 1).ToString();
            //}, (true, 11)).Item2);

            Console.WriteLine(FromRainbow("1"));
        }

        /// <summary>
        /// 枚举模式
        /// </summary>
        /// <param name="colorBand"></param>
        /// <returns></returns>
        public static string FromRainbow(Rainbow colorBand) =>
        colorBand switch
        {
            Rainbow.Red => Rainbow.Red.ToString(),
            Rainbow.Orange => Rainbow.Orange.ToString(),
            Rainbow.Yellow => Rainbow.Yellow.ToString(),
            Rainbow.Green => Rainbow.Green.ToString(),
            Rainbow.Blue => Rainbow.Blue.ToString(),
            Rainbow.Indigo => Rainbow.Indigo.ToString(),
            Rainbow.Violet => Rainbow.Violet.ToString(),
            _ => throw new ArgumentException(message: "invalid enum value", paramName: nameof(colorBand)),
        };

        public static string FromRainbow(string str)
        {
            return (str) switch
            {
                "0" => str,
                "1" => $"{str}%1",
                _ => string.Empty
            };
        }

        /// <summary>
        /// 元组模式
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static string RockPaperScissors(string first, string second)
        => (first, second) switch
        {
            ("rock", "paper") => "rock is covered by paper. Paper wins.",
            ("rock", "scissors") => "rock breaks scissors. Rock wins.",
            ("paper", "rock") => "paper covers rock. Paper wins.",
            ("paper", "scissors") => "paper is cut by scissors. Scissors wins.",
            ("scissors", "rock") => "scissors is broken by rock. Rock wins.",
            ("scissors", "paper") => "scissors cuts paper. Scissors wins.",
            (_, _) => "tie"
        };

        /// <summary>
        /// 元组返回值
        /// </summary>
        /// <returns></returns>
        public static (bool, int) GetTupleResult(Tuple<bool, int> tuple)
        {
            (bool, int) result = (false, 0);
            result.Item1 = tuple.Item1;
            result.Item2 = tuple.Item2;
            return result;
        }

        /// <summary>
        /// 元组Lambda表达式结合使用
        /// </summary>
        /// <param name="func"></param>
        /// <param name="tuple"></param>
        /// <returns></returns>
        public static (bool, int) GetTupleResult(Func<(bool, int), string> func, (bool, int) tuple)
        {
            (bool, int) result = (false, 0);
            result.Item1 = tuple.Item1;
            result.Item2 = Convert.ToInt32(func(tuple));
            return result;
        }
    }

    public enum Rainbow
    {
        Red,
        Orange,
        Yellow,
        Green,
        Blue,
        Indigo,
        Violet
    }
}