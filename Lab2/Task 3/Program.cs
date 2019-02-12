using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Task_3
{
    class Program
    {
        static void Main(string[] args)
        {
            //Путь к папке
            string path = Console.ReadLine();
            DirectoryInfo file = new DirectoryInfo(path);
            PrintInfo(file, 0);
        }
        /// <summary>
        /// Функция для создания дерева папок и файлов.
        /// </summary>
        /// <param name="file">Рассматриваемый объект FileSystemInfo</param>
        /// <param name="k">Количество пробелов при переходе к следующему уровню иерархии</param>
        private static void PrintInfo(FileSystemInfo file, int k)
        {
            if (file.GetType() == typeof(DirectoryInfo))
            {
                Console.ForegroundColor = ConsoleColor.Gray;
            } else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            
            Console.WriteLine(new string(' ', k) + file.Name);
            if (file.GetType() == typeof(DirectoryInfo))
            {
                var y = file as DirectoryInfo;
                foreach (var x in y.EnumerateFileSystemInfos())
                {
                    PrintInfo(x, k + 3);
                }
            }
        }
    }
}
