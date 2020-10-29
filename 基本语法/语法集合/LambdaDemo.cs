using System;
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
            };
        }

        public void ConsoleWriteLine()
        {
            var list2 = users.Distinct(new DistinctCompareModel<User>((u, s) => u.DateInt == s.DateInt || u.DateTime == s.DateTime)).ToList();
            var list1 = users.DistinctLingbug<User, int>(s => s.DateInt).ToList();
        }
    }

    public class User
    {
        public int DateInt { get; set; }
        public string DateTimeStr { get; set; }

        public DateTime DateTime { get; set; }
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