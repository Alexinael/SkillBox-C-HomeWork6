using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Windows.Forms;

namespace Homework_06
{
    class Program
    {
        /// <summary>
        /// Добавить элемент в массив
        /// </summary>
        /// <param name="array">Исходный массив</param>
        /// <param name="i">Число для добавления</param>
        /// <returns></returns>
        static int[] addToArray(ref int[] array, int i)
        {
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = i;

            return array;
        }
        #region CALC

        /// <summary>
        /// Калькуляция
        /// </summary>
        /// <param name="inputInt">Входное число</param>
        /// <param name="">arrayList</param>
        /// <returns></returns>
        static List<string> calc(int inputInt = 0)
            {
            if (inputInt < 1 || inputInt >= 1_000_000_000) return new List<string>(); // Рабочий диапазон 1-1_000_000_000

            int m = 0;
            List<string> list = new List<string>();

            for (int n = 1; n <= inputInt; n++)
            {
                bool needNewLine = true;
                string nString = n.ToString();
                for (int i = 0; i < list.Count; i++)
                {
                    string line = list[i];
                    string[] arrayString = line.Split(';');
                    bool haveMult = false;
                    // разбираем строку с разделителями. да, массив был бы проще , но в итоге надо писать строки в текстовый документ, поэтому буду сохранять 
                    // в виде CSV
                    foreach (var arString in arrayString)
                    {
                        int _arInt = int.Parse(arString);
                        if (n % _arInt == 0)
                        {
                            haveMult = true;
                            break; // переход на следующую строку
                        }
                    }
                    if (!haveMult)
                    {
                        Array.Resize(ref arrayString, arrayString.Length + 1);
                        arrayString[arrayString.Length - 1] = nString;
                        needNewLine = false;
                        list[i] = String.Join(";", arrayString);
                        break;
                    }
                }
                if (needNewLine)
                {
                    list.Add(nString);
                }

            }


            // Покажем исходную таблицу
            foreach (var line in list)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    Console.Write(line[i].ToString().Replace(';','\t'));
                }
                Console.WriteLine("\n");
            }

