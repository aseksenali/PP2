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
            bool run = true;
            //в history засовываем папку root.
            history.Push(
                new Layer
                {
                    Directories = root.GetDirectories().ToList(),
                    Files = root.GetFiles().ToList(),
                    SelectedItem = 0
                });
            //цикл программы
            while (run)
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
                        if (history.Count == 1)
                        {
                            break;
                        }

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
                    //на escape завершить выполнение программы
                    case ConsoleKey.Escape:
                        run = false;
                        break;
                    //на R переименовать файл или папку
                    case ConsoleKey.R:
                        string parentpath;
                        Console.BackgroundColor = ConsoleColor.Black;
                        int a = history.Peek().SelectedItem;
                        Console.Clear();
                        Console.Write("Введите новое имя файла: ");
                        string txt = Console.ReadLine();
                        if (a < history.Peek().Directories.Count)
                        {
                            parentpath = history.Peek().Directories[a].Parent.FullName;
                            Directory.Move(history.Peek().Directories[a].FullName, Path.Combine(parentpath, txt));
                            DirectoryInfo df = new DirectoryInfo(parentpath);
                            history.Peek().Directories = df.EnumerateDirectories().ToList();
                        }
                        else
                        {
                            parentpath = history.Peek().Files[a - history.Peek().Directories.Count].FullName.Remove(history.Peek().Files[a - history.Peek().Directories.Count].FullName.Length - history.Peek().Files[a - history.Peek().Directories.Count].Name.Length);
                            File.Copy(history.Peek().Files[a - history.Peek().Directories.Count].FullName, Path.Combine(parentpath, txt));
                            File.Delete(history.Peek().Files[a - history.Peek().Directories.Count].FullName);
                            DirectoryInfo df = new DirectoryInfo(parentpath);
                            history.Peek().Files = df.EnumerateFiles().ToList();
                        }

                        break;
                }
            }
        }
    }
}
