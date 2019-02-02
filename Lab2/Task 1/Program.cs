using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Task_1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (FileStream fs = new FileStream(@"D:\Users\Aset\Desktop\PP2\files\Task 1.txt", FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    string text = sr.ReadLine();
                    bool palindrome = true;
                    for (int i = 0; i < text.Length / 2; i++)
                    {
                        if (text[i] != text[text.Length - i - 1])
                        {
                            palindrome = false;
                        }
                    }

                    if (palindrome)
                    {
                        Console.WriteLine("Yes");
                    } else
                    {
                        Console.WriteLine("No");
                    }
                }
            }
                
        }
    }
}
