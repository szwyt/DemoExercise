﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace 基本语法
{
    public class LambdaDemo : IConsoleTool

    {
        private List<User> users;

        public LambdaDemo()
        {
            users = new List<User>
            {
                new User{ DateInt=1, DateTimeStr ="2019-15-20" ,DateTime =DateTime.Now.Date},
                new User{DateInt =2, DateTimeStr ="2019-15-21" ,DateTime =DateTime.Now.Date},
                new User{ DateInt=1, DateTimeStr ="2019-15-23" ,DateTime =DateTime.Now.Date},
                new User{DateInt =2, DateTimeStr ="2019-15-21" ,DateTime =DateTime.Now.Date},
            };
        }


        class Student
        {
            public int Score { get; set; }

            public Student(int score)
            {
                this.Score = score;
            }
        }

        class Teacher
        {
            public string Name { get; set; }

            public List<Student> Students;

            public Teacher(string order, List<Student> students)
            {
                this.Name = order;

                this.Students = students;
            }
        }


        public void ConsoleWriteLine()
        {
            string[] words = new string[] { "Able", "was", "I", "ere", "I", "saw", "Elba" };
            string s = words.Aggregate((a, n) =>$"{a},{n}");
            Console.WriteLine(s);
            #region Lambda表达式

            {
                List<Teacher> teachers = new List<Teacher>
                {
                    new Teacher("a",new List<Student>{ new Student(100),new Student(90),new Student(30) }),
                    new Teacher("b",new List<Student>{ new Student(100),new Student(90),new Student(60) }),
                    new Teacher("c",new List<Student>{ new Student(100),new Student(90),new Student(40) }),
                    new Teacher("d",new List<Student>{ new Student(100),new Student(90),new Student(60) }),
                    new Teacher("e",new List<Student>{ new Student(100),new Student(90),new Student(50) }),
                    new Teacher("f",new List<Student>{ new Student(100),new Student(90),new Student(60) }),
                    new Teacher("g",new List<Student>{ new Student(100),new Student(90),new Student(60) })
                };
                //List<User> listDistinct1 = users.DistinctLingbug<User, int>(s => s.DateInt).ToList();
                List<User> listDistinct2 = users.Distinct(new DistinctCompareModel<User>((u, s) => u.DateInt == s.DateInt || u.DateTimeStr == s.DateTimeStr)).ToList();
                //var list = users.GroupBy(o => new { o.DateInt, o.DateTimeStr });
                //foreach (var item in list)
                //{
                //    Console.WriteLine(item.Key.DateInt);
                //    Console.WriteLine(item.Key.DateTimeStr);
                //    foreach (var info in item)
                //    {
                //        Console.WriteLine(info.DateInt);
                //    }
                //}

                //foreach (var info in listDistinct2)
                //{
                //    Console.WriteLine(info.DateInt);
                //}

                //users = new List<User>();
                // 数据数量为0的时候除了sum,其他的聚合函数都把报错
                //Console.WriteLine(users.Sum(s => s.DateInt));
                //Console.WriteLine(users.Average(s => s.DateInt));
                //Console.WriteLine(users.Max(s => s.DateInt));
                //Console.WriteLine(users.Min(s => s.DateInt));

                var list2 = teachers.SelectMany(t => t.Students).ToList();
                var list3 = teachers.SelectMany(
                               t => t.Students,
                               (t, s) => new { t.Name, s.Score }).ToList();

                foreach (var item in list3)
                {
                    Console.WriteLine($"{item.Name}-->{item.Score}");
                }
            }

            #endregion Lambda表达式

            #region 九九乘法表

            {
                //for (int i = 9; i > 0; i--)
                //{
                //    for (int j = i; j > 0; j--)
                //    {
                //        Console.Write($"{i}*{j}={i * j}\t");
                //    }
                //    Console.Write($"{System.Environment.NewLine}");
                //}
                //Console.WriteLine($"===================================");
                //for (int i = 1; i < 10; i++)
                //{
                //    for (int j = 1; j <= i; j++)
                //    {
                //        Console.Write($"{i}*{j}={i * j}\t");
                //    }
                //    Console.Write($"{System.Environment.NewLine}");
                //}
            }

            #endregion 九九乘法表

            #region 冒泡排序

            {
                ////从小到大
                //Console.WriteLine("从小到大");
                //int[] nums = { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
                //for (int i = 0; i < nums.Length - 1; i++)
                //{
                //    for (int j = 0; j < nums.Length - 1 - i; j++)
                //    {
                //        if (nums[j] > nums[j + 1])
                //        {
                //            int temp = nums[j];
                //            nums[j] = nums[j + 1];
                //            nums[j + 1] = temp;
                //        }
                //    }
                //}
                ////打印数组
                //for (int i = 0; i < nums.Length; i++)
                //{
                //    Console.WriteLine(nums[i]);
                //}

                //// 从大到小
                //Console.WriteLine("从大到小");
                //for (int i = 0; i < nums.Length - 1; i++)
                //{
                //    for (int j = 0; j < nums.Length - 1 - i; j++)
                //    {
                //        if (nums[j] < nums[j + 1])
                //        {
                //            int temp = nums[j];
                //            nums[j] = nums[j + 1];
                //            nums[j + 1] = temp;
                //        }
                //    }
                //}
                //for (int i = 0; i < nums.Length; i++)
                //{
                //    Console.WriteLine(nums[i]);
                //}
                //Console.ReadKey();
            }

            #endregion 冒泡排序

            #region 递归

            {
                //Console.WriteLine(User.Sum(1, 100));
            }

            #endregion 递归
        }
    }

    public class User
    {
        public int DateInt { get; set; }
        public string DateTimeStr { get; set; }

        public DateTime DateTime { get; set; }

        /// <summary>
        /// 递归斐波拉切数列
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long Fibonacci(long n)
        {
            if (n < 3) return 1;
            return Fibonacci(n - 1) + Fibonacci(n - 2);
        }

        /// <summary>
        /// 递归相加
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int Sum(int startint, int endint)
        {
            if (startint == endint)
                return endint;
            return startint + Sum(startint + 1, endint);
        }
    }

    public static class DistinctExtension
    {
        /// <summary>
        /// 第一种方法：利用HashSet无法添加重复数据的机制
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static IEnumerable<T> DistinctLingbug<T, TResult>(this IEnumerable<T> source, Func<T, TResult> selector)
        {
            //初始化HashSet
            var hs = new HashSet<TResult>();
            //循环参数集合
            foreach (var item in source)
            {
                //拿到数据
                var result = selector(item);
                //将数据添加到HashSet，这里利用了HashSet无法添加重复数据的机制，如果重复是无法添加成功的
                var isAddSuccess = hs.Add(result);
                //如果添加成功，说明满足去重条件，返回该数据，yield关键字一会说明
                if (isAddSuccess) yield return item;
            }
        }
    }

    /// <summary>
    /// 第二种方式：传递Distinct方法的第二个参数，这里使用委托+泛型来实现
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DistinctCompareModel<T> : IEqualityComparer<T>
    {
        private Func<T, T, bool> _Equals;

        private Func<T, int> _GetHashCode;

        public DistinctCompareModel(Func<T, T, bool> equals, Func<T, int> getHashCode)
        {
            this._Equals = equals;
            this._GetHashCode = getHashCode;
        }

        public DistinctCompareModel(Func<T, T, bool> equals) : this(equals, r => 0)
        {
        }

        public bool Equals(T x, T y)
        {
            return _Equals(x, y);
        }

        public int GetHashCode(T obj)
        {
            return _GetHashCode(obj);
        }
    }
}