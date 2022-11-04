using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algLab_4.Task3.Sorts
{
    class BubbleSort
    {
        public static void BubbleSorting(string[] arrForSorting)
        {
            for (int i = 0; i < arrForSorting.Length; i++)
            {
                for (int j = 0; j < arrForSorting.Length - 1; j++)
                {
                    if (arrForSorting[j].CompareTo(arrForSorting[j + 1]) > 0)
                    {
                        string temp = arrForSorting[j];
                        arrForSorting[j] = arrForSorting[j + 1];
                        arrForSorting[j + 1] = temp;
                    }
                }
            }


            for (int i = 0; i < arrForSorting.Length; i++)
                Console.WriteLine(arrForSorting[i]);
        }
    }
}
