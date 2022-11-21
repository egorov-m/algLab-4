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

    class doSortingMerge
    {
        private static Logger.Logger SortLogger = Logger.Logger.GetLogger(0);

        string filename_in;
        string filename_out;
        int M; //размер фрагмента для сортировки по кол-ву строк, а также размер отсортированного списка
        int B; //размер буфера в символах (1 символ = 2 байта), если В == 8192 символа, то это 16К байт
        int C; //сортировать столбец
        string sep; //разделитель столбцов
        string tmpFilePrefix = "tmpfile";
        int numChunk = 0;


        public doSortingMerge(string filename_in, string filename_out, int m, int b, int c, string sep)
        {
            this.filename_in = filename_in;
            this.filename_out = filename_out;
            M = m;
            B = b;
            C = c;
            this.sep = sep;
        }

        public void doSort()
        {
            SortLogger.Info("Первая стадия:");
            using (StreamReader sr = new StreamReader(filename_in))
            {
                int cnt = 0;
                string[] chunk = new string[M];

                string line;
                SortLogger.Info("Рассфасовываем по файликам txt!");
                while ((line = sr.ReadLine()) != null)
                {
                    chunk[cnt] = line;
                    cnt++;
                    SortLogger.Info($"{cnt} файл - пошел!");
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

                StreamReader[] readers = new StreamReader[numChunk];

                var heads = new PriorityQueue<HeadIndexPair, HeadIndexPair>(Comparer<HeadIndexPair>.Create((a, b) => compare(a.head, b.head)));



                for (int i = 0; i < numChunk; i++)
                {
                    StreamReader strRead = new StreamReader(tmpFilePrefix + i);
                    readers[i] = strRead;     
                }


                using (StreamWriter streamOut = new StreamWriter(filename_out))
                {
                    for (int i = 0; i < numChunk; i++)
                        heads.Enqueue(new HeadIndexPair(readers[i].ReadLine(), i), new HeadIndexPair(readers[i].ReadLine(), i));

                    while (true)
                    {
                        HeadIndexPair? minh = heads.Count > 0 ? heads.Dequeue() : null;

                        if (null == minh) break;

                        streamOut.WriteLine(minh.head);

                        if ((line = readers[minh.i].ReadLine()) != null)
                            heads.Enqueue(new HeadIndexPair(line, minh.i), new HeadIndexPair(line, minh.i));
                        
                    }


                    for (int i = 0; i < numChunk; i++)
                        readers[i].Close();
                }

                sr.Close();
                SortLogger.Info("Сортировка закончена!");
            }
        }

        public void sortAndSaveChunk(string[] chunk, string filename)
        {
            Array.Sort(chunk, (a, b) => compare(a, b));
            using (StreamWriter sw = new StreamWriter(filename))
            {
                for (int i = 0; i < chunk.Length; i++)
                {
                    if (chunk[i] != null)
                        sw.WriteLine(chunk[i]);
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

        public static void CalledMultiWay()
        {
            var mySort = new doSortingMerge(
                "taxpayers_3M.txt",
                "taxpayers_3M_sorted.txt",
                3000,
                8192,
                0,
                "\t");

            mySort.doSort();
        }
    }
}
