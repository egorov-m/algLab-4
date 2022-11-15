namespace algLab_4.Task2
{
    public class HeadIndexPair
    {
        public string Head { get; set; }
        public int I { get; set; }

        public HeadIndexPair(string head, int i)
        {
            this.Head = head;
            this.I = i;
        }
    }
    class MultiwayMergeSorter
    {
        /// <summary> Логгер для ведения журнала выполнения сортировки </summary>
        private static Logger.Logger SortLogger = Logger.Logger.GetLogger(0);

        public string FilenameIn { get; set; }
        public string FilenameOut { get; set; }
        private int _m;
        private int _b;
        private int _c;
        private string _sep;
        private string _tmpFilePrefix = "tmpfile";
        private int _numChunk;


        public MultiwayMergeSorter(string filename_in, string filename_out, int m, int b, int c, string sep)
        {
            FilenameIn = filename_in;
            FilenameOut = filename_out;
            _m = m;
            _b = b;
            _c = c;
            _sep = sep;
        }

        public void DoSortingMerge()
        {
            Console.WriteLine("Phase 1 is started");
            using (StreamReader sr = new StreamReader(FilenameIn))
            {
                int cnt = 0;
                string[] chunk = new string[_m];

                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    chunk[cnt] = line;
                    cnt++;
                    if (cnt % _m == 0)
                    {
                        SortAndSaveChunk(chunk, _tmpFilePrefix + _numChunk);
                        cnt = 0;
                        _numChunk++;
                    }
                }

                if (cnt != 0)
                {
                    SortAndSaveChunk(chunk, _tmpFilePrefix + _numChunk);
                    _numChunk++;
                }

                Console.WriteLine("Phase 1 Time elapsed (sec) = ");

                Console.WriteLine("Phase 2 started");

                //у нас есть отсортированные подсписки numChunks, которые нам нужно объединить
                //нам не нужно управлять буфером напрямую
                //Управление буфером осуществляется StreamReader

                StreamReader[] readers = new StreamReader[_numChunk];

                /*
                 Мы будем использовать приоритетную очередь, чтобы эффективно реализовать конкуренцию между головками отсортированных
                подсписков (чанков). Мы создаем пару голова - индекс, которая будет использоваться в качестве типа для элементов PriorityQueue 
                Индекс - номер отсортированного подсписка, которому принадлежит голова. Зачем нам индекс? Когда голова "выйгрывает" (наименьшая среди голов)
                она мигрирует в выходной буфер. Таким образом, нам нужно вставить новый заголовок из соответствующего подсписка (чанка), а индекс сообщает 
                нам, какой это подсписок
                 */

                var heads = new PriorityQueue<HeadIndexPair, HeadIndexPair>(Comparer<HeadIndexPair>.Create((a, b) => compare(a.Head, b.Head)));



                for (int i = 0; i < _numChunk; i++)
                {
                    using (StreamReader strRead = new StreamReader(_tmpFilePrefix + i))
                    {
                        readers[i] = strRead;
                        strRead.Close();
                    }
                }


                using (StreamWriter streamOut = new StreamWriter(FilenameOut))
                {
                    for (int i = 0; i < _numChunk; i++)
                        heads.Enqueue(new HeadIndexPair(readers[i].ReadLine(), i), new HeadIndexPair(readers[i].ReadLine(), i));

                    while (true)
                    {
                        HeadIndexPair minh = heads.Dequeue();

                        if (null == minh) break;

                        streamOut.WriteLine(minh.Head);

                        if ((line = readers[minh.I].ReadLine()) != null)
                        {
                            heads.Enqueue(new HeadIndexPair(line, minh.I), new HeadIndexPair(line, minh.I));
                        }
                    }


                    for (int i = 0; i < _numChunk; i++)
                    {
                        readers[i].Close();
                    }
                }

                sr.Close();
                Console.WriteLine("Sort Complete");
            }
        }
        public void SortAndSaveChunk(string[] chunk, string filename)
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

        public string ExtractCol(string line)
        {
            string[] columns = line.Split(_sep);
            return columns[_c];
        }


        public int compare(string a, string b)
        {
            if (a == null && b == null)
                return 0;
            if (a == null)
                return 1;
            if (b == null)
                return -1;
            return ExtractCol(a).CompareTo(ExtractCol(b));
        }

        public static void CalledMultiWay()
        {
            var mySort = new MultiwayMergeSorter(
                "taxpayers_3M.txt",
                "taxpayers_3M_sorted.txt",
                3000,
                8192,
                0,
                "\t");

            mySort.DoSortingMerge();
        }
    }
}
