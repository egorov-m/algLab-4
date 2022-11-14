using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace algLab_4.Task3.Sorts
{
    static class RadixSort
    {
        public static void LSDSort(this List<string> array, int radix, int width)
        {
            for (int i = width - 1; i >= 0; i--)
                LSDSorting(array, i, radix);

        }

        public static void RadixSorting(this List<string> array, int radix, int width) => array.LSDSort(radix, width);

        public static void LSDSorting(this List<string> arr, int position, int radix)
        {
            int numItems = arr.Count;
            int[] countArray = new int[radix];

            foreach (var value in arr)
                countArray[getIndex(position, value)]++;

            for (int j = 1; j < radix; j++)
                countArray[j] += countArray[j - 1];

            string[] temp = new string[numItems];
            for(int tempIndex = numItems - 1; tempIndex >= 0; tempIndex--)
                temp[--countArray[getIndex(position, arr[tempIndex])]] = arr[tempIndex];

            for(int tempIndex = 0; tempIndex < numItems; tempIndex++)
                arr[tempIndex] = temp[tempIndex];
        }

        public static int getIndex(int position, string value)
        {
            return value[position] - 'a';
        }
    }
}
