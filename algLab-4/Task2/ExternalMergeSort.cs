using algLab_4.Logger;

namespace algLab_4.Task2
{
    public class ExternalMergeSort
    {
        private readonly int _attributeId;
        private readonly string _fileNameIn;
        private readonly string _fileNameOut;
        private int _iterations;
        private Logger.Logger SortLogger = Logger.Logger.GetLogger(
            "sortLogger", 
            Level.INFO, 
            new List<IMessageHandler>() { new DelayHandler(1000, new List<IMessageHandler>() { new ConsoleHandler(), new FileHandler()})});

        public ExternalMergeSort(int attributeId, string fileIn = "unsorted.csv", string fileOut = "sorted.csv")
        {
            _attributeId = attributeId;
            _fileNameIn = fileIn;
            _fileNameOut = fileOut;
            _iterations = 1;
        }

        public void Sort()
        {
            var count = GetCountFileLines(_fileNameIn);
            SortLogger.Info($"Начинается сортировка (Метод внешней сортировки: прямое слияние), коллекция состоит из: {count} элементов.");

            for (var i = 1; i < count + 1; i++)
            {
                SortLogger.Info($"Начинается выполнение прохода номер: {i}.");
                Split(i, i == 1 ? _fileNameIn : _fileNameOut);
                Merge();
            }
            SortLogger.Info($"Сортировка завершена, результат смотри в файле: {_fileNameOut}.");
        }

        private void Split(int count, string fileName)
        {
            SortLogger.Info("Выполняется разбиение:");

            SortLogger.Info("|   Инициализация StreamReader — чтение из файла.");
            using var sr = new StreamReader(fileName);

            SortLogger.Info("|   Инициализация 2-x StreamWriter — запись в промежуточные файлы.");
            using var sw1 = new StreamWriter("A.csv");
            using var sw2 = new StreamWriter("B.csv");
            string? str;
            var index = 0;
            while ((str = sr.ReadLine()) != null)
            {
                SortLogger.Info($"|   Прочитано: {str}");
                if (index < count)
                {
                    SortLogger.Info("|   |   Запись выполнена в файл A.csv.");
                    sw1.WriteLine(str);
                    index++;
                }
                else
                {
                    SortLogger.Info("|   |   Запись выполнена в файл B.csv.");
                    sw2.WriteLine(str);
                    index = 0;
                }
            }

            SortLogger.Info("Разбиение завершено.");
        }

        private void Merge()
        {
            SortLogger.Info("Выполняется слияние:");

            SortLogger.Info("|   Инициализация 2-x StreamReader — чтение из промежуточных файлов.");
            using var sr1 = new StreamReader("A.csv");
            using var sr2 = new StreamReader("B.csv");

            SortLogger.Info("|   Инициализация StreamWriter — запись в основной файл.");
            using var sw = new StreamWriter(_fileNameOut);

            long counterA = _iterations;
            long counterB = _iterations;
            string elementA = null, elementB = null;
            bool flagA = false, flagB = false;
            while (!sr1.EndOfStream || !sr2.EndOfStream)
            {
                if (counterA == 0 && counterB == 0)
                {
                    counterA = _iterations;
                    counterB = _iterations;
                }

                if (!sr1.EndOfStream)
                {
                    if (counterA > 0 && !flagA)
                    {
                        SortLogger.Info($"|   Считываем элемент {elementA} из файла A.csv.");
                        elementA = sr1.ReadLine();
                        flagA = true;
                    }
                }

                if (!sr2.EndOfStream)
                {
                    if (counterB > 0 && !flagB)
                    {
                        SortLogger.Info($"|   Считываем элемент {elementB} из файла B.csv.");
                        elementB = sr2.ReadLine();
                        flagB = true;
                    }
                }

                if (flagA)
                {
                    if (flagB)
                    {
                        if (int.Parse(elementA.Split(";")[_attributeId]) <= int.Parse(elementB.Split(";")[_attributeId]))
                        {
                            SortLogger.Info($"|   Добавляем элемент {elementA} из файла A.csv в основной файл {_fileNameOut}.");
                            sw.WriteLine(elementA);
                            counterA--;
                            flagA = false;
                        }
                        else
                        {
                            SortLogger.Info($"|   Добавляем элемент {elementB} из файла B.csv в основной файл {_fileNameOut}.");
                            sw.WriteLine(elementB);
                            counterB--;
                            flagB = false;
                        }
                    }
                    else
                    {
                        SortLogger.Info($"|   Добавляем элемент {elementA} из файла A.csv в основной файл {_fileNameOut}.");
                        sw.WriteLine(elementA);
                        counterA--;
                        flagA = false;
                    }
                }
                else if (flagB)
                {
                    SortLogger.Info($"|   Добавляем элемент {elementB} из файла B.csv в основной файл {_fileNameOut}.");
                    sw.WriteLine(elementB);
                    counterB--;
                    flagB = false;
                }
            }
            _iterations *= 2;

            SortLogger.Info($"|   Увеличиваем размер серии в 2 раза: {_iterations / 2} * 2 = {_iterations}.");
            SortLogger.Info("|   Слияние завершено.");

            //var sr1Len = GetCountFileLines("A.csv");
            //var sr2Len = GetCountFileLines("B.csv");

            //var sr1Pos = 0;
            //var sr2Pos = 0;
            //var targetPos = 0;

            //var target = new string[sr1Len + sr2Len];

            //string? str1;
            //string? str2;

            //var array1 = new List<string>();
            //while((str1 = sr1.ReadLine()) != null) array1.Add(str1);

            //var array2 = new List<string>();
            //while((str2 = sr2.ReadLine()) != null) array2.Add(str2);


            //while (sr1Pos < sr1Len && sr2Pos < sr2Len)
            //{
            //    if (int.Parse(array1[sr1Pos].Split(";")[_attributeId]) <= int.Parse(array2[sr2Pos].Split(";")[_attributeId]))
            //    {
            //        target[targetPos] = array1[sr1Pos];
            //        sr1Pos++;
            //    }
            //    else
            //    {
            //        target[targetPos] = array1[sr2Pos];
            //        sr2Pos++;
            //    }

            //    targetPos++;
            //}

            //for (var i = sr1Pos; i < sr1Len; i++)
            //{
            //    target[targetPos] = array1[i];
            //    targetPos++;
            //}

            //for (var i = sr2Pos; i < sr2Len; i++)
            //{
            //    target[targetPos] = array2[i];
            //    targetPos++;
            //}

            //foreach (var t in target)
            //{
            //    sw.WriteLine(t);
            //}
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
