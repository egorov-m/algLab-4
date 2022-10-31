namespace algLab_4.ConsoleMenu
{
    public static class ConsoleHelper
    {
        /// <summary> Очистка экрана </summary>
        public static void ClearScreen() 
        {
            for (int i = 0; i < Console.WindowHeight; i++)
            {
                Console.SetCursorPosition(0, i);
                int width = Console.WindowWidth;
                string str = new(' ', width);
                Console.SetCursorPosition(0, i);
                Console.Write(str);
            }
            Console.SetCursorPosition(0, 0);
        }

        /// <summary> Получить список целых чисел из консольного ввода от пользователя </summary>
        /// <param name="list"> Коллекция для записи </param>
        public static void GetListIntegerNumbersFromConsoleInput(this IList<int> list)
        {
            var input = GetInputElements();
            foreach (var item in input)
            {
                if (!int.TryParse(item, out var element)) throw new InvalidCastException("ОШИБКА: нужно ввести целочисленные элементы списка через пробел.");
                list.Add(element);
            }
        }

        /// <summary> Получить коллекцию элементов от ввода пользователя </summary>
        /// <param name="message"> Выводимое сообщение </param>
        private static IEnumerable<string> GetInputElements(string message = "Введите список целочисленных элементов (разделитель пробел): ")
        {
            System.Console.Write(message);
            var src = System.Console.ReadLine();
            return src != null ? src.Split(' ') : Array.Empty<string>();
        }
    }
}
