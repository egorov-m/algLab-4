using algLab_4.Task1;

namespace algLab_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var array = new [] { 15, 11, 10, 9, 1 };
            //array.InsertionSort();
            array.QuickSortHoare(0, array.Length - 1);
        }
    }
}