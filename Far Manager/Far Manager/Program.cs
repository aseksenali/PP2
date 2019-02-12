using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace Far_Manager
{
    //список с возможными состояниями системы
    enum FarMode
    {
        DIR,
        FILE
    }

    class Program
    {
        static void Main(string[] args)
        {
            /* Title - заголовок консоли
             * history - стэк вложенных папок
             * mode - текущее состояние системы
             * root - путь к начальной папке
             */
            Console.Title = "Far Manager";
            Stack<Layer> history = new Stack<Layer>();
            FarMode mode = FarMode.DIR;
            DirectoryInfo root = new DirectoryInfo(@"D:\");
            //в history засовываем папку root.
            history.Push(
                new Layer
                {
                    Directories = root.GetDirectories().ToList(),
                    Files = root.GetFiles().ToList(),
                    SelectedItem = 0
                });
            //цикл программы
            while (true)
            {
                //автоматически обновлять интерфейс после каждого нажатия клавиши
                if (mode == FarMode.DIR)
                {
                    history.Peek().Draw();
                }
                //ввод клавиши с клавиатуры и дальнейшие действия с ними
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
                switch (consoleKeyInfo.Key)
                {
                    //на delete удалить файл
                    case ConsoleKey.Delete:
                        history.Peek().DeleteSelectedItem();
                        break;
                    //на вверх листать папки наверх
                    case ConsoleKey.UpArrow:
                        history.Peek().SelectedItem--;
                        break;
                    //на вниз - листать вниз
                    case ConsoleKey.DownArrow:
                        history.Peek().SelectedItem++;
                        break;
                    //на backspace - вернуться в прошлую папку, или закрыть файл
                    case ConsoleKey.Backspace:
                        if (mode == FarMode.DIR)
                        {
                            history.Pop();
                        }
                        else
                        {
                            mode = FarMode.DIR;
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        break;
                    //на enter - открыть папку или файл
                    case ConsoleKey.Enter:
                        int x = history.Peek().SelectedItem;

                        if (x < history.Peek().Directories.Count)
                        {
                            DirectoryInfo fileSystemInfo = history.Peek().Directories[x];

                            DirectoryInfo directoryInfo = fileSystemInfo as DirectoryInfo;
                            history.Push(
                                new Layer
                                {
                                    Directories = directoryInfo.GetDirectories().ToList(),
                                    Files = directoryInfo.GetFiles().ToList(),
                                    SelectedItem = 0
                                });
                        }
                        else
                        {
                            mode = FarMode.FILE;
                            FileInfo fileInfo = history.Peek().Files[x - history.Peek().Directories.Count];
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Black;
                            using(StreamReader sr = new StreamReader(fileInfo.FullName))
                            {
                                Console.WriteLine(sr.ReadToEnd());
                            }
                        }
                        break;
                }
            }
        }
    }
}
