using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Food
    {
        public int x_position;
        public int y_position;
        Random rand = new Random((int)DateTime.Now.Ticks);

        public void Draw()
        {
            x_position = rand.Next(Console.WindowWidth - 1);
            y_position = rand.Next(Console.WindowHeight - 2);
        }

        public void Show()
        {
            Console.SetCursorPosition(x_position, y_position);
            Console.Write('@');
        }
    }
}