            return list;
        }
        #endregion

        [STAThread]
        static void Main(string[] args)
        {

            /// Домашнее задание
            ///
            /// Группа начинающих программистов решила поучаствовать в хакатоне с целью демонстрации
            /// своих навыков. 
            /// 
            /// Немного подумав они вспомнили, что не так давно на занятиях по математике
            /// они проходили тему "свойства делимости целых чисел". На этом занятии преподаватель показывал
            /// пример с использованием фактов делимости. 
            /// Пример заключался в следующем: 
            /// Написав на доске все числа от 1 до N, N = 50, преподаватель разделил числа на несколько групп
            /// так, что если одно число делится на другое, то эти числа попадают в разные руппы. 
            /// В результате этого разбиения получилось M групп, для N = 50, M = 6
            /// 
            /// N = 50
            /// Группы получились такими: 
            /// 
            /// Группа 1: 1
            /// Группа 2: 2 3 5 7 11 13 17 19 23 29 31 37 41 43 47
            /// Группа 3: 4 6 9 10 14 15 21 22 25 26 33 34 35 38 39 46 49
            /// Группа 4: 8 12 18 20 27 28 30 42 44 45 50
            /// Группа 5: 16 24 36 40
            /// Группа 6: 32 48
            /// 
            /// M = 6
            /// 
            /// ===========
            /// 
            /// N = 10
            /// Группы получились такими: 
            /// 
            /// Группа 1: 1
            /// Группа 2: 2 7 9
            /// Группа 3: 3 4 10
            /// Группа 4: 5 6 8
            /// 
            /// M = 4
            /// 
            /// Участники хакатона решили эту задачу, добавив в неё следующие возможности:
            /// 1. Программа считыват из файла (путь к которому можно указать) некоторое N, 
            ///    для которого нужно подсчитать количество групп
            ///    Программа работает с числами N не превосходящими 1 000 000 000
            ///   
            /// 2. В ней есть два режима работы:
            ///   2.1. Первый - в консоли показывается только количество групп, т е значение M
            ///   2.2. Второй - программа получает заполненные группы и записывает их в файл используя один из
            ///                 вариантов работы с файлами
            ///            
            /// 3. После выполения пунктов 2.1 или 2.2 в консоли отображается время, за которое был выдан результат 
            ///    в секундах и миллисекундах
            /// 
            /// 4. После выполнения пунта 2.2 программа предлагает заархивировать данные и если пользователь соглашается -
            /// делает это.
            /// 
            /// Попробуйте составить конкуренцию начинающим программистам и решить предложенную задачу
            /// (добавление новых возможностей не возбраняется)
            ///
            /// * При выполнении текущего задания, необходимо документировать код 
            ///   Как пометками, так и xml документацией
            ///   В обязательном порядке создать несколько собственных методов
            ///   
            int N = 50;
            Console.WriteLine($"TYPE 1: Входящее N={N}\n");
            DateTime dateTimeStart = DateTime.Now;
            int M = calc(N).Count;
            Console.WriteLine($"M = {M}\n");
            TimeSpan timeSpan = DateTime.Now - dateTimeStart;
            Console.WriteLine($"Выполнено за {timeSpan}\n");

            if (answerNo("Прочитать из файла? [Y/n]"))
            {
                Console.WriteLine("пропускаю чтение из файла!\n");
            }
            else
            {
                string file = "";
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Выбери файл с числом";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    file = openFileDialog.FileName;
                    Console.WriteLine($"TYPE 2: Берем N({N}) из {file}");

                    N = readFile(file);
                    List<string> mFromFile = calc(N);
                    M = mFromFile.Count;
                    Console.WriteLine($"M = {M}\n");
                    timeSpan = DateTime.Now - dateTimeStart;
                    Console.WriteLine($"Выполнено за {timeSpan}\n");

                    
                    if (answerNo("Записать данные в файл?[Y/n]")) 
                    { Console.WriteLine("Пропускаю запись в файл."); }
                    else
                    {
                        SaveFileDialog saveFileDialog = new SaveFileDialog();
                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {

                            writeFile(saveFileDialog.FileName, mFromFile);
                        }
                        else
                        {
                            Console.WriteLine("Вы отменили сохранение файла. Файл не будет записан!");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Вы отменили выбор файла! Пропускаю чтение из файла");
                }
            }
            Console.WriteLine("Press any key for continue");
            Console.ReadKey();



        }
        static bool answerNo(string _text)
        {
            Console.WriteLine(_text);

            var answer = Console.ReadKey();
            Console.WriteLine("\n");
            return ConsoleKey.N == answer.Key;
        }
        #region РАБОТА С ФАЙЛАМИ
        /// <summary>
        /// Чтение файла
        /// </summary>
        /// <param name="fileName">Путь к файлу</param>
        /// <returns></returns>
        static int readFile(string fileName = "")
        {
            if (fileName.Trim() == "")
            {
                Console.WriteLine("Пустое имя файла! Пропускаю!");
                return 0;
            }
            int ret = 0;

            FileInfo file = new FileInfo(fileName);
            if (!file.Exists) return 0;

            StreamReader streamReader = new StreamReader(fileName);
            string fileContent = streamReader.ReadToEnd();
            int.TryParse(fileContent, out ret);

            return ret;
        }

        /// <summary>
        /// ЗАпись файла
        /// </summary>
        /// <param name="fileName">путь к файлу</param>
        /// <param name="text">текст, который надо записать</param>
        static void writeFile(string fileName , List<string> _list)
        {
            if (fileName.Trim() == "") 
            {
                Console.WriteLine("Пустое имя файла! Пропускаю!");
                return; 
            }
            FileInfo file = new FileInfo(fileName);
            try
            {
                StreamWriter streamWriter = new StreamWriter(fileName);
                foreach (string text in _list)
                {
                    if (text.Trim() == "") continue;
                    streamWriter.WriteLine(text);
                    //streamWriter.WriteLine(text.Replace(';','\t'); // как вариант
                }
                
                streamWriter.Close();

            }
            catch (Exception error)
            {

                Console.WriteLine($"ERROR WITH WRITE TO FILE : {error}");
            }
        }
        #endregion

    }
}
