using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace algLab_4.Task3
{
    class Lexic
    {
        public async void GetWordsFromFile()
        {
            List<string> words = new List<string>();

            string path = "words.txt";
            using (FileStream fstream = File.OpenRead(path))
            {
                byte[] buffer = new byte[fstream.Length];
                await fstream.ReadAsync(buffer, 0, buffer.Length);
                // декодируем байты в строку
                string textFromFile = Encoding.Default.GetString(buffer);

                var punctuation = textFromFile.Where(Char.IsPunctuation).Distinct().ToArray();
                var separateWord = textFromFile.Split().Select(x => x.Trim(punctuation));

                foreach (var i in separateWord)
                {
                    words.Add(i);
                    if (i.Length == 0)
                        words.Remove(i);
                }

                GetWords(words);
            }

        }


        public void GetWords(List<string> words)
        {
            foreach (var i in words)
                Console.WriteLine(i);

            //Sorts.BubbleSort.BubbleSorting(words);
            Sorts.QuickSort.QuickSorting(words, 0, words.Count - 1);

            CountingRepeatWords.CountWords(words);

        }
    }
}
