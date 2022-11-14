using System.Text;

namespace algLab_4.Task3
{
    public static class Lexic
    {
        /// <summary> Получить слова из файла с текстом </summary>
        /// <param name="path"> Путь до файла </param>
        public static List<string> GetWordsFromFile(string path)
        {
            using var streamReader = new StreamReader(path, Encoding.UTF8);
            var separateWords = new List<string>();
            string? src;

            while ((src = streamReader.ReadLine()) != null)
            {
                // Разделение текста на слова
                var punctuation = src.Where(char.IsPunctuation).Distinct();
                separateWords.AddRange(src.Split()
                    .Select(x => x.Trim(punctuation.ToArray()))
                    .Where(x => x != ""));
            }

            return separateWords;
        }

        /// <summary> Получить слова из текста </summary>
        /// <param name="text"> Текст </param>
        public static List<string> GetWordsFromText(this string text)
        {
            var separateWords = new List<string>();

            // Разделение текста на слова
            var punctuation = text.Where(char.IsPunctuation).Distinct();
            separateWords.AddRange(text.Split()
                .Select(x => x.Trim(punctuation.ToArray()))
                .Where(x => x != ""));

            return separateWords;
        }
    }
}
