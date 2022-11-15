using algLab_4.Logger;
using System.Globalization;

namespace algLab_4.Task2
{
    /// <summary> Сортировщик методом прямого слияния </summary>
    public class DirectMergeSorter
    {
        /// <summary> Логгер для ведения журнала выполнения сортировки </summary>
        private static Logger.Logger SortLogger = Logger.Logger.GetLogger(0);

        /// <summary> Путь до входного файла для сортировки </summary>
        public string InputFilePath { get; set; } = "../../../Task2/unsorted.csv";

        /// <summary> Путь до выходного файла с отсортированными данными </summary>
        public string OutputFilePath { get; set; } = "sorted.csv";

        /// <summary> Ключ для сортировки (индекс колонки таблицы) </summary>
        public int SortKey { get; set; } = 4;

        /// <summary> (Степень двойки) Количество элементов последовательности для слияния </summary>
        private long _iterations;

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

        public DirectMergeSorter(string filePath)
        {
            InputFilePath = filePath;
            _iterations = 1;
        }

        public DirectMergeSorter(string filePath, int sortKey)
        {
            InputFilePath = filePath;
            SortKey = sortKey;
            _iterations = 1;
        }

        public DirectMergeSorter(int sortKey)
        {
            SortKey = sortKey;
            _iterations = 1;
        }

        public DirectMergeSorter() => _iterations = 1;

        /// <summary> Выполнить внешнюю сортировку по возрастанию методом прямого слияния </summary>
        public void Sort()
        {
            SortLogger.Info("Начинается сортировка (Метод внешней сортировки: прямое слияние).");
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
                SplitToFiles();

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

        /// <summary> Выполнять разбиение по двум файлам </summary>
        private void SplitToFiles()
        {
            SortLogger.Info("Выполняется разбиение:");

            _segments = 1;

            SortLogger.Info("|   Инициализация StreamReader — чтение из файла.");

            using var sr = _iterations == 1 ? new StreamReader(InputFilePath) : new StreamReader(OutputFilePath);

            SortLogger.Info("|   Инициализация 2-x StreamWriter — запись в промежуточные файлы.");

            using var swA = new StreamWriter(_auxiliaryFilePathA);
            using var swB = new StreamWriter(_auxiliaryFilePathB);

            var counter = 0L;
            var isFirstFile = true;

            var length = GetCountLinesFile(_iterations == 1 ? InputFilePath : OutputFilePath);
            var position = 0L;

            while (position != length)
            {
                if (counter == _iterations)
                {
                    isFirstFile = !isFirstFile;
                    counter = 0;
                    _segments++;
                }

                var str = sr.ReadLine();

                SortLogger.Info($"|   Прочитано: {str}");

                position++;

                if (isFirstFile)
                {
                    swA.WriteLine(str);

                    SortLogger.Info($"|   |   Запись выполнена в файл {_auxiliaryFilePathA}.");
                }
                else
                {
                    swB.WriteLine(str);

                    SortLogger.Info($"|   |   Запись выполнена в файл {_auxiliaryFilePathB}.");
                }

                counter++;
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

            var counterA = _iterations; 
            var counterB = _iterations;

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

            while (!isEndA || !isEndB)
            {
                if (counterA == 0 && counterB == 0)
                {
                    counterA = _iterations;
                    counterB = _iterations;
                }

                if (positionA != lengthA)
                {
                    if (counterA > 0 && !isPickedA)
                    {
                        strA = srA.ReadLine();

                        SortLogger.Info($"|   Считан элемент {strA} из файла {_auxiliaryFilePathA}.");

                        positionA++;
                        isPickedA = true;
                    }
                }
                else
                {
                    isEndA = true;
                }

                if (positionB != lengthB)
                {
                    if (counterB > 0 && !isPickedB)
                    {
                        strB = srB.ReadLine();

                        SortLogger.Info($"|   Считан элемент {strB} из файла {_auxiliaryFilePathB}.");

                        positionB++;
                        isPickedB = true;
                    }
                }
                else
                {
                    isEndB = true;
                }

                if (isPickedA)
                {
                    if (isPickedB)
                    {
                        if (double.Parse(strA.Split(";")[SortKey], CultureInfo.InvariantCulture) < double.Parse(strB.Split(";")[SortKey], CultureInfo.InvariantCulture))
                        {
                            SortLogger.Info($"|   Добавляем элемент {strA} из файла {_auxiliaryFilePathA} в основной файл {OutputFilePath}.");

                            sw.WriteLine(strA);
                            counterA--;
                            isPickedA = false;
                        }
                        else
                        {
                            SortLogger.Info($"|   Добавляем элемент {strB} из файла {_auxiliaryFilePathB} в основной файл {OutputFilePath}.");

                            sw.WriteLine(strB);
                            counterB--;
                            isPickedB = false;
                        }
                    }
                    else
                    {
                        SortLogger.Info($"|   Добавляем элемент {strA} из файла {_auxiliaryFilePathA} в основной файл {OutputFilePath}.");

                        sw.WriteLine(strA);
                        counterA--;
                        isPickedA = false;
                    }
                }
                else if (isPickedB)
                {
                    SortLogger.Info($"|   Добавляем элемент {strB} из файла {_auxiliaryFilePathB} в основной файл {OutputFilePath}.");

                    sw.WriteLine(strB);
                    counterB--;
                    isPickedB = false;
                }
            }

            _iterations *= 2;

            SortLogger.Info($"|   Увеличиваем размер последовательности в 2 раза: {_iterations / 2} * 2 = {_iterations}.");
            SortLogger.Info("|   Слияние завершено.");
        }
    }
}
