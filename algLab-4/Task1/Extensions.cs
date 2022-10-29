using algLab_4.Logger;

namespace algLab_4.Task1
{
    public static class Extensions
    {
        /// <summary> Логгер для ведения журнала операций производимых во время сортировки </summary>
        private static readonly Logger.Logger SortLogger = Logger.Logger.GetLogger(
            "sortLogger", 
            Level.INFO, 
            new List<IMessageHandler>() { new DelayHandler(1000, new List<IMessageHandler>() { new ConsoleHandler(), new FileHandler()})});

        /// <summary> Сортировка вставками, ведётся журнал производимых операций </summary>
        /// <typeparam name="T"> Тип данных элементов коллекции </typeparam>
        /// <param name="collection"> Коллекция для сортировки </param>
        public static void InsertionSort<T>(this IList<T> collection) where T : IComparable
        {
            var count = collection.Count;

            SortLogger.Info($"Начинается сортировка (Метод: Insertion Sort) массива длинной: {count}.");
            SortLogger.Info($"Исходный массив: {collection.GetArrayForLog()}");

            for (var i = 1; i < count; ++i)
            {
                SortLogger.Info($"Проход номер: {i}.");

                SortLogger.Info($"|\tЗапоминаем элемент array[{i}] = {collection[i]}");
                var item = collection[i];
                var j = i - 1;

                while (j >= 0 && collection[j].CompareTo(item) > 0) // > заменить на < для сортировки по убыванию
                {
                    SortLogger.Info($"|\tЭлемент: array[{j}] = {collection[j]}, ставим на место элемента: array[{j + 1}] = {collection[j + 1]}.");
                    collection[j + 1] = collection[j];
                    j--;
                }

                SortLogger.Info($"|\tЗапомненный элемент в начале прохода: array[{i}] = {item}, ставим на место array[{j + 1}].");
                collection[j + 1] = item;
            }

            SortLogger.Info("Массив отсортирован!");
            SortLogger.Info($"Результат: {collection.GetArrayForLog()}");
        }
    }
}
