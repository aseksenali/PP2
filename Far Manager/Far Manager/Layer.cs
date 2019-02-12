using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Far_Manager
{
    /// <summary>
    /// Класс для хранения информации о текущей директории, и возможных действиях с ними
    /// </summary>
    class Layer
    {
        //номер текущего элемента
        private int selectedItem;
        public int SelectedItem
        {
            get
            {
                return selectedItem;
            }

            set
            {
                //зацикливание номера выбранного файла
                if (value >= Directories.Count + Files.Count)
                {
                    selectedItem = 0;
                } else if (value < 0)
                {
                    selectedItem = Directories.Count + Files.Count - 1;
                } else
                {
                    selectedItem = value;
                }
            }
        }

        /// <summary>
        /// Лист со всеми папками
        /// </summary>
        public List<DirectoryInfo> Directories
        {
            get;
            set;
        }
        /// <summary>
        /// Лист со всеми файлами
        /// </summary>
        public List<FileInfo> Files
        {
            get;
            set;
        }
        /// <summary>
        /// Метод для удаления выбранной папки или файла
        /// </summary>
        public void DeleteSelectedItem()
        {
            if (selectedItem < Directories.Count)
            {
                Directory.Delete(Directories[selectedItem].FullName, true);
                Directories.RemoveAt(selectedItem);
            } else
            {
                File.Delete(Files[selectedItem - Directories.Count].FullName);
                Files.RemoveAt(selectedItem - Directories.Count);
            }
                        
            selectedItem--;
        }

        /// <summary>
        /// Метод для рисования, и обновления состояния
        /// </summary>
        public void Draw()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            int page = selectedItem / (Console.WindowHeight - 1);
            int i = (selectedItem + page) / (Console.WindowHeight - 1) * (Console.WindowHeight - 1);
            for (; i < Math.Min(Directories.Count, (selectedItem + page) / (Console.WindowHeight - 1) * (Console.WindowHeight - 1) + Console.WindowHeight - 1); i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                if (i == SelectedItem)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.WriteLine(i + 1 + ". " + Directories[i].Name);
            }

            i = Math.Max(Directories.Count, selectedItem / (Console.WindowHeight - 1) * (Console.WindowHeight - 1));
            for (; i < Math.Min(Files.Count + Directories.Count, selectedItem / (Console.WindowHeight - 1) * (Console.WindowHeight - 1) + Console.WindowHeight - 1); i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                if (i == SelectedItem)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.WriteLine(i + 1 + ". " + Files[i - Directories.Count].Name);
            }
        }
    }
}
