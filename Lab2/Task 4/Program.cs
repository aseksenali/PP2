using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Task_4
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Console.ReadLine();
            string path1 = Console.ReadLine();

            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
            FileStream fs2 = new FileStream(path1, FileMode.Create, FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);
            StreamReader sr = new StreamReader(fs);
            StreamWriter sw2 = new StreamWriter(fs2);

            sw.Write("Original");

            string text = sr.ReadToEnd();
            sw2.Write(text);
            File.Delete(path);

            sr.Close();
            sw2.Close();
            sw.Close();
            fs.Close();
            fs2.Close();
        }
    }
}
