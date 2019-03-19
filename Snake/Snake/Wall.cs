using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Snake
{
    public class Wall
    {
        public int[,] walls;

        public Wall()
        {
            walls = new int[Console.WindowWidth, Console.WindowHeight];
        }

        public void LoadLevel(int level)
        {
            string name = string.Format("Levels/Level{0}.txt", level);
            int height = 0;
            using (StreamReader sr = new StreamReader(name))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    for (int c = 0; c < line.Length; c++)
                    {
                        if (line[c] == '#')
                        {
                            Console.SetCursorPosition(c, height);
                            Console.WriteLine('#');
                            walls[c, height] = 1;
                        } else
                        {
                            walls[c, height] = 0;
                        }
                    }
                    height++;
                }
            }
        }
    }
}
