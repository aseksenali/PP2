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
            //Папки откуда и куда копировать
            string path = Console.ReadLine();
            string path1 = Console.ReadLine();
            //Все необходимые потоки чтения и записи файлов
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
            FileStream fs2 = new FileStream(path1, FileMode.Create, FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);
            StreamReader sr = new StreamReader(fs);
            StreamWriter sw2 = new StreamWriter(fs2);

            sw.Write("Original");
            //Чтения текста из первого файла
            string text = sr.ReadToEnd();
            //Запись текста в другой файл
            sw2.Write(text);
            //Удаление исходного файла
            File.Delete(path);
            //Закрытие всех потоков
            sr.Close();
            sw2.Close();
            sw.Close();
            fs.Close();
            fs2.Close();
        }
    }
}
