using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /// <summary>
    /// The class of student
    /// </summary>
    class Student
    {
        private string name; //student name
        private int id; //student ID
        private int year; //student acception year

        /// <summary>
        /// The basic constructor, which sets name, ID and year of acception
        /// </summary>
        /// <param name="name">Name of the student</param>
        /// <param name="id">ID of the student</param>
        public Student(string name, int id)
        {
            this.name = name;
            this.id = id;
            this.year = 2018;
        }
        /// <summary>
        /// Mathod for printing information about the student
        /// </summary>
        public void PrintInfo()
        {
            Console.WriteLine("ID: " + id);
            Console.WriteLine("Имя: " + name);
            Console.WriteLine("Год: " + year);
        }
        /// <summary>
        /// Method for increasing the acception year of particular student. Method writes OK, if everything was correct.
        /// </summary>
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
            Student s = new Student("Адиль", 1);//creating a student
            s.PrintInfo();
            s.YearInc();
            s.PrintInfo();
        }
    }
}
