using algLab_4.Task1;
using algLab_4.Task3;
using algLab_4.Task3.Sorts;

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
                                ExecuteSortTextFromFile(title, (x) => x.QuickSorting(), "100.txt");
                                break;

                            case '2':
                                ExecuteSortTextFromFile(title, (x) => x.QuickSorting(), "500.txt");
                                break;

                            case '3':
                                ExecuteSortTextFromFile(title, (x) => x.QuickSorting(), "1000.txt");
                                break;

                            case '4':
                                ExecuteSortTextFromFile(title, (x) => x.QuickSorting(), "3000.txt");
                                break;

                            case '5':
                                ExecuteSortTextFromFile(title, (x) => x.QuickSorting(), "5000.txt");
                                break;

                            case '6':
                                ExecuteSortTextFromFile(title, (x) => x.QuickSorting(), "3000en.txt");
                                break;

                            case '7':
                                ExecuteSortTextFromFile(title, (x) => x.QuickSorting(), "5000en.txt");
                                break;

                            case '8':
                                ExecuteSortTextFromFile(title, (x) => x.QuickSorting(), "10000en.txt");
                                break;

                            case '9':
                                ExecuteSortTextFromFile(title, (x) => x.QuickSorting(), "15000en.txt");
                                break;
                        }


                    }

                    break;
                case '2':
                    ExecuteSortText(title, (x) => x.BubbleSorting());
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
                                ExecuteSortTextFromFile(title, (x) => x.QuickSorting(), "100.txt");
                                break;

                            case '2':
                                ExecuteSortTextFromFile(title, (x) => x.QuickSorting(), "500.txt");
                                break;

                            case '3':
                                ExecuteSortTextFromFile(title, (x) => x.QuickSorting(), "1000.txt");
                                break;

                            case '4':
                                ExecuteSortTextFromFile(title, (x) => x.QuickSorting(), "3000.txt");
                                break;

                            case '5':
                                ExecuteSortTextFromFile(title, (x) => x.QuickSorting(), "5000.txt");
                                break;

                            case '6':
                                ExecuteSortTextFromFile(title, (x) => x.QuickSorting(), "3000en.txt");
                                break;

                            case '7':
                                ExecuteSortTextFromFile(title, (x) => x.QuickSorting(), "5000en.txt");
                                break;

                            case '8':
                                ExecuteSortTextFromFile(title, (x) => x.QuickSorting(), "10000en.txt");
                                break;

                            case '9':
                                ExecuteSortTextFromFile(title, (x) => x.QuickSorting(), "15000en.txt");
                                break;
                        }

                       
                    }
                    
                    break;
                case '2':
                    ExecuteSortText(title, (x) => x.QuickSorting());
                    break;
            }
        }

        /// <summary> Выполнить сортировку пузырьком для текста (с подсчётом повторений слов) </summary>
        /// <param name="title"> Заголовок </param>
        /// <param name="sortAlg"> Алгоритм сортировки </param>
        private static void ExecuteSortText(string title, Action<IList<string>> sortAlg)
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
                sortAlg(words); ;
                var dicRepWords = words.CountWords();
                dicRepWords.PrintDictionaryRepeatingWords();
            }
        }

        private static void ExecuteSortTextFromFile(string title, Action<IList<string>> sortAlg, string fileName)
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
    }
}
