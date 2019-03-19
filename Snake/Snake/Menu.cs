using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Menu
    {
        string path = "Menu.txt";
        int currentPosition = 0;
        string[] menuItems = { "Play", "Options", "Records", "Exit" };
        int menuSize = 4;
        public int CurrentPosition
        {
            get
            {
                return currentPosition;
            }
            set
            {
                if (value >= menuSize)
                {
                    currentPosition = 0;
                } else if (value < 0)
                {
                    currentPosition = menuSize - 1;
                } else
                {
                    currentPosition = value;
                }
            }
        }


        public void Draw()
        {
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            Console.WriteLine(sr.ReadToEnd());

            for (int i = 0; i < menuSize; i++)
            {
                Console.SetCursorPosition(35, 18 + i);
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                if (i == CurrentPosition)
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                Console.Write(menuItems[i]);
            }
        }

        public void Update(Direction direction)
        {
            Console.SetCursorPosition(35, 18 + CurrentPosition);
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write(menuItems[CurrentPosition] + " ");

            if (direction == Direction.TOP)
            {
                CurrentPosition--;
            }
            else
            {
                CurrentPosition++;
            }

            Console.SetCursorPosition(35, 18 + CurrentPosition);
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(menuItems[CurrentPosition]);
        }
    }
}
