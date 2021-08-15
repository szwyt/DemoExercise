using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace 基本语法
{
    /// <summary>
    /// 表达式树
    /// </summary>
    public class LambdaTree : IConsoleTool
    {
        public void ConsoleWriteLine()
        {
            //System.Linq.Expressions.Expression<Func<int, bool>> expr = i => i < 5;
            //Func<int, bool> deleg2 = expr.Compile();
            //Console.WriteLine("deleg2(4) = {0}", deleg2(4));
            //i*j+w*x
            //ParameterExpression a = Expression.Parameter(typeof(int), "i");   //创建一个表达式树中的参数，作为一个节点，这里是最下层的节点
            //ParameterExpression b = Expression.Parameter(typeof(int), "j");
            //BinaryExpression r1 = Expression.Multiply(a, b);    //这里i*j,生成表达式树中的一个节点，比上面节点高一级

            //ParameterExpression c = Expression.Parameter(typeof(int), "w");
            //ParameterExpression d = Expression.Parameter(typeof(int), "x");
            //BinaryExpression r2 = Expression.Multiply(c, d);

            //BinaryExpression result = Expression.Subtract(r1, r2);   //运算两个中级节点，产生终结点

            //Expression<Func<int, int, int, int, int>> lambda = Expression.Lambda<Func<int, int, int, int, int>>(result, a, b, c, d);

            //Console.WriteLine(lambda + "");   //输出‘(i,j,w,x)=>((i*j)+(w*x))’，z对应参数b，p对应参数a
            //Console.WriteLine(lambda.Body);   //输出‘(i,j,w,x)=>((i*j)+(w*x))’，z对应参数b，p对应参数a

            //Func<int, int, int, int, int> f = lambda.Compile();  //将表达式树描述的lambda表达式，编译为可执行代码，并生成该lambda表达式的委托；

            //Console.WriteLine(f.Invoke(1, 1, 1, 1) + "");  //输出结果2
            Expression<Func<demo, bool>> conditions = f => true;
            Expression<Func<demo, bool>> filter1 = f => false;
            List<demo> lambdas = new List<demo>();
            lambdas.Add(new demo { Id = 1, Name = "szw" });
            lambdas.Add(new demo { Id = 2, Name = "baifan" });
            lambdas.Add(new demo { Id = 3, Name = "bf" });
            //conditions = conditions.And(LambdaUtil.Equal<demo>("Id", 1));
            //conditions = conditions.And(LambdaUtil.StartWith<demo>("Name", "s"));
            filter1 = filter1.Or(o => o.Name.StartsWith("b"));
            conditions = conditions.And(filter1);
            var test = lambdas.OrderByDescending(LambdaUtil.Order<demo,int>("Id").Compile()).Where(conditions.Compile()).FirstOrDefault();
            Console.WriteLine(JsonConvert.SerializeObject(test));
        }
    }

    public class demo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public static class LambdaUtil
    {
        /// <summary>
        /// lambda表达式:t=>true
        /// </summary>
        /// <returns></returns>
        public static Expression<Func<T, bool>> True<T>()
        {
            return t => true;
        }

        /// <summary>
        /// lambda表达式:t=>false
        /// </summary>
        /// <returns></returns>
        public static Expression<Func<T, bool>> False<T>()
        {
            return t => false;
        }

        /// <summary>
        /// lambda表达式:t=>t.propName
        /// 多用于order排序
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <typeparam name="TKey">返回类型</typeparam>
        /// <param name="propName">属性名</param>
        /// <returns></returns>
        public static Expression<Func<T, TKey>> Order<T, TKey>(string propName)
        {
            // 创建节点参数t
            ParameterExpression parameter = Expression.Parameter(typeof(T), "t");
            // 创建一个属性
            MemberExpression property = Expression.Property(parameter, propName);
            // 生成lambda表达式
            return Expression.Lambda<Func<T, TKey>>(property, parameter);
        }

        /// <summary>
        /// lambda表达式:t=>t.propName==propValue
        /// 多用于where条件
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="propName">属性名称</param>
        /// <param name="propValue">属性值</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Equal<T>(string propName, object propValue)
        {
            // 创建节点参数t
            ParameterExpression parameter = Expression.Parameter(typeof(T), "t");
            // 创建一个成员(字段/属性)
            MemberExpression member = Expression.PropertyOrField(parameter, propName);
            // 创建一个常数
            ConstantExpression constant = Expression.Constant(propValue);
            // 创建一个相等比较Expression
            BinaryExpression binary = Expression.Equal(member, constant);
            // 生成lambda表达式
            return Expression.Lambda<Func<T, bool>>(binary, parameter);
        }

        /// <summary>
        /// lambda表达式:t=>t.propName!=propValue
        /// 多用于where条件
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="propName">属性名称</param>
        /// <param name="propValue">属性值</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> NotEqual<T>(string propName, object propValue)
        {
            // 创建节点参数t
            ParameterExpression parameter = Expression.Parameter(typeof(T), "t");
            // 创建一个成员(字段/属性)
            MemberExpression member = Expression.PropertyOrField(parameter, propName);
            // 创建一个常数
            ConstantExpression constant = Expression.Constant(propValue);
            // 创建一个不相等比较Expression
            BinaryExpression binary = Expression.NotEqual(member, constant);
            // 生成lambda表达式
            return Expression.Lambda<Func<T, bool>>(binary, parameter);
        }

        /// <summary>
        /// lambda表达式:t=>t.propName&lt;propValue
        /// 多用于where条件
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="propName">属性名称</param>
        /// <param name="propValue">属性值</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> LessThan<T>(string propName, object propValue)
        {
            // 创建节点参数t
            ParameterExpression parameter = Expression.Parameter(typeof(T), "t");
            // 创建一个成员(字段/属性)
            MemberExpression member = Expression.PropertyOrField(parameter, propName);
            // 创建一个常数
            ConstantExpression constant = Expression.Constant(propValue);
            // 创建一个不相等比较Expression
            BinaryExpression binary = Expression.LessThan(member, constant);
            // 生成lambda表达式
            return Expression.Lambda<Func<T, bool>>(binary, parameter);
        }

        /// <summary>
        /// lambda表达式:t=>t.propName&lt;=propValue
        /// 多用于where条件
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="propName">属性名称</param>
        /// <param name="propValue">属性值</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> LessThanOrEqual<T>(string propName, object propValue)
        {
            // 创建节点参数t
            ParameterExpression parameter = Expression.Parameter(typeof(T), "t");
            // 创建一个成员(字段/属性)
            MemberExpression member = Expression.PropertyOrField(parameter, propName);
            // 创建一个常数
            ConstantExpression constant = Expression.Constant(propValue);
            // 创建一个不相等比较Expression
            BinaryExpression binary = Expression.LessThanOrEqual(member, constant);
            // 生成lambda表达式
            return Expression.Lambda<Func<T, bool>>(binary, parameter);
        }

        /// <summary>
        /// lambda表达式:t=>t.propName>propValue
        /// 多用于where条件
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="propName">属性名称</param>
        /// <param name="propValue">属性值</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> GreaterThan<T>(string propName, object propValue)
        {
            // 创建节点参数t
            ParameterExpression parameter = Expression.Parameter(typeof(T), "t");
            // 创建一个成员(字段/属性)
            MemberExpression member = Expression.PropertyOrField(parameter, propName);
            // 创建一个常数
            ConstantExpression constant = Expression.Constant(propValue);
            // 创建一个不相等比较Expression
            BinaryExpression binary = Expression.GreaterThan(member, constant);
            // 生成lambda表达式
            return Expression.Lambda<Func<T, bool>>(binary, parameter);
        }

        /// <summary>
        /// lambda表达式:t=>t.propName>=propValue
        /// 多用于where条件
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="propName">属性名称</param>
        /// <param name="propValue">属性值</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> GreaterThanOrEqual<T>(string propName, object propValue)
        {
            // 创建节点参数t
            ParameterExpression parameter = Expression.Parameter(typeof(T), "t");
            // 创建一个成员(字段/属性)
            MemberExpression member = Expression.PropertyOrField(parameter, propName);
            // 创建一个常数
            ConstantExpression constant = Expression.Constant(propValue);
            // 创建一个不相等比较Expression
            BinaryExpression binary = Expression.GreaterThanOrEqual(member, constant);
            // 生成lambda表达式
            return Expression.Lambda<Func<T, bool>>(binary, parameter);
        }

        /// <summary>
        /// lambda表达式:t=>{t.contains(propvalue1) ||...||t.contains(propvalueN)}
        /// 多用于where条件
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="propName">属性名称</param>
        /// <param name="propValues">属性值数组</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> In<T>(string propName, string[] propValues)
        {
            // 创建节点参数t
            ParameterExpression parameter = Expression.Parameter(typeof(T), "t"); // left
            // 创建一个成员(字段/属性)
            MemberExpression member = Expression.PropertyOrField(parameter, propName);
            // 创建一个常数
            Expression constant = Expression.Constant(false);
            // 创建一个方法
            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            foreach (string item in propValues)
            {
                // 创建一个带参数方法Expression
                MethodCallExpression methodCall = Expression.Call(member, method, Expression.Constant(item)); // right
                // 连接参数方法
                constant = Expression.Or(methodCall, constant);
            }

            // 生成lambda表达式
            return Expression.Lambda<Func<T, bool>>(constant, new ParameterExpression[] { parameter });
        }

        /// <summary>
        /// lambda表达式:t=>{!(t.contains(propvalue1) ||...||t.contains(propvalueN))}
        /// 多用于where条件
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="propName">属性名称</param>
        /// <param name="propValues">属性值数组</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> NotIn<T>(string propName, string[] propValues)
        {
            // 创建节点参数t
            ParameterExpression parameter = Expression.Parameter(typeof(T), "t");
            // 创建一个成员(字段/属性)
            MemberExpression member = Expression.PropertyOrField(parameter, propName);
            // 创建一个常数
            Expression constant = Expression.Constant(false);
            // 创建一个方法
            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            foreach (string item in propValues)
            {
                // 创建一个带参数方法Expression
                MethodCallExpression methodCall = Expression.Call(member, method, Expression.Constant(item)); // right
                // 连接参数方法
                constant = Expression.Or(methodCall, constant);
            }

            // 生成lambda表达式
            return Expression.Lambda<Func<T, bool>>(Expression.Not(constant), new ParameterExpression[] { parameter });
        }

        /// <summary>
        /// lambda表达式:t=>t.propName.Contains(propValue)
        /// 多用于where条件
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="propName">属性名称</param>
        /// <param name="propValue">属性值</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Contains<T>(string propName, string propValue)
        {
            // 创建节点参数t
            ParameterExpression parameter = Expression.Parameter(typeof(T), "t");
            // 创建一个成员(字段/属性)
            MemberExpression member = Expression.PropertyOrField(parameter, propName);
            // 创建一个常数
            ConstantExpression constant = Expression.Constant(propValue, typeof(string));
            // 创建一个方法
            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            // 创建一个带参数方法Expression
            MethodCallExpression methodCall = Expression.Call(member, method, constant);
            // 生成lambda表达式
            return Expression.Lambda<Func<T, bool>>(methodCall, parameter);
        }

        /// <summary>
        /// lambda表达式:t=>t.propName.Contains(propValue)
        /// 多用于where条件
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="propName">属性名称</param>
        /// <param name="propValue">属性值</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> StartWith<T>(string propName, string propValue)
        {
            // 创建节点参数t
            ParameterExpression parameter = Expression.Parameter(typeof(T), "t");
            // 创建一个成员(字段/属性)
            MemberExpression member = Expression.PropertyOrField(parameter, propName);
            // 创建一个常数
            ConstantExpression constant = Expression.Constant(propValue, typeof(string));
            // 创建一个方法
            MethodInfo method = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
            // 创建一个带参数方法Expression
            MethodCallExpression methodCall = Expression.Call(member, method, constant);
            // 生成lambda表达式
            return Expression.Lambda<Func<T, bool>>(methodCall, parameter);
        }

        /// <summary>
        /// lambda表达式:t=>t.propName.Contains(propValue)
        /// 多用于where条件
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="propName">属性名称</param>
        /// <param name="propValue">属性值</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> EndsWith<T>(string propName, string propValue)
        {
            // 创建节点参数t
            ParameterExpression parameter = Expression.Parameter(typeof(T), "t");
            // 创建一个成员(字段/属性)
            MemberExpression member = Expression.PropertyOrField(parameter, propName);
            // 创建一个常数
            ConstantExpression constant = Expression.Constant(propValue, typeof(string));
            // 创建一个方法
            MethodInfo method = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });
            // 创建一个带参数方法Expression
            MethodCallExpression methodCall = Expression.Call(member, method, constant);
            // 生成lambda表达式
            return Expression.Lambda<Func<T, bool>>(methodCall, parameter);
        }

        /// <summary>
        /// lambda表达式:!(t=>t.propName.Contains(propValue))
        /// 多用于where条件
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="propName">属性名称</param>
        /// <param name="propValue">属性值</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> NotContains<T>(string propName, string propValue)
        {
            // 创建节点参数t
            ParameterExpression parameter = Expression.Parameter(typeof(T), "t");
            // 创建一个成员(字段/属性)
            MemberExpression member = Expression.PropertyOrField(parameter, propName);
            // 创建一个常数
            ConstantExpression constant = Expression.Constant(propValue, typeof(string));
            // 创建一个方法
            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            // 创建一个带参数方法Expression
            MethodCallExpression methodCall = Expression.Call(member, method, constant);
            // 生成lambda表达式
            return Expression.Lambda<Func<T, bool>>(Expression.Not(methodCall), parameter);
        }

        /// <summary>
        /// lambda表达式:t=>{left and right}
        /// 多用于where条件
        /// </summary>
        /// <param name="left">左侧条件</param>
        /// <param name="right">右侧条件</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            // 创建参数表达式
            InvocationExpression invocation = Expression.Invoke(right, left.Parameters.Cast<Expression>());
            // 创建and运算
            BinaryExpression binary = Expression.And(left.Body, invocation);
            // 生成lambda表达式
            return Expression.Lambda<Func<T, bool>>(binary, left.Parameters);
        }

        /// <summary>
        /// lambda表达式:t=>{left or right}
        /// 多用于where条件
        /// </summary>
        /// <param name="left">左侧条件</param>
        /// <param name="right">右侧条件</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            // 创建参数表达式
            InvocationExpression invocation = Expression.Invoke(right, left.Parameters.Cast<Expression>());
            // 创建or运算
            BinaryExpression binary = Expression.Or(left.Body, invocation);
            // 生成lambda表达式
            return Expression.Lambda<Func<T, bool>>(binary, left.Parameters);
        }
    }

}
