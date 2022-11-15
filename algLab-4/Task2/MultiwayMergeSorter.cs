using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algLab_4.Task2
{
    public class HeadIndexPair
    {
        public string head;
        public int i;

        public HeadIndexPair(string head, int i)
        {
            this.head = head;
            this.i = i;
        }
    }
    class MultiwayMergeSorter
    {
        string filename_in;
        string filename_out;
        int M;
        int B;
        int C;
        string sep;
        string tmpFilePrefix = "tmpfile";
        int numChunk = 0;


        public MultiwayMergeSorter(string filename_in, string filename_out, int m, int b, int c, string sep)
        {
            this.filename_in = filename_in;
            this.filename_out = filename_out;
            M = m;
            B = b;
            C = c;
            this.sep = sep;
        }

        public void doSortingMerge()
        {
            Console.WriteLine("Phase 1 is started");
            using (StreamReader sr = new StreamReader(filename_in))
            {
                int cnt = 0;
                string[] chunk = new string[M];

                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    chunk[cnt] = line;
                    cnt++;
                    if (cnt % M == 0)
                    {
                        sortAndSaveChunk(chunk, tmpFilePrefix + numChunk);
                        cnt = 0;
                        numChunk++;
                    }
                }

                if (cnt != 0)
                {
                    sortAndSaveChunk(chunk, tmpFilePrefix + numChunk);
                    numChunk++;
                }

                Console.WriteLine("Phase 1 Time elapsed (sec) = ");

                Console.WriteLine("Phase 2 started");

                //у нас есть отсортированные подсписки numChunks, которые нам нужно объединить
                //нам не нужно управлять буфером напрямую
                //Управление буфером осуществляется StreamReader

                StreamReader[] readers = new StreamReader[numChunk];

                /*
                 Мы будем использовать приоритетную очередь, чтобы эффективно реализовать конкуренцию между головками отсортированных
                подсписков (чанков). Мы создаем пару голова - индекс, которая будет использоваться в качестве типа для элементов PriorityQueue 
                Индекс - номер отсортированного подсписка, которому принадлежит голова. Зачем нам индекс? Когда голова "выйгрывает" (наименьшая среди голов)
                она мигрирует в выходной буфер. Таким образом, нам нужно вставить новый заголовок из соответствующего подсписка (чанка), а индекс сообщает 
                нам, какой это подсписок
                 */

                var heads = new PriorityQueue<HeadIndexPair, HeadIndexPair>(Comparer<HeadIndexPair>.Create((a, b) => compare(a.head, b.head)));



                for (int i = 0; i < numChunk; i++)
                {
                    using (StreamReader strRead = new StreamReader(tmpFilePrefix + i))
                    {
                        readers[i] = strRead;
                        strRead.Close();
                    }
                }


                using (StreamWriter streamOut = new StreamWriter(filename_out))
                {
                    for (int i = 0; i < numChunk; i++)
                        heads.Enqueue(new HeadIndexPair(readers[i].ReadLine(), i), new HeadIndexPair(readers[i].ReadLine(), i));

                    while (true)
                    {
                        HeadIndexPair minh = heads.Dequeue();

                        if (null == minh) break;

                        streamOut.WriteLine(minh.head);

                        if ((line = readers[minh.i].ReadLine()) != null)
                        {
                            heads.Enqueue(new HeadIndexPair(line, minh.i), new HeadIndexPair(line, minh.i));
                        }
                    }


                    for (int i = 0; i < numChunk; i++)
                    {
                        readers[i].Close();
                    }
                }

                sr.Close();
                Console.WriteLine("Sort Complete");
            }
        }
        public void sortAndSaveChunk(string[] chunk, string filename)
        {
            Console.WriteLine("sorting and saving" + filename);
            Array.Sort(chunk, (a, b) => compare(a, b));
            using (StreamWriter sw = new StreamWriter(filename))
            {
                for (int i = 0; i < chunk.Length; i++)
                {
                    if (chunk[i] != null)
                    {
                        sw.WriteLine(chunk[i]);

                    }
                }

                sw.Close();
            }

        }

        public string extractCol(string line)
        {
            string[] columns = line.Split(sep);
            return columns[C];
        }


        public int compare(string a, string b)
        {
            if (a == null && b == null)
                return 0;
            if (a == null)
                return 1;
            if (b == null)
                return -1;
            return extractCol(a).CompareTo(extractCol(b));
        }

        public static void calledMultiWay()
        {
            MultiwayMergeSorter mySort = new MultiwayMergeSorter(
                "taxpayers_3M.txt",
                "taxpayers_3M_sorted.txt",
                3000,
                8192,
                0,
                "\t");

            mySort.doSortingMerge();
        }
    }
}
