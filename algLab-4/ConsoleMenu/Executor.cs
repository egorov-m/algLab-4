using algLab_4.Task1;
using algLab_4.Task3;
using algLab_4.Task3.Sorts;
using System.Diagnostics;
using algLab_4.Task2;

namespace algLab_4.ConsoleMenu
{
    /// <summary> Исполнитель </summary>
    public class Executor
    {
        /// <summary> Выполнить возврат </summary>
        /// <param name="basePage"> Базовая страница </param>
        /// <param name="mainPage"> Главная страница </param>
        public static void ExecuteReturn(Action basePage, Action mainPage)
        {
            Console.WriteLine("\nНажмите Enter, чтобы вернуться к выбору пункта.\nНажмите Escape, чтобы вернуться в главное меню.");
            ConsoleKey key = default;
            while (key is not (ConsoleKey.Enter or ConsoleKey.Escape))
            {
                key = Console.ReadKey(true).Key;
            }
            Console.Clear();
            switch (key)
            {
                case ConsoleKey.Enter:
                    basePage();
                    break;
                case ConsoleKey.Escape:
                    mainPage();
                    break;
            }
        }

        /// <summary> Выполнить сортировку вставками </summary>
        public static void ExecuteInsertionSort()
        {
            ConsoleHelper.ClearScreen();
            Console.CursorVisible = true;
            Console.WriteLine(
@"  _____                     _      _____            _   
 |_   _|                   | |    / ____|          | |  
   | |  _ __  ___  ___ _ __| |_  | (___   ___  _ __| |_ 
   | | | '_ \/ __|/ _ \ '__| __|  \___ \ / _ \| '__| __|
  _| |_| | | \__ \  __/ |  | |_   ____) | (_) | |  | |_ 
 |_____|_| |_|___/\___|_|   \__| |_____/ \___/|_|   \__|
");

            var list = new List<int>();
            try
            {
                list.GetListIntegerNumbersFromConsoleInput();
                Console.WriteLine();
                list.InsertionSort();
            }
            catch (InvalidCastException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary> Выполнить сортировку вставками </summary>
        public static void ExecuteQuickSort()
        {
            ConsoleHelper.ClearScreen();
            Console.CursorVisible = true;
            Console.WriteLine(
@"   ____        _      _       _____            _   
  / __ \      (_)    | |     / ____|          | |  
 | |  | |_   _ _  ___| | __ | (___   ___  _ __| |_ 
 | |  | | | | | |/ __| |/ /  \___ \ / _ \| '__| __|
 | |__| | |_| | | (__|   <   ____) | (_) | |  | |_ 
  \___\_\\__,_|_|\___|_|\_\ |_____/ \___/|_|   \__|
");

            var list = new List<int>();
            try
            {
                list.GetListIntegerNumbersFromConsoleInput();
                Console.WriteLine();
                list.QuickSortHoare();
            }
            catch (InvalidCastException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary> Выполнить сортировку пузырьком для текста (с подсчётом повторений слов) </summary>
        public static void ExecuteBubbleSortText()
        {
            var title =
@"  ____        _     _     _         _____            _     _______        _   
 |  _ \      | |   | |   | |       / ____|          | |   |__   __|      | |  
 | |_) |_   _| |__ | |__ | | ___  | (___   ___  _ __| |_     | | _____  _| |_ 
 |  _ <| | | | '_ \| '_ \| |/ _ \  \___ \ / _ \| '__| __|    | |/ _ \ \/ / __|
 | |_) | |_| | |_) | |_) | |  __/  ____) | (_) | |  | |_     | |  __/>  <| |_ 
 |____/ \__,_|_.__/|_.__/|_|\___| |_____/ \___/|_|   \__|    |_|\___/_/\_\\__|

";
            Console.WriteLine();
            Console.WriteLine("1. Считать данные из файла");
            Console.WriteLine("2. Ввести текст");

            var input = Console.ReadKey();
            switch (input.KeyChar)
            {
                case '1':
                    {
                        Console.WriteLine();
                        Console.WriteLine("С какого файла считать данные?");
                        Console.WriteLine();
                        Console.WriteLine("1. 100txt - rus");
                        Console.WriteLine("2. 500txt - rus");
                        Console.WriteLine("3. 1000txt - rus");
                        Console.WriteLine("4. 3000txt - rus");
                        Console.WriteLine("5. 5000txt - rus");
                        Console.WriteLine("6. 3000txt - eng");
                        Console.WriteLine("7. 5000txt - eng");
                        Console.WriteLine("8. 10000txt - eng");
                        Console.WriteLine("9. 15000txt - eng");

                        var inputFile = Console.ReadKey();


                        switch (inputFile.KeyChar)
                        {
                            case '1':
                                {
                                    Stopwatch stopwatch1 = new Stopwatch();
                                    stopwatch1.Start();
                                    ExecuteSortTextFromFile(title, (x) => x.BubbleSorting(), "100.txt");
                                    Console.WriteLine(Convert.ToString(stopwatch1.Elapsed.TotalMilliseconds) + "ms");
                                    stopwatch1.Stop();
                                    break;
                                }


                            case '2':
                                {
                                    Stopwatch stopwatch2 = new Stopwatch();
                                    stopwatch2.Start();
                                    ExecuteSortTextFromFile(title, (x) => x.BubbleSorting(), "500.txt");
                                    Console.WriteLine(Convert.ToString(stopwatch2.Elapsed.TotalMilliseconds) + "ms");
                                    stopwatch2.Stop();
                                    break;
                                }


                            case '3':
                                {
                                    Stopwatch stopwatch3 = new Stopwatch();
                                    stopwatch3.Start();
                                    ExecuteSortTextFromFile(title, (x) => x.BubbleSorting(), "1000.txt");
                                    Console.WriteLine(Convert.ToString(stopwatch3.Elapsed.TotalMilliseconds) + "ms");
                                    stopwatch3.Stop();
                                    break;
                                }


                            case '4':
                                {
                                    Stopwatch stopwatch4 = new Stopwatch();
                                    stopwatch4.Start();
                                    ExecuteSortTextFromFile(title, (x) => x.BubbleSorting(), "3000.txt");
                                    Console.WriteLine(Convert.ToString(stopwatch4.Elapsed.TotalMilliseconds) + "ms");
                                    stopwatch4.Stop();
                                    break;
                                }


                            case '5':
                                {
                                    Stopwatch stopwatch5 = new Stopwatch();
                                    stopwatch5.Start();
                                    ExecuteSortTextFromFile(title, (x) => x.BubbleSorting(), "5000.txt");
                                    Console.WriteLine(Convert.ToString(stopwatch5.Elapsed.TotalMilliseconds) + "ms");
                                    stopwatch5.Stop();
                                    break;
                                }


                            case '6':
                                {
                                    Stopwatch stopwatch6 = new Stopwatch();
                                    stopwatch6.Start();
                                    ExecuteSortTextFromFile(title, (x) => x.BubbleSorting(), "3000en.txt");
                                    Console.WriteLine(Convert.ToString(stopwatch6.Elapsed.TotalMilliseconds) + "ms");
                                    stopwatch6.Stop();
                                    break;
                                }


                            case '7':
                                {
                                    Stopwatch stopwatch7 = new Stopwatch();
                                    stopwatch7.Start();
                                    ExecuteSortTextFromFile(title, (x) => x.BubbleSorting(), "5000en.txt");
                                    Console.WriteLine(Convert.ToString(stopwatch7.Elapsed.TotalMilliseconds) + "ms");
                                    stopwatch7.Stop();
                                    break;
                                }


                            case '8':
                                {
                                    Stopwatch stopwatch8 = new Stopwatch();
                                    stopwatch8.Start();
                                    ExecuteSortTextFromFile(title, (x) => x.BubbleSorting(), "10000en.txt");
                                    Console.WriteLine(Convert.ToString(stopwatch8.Elapsed.TotalMilliseconds) + "ms");
                                    stopwatch8.Stop();
                                    break;
                                }


                            case '9':
                                {
                                    Stopwatch stopwatch9 = new Stopwatch();
                                    stopwatch9.Start();
                                    ExecuteSortTextFromFile(title, (x) => x.BubbleSorting(), "15000en.txt");
                                    Console.WriteLine(Convert.ToString(stopwatch9.Elapsed.TotalMilliseconds) + "ms");
                                    stopwatch9.Stop();
                                    break;
                                }
                        }


                    }

                    break;
                case '2':
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    ExecuteSortText(title, (x) => x.BubbleSorting());
                    Console.WriteLine(Convert.ToString(stopwatch.Elapsed.TotalMilliseconds) + "ms");
                    stopwatch.Stop();
                    break;
            }

            
        }

        /// <summary> Выполнить быструю сортировку для текста (с подсчётом повторений слов) </summary>
        public static void ExecuteQuickSortText()
        {
            var title =
@"   ____        _      _       _____            _     _______        _   
  / __ \      (_)    | |     / ____|          | |   |__   __|      | |  
 | |  | |_   _ _  ___| | __ | (___   ___  _ __| |_     | | _____  _| |_ 
 | |  | | | | | |/ __| |/ /  \___ \ / _ \| '__| __|    | |/ _ \ \/ / __|
 | |__| | |_| | | (__|   <   ____) | (_) | |  | |_     | |  __/>  <| |_ 
  \___\_\\__,_|_|\___|_|\_\ |_____/ \___/|_|   \__|    |_|\___/_/\_\\__|
";

            Console.WriteLine();
            Console.WriteLine("1. Считать данные из файла");
            Console.WriteLine("2. Ввести текст");

            var input = Console.ReadKey();
            switch (input.KeyChar)
            {
                case '1':
                    {
                        Console.WriteLine();
                        Console.WriteLine("С какого файла считать данные?");
                        Console.WriteLine();
                        Console.WriteLine("1. 100txt - rus");
                        Console.WriteLine("2. 500txt - rus");
                        Console.WriteLine("3. 1000txt - rus");
                        Console.WriteLine("4. 3000txt - rus");
                        Console.WriteLine("5. 5000txt - rus");
                        Console.WriteLine("6. 3000txt - eng");
                        Console.WriteLine("7. 5000txt - eng");
                        Console.WriteLine("8. 10000txt - eng");
                        Console.WriteLine("9. 15000txt - eng");

                        
                        var inputFile = Console.ReadKey();
                        switch (inputFile.KeyChar)
                        {
                            case '1':
                                {
                                    Stopwatch stopwatch1 = new Stopwatch();
                                    stopwatch1.Start();
                                    ExecuteSortTextFromFile(title, (x) => x.QuickSorting(), "100.txt");
                                    Console.WriteLine(Convert.ToString(stopwatch1.Elapsed.TotalMilliseconds) + "ms");
                                    stopwatch1.Stop();
                                    break;
                                }
                                

                            case '2':
                                {
                                    Stopwatch stopwatch2 = new Stopwatch();
                                    stopwatch2.Start();
                                    ExecuteSortTextFromFile(title, (x) => x.QuickSorting(), "500.txt");
                                    Console.WriteLine(Convert.ToString(stopwatch2.Elapsed.TotalMilliseconds) + "ms");
                                    stopwatch2.Stop();
                                    break;
                                }
                                

                            case '3':
                                {
                                    Stopwatch stopwatch3 = new Stopwatch();
                                    stopwatch3.Start();
                                    ExecuteSortTextFromFile(title, (x) => x.QuickSorting(), "1000.txt");
                                    Console.WriteLine(Convert.ToString(stopwatch3.Elapsed.TotalMilliseconds) + "ms");
                                    stopwatch3.Stop();
                                    break;
                                }
                               

                            case '4':
                                {
                                    Stopwatch stopwatch4 = new Stopwatch();
                                    stopwatch4.Start();
                                    ExecuteSortTextFromFile(title, (x) => x.QuickSorting(), "3000.txt");
                                    Console.WriteLine(Convert.ToString(stopwatch4.Elapsed.TotalMilliseconds) + "ms");
                                    stopwatch4.Stop();
                                    break;
                                }
                                

                            case '5':
                                {
                                    Stopwatch stopwatch5 = new Stopwatch();
                                    stopwatch5.Start();
                                    ExecuteSortTextFromFile(title, (x) => x.QuickSorting(), "5000.txt");
                                    Console.WriteLine(Convert.ToString(stopwatch5.Elapsed.TotalMilliseconds) + "ms");
                                    stopwatch5.Stop();
                                    break;
                                }
                               

                            case '6':
                                {
                                    Stopwatch stopwatch6 = new Stopwatch();
                                    stopwatch6.Start();
                                    ExecuteSortTextFromFile(title, (x) => x.QuickSorting(), "3000en.txt");
                                    Console.WriteLine(Convert.ToString(stopwatch6.Elapsed.TotalMilliseconds) + "ms");
                                    stopwatch6.Stop();
                                    break;
                                }
                                

                            case '7':
                                {
                                    Stopwatch stopwatch7 = new Stopwatch();
                                    stopwatch7.Start();
                                    ExecuteSortTextFromFile(title, (x) => x.QuickSorting(), "5000en.txt");
                                    Console.WriteLine(Convert.ToString(stopwatch7.Elapsed.TotalMilliseconds) + "ms");
                                    stopwatch7.Stop();
                                    break;
                                }
                               

                            case '8':
                                {
                                    Stopwatch stopwatch8 = new Stopwatch();
                                    stopwatch8.Start();
                                    ExecuteSortTextFromFile(title, (x) => x.QuickSorting(), "10000en.txt");
                                    Console.WriteLine(Convert.ToString(stopwatch8.Elapsed.TotalMilliseconds) + "ms");
                                    stopwatch8.Stop();
                                    break;
                                }
                               

                            case '9':
                                {
                                    Stopwatch stopwatch9 = new Stopwatch();
                                    stopwatch9.Start();
                                    ExecuteSortTextFromFile(title, (x) => x.QuickSorting(), "15000en.txt");
                                    Console.WriteLine(Convert.ToString(stopwatch9.Elapsed.TotalMilliseconds) + "ms");
                                    stopwatch9.Stop();
                                    break;
                                }
                                
                        }

                       
                    }
                    
                    break;
                case '2':
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    ExecuteSortText(title, (x) => x.QuickSorting());
                    Console.WriteLine(Convert.ToString(stopwatch.Elapsed.TotalMilliseconds) + "ms");
                    stopwatch.Stop();
                    break;
            }
        }


        public static void ExecuteRadixSortText()
        {
            var title =
@"
  _____           _ _       _____            _     _______        _   
 |  __ \         | (_)     / ____|          | |   |__   __|      | |  
 | |__) |__ _  __| |___  _| (___   ___  _ __| |_     | | _____  _| |_ 
 |  _  // _` |/ _` | \ \/ /\___ \ / _ \| '__| __|    | |/ _ \ \/ / __|
 | | \ \ (_| | (_| | |>  < ____) | (_) | |  | |_     | |  __/>  <| |_ 
 |_|  \_\__,_|\__,_|_/_/\_\_____/ \___/|_|   \__|    |_|\___/_/\_\\__|
";

            Console.WriteLine();
            Console.WriteLine("1. Считать данные из файла");
            Console.WriteLine("2. Ввести текст");

            var input = Console.ReadKey();
            switch (input.KeyChar)
            {
                case '1':
                    {
                        Console.WriteLine();
                        Console.WriteLine("С какого файла считать данные?");
                        Console.WriteLine();
                        Console.WriteLine("1. radix(3) 500");
                        Console.WriteLine("2. radix(3) 1000");
                        Console.WriteLine("3. radix(3) 5000");
                        Console.WriteLine("4. radix(3) 10000");
                        Console.WriteLine("5. radix(3) 15000");
                        Console.WriteLine("6. radix(10) 500");
                        Console.WriteLine("7. radix(10) 1000");
                        Console.WriteLine("8. radix(10) 5000");
                        Console.WriteLine("9. radix(10) 10000");
                        Console.WriteLine("0. radix(10) 15000");

                        var inputFile = Console.ReadKey();
                        switch (inputFile.KeyChar)
                        {
                            case '1':
                                {
                                    Stopwatch stopwatch1 = new Stopwatch();
                                    stopwatch1.Start();
                                    ExecuteSortTextFromFile(title, (x) => x.RadixSorting(26, 3), "radix(3)500.txt");
                                    Console.WriteLine(Convert.ToString(stopwatch1.Elapsed.TotalMilliseconds) + "ms");
                                    stopwatch1.Stop();
                                    break;
                                }


                            case '2':
                                {
                                    Stopwatch stopwatch2 = new Stopwatch();
                                    stopwatch2.Start();
                                    ExecuteSortTextFromFile(title, (x) => x.RadixSorting(26, 3), "radix(3)1000.txt");
                                    Console.WriteLine(Convert.ToString(stopwatch2.Elapsed.TotalMilliseconds) + "ms");
                                    stopwatch2.Stop();
                                    break;
                                }


                            case '3':
                                {
                                    Stopwatch stopwatch3 = new Stopwatch();
                                    stopwatch3.Start();
                                    ExecuteSortTextFromFile(title, (x) => x.RadixSorting(26, 3), "radix(3)5000.txt");
                                    Console.WriteLine(Convert.ToString(stopwatch3.Elapsed.TotalMilliseconds) + "ms");
                                    stopwatch3.Stop();
                                    break;
                                }


                            case '4':
                                {
                                    Stopwatch stopwatch4 = new Stopwatch();
                                    stopwatch4.Start();
                                    ExecuteSortTextFromFile(title, (x) => x.RadixSorting(26, 3), "radix(3)10000.txt");
                                    Console.WriteLine(Convert.ToString(stopwatch4.Elapsed.TotalMilliseconds) + "ms");
                                    stopwatch4.Stop();
                                    break;
                                }


                            case '5':
                                {
                                    Stopwatch stopwatch5 = new Stopwatch();
                                    stopwatch5.Start();
                                    ExecuteSortTextFromFile(title, (x) => x.RadixSorting(26, 3), "radix(3)15000.txt");
                                    Console.WriteLine(Convert.ToString(stopwatch5.Elapsed.TotalMilliseconds) + "ms");
                                    stopwatch5.Stop();
                                    break;
                                }


                            case '6':
                                {
                                    Stopwatch stopwatch6 = new Stopwatch();
                                    stopwatch6.Start();
                                    ExecuteSortTextFromFile(title, (x) => x.RadixSorting(32, 10), "radix(10)500.txt");
                                    Console.WriteLine(Convert.ToString(stopwatch6.Elapsed.TotalMilliseconds) + "ms");
                                    stopwatch6.Stop();
                                    break;
                                }


                            case '7':
                                {
                                    Stopwatch stopwatch7 = new Stopwatch();
                                    stopwatch7.Start();
                                    ExecuteSortTextFromFile(title, (x) => x.RadixSorting(26, 10), "radix(10)1000.txt");
                                    Console.WriteLine(Convert.ToString(stopwatch7.Elapsed.TotalMilliseconds) + "ms");
                                    stopwatch7.Stop();
                                    break;
                                }


                            case '8':
                                {
                                    Stopwatch stopwatch8 = new Stopwatch();
                                    stopwatch8.Start();
                                    ExecuteSortTextFromFile(title, (x) => x.RadixSorting(26, 10), "radix(10)5000.txt");
                                    Console.WriteLine(Convert.ToString(stopwatch8.Elapsed.TotalMilliseconds) + "ms");
                                    stopwatch8.Stop();
                                    break;
                                }

                            case '9':
                                {
                                    Stopwatch stopwatch8 = new Stopwatch();
                                    stopwatch8.Start();
                                    ExecuteSortTextFromFile(title, (x) => x.RadixSorting(26, 10), "radix(10)10000.txt");
                                    Console.WriteLine(Convert.ToString(stopwatch8.Elapsed.TotalMilliseconds) + "ms");
                                    stopwatch8.Stop();
                                    break;
                                }
                            case '0':
                                {
                                    Stopwatch stopwatch8 = new Stopwatch();
                                    stopwatch8.Start();
                                    ExecuteSortTextFromFile(title, (x) => x.RadixSorting(26, 10), "radix(10)15000.txt");
                                    Console.WriteLine(Convert.ToString(stopwatch8.Elapsed.TotalMilliseconds) + "ms");
                                    stopwatch8.Stop();
                                    break;
                                }
                        }


                    }

                    break;
                case '2':
                    ExecuteSortText(title, (x) => x.BubbleSorting());
                    break;
                //case '2':
                //    Stopwatch stopwatch = new Stopwatch();
                //    stopwatch.Start();
                //    ExecuteSortText(title, (x) => x.QuickSorting());
                //    Console.WriteLine(Convert.ToString(stopwatch.Elapsed.TotalMilliseconds) + "ms");
                //    stopwatch.Stop();
                //    break;
            }
        }


        /// <summary> Выполнить сортировку пузырьком для текста (с подсчётом повторений слов) </summary>
        /// <param name="title"> Заголовок </param>
        /// <param name="sortAlg"> Алгоритм сортировки </param>
        private static void ExecuteSortText(string title, Action<List<string>> sortAlg)
        {
            ConsoleHelper.ClearScreen();
            Console.CursorVisible = true;
            Console.WriteLine(title);
            Console.WriteLine("Введите текст для сортировки слов и подсчёта количества повторений слов:");
            Console.Write("Ваш текст (латиница): ");
            var text = Console.ReadLine();
            if (text != null)
            {
                var words = text.GetWordsFromText();
                Console.WriteLine("\nРезультат (слово — число повторений):");
                sortAlg(words);
                var dicRepWords = words.CountWords();
                dicRepWords.PrintDictionaryRepeatingWords();
            }
        }

        private static void ExecuteSortTextFromFile(string title, Action<List<string>> sortAlg, string fileName)
        {
            ConsoleHelper.ClearScreen();
            Console.CursorVisible = true;
            Console.WriteLine(title);

            var words = Lexic.GetWordsFromFile(fileName);

            Console.WriteLine("\nРезультат (слово - число повторений):");
            sortAlg(words);
            var dicRepWords = words.CountWords();
            dicRepWords.PrintDictionaryRepeatingWords();
        }

        public static void CreateOrSaveForCVS(double middletimes)
        {
            string writePath = @"TimeSorting.csv";
            using StreamWriter sw = new StreamWriter(writePath, true);
            sw.WriteLine(Convert.ToString(middletimes) + ";");
        }

        public static void ExecuteExternalDirectMergeSort()
        {
            ConsoleHelper.ClearScreen();
            Console.CursorVisible = true;
            Console.WriteLine(
@"  ______      _                        _   _____  _               _     __  __                        _____            _   
 |  ____|    | |                      | | |  __ \(_)             | |   |  \/  |                      / ____|          | |  
 | |__  __  _| |_ ___ _ __ _ __   __ _| | | |  | |_ _ __ ___  ___| |_  | \  / | ___ _ __ __ _  ___  | (___   ___  _ __| |_ 
 |  __| \ \/ / __/ _ \ '__| '_ \ / _` | | | |  | | | '__/ _ \/ __| __| | |\/| |/ _ \ '__/ _` |/ _ \  \___ \ / _ \| '__| __|
 | |____ >  <| ||  __/ |  | | | | (_| | | | |__| | | | |  __/ (__| |_  | |  | |  __/ | | (_| |  __/  ____) | (_) | |  | |_ 
 |______/_/\_\\__\___|_|  |_| |_|\__,_|_| |_____/|_|_|  \___|\___|\__| |_|  |_|\___|_|  \__, |\___| |_____/ \___/|_|   \__|
                                                                                         __/ |                             
                                                                                        |___/                              

В качестве примера будет использоваться небольшая таблица с информацией о странах:
Формат: Название;Континент;Столица;Площадь;Численность населения
Сортировка по возрастанию численности населения.
");
            Console.WriteLine("Введите индекс колонки для сортировки: ");

            var key = GetSortKey();
            if (key != null)
            {
                var sorter = new DirectMergeSorter();
                sorter.Sort();
            }
        }
        

        public static void ExecuteExternalNaturalMergeSort()
        {
            ConsoleHelper.ClearScreen();
            Console.CursorVisible = true;
            Console.WriteLine(
@"  ______      _                        _   _   _       _                   _   __  __                        _____            _   
 |  ____|    | |                      | | | \ | |     | |                 | | |  \/  |                      / ____|          | |  
 | |__  __  _| |_ ___ _ __ _ __   __ _| | |  \| | __ _| |_ _   _ _ __ __ _| | | \  / | ___ _ __ __ _  ___  | (___   ___  _ __| |_ 
 |  __| \ \/ / __/ _ \ '__| '_ \ / _` | | | . ` |/ _` | __| | | | '__/ _` | | | |\/| |/ _ \ '__/ _` |/ _ \  \___ \ / _ \| '__| __|
 | |____ >  <| ||  __/ |  | | | | (_| | | | |\  | (_| | |_| |_| | | | (_| | | | |  | |  __/ | | (_| |  __/  ____) | (_) | |  | |_ 
 |______/_/\_\\__\___|_|  |_| |_|\__,_|_| |_| \_|\__,_|\__|\__,_|_|  \__,_|_| |_|  |_|\___|_|  \__, |\___| |_____/ \___/|_|   \__|
                                                                                                __/ |                             
                                                                                               |___/                              

В качестве примера будет использоваться небольшая таблица с информацией о странах:
Формат: Название (0);Континент (1);Столица (2);Площадь (3);Численность населения (4)
Сортировка по возрастанию.
Для данной таблицы доступны SortKey: 3 - площадь, 4 - численность населения; другие значения будут отклонены.
");
            Console.WriteLine("Введите индекс колонки для сортировки: ");

            var key = GetSortKey();
            if (key != null)
            {
                var sorter = new NaturalMergeSorter(key ?? 4);
                sorter.Sort();
            }
            
        }

        /// <summary> Запрашивать ключ сортировки от пользователя </summary>
        private static int? GetSortKey()
        {
            Console.WriteLine("Введите индекс колонки для сортировки: ");
            try
            {
                var key = int.Parse(Console.ReadLine() ?? "0");
                if (key != 3 && key != 4)
                {
                    Console.WriteLine("ОШИБКА! Недопустимое значение, колонка для сортировки недоступна.");
                    return null;
                }

                return key;
            }
            catch (FormatException)
            {
                Console.WriteLine("ОШИБКА! Нужно ввести целое неотрицательное число.");
                return null;
            }
        }
    }
}
