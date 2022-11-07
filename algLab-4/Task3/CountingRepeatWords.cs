using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algLab_4.Task3
{
    public static class CountingRepeatWords
    {
        public static Dictionary<string, int> CountWords(this IList<string> arraySorting)
        {
            var repeatWordCount = new Dictionary<string, int>();

            for (var i = 0; i < arraySorting.Count; i++)
            {
                if (repeatWordCount.ContainsKey(arraySorting[i]))
                {
                    var value = repeatWordCount[arraySorting[i]];
                    repeatWordCount[arraySorting[i]] = value + 1;
                }
                else
                {
                    repeatWordCount.Add(arraySorting[i], 1);
                }
            }

            return repeatWordCount;
        }
    }
}
