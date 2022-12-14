using System.Globalization;

namespace algLab_4.Task2
{
    /// <summary> Сортировщик методом естественного слияния </summary>
    public class NaturalMergeSorter
    {
        /// <summary> Логгер для ведения журнала выполнения сортировки </summary>
        private static Logger.Logger SortLogger = Logger.Logger.GetLogger(0);

        /// <summary> Путь до входного файла для сортировки </summary>
        public string InputFilePath { get; set; } = "../../../Task2/unsorted.csv";

        /// <summary> Путь до выходного файла с отсортированными данными </summary>
        public string OutputFilePath { get; set; } = "sorted.csv";

        /// <summary> Ключ для сортировки (индекс колонки таблицы) </summary>
        public int SortKey { get; set; } = 4;

        /// <summary> Первый ли проход выполнять </summary>
        private bool _isPassageFirst = true;

        /// <summary> Количество сегментов </summary>
        private long _segments;

        /// <summary> Путь до вспомогательного файла A </summary>
        private string _auxiliaryFilePathA = "A.csv";
        /// <summary> Путь до вспомогательного файла B </summary>
        private string _auxiliaryFilePathB = "B.csv";

        /// <summary> Сравнение: сортировка по возрастанию </summary>
        private Func<double, double, bool> _ascending = (x, y) => x < y;

        /// <summary> Сравнение: сортировка по убыванию </summary>
        private Func<double, double, bool> _descending = (x, y) => x < y;

        public NaturalMergeSorter(string filePath)
        {
            InputFilePath = filePath;
        }

        public NaturalMergeSorter(string filePath, int sortKey)
        {
            InputFilePath = filePath;
            SortKey = sortKey;
        }

        public NaturalMergeSorter(int sortKey)
        {
            SortKey = sortKey;
        }

        public NaturalMergeSorter() {}

        /// <summary> Выполнить внешнюю сортировку по возрастанию методом прямого слияния </summary>
        public void Sort()
        {
            SortLogger.Info("Начинается сортировка (Метод внешней сортировки: естественное слияние).");
            Sort(_ascending);
        }

        /// <summary> Выполнить внешнюю сортировку по убыванию методом прямого слияния </summary>
        public void SortDescending() => Sort(_descending);

        /// <summary> Выполнить внешнюю сортировку в указанном порядке методом прямого слияния </summary>
        /// <param name="order"> Порядок сортировки </param>
        private void Sort(Func<double, double, bool> order)
        {
            var index = 1;
            while (true)
            {
                SortLogger.Info($"Начинается выполнение прохода номер: {index++}.");
                SplitToFiles(order);

                if (_segments == 1) break;

                Merge(order);
            }

            SortLogger.Info($"Сортировка завершена, результат смотри в файле: {OutputFilePath}.");
        }

        /// <summary> Получить количество строк в файле </summary>
        /// <param name="filePath"> Путь до файла </param>
        private static int GetCountLinesFile(string filePath)
        {
            using var sr = new StreamReader(filePath);
            var count = 0;
            while ((sr.ReadLine()) != null)
            {
                count++;
            }

            return count;
        }

