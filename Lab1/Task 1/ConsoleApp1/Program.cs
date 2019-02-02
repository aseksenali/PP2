using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Student
    {
        private string name;
        private int id;
        private int year;
        public Student(string name, int id)
        {
            this.name = name;
            this.id = id;
            this.year = 2018;
        }

        public void PrintInfo()
        {
            Console.WriteLine("ID: " + id);
            Console.WriteLine("Имя: " + name);
            Console.WriteLine("Год: " + year);
        }

        public void YearInc()
        {
            year++;
            Console.WriteLine("OK!");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Student s = new Student("Адиль", 1);
            s.PrintInfo();
            s.YearInc();
            s.PrintInfo();
        }
    }
}
