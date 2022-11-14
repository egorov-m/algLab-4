namespace algLab_4.Task3.Sorts
{
    public static class BubbleSort
    {
        /// <summary> Сортировать коллекцию строк алгоритмом "Пузырьковая сортировка" </summary>
        /// <param name="arrForSorting"> Коллекция для сортировки </param>
        public static void BubbleSorting(this List<string> arrForSorting)
        {
            for (var i = 0; i < arrForSorting.Count; i++)
            {
                for (var j = 0; j < arrForSorting.Count - 1; j++)
                {
                    if (arrForSorting[j].CompareTo(arrForSorting[j + 1]) > 0)
                    {
                        (arrForSorting[j], arrForSorting[j + 1]) = (arrForSorting[j + 1], arrForSorting[j]);
                    }
                }
            }
        }
    }
}
