namespace algLab_4.Task3.Sorts
{
    public static class QuickSort
    {
         /// <summary> Менять элементы местами </summary>
         /// <param name="array"> Коллекция элементов </param>
         /// <param name="i"> Индекс первого элемента </param>
         /// <param name="j"> Индекс второго элемента </param>
        private static void Swap(this IList<string> array, int i, int j)
        {
            (array[i], array[j]) = (array[j], array[i]);
        }

        /// <summary> Сортировать коллекцию строк алгоритмом "Быстрая сортировка" </summary>
        /// <param name="array"> Коллекция для сортировки </param>
        public static void QuickSorting(this IList<string> array) => array.QuickSorting(0, array.Count - 1);

        /// <summary> Сортировать коллекцию строк алгоритмом "Быстрая сортировка" </summary>
        /// <param name="array"> Коллекция для сортировки </param>
        /// <param name="start"> Индекс начала подмассива </param>
        /// <param name="end"> Индекс конца подмассива </param>
        public static void QuickSorting(this IList<string> array, int start, int end)
        {
            var i = start;
            var k = end;
            if (end - start >= 1)
            {
                var pivot = array[start];
                while (k > i)
                {
                    while (array[i].CompareTo(pivot) <= 0 && i <= end && k > i)
                        i++;
                    while (array[k].CompareTo(pivot) > 0 && k >= start && k >= i)
                        k--;
                    if (k > i)
                        Swap(array, i, k);
                }

                Swap(array, start, k);
                array.QuickSorting(start, k - 1);
                array.QuickSorting(k + 1, end);
            }
        }
    }
}
