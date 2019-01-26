using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Student
    {
        string name;
        int id;
        int year;
        public Student(string name, int id)
        {
            this.name = name;
            this.id = id;
        }

        public void printInfo()
        {
            Console.WriteLine(name + " " + id);
            year++;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

        }
    }
}
