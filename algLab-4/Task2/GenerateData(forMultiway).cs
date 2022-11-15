using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algLab_4.Task2
{
    internal class GenerateData_forMultiway_
    {
        public static async Task MakeFile()
        {
            int N = 3000000; //number of rows to generate

            List<string> firstnames = new List<string>();
            List<string> lastnames = new List<string>();



            string file1 = "popular-first.txt";
            string file2 = "popular-last.txt";

            string outFile = "taxpayers_3M.txt";

            firstnames = File.ReadAllLines(file1).ToList();
            lastnames = File.ReadAllLines(file2).ToList();


            using (StreamWriter writer = new StreamWriter(outFile, false))
            {
                Random random = new Random();
                for (int i = 0; i < N; i++)
                {
                    if ((i + 1) % 1000000 == 0)
                        Console.WriteLine(i + 1);

                    string SIN = randomString("0123456789", 3) + "-" +
                                 randomString("0123456789", 3) + "-" +
                                 randomString("0123456789", 3);

                    string firstname = firstnames[random.Next(firstnames.Count)];
                    string lastname = lastnames[random.Next(lastnames.Count)];

                    int salary = random.Next(10000, 1000000) * 10000 + 50000;

                    await writer.WriteLineAsync(firstname + "\t" + SIN + "\t" + lastname + "\t" + salary);
                }

                writer.Close();
            }
        }


        public static string randomString(string candidateChars, int length)
        {
            StringBuilder stringBuilder = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < length; i++)
                stringBuilder.Append(candidateChars[random.Next(candidateChars.Length)]);
            return stringBuilder.ToString();
        }
    }
}
