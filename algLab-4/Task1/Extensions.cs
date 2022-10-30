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

        /// <summary> Быстрая сортировка, ведётся журнал производимых операций </summary>
        /// <typeparam name="T"> Тип данных элементов коллекции </typeparam>
        /// <param name="collection"> Коллекция для сортировки </param>
        /// <param name="left"> Индекс начала коллекции </param>
        /// <param name="right"> Индекс конца коллекции </param>
        public static void QuickSortHoare<T>(this IList<T> collection, int left, int right) where T : IComparable
        {
            SortLogger.Info($"Начинается сортировка (Метод: Quick Sort) массива длинной: {collection.Count}.");
            SortLogger.Info($"Исходный массив: {collection.GetArrayForLog()}");

            collection.InsideQuickSortHoare(left, right);

            SortLogger.Info("Массив отсортирован!");
            SortLogger.Info($"Результат: {collection.GetArrayForLog()}");
        }

        /// <summary> Быстрая сортировка, ведётся журнал производимых операций </summary>
        /// <typeparam name="T"> Тип данных элементов коллекции </typeparam>
        /// <param name="collection"> Коллекция для сортировки </param>
        /// <param name="left"> Индекс начала коллекции </param>
        /// <param name="right"> Индекс конца коллекции </param>
        private static void InsideQuickSortHoare<T>(this IList<T> collection, int left, int right) where T : IComparable
        {
            if (left < right)
            {
                SortLogger.Info($"Начинается поиск разделителя для под массива: array[{left}:{right}].");

                var pivotIndex = collection.PartitionHoare(left, right);
                collection.InsideQuickSortHoare(left, pivotIndex);
                collection.InsideQuickSortHoare(pivotIndex + 1, right);
            }
        }

        /// <summary> Поиск разделителя для под массива </summary>
        /// <typeparam name="T"> Тип данных элементов коллекции </typeparam>
        /// <param name="collection"> Коллекция для сортировки </param>
        /// <param name="left"> Индекс начала под массива </param>
        /// <param name="right"> Индекс конца под массива </param>
        private static int PartitionHoare<T>(this IList<T> collection, int left, int right) where T : IComparable
        {
            var pivot = collection[left]; // В качестве опорного элемента выбирается самый левый элемент
            SortLogger.Info($"|   Опорный элемент (самый левый): {pivot}.");

            var i = left - 1;
            var j = right + 1;

            while (true)
            {
                SortLogger.Info($"|   Левый указатель i: {i}.");
                do
                {
                    i++;
                    SortLogger.Info($"|   |   Левый указатель сдвигаем вправо, i++: {i}.");
                } while (pivot.CompareTo(collection[i]) > 0); // > заменить на < для сортировки по убыванию

                SortLogger.Info($"|   Левый указатель достиг цели (pivot = {pivot} <= array[{i}] = {collection[i]} - выполняется).");

                SortLogger.Info($"|   Правый указатель j: {j}.");
                do
                {
                    j--;
                    SortLogger.Info($"|   |   Правый указатель сдвигаем влево, j--: {j}.");
                } while (collection[j].CompareTo(pivot) > 0); // > заменить на < для сортировки по убыванию

                SortLogger.Info($"|   Правый указатель достиг цели (array[{j}] = {collection[j]} <= pivot = {pivot} - выполняется).");

                if (i >= j)
                {
                    SortLogger.Info($"|   Разделить для под массива array[{left}:{right}] найден: {j}.");
                    return j;
                }

                SortLogger.Info($"|   Элементы array[{i}] = {collection[i]} и array[{j}] = {collection[j]} меняем местами.");
                (collection[i], collection[j]) = (collection[j], collection[i]);
            }
        }
    }
}
