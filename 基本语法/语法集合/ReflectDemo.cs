using System;
using System.Reflection;

namespace 基本语法
{
    public class ReflectDemo : IConsoleTool
    {
        public void ConsoleWriteLine()
        {
            Type type = typeof(GenericDouble<>).MakeGenericType(typeof(int));
            dynamic oObject = Activator.CreateInstance(type);
            MethodInfo method = type.GetMethod("Show").MakeGenericMethod(typeof(string), typeof(DateTime));
            method.Invoke(oObject, new object[] { 345, "感谢有梦", DateTime.Now });

            Type type1 = typeof(GenericClass<,,>).MakeGenericType(typeof(int), typeof(string), typeof(DateTime));
            dynamic oObject1 = Activator.CreateInstance(type1);
            MethodInfo method1 = type1.GetMethod("Show");
            method1.Invoke(oObject1, new object[] { 345, "感谢有梦", DateTime.Now });

            Type type2 = typeof(Singleton);
            Singleton singletonA = (Singleton)Activator.CreateInstance(type2, true);
        }
    }
}