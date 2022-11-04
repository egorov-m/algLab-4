using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algLab_4.Task3.Sorts
{
     class QuickSort
    {
        static void Swap(string[] array, int i, int j)
        {
            string temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        public static void QuickSorting(string[] array, int start, int end)
        {
            int i = start;
            int k = end;
            if (end - start >= 1)
            {
                string pivot = array[start];
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
                QuickSorting(array, start, k - 1);
                QuickSorting(array, k + 1, end);
            }

            Print(array);
        }


        static void Print(string[] arraySort)
        {
            foreach (var i in arraySort)
                Console.WriteLine(i);
        }
    }
}
