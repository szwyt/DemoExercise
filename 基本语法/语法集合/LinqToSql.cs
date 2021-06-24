using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 基本语法
{
    public class LinqToSql : IConsoleTool
    {
        public List<Userinfo> UserList { get; set; } = new List<Userinfo>
        {
            new Userinfo{Id=1,UserName="user1" },
            new Userinfo{Id=2,UserName="user1" }
        };

        public List<Info> InfoList { get; set; } = new List<Info>
        {
            new Info{Id =1,InfoName="info1" },
            new Info{ }
        };

        public static List<Userinfo> treelist { get; set; } = new List<Userinfo>
        {
            new Userinfo{Id =1,UserName="一级",Pid=0 },
            new Userinfo{Id =2,UserName="二级",Pid=1 },
            new Userinfo{Id =3,UserName="三级",Pid=2 },
            new Userinfo{Id =4,UserName="一级1",Pid=0 },
            new Userinfo{Id =5,UserName="二级1",Pid=4 },
             new Userinfo{Id =6,UserName="三级1",Pid=5 },
        };

        public LinqToSql()
        {
        }

        public void ConsoleWriteLine()
        {
            #region 左连接

            //var list = (from u in UserList
            //            join i in InfoList on u.Id equals i.Id into uiresult
            //            from ui in uiresult.DefaultIfEmpty()
            //            select new
            //            {
            //                u,
            //                ui,
            //                infoid = ui?.Id ?? 0
            //            });

            //TestToListPerformance();
            //TestNoToList();
            var s = JsonConvert.SerializeObject(diguilist(0));
            var list = JsonConvert.DeserializeObject<List<Userinfo>>(s);
            var a = new List<Userinfo>();
            GetMenuInfoLists(list, a, 0);
            Console.WriteLine(s);

            #endregion 左连接
        }

        /// <summary>
        /// 将父子级数据结构转换为普通list
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static void GetMenuInfoLists(List<Userinfo> list, List<Userinfo> Resultlist, int pid)
        {
            foreach (var item in list)
            {
                Resultlist.Add(item);
                if (item.ChildrenList.Count(c => c.Pid == item.Id) > 0)
                {
                    GetMenuInfoLists(item.ChildrenList, Resultlist, item.Id);
                }
            }
        }

        /// <summary>
        /// 将父子级数据结构转换为普通list
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<Userinfo> GetMenuInfoList(List<Userinfo> list)
        {
            List<Userinfo> Resultlist = new List<Userinfo>();
            foreach (var item in list)
            {
                OperationChildData(Resultlist, item);
                Resultlist.Add(item);
            }
            return Resultlist;
        }

        /// <summary>
        /// 递归子级数据
        /// </summary>
        /// <param name="treeDataList">树形列表数据</param>
        /// <param name="parentItem">父级model</param>
        public static void OperationChildData(List<Userinfo> AllList, Userinfo item)
        {
            if (item.ChildrenList != null)
            {
                if (item.ChildrenList.Count > 0)
                {
                    AllList.AddRange(item.ChildrenList);
                    foreach (var subItem in item.ChildrenList)
                    {
                        OperationChildData(AllList, subItem);
                    }
                }
            }
        }

        /// <summary>
        /// 递归
        /// </summary>
        public static string digui(int pid)
        {
            string str = string.Empty;
            var list = treelist.Where(w => w.Pid == pid).ToList();
            foreach (var item in list)
            {
                str += item.UserName;
                if (treelist.Count(c => c.Pid == item.Id) > 0)
                {
                    str += digui(item.Id);
                }
                else
                {
                    str += ";";
                }
            }
            return str;
        }

        public static List<Userinfo> diguilist(int pid)
        {
            List<Userinfo> userinfos = new List<Userinfo>();
            var list = treelist.Where(w => w.Pid == pid).ToList();
            foreach (var item in list)
            {
                Userinfo userinfo = new Userinfo() { Id = item.Id, UserName = item.UserName, Pid = item.Pid };
                if (treelist.Count(c => c.Pid == item.Id) > 0)
                {
                    var child_menu = diguilist(item.Id);
                    if (child_menu.Count() > 0)
                    {
                        userinfo.ChildrenList = child_menu;
                    }
                    else
                    {
                        userinfo.ChildrenList = new List<Userinfo>();
                    }
                }
                else
                {
                    userinfo.ChildrenList = new List<Userinfo>();
                }
                userinfos.Add(userinfo);
            }
            return userinfos;
        }

        /// <summary>
        /// 递归
        /// </summary>
        public static string diguis(int pid)
        {
            string path = string.Empty;
            var info = treelist.FirstOrDefault(f => f.Id == pid);
            if (info == null)
            {
                return string.Empty;
            }
            else
            {
                path = $"{info.Id},{info.UserName};";
                if (info.Pid > 0)
                {
                    path = diguis(info.Pid) + path;
                }
            }
            return path;
        }

        /// <summary>
        /// 测试使用ToList():
        /// </summary>
        private static void TestToListPerformance()
        {
            Stopwatch sw = new Stopwatch();
            List<Userinfo> arrList = new List<Userinfo>();

            for (var i = 0; i < 2000000; i++)
            {
                arrList.Add(new Userinfo { Id = i, UserName = $"{i}你好" });
            }

            sw.Start();
            var query = (from l in arrList where l.Id > 10 select l).ToList();
            sw.Stop();
            Console.WriteLine("使用ToList()方法构造数据：消耗{0}毫秒", sw.ElapsedMilliseconds);
            sw.Restart();
            foreach (var v in query)
            {
            }
            sw.Stop();
            Console.WriteLine("使用ToList()方法查询：消耗{0}毫秒", sw.ElapsedMilliseconds);
            Console.WriteLine();
        }

        /// <summary>
        /// 测试不使用ToList()方法：
        /// </summary>
        private static void TestNoToList()
        {
            Stopwatch sw = new Stopwatch();
            List<Userinfo> arrList2 = new List<Userinfo>();

            for (var i = 0; i < 2000000; i++)
            {
                arrList2.Add(new Userinfo { Id = i, UserName = $"{i}你好" });
            }

            sw.Start();
            var query = from l in arrList2 where l.Id > 10 select l;
            sw.Stop();
            Console.WriteLine("不使用ToList()方法构造数据：消耗{0}毫秒", sw.ElapsedMilliseconds);
            sw.Restart();
            foreach (var v in query)
            {
            }
            sw.Stop();
            Console.WriteLine("不使用ToList()方法查询：消耗{0}毫秒", sw.ElapsedMilliseconds);
            Console.WriteLine();
        }
    }

    public class Userinfo
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public int Pid { get; set; }

        public List<Userinfo> ChildrenList { get; set; } = new List<Userinfo>();
    }

    public class Info
    {
        public int Id { get; set; }
        public string InfoName { get; set; }
    }
}