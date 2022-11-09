namespace algLab_4.Task2
{
    public class ExternalMergeSort
    {
        private readonly int _attributeId;
        private readonly string _fileNameIn;
        private readonly string _fileNameOut;

        public ExternalMergeSort(int attributeId, string fileIn = "unsorted.csv", string fileOut = "sorted.csv")
        {
            _attributeId = attributeId;
            _fileNameIn = fileIn;
            _fileNameOut = fileOut;
        }

        public void Sort()
        {
            var count = GetCountFileLines(_fileNameIn);

            for (var i = 1; i < count + 1; i++)
            {
                Split(i, i == 1 ? _fileNameIn : _fileNameOut);
                Merge();
            }
        }

        private void Split(int count, string fileName)
        {
            using var sr = new StreamReader(fileName);
            using var sw1 = new StreamWriter("A.csv");
            using var sw2 = new StreamWriter("B.csv");
            string? str;
            var index = 0;
            while ((str = sr.ReadLine()) != null)
            {
                if (index < count)
                {
                    sw1.WriteLine(str);
                    index++;
                }
                else
                {
                    sw2.WriteLine(str);
                    index = 0;
                }
            }
        }

        private void Merge()
        {
            using var sr1 = new StreamReader("A.csv");
            using var sr2 = new StreamReader("B.csv");
            using var sw = new StreamWriter(_fileNameOut);

            var sr1Len = GetCountFileLines("A.csv");
            var sr2Len = GetCountFileLines("B.csv");

            var sr1Pos = 0;
            var sr2Pos = 0;
            var targetPos = 0;

            var target = new string[sr1Len + sr2Len];

            string? str1;
            string? str2;

            var array1 = new List<string>();
            while((str1 = sr1.ReadLine()) != null) array1.Add(str1);

            var array2 = new List<string>();
            while((str2 = sr2.ReadLine()) != null) array2.Add(str2);


            while (sr1Pos < sr1Len && sr2Pos < sr2Len)
            {
                if (int.Parse(array1[sr1Pos].Split(";")[_attributeId]) <= int.Parse(array2[sr2Pos].Split(";")[_attributeId]))
                {
                    target[targetPos] = array1[sr1Pos];
                    sr1Pos++;
                }
                else
                {
                    target[targetPos] = array1[sr2Pos];
                    sr2Pos++;
                }

                targetPos++;
            }

            for (var i = sr1Pos; i < sr1Len; i++)
            {
                target[targetPos] = array1[i];
                targetPos++;
            }

            for (var i = sr2Pos; i < sr2Len; i++)
            {
                target[targetPos] = array2[i];
                targetPos++;
            }

            foreach (var t in target)
            {
                sw.WriteLine(t);
            }
        }

        private int GetCountFileLines(string fileName)
        {
            using var sr = new StreamReader(fileName);
            var count = 0;
            while ((sr.ReadLine()) != null)
            {
                count++;
            }

            return count;
        }
    }
}
