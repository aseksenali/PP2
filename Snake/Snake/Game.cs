using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace Snake
{
    [Serializable]
    public class Game
    {
        public string UserName;
        public int score;
        public int levelNumber;
        public Snake snake;
        [XmlIgnore]
        Food food = new Food();
        Wall level = new Wall();

        public Game()
        {
            snake = new Snake(Console.WindowWidth / 2, Console.WindowHeight / 2);
            score = 0;
            levelNumber = 0;
        }

        public void ChangeLevel()
        {
            levelNumber++;
        }

        public void Serialize_Record()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load("records.xml");
            XmlNode xmlNode = xml.DocumentElement;
            bool found = false;
            XmlNodeList nodeList = xmlNode.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (node.Attributes[0].Value == UserName)
                {
                    found = true;
                    if (int.Parse(node.Attributes[1].Value) < score)
                    {
                        node.Attributes[1].Value = score.ToString();
                        xml.Save("records.xml");
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (!found)
            {
                XmlNode xmlNode2 = xml.CreateNode(XmlNodeType.Element, "record", "");
                XmlAttribute attr1 = xml.CreateAttribute("username");
                XmlAttribute attr2 = xml.CreateAttribute("score");
                attr1.InnerText = UserName;
                attr2.InnerText = score.ToString();
                xmlNode2.Attributes.Append(attr1);
                xmlNode2.Attributes.Append(attr2);
                xmlNode.AppendChild(xmlNode2);
                xml.Save("records.xml");
            }
        }

        public void Serialize()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Game));
            string fname = string.Format("game.xml");
            using (FileStream fs = new FileStream(fname, FileMode.Create, FileAccess.Write))
            {
                xmlSerializer.Serialize(fs, this);
            }
        }

        public static Game Deserialize()
        {
            Game game;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Game));
            string fname = string.Format("game.xml");
            using (FileStream fs = new FileStream(fname, FileMode.Open, FileAccess.Read))
            {
                game = (Game)xmlSerializer.Deserialize(fs);
            }

            return game;
        }

        public void Login()
        {
            Console.Clear();
            Console.SetCursorPosition(Console.WindowWidth / 2 - 20, Console.WindowHeight / 2 - 1);
            Console.Write("Enter your username: ");
            while (UserName == null || UserName == "")
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 + 1, Console.WindowHeight / 2 - 1);
                UserName = Console.ReadLine();
            }
        }

        public void Play(int speed)
        {
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
            snake.DrawOnLoad();
            level.LoadLevel(levelNumber);    

            do
            {
                food.Draw();
            } while (snake.getHistory().Contains(new Tuple<int, int>(food.x_position, food.y_position)) || level.walls[food.x_position, food.y_position] == 1);

            food.Show();

            do
            {
                while (!Console.KeyAvailable && snake.isAlive())
                {
                    Console.SetCursorPosition(75, 31);
                    if (score < 10)
                        Console.Write("0");
                    Console.Write(score);


                    if (snake.X_position == food.x_position && snake.Y_position == food.y_position)
                    {
                        snake.Eat(food);
                        score++;
                        if (score == 20)
                        {
                            score = 0;
                            ChangeLevel();
                            Console.Clear();
                            level.LoadLevel(levelNumber);
                            snake = new Snake(1, 1);
                        }
                        Console.SetCursorPosition(75, 31);
                        Console.Write(score);
                        do
                        {
                            food.Draw();
                        } while (level.walls[food.x_position, food.y_position] == 1 || snake.getHistory().Contains(new Tuple<int, int>(food.x_position, food.y_position)));

                        food.Show();
                    }

                    if (level.walls[snake.X_position, snake.Y_position] == 1)
                    {
                        snake.Kill();
                        break;
                    }

                    snake.Move(snake.prev);
                    if (snake.prev == Direction.LEFT || snake.prev == Direction.RIGHT)
                    {
                        Thread.Sleep(333 - 27 * speed);
                    }
                    else if (snake.prev == Direction.TOP || snake.prev == Direction.BOTTOM)
                    {
                        Thread.Sleep(500 - 40 * speed);
                    }
                    snake.Draw();
                }
                if (snake.isAlive())
                {
                    ConsoleKeyInfo csi = Console.ReadKey(true);
                    switch (csi.Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (snake.prev != Direction.BOTTOM)
                                snake.prev = Direction.TOP;
                            break;
                        case ConsoleKey.DownArrow:
                            if (snake.prev != Direction.TOP)
                                snake.prev = Direction.BOTTOM;
                            break;
                        case ConsoleKey.LeftArrow:
                            if (snake.prev != Direction.RIGHT)
                                snake.prev = Direction.LEFT;
                            break;
                        case ConsoleKey.RightArrow:
                            if (snake.prev != Direction.LEFT)
                                snake.prev = Direction.RIGHT;
                            break;
                        case ConsoleKey.Escape:
                            Serialize();
                            snake.Kill();
                            break;
                    }
                }
            } while (snake.isAlive());
            Serialize_Record();
        }
    }
}
