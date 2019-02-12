using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Task_2
{
    class Program
    {
        /// <summary>
        /// Функция для перевода строки текста в массив чисел
        /// </summary>
        /// <param name="text">Преобразуемый текст</param>
        /// <returns></returns>
        private static int[] F(string text)
        {
            List<int> res = new List<int>();
            string[] tmp = text.Split();
            for (int i = 0; i < tmp.Length; i++)
            {
                res.Add(int.Parse(tmp[i]));
            }
            return res.ToArray();
        }

        static void Main(string[] args)
        {
            //Массив чисел
            int[] numbers;
            //Реализация потоков чтения и записи в файлы
            using (FileStream fs = new FileStream(@"D:\Users\Aset\Desktop\PP2\files\Task_2_input.txt", FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    string text = sr.ReadLine();
                    numbers = F(text);
                }
            }
            using (FileStream fs2 = new FileStream(@"D:\Users\Aset\Desktop\PP2\files\Task_2_output.txt", FileMode.OpenOrCreate, FileAccess.Write)) {
                using (StreamWriter sw = new StreamWriter(fs2))
                {
                    //Проверка чисел на простоту, а также запись всех простых чисел в отдельный файл
                    foreach (var x in numbers)
                    {
                        bool prime = true;
                        for (int i = 2; i <= Math.Sqrt(x); i++)
                        {
                            if (x % i == 0)
                            {
                                prime = false;
                            }
                        }

                        if (prime)
                        {
                            sw.Write(x + " ");
                        }
                    }
                }
            }
        }
    }
}
