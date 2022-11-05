using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algLab_4.Task3
{
    class CountingRepeatWords
    {
        public static void CountWords(IList<string> arraySorting)
        {
            Dictionary<string, int> repeatWordCount = new Dictionary<string, int>();

            for (int i = 0; i < arraySorting.Count; i++)
            {
                if (repeatWordCount.ContainsKey(arraySorting[i]))
                {
                    int value = repeatWordCount[arraySorting[i]];
                    repeatWordCount[arraySorting[i]] = value + 1;
                }

                else
                {
                    repeatWordCount.Add(arraySorting[i], 1);
                }
            }

            OutputResult(repeatWordCount);
        }

        public static void OutputResult(Dictionary<string, int> resultOutput)
        {
            Console.WriteLine();
            Console.WriteLine("-----------------");
            Console.WriteLine("Повторяющиеся слова");
            foreach (KeyValuePair<string, int> pair in resultOutput)
                Console.WriteLine(pair.Key + " : повторяется " + pair.Value + " раз");

            Console.ReadKey();
        }
    }
}
