using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Xml;
using System.IO;

namespace Snake
{
    class Program
    {
        enum State
        {
            MENU,
            GAME,
            OPTION,
            RECORD,
            EXIT
        }
        public static CancellationTokenSource cancel = new CancellationTokenSource();
        public static CancellationToken ct = cancel.Token;

        public static void Deserialize_Record()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load("records.xml");
            XmlNode root = xml.DocumentElement;
            int counter = 0;
            List<Tuple<string, int>> list = new List<Tuple<string, int>>();
            foreach (XmlNode node in root.ChildNodes)
            {
                list.Add(new Tuple<string, int>(node.Attributes[0].Value, int.Parse(node.Attributes[1].Value)));
            }

            list.Sort((x, y) =>
            {
                return y.Item2 - x.Item2;
            });
            foreach (Tuple<string, int> tuple in list)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 - 20, Console.WindowHeight / 2 - 10 + counter);
                Console.Write(tuple.Item1);
                Console.SetCursorPosition(Console.WindowWidth / 2 + 20 - tuple.Item2.ToString().Length, Console.WindowHeight / 2 - 10 + counter);
                Console.Write(tuple.Item2);
                counter++;
            }
        }

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(80, 32);
            Console.SetBufferSize(80, 32);
            State state = State.MENU;
            Menu menu = new Menu();
            int speed = 5;
            menu.Draw();
            while (true)
            {
                switch (state)
                {
                    case State.MENU:
                        ConsoleKeyInfo cki = Console.ReadKey(true);
                        switch (cki.Key)
                        {
                            case (ConsoleKey.DownArrow):
                                menu.Update(Direction.BOTTOM);
                                break;
                            case (ConsoleKey.UpArrow):
                                menu.Update(Direction.TOP);
                                break;
                            case (ConsoleKey.Enter):
                                switch (menu.CurrentPosition)
                                {
                                    case 0:
                                        state = State.GAME;
                                        break;
                                    case 1:
                                        state = State.OPTION;
                                        break;
                                    case 2:
                                        state = State.RECORD;
                                        break;
                                    case 3:
                                        state = State.EXIT;
                                        break;
                                }
                                break;
                            default:
                                Console.BackgroundColor = ConsoleColor.Cyan;
                                Console.Write(' ');
                                break;
                        }
                        break;

                    case State.OPTION:
                        Console.BackgroundColor = ConsoleColor.Cyan;
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Clear();
                        Console.SetCursorPosition(Console.WindowWidth / 2 - 10, Console.WindowHeight / 2);
                        Console.Write("Difficulty: ");
                        for (int i = 0; i < speed; i++)
                        {
                            Console.Write("■");
                        }
                        ConsoleKeyInfo cki2 = Console.ReadKey(true);

                        switch (cki2.Key)
                        {
                            case ConsoleKey.LeftArrow:
                                if (speed > 0)
                                {
                                    Console.SetCursorPosition(Console.WindowWidth / 2 + 2 + speed, Console.WindowHeight / 2);
                                    Console.Write(" ");
                                    speed--;
                                }
                                break;
                            case ConsoleKey.RightArrow:
                                if (speed <= 10)
                                {
                                    Console.SetCursorPosition(Console.WindowWidth / 2 + 2 + speed, Console.WindowHeight / 2);
                                    Console.Write("■");
                                    speed++;
                                }
                                break;
                            case ConsoleKey.Escape:
                                state = State.MENU;
                                menu.Draw();
                                break;
                        }
                        break;
                    case State.GAME:
                        Console.BackgroundColor = ConsoleColor.Cyan;
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Game game = new Game();
                        Console.Clear();
                        Console.SetCursorPosition(Console.WindowWidth / 2 - 17, Console.WindowHeight / 2);
                        Console.Write("Do you want to load previous game?");
                        Console.SetCursorPosition(Console.WindowWidth / 2 - 20, Console.WindowHeight / 2 + 1);
                        Console.Write("Press F2 if YES, or anything else for NO.");
                        ConsoleKeyInfo cki3 = Console.ReadKey(true);

                        if (cki3.Key == ConsoleKey.F2)
                        {
                            game = Game.Deserialize();
                        }
                        else
                        {
                            game = new Game();
                            game.Login();
                        }

                        Task task = new Task(new Action(() => game.Play(speed)), cancel.Token);
                        task.RunSynchronously();
                        state = State.MENU;
                        menu.Draw();
                        break;
                    case State.RECORD:
                        Console.BackgroundColor = ConsoleColor.Cyan;
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Clear();
                        Deserialize_Record();
                        ConsoleKeyInfo rec = Console.ReadKey();
                        state = State.MENU;
                        menu.Draw();
                        break;
                    case State.EXIT:
                        return;
                }
            }
        }
    }
}
