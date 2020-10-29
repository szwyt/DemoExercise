using System;
using System.Collections.Generic;
using System.Linq;

namespace 基本语法
{
    public class SelectManyDemo : IConsoleTool
    {
        public void ConsoleWriteLine()
        {
            List<Teacher> teachers = new List<Teacher>
            {
                new Teacher("a",new List<Student>{ new Student(100),new Student(30),new Student(30) }),
                new Teacher("b",new List<Student>{ new Student(100),new Student(90),new Student(60) }),
                new Teacher("c",new List<Student>{ new Student(100),new Student(90),new Student(40) }),
                new Teacher("d",new List<Student>{ new Student(100),new Student(90),new Student(60) }),
                new Teacher("e",new List<Student>{ new Student(100),new Student(90),new Student(50) }),
                new Teacher("f",new List<Student>{ new Student(100),new Student(90),new Student(60) }),
                new Teacher("g",new List<Student>{ new Student(100),new Student(90),new Student(60) })
            };

            var dogs = teachers.SelectMany(d => d.Students);
            var list3 = teachers.SelectMany(
                t => t.Students,
                (t, s) => new { t, s.Score })
                .Where(n => n.Score < 60 && n.t.Name == "a").ToList();
            Console.WriteLine(list3.FirstOrDefault().Score);
        }
    }

    internal class Student
    {
        public int Score { get; set; }

        public Student(int score)
        {
            this.Score = score;
        }
    }

    internal class Teacher
    {
        public string Name { get; set; }

        public List<Student> Students;

        public Teacher(string order, List<Student> students)
        {
            this.Name = order;

            this.Students = students;
        }
    }
}