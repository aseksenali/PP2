using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;

namespace Snake
{
    public enum Direction
    {
        RIGHT,
        LEFT,
        TOP,
        BOTTOM
    }

    public class Snake: IXmlSerializable
    {
        private int x_position;
        private int y_position;
        public int size;
        private bool alive;
        private Queue<Tuple<int, int>> history = new Queue<Tuple<int, int>>();
        public string hist;
        public Queue<Tuple<int, int>> getHistory()
        {
            return history;
        }

       

        public int X_position
        {
            get
            {
                return x_position;
            }

            set
            {
                if (value >= Console.WindowWidth)
                {
                    x_position = 0;
                }
                else if (value < 0)
                {
                    x_position = Console.WindowWidth - 1;
                } else
                {
                    x_position = value;
                }
            }
        }

        public int Y_position
        {
            get
            {
                return y_position;
            }

            set
            {
                y_position = value;

                if (y_position >= Console.WindowHeight)
                {
                    y_position = 0;
                }
                else if (y_position < 0)
                {
                    y_position = Console.WindowHeight - 2;
                }
                else
                {
                    y_position = value;
                }
            }
        }

        public bool isAlive()
        {
            return alive;
        }

        public Direction prev
        {
            get;
            set;
        }

        public Snake()
        {
            size = 1;
            alive = true;
            hist = "";
            X_position = Console.WindowWidth / 2;
            Y_position = Console.WindowHeight / 2;
            prev = Direction.RIGHT;
            string[] tmp = hist.Split(' ');
            for (int i = 0; i < tmp.Length - 1; i += 2)
            {
                int a = int.Parse(tmp[i]);
                int b = int.Parse(tmp[i + 1]);
                history.Enqueue(new Tuple<int, int>(a, b));
            }
        }

        public Snake(int startX, int startY)
        {
            size = 1;
            alive = true;
            hist = "";
            X_position = startX;
            Y_position = startY;
            prev = Direction.RIGHT;
            string[] tmp = hist.Split(' ');
            for (int i = 0; i < tmp.Length - 1; i += 2)
            {
                int a = int.Parse(tmp[i]);
                int b = int.Parse(tmp[i + 1]);
                history.Enqueue(new Tuple<int, int>(a, b));
            }
        }

        public void Move(Direction dir)
        {
            switch (dir)
            {
                case Direction.RIGHT:
                    X_position++;
                    break;
                case Direction.LEFT:
                    X_position--;
                    break;
                case Direction.TOP:
                    Y_position--;
                    break;
                case Direction.BOTTOM:
                    Y_position++;
                    break;
            }
        }

        public void Kill()
        {
            alive = false;
        }

        public void Draw()
        {
            if (history.Contains(new Tuple<int, int>(X_position, Y_position)))
            {
                Kill();
            } else
            {
                if (history.Count >= size)
                {
                    Console.SetCursorPosition(history.Peek().Item1, history.Peek().Item2);
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.Write(' ');
                    history.Dequeue();
                }
                Console.SetCursorPosition(X_position, Y_position);
                Console.Write('█');
                history.Enqueue(new Tuple<int, int>(X_position, Y_position));
            }
        }

        public void DrawOnLoad()
        {
            foreach(Tuple<int, int> point in history)
            {
                Console.SetCursorPosition(point.Item1, point.Item2);
                Console.Write("█");
            }
        }

        public void Eat(Food food)
        {
            size++;
        }

        public XmlSchema GetSchema()
        {
            return(null);
        }

        public void ReadXml(XmlReader reader)
        {
            reader.ReadStartElement("snake");
            reader.ReadStartElement("hist");
            hist = reader.ReadContentAsString();
            reader.ReadEndElement();
            string[] tmp = hist.Split(' ');
            for (int i = 0; i < tmp.Length - 1; i += 2)
            {
                int a = int.Parse(tmp[i]);
                int b = int.Parse(tmp[i + 1]);
                history.Enqueue(new Tuple<int, int>(a, b));
            }

            reader.ReadStartElement("size");
            size = reader.ReadContentAsInt();
            reader.ReadEndElement();

            reader.ReadStartElement("X_position");
            X_position = reader.ReadContentAsInt();
            reader.ReadEndElement();
            reader.ReadStartElement("Y_position");
            Y_position = reader.ReadContentAsInt();
            reader.ReadEndElement();
            reader.ReadStartElement("prev");
            string previous = reader.ReadContentAsString();
            reader.ReadEndElement();
            switch (previous)
            {
                case "LEFT":
                    prev = Direction.LEFT;
                    break;
                case "RIGHT":
                    prev = Direction.RIGHT;
                    break;
                case "TOP":
                    prev = Direction.TOP;
                    break;
                case "BOTTOM":
                    prev = Direction.BOTTOM;
                    break;
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            hist = "";
            foreach(Tuple<int, int> tuple in history)
            {
                hist += tuple.Item1 + " " + tuple.Item2 + " ";
            }
            writer.WriteStartElement("hist");
            writer.WriteString(hist);
            writer.WriteEndElement();
            writer.WriteStartElement("size");
            writer.WriteValue(size);
            writer.WriteEndElement();
            writer.WriteStartElement("X_position");
            writer.WriteValue(X_position);
            writer.WriteEndElement();
            writer.WriteStartElement("Y_position");
            writer.WriteValue(Y_position);
            writer.WriteEndElement();
            writer.WriteStartElement("prev");
            writer.WriteString(prev.ToString());
            writer.WriteEndElement();
        }
    }
}