        /// <summary> Выполнить внешнюю сортировку по убыванию методом прямого слияния </summary>
        private void SplitToFiles(Func<double, double, bool> comparer)
        {
            SortLogger.Info("Выполняется разбиение:");

            _segments = 1;

            SortLogger.Info("|   Инициализация StreamReader — чтение из файла.");

            using var sr = _isPassageFirst ? new StreamReader(InputFilePath) : new StreamReader(OutputFilePath);

            SortLogger.Info("|   Инициализация 2-x StreamWriter — запись в промежуточные файлы.");

            using var swA = new StreamWriter(_auxiliaryFilePathA);
            using var swB = new StreamWriter(_auxiliaryFilePathB);

            var isStart = false;
            var isFirstFile = true;

            var length = GetCountLinesFile(_isPassageFirst ? InputFilePath : OutputFilePath);
            _isPassageFirst = false;
            var position = 0L;

            var str = "";
            var nextStr = "";

            if (length == 1)
            {
                SortLogger.Info("В файле найдена только одна строка.");
                SortLogger.Info($"|   |   Запись выполнена в файл {_auxiliaryFilePathA}.");

                swA.WriteLine(sr.ReadLine());
                return;
            }

            SortLogger.Info("|---Начинаем собирать серию.");

            while (position != length)
            {
                if (!isStart)
                {
                    str = sr.ReadLine();

                    SortLogger.Info($"|   Считываем элемент: {str}.");

                    position++;

                    SortLogger.Info($"|   |   Выполняем запись в файл {_auxiliaryFilePathA}.");

                    swA.WriteLine(str);
                    isStart = true;
                }

                nextStr = sr.ReadLine();

                SortLogger.Info($"|   Считываем элемент: {nextStr}.");

                position++;

                SortLogger.Info($"|   Выполняем сравнение данных из колонок с индексами: {SortKey}.");

                if (comparer(double.Parse(nextStr.Split(";")[SortKey], CultureInfo.InvariantCulture), double.Parse(str.Split(";")[SortKey], CultureInfo.InvariantCulture)))
                {
                    var tmp = isFirstFile;
                    isFirstFile = !isFirstFile;
                    
                    if (tmp != isFirstFile) SortLogger.Info("|---Начинается новая серия");

                    _segments++;
                }

                if (isFirstFile)
                {
                    SortLogger.Info($"|   |   Выполняем запись в файл {_auxiliaryFilePathA} строки {nextStr}.");

                    swA.WriteLine(nextStr);
                }
                else
                {
                    SortLogger.Info($"|   |   Выполняем запись в файл {_auxiliaryFilePathB} строки {nextStr}.");

                    swB.WriteLine(nextStr);
                }

                str = nextStr;
            }

            SortLogger.Info("Разбиение завершено.");
        }

        /// <summary> Выполнять слияние данных из двух файлов </summary>
        private void Merge(Func<double, double, bool> comparer)
        {
            SortLogger.Info("Выполняется слияние:");

            SortLogger.Info("|   Инициализация 2-x StreamReader — чтение из промежуточных файлов.");

            using var srA = new StreamReader(_auxiliaryFilePathA);
            using var srB = new StreamReader(_auxiliaryFilePathB);

            SortLogger.Info("|   Инициализация StreamWriter — запись в основной файл.");

            using var sw = new StreamWriter(OutputFilePath);

            var strA = "";
            var strB = "";

            var isPickedA = false;
            var isPickedB = false;
            var isEndA = false;
            var isEndB = false;

            var lengthA = GetCountLinesFile(_auxiliaryFilePathA);
            var lengthB = GetCountLinesFile(_auxiliaryFilePathB);

            var positionA = 0L;
            var positionB = 0L;

            while (!isEndA || !isEndB || isPickedA || isPickedB)
            {
                isEndA = positionA == lengthA;
                isEndB = positionB == lengthB;

                if (!isEndA && !isPickedA)
                {
                    strA = srA.ReadLine();

                    SortLogger.Info($"|   Считан элемент {strA} из файла {_auxiliaryFilePathA}.");

                    positionA++;
                    isPickedA = true;
                }

                if (!isEndB && !isPickedB)
                {
                    strB = srB.ReadLine();

                    SortLogger.Info($"|   Считан элемент {strB} из файла {_auxiliaryFilePathB}.");

                    positionB++;
                    isPickedB = true;
                }

                if (isPickedA)
                {
                    if (isPickedB)
                    {
                        SortLogger.Info($"|   Выполняем сравнение данных из колонок с индексами: {SortKey}.");

                        if (comparer(double.Parse(strA.Split(";")[SortKey], CultureInfo.InvariantCulture),
                                double.Parse(strB.Split(";")[SortKey], CultureInfo.InvariantCulture)))
                        {
                            SortLogger.Info($"|   Добавляем элемент {strA} из файла {_auxiliaryFilePathA} в основной файл {OutputFilePath}.");

                            sw.WriteLine(strA);
                            isPickedA = false;
                        }
                        else
                        {
                            SortLogger.Info($"|   Добавляем элемент {strB} из файла {_auxiliaryFilePathB} в основной файл {OutputFilePath}.");

                            sw.WriteLine(strB);
                            isPickedB = false;
                        }
                    }
                    else
                    {
                        SortLogger.Info($"|   Добавляем элемент {strA} из файла {_auxiliaryFilePathA} в основной файл {OutputFilePath}.");

                        sw.WriteLine(strA);
                        isPickedA = false;
                    }
                } else if (isPickedB)
                {
                    SortLogger.Info($"|   Добавляем элемент {strB} из файла {_auxiliaryFilePathB} в основной файл {OutputFilePath}.");

                    sw.WriteLine(strB);
                    isPickedB = false;
                }
            }

            SortLogger.Info("|   Слияние завершено.");
        }
    }
}
