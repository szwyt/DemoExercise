using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 基本语法
{
    public class 迭代器 : IConsoleTool
    {
        public void ConsoleWriteLine()
        {
            //int[] arrInt = new int[] { 11, 12, 13, 14 };
            //foreach (var item in arrInt)
            //{
            //    Console.WriteLine(item);
            //}

            //var enumerator = arrInt.GetEnumerator();
            //while (enumerator.MoveNext())
            //{
            //    var item = (int)enumerator.Current;
            //    Console.WriteLine(item);
            //}

            var myColor = new MyColors();
            foreach (var item in myColor)
            {
                Console.WriteLine(item);
            }
        }
    }

    class ColorEnumeraotr : IEnumerator
    {
        string[] _colors;
        int _position = -1;

        public ColorEnumeraotr(string[] theColors)
        {
            _colors = new string[theColors.Length];
            for (int i = 0; i < theColors.Length; i++)
            {
                _colors[i] = theColors[i];
            }
        }

        public object Current
        {
            get
            {
                if (_position == -1)
                    throw new InvalidOperationException();
                if (_position >= _colors.Length)
                    throw new InvalidOperationException();

                return _colors[_position];
            }
        }

        public bool MoveNext()
        {
            if (_position < _colors.Length - 1)
            {
                _position++;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            _position = -1;
        }
    }

    class MyColors : IEnumerable
    {
        string[] Colors = new string[] { "red", "blue", "yellow", "green", "white" };

        public IEnumerator GetEnumerator()
        {
            return new ColorEnumeraotr(Colors);
        }
    }
}
