using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 并发锁的使用
{
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        //不使用lock锁
    //        Console.WriteLine("incorrect counter");
    //        var c = new Counter();
    //        var t1 = new Thread(() => testCounter(c));
    //        var t2 = new Thread(() => testCounter(c));
    //        var t3 = new Thread(() => testCounter(c));
    //        t1.Start();
    //        t2.Start();
    //        t3.Start();
    //        t1.Join();
    //        t2.Join();
    //        t3.Join();

    //        Console.WriteLine("Total count:{0}", c.Count);
    //        Console.WriteLine("------------------------");
    //        Console.WriteLine("correct counter");
    //        var c1 = new CountWithLock();
    //        t1 = new Thread(() => testCounter(c1));
    //        t2 = new Thread(() => testCounter(c1));
    //        t3 = new Thread(() => testCounter(c1));
    //        t1.Start();
    //        t2.Start();
    //        t3.Start();
    //        t1.Join();
    //        t2.Join();
    //        t3.Join();
    //        Console.WriteLine("Total count:{0}", c1.Count);

    //    }
    //    static void testCounter(CounterBase c)
    //    {
    //        for (int i = 0; i < 100; i++)
    //        {
    //            c.Increment();
    //            c.Decrement();
    //        }

    //    }
    //}
    ////定义抽象类
    //abstract class CounterBase
    //{
    //    public abstract void Increment();
    //    public abstract void Decrement();
    //}
    ////继承 抽象类
    //class Counter : CounterBase
    //{
    //    public int Count { get; private set; }
    //    //实现抽象方法
    //    public override void Increment()
    //    {
    //        Count++;
    //    }
    //    public override void Decrement()
    //    {
    //        Count--;
    //    }


    //}
    ////lock锁
    //class CountWithLock : CounterBase
    //{
    //    private readonly object _syncRoot = new object();
    //    public int Count { get; private set; }
    //    //实现抽象方法
    //    public override void Increment()
    //    {
    //        lock (_syncRoot)
    //        {
    //            Count++;
    //        }
    //    }
    //    public override void Decrement()
    //    {
    //        lock (_syncRoot)
    //        {
    //            Count--;
    //        }
    //    }
    //}

    class Program
    {
        static void Main(string[] args)
        {
            C1 c1 = new C1();
            //在t1线程中调用LockMe，并将deadlock设为true（将出现死锁）
            Thread t1 = new Thread(c1.LockMe);
            t1.Start(true);
            Thread.Sleep(100);
            //在主线程中lock c1
            lock (c1)
            {
                //调用没有被lock的方法
                c1.DoNotLockMe();
                //调用被lock的方法，并试图将deadlock解除
                c1.LockMe(false);
            }
        }
    }

    //class C1
    //{
    //    private bool deadlocked = true;
    //    //这个方法用到了lock，我们希望lock的代码在同一时刻只能由一个线程访问
    //    public void LockMe(object o)
    //    {
    //        lock (this)
    //        {
    //            while (deadlocked)
    //            {
    //                deadlocked = (bool)o;
    //                Console.WriteLine("Foo: I am locked :(");
    //                Thread.Sleep(500);
    //            }
    //        }
    //    }
    //    //所有线程都可以同时访问的方法
    //    public void DoNotLockMe()
    //    {
    //        Console.WriteLine("I am not locked :)");
    //    }
    //}

    class C1
    {
        private bool deadlocked = true;
        private object locker = new object();
        //这个方法用到了lock，我们希望lock的代码在同一时刻只能由一个线程访问
        public void LockMe(object o)
        {
            lock (locker)
            {
                while (deadlocked)
                {
                    deadlocked = (bool)o;
                    Console.WriteLine("Foo: I am locked :(");
                    Thread.Sleep(500);
                }
            }
        }
        //所有线程都可以同时访问的方法
        public void DoNotLockMe()
        {
            Console.WriteLine("I am not locked :)");
        }
    }
}
