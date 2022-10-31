using algLab_4.Task1;

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
    }
}
