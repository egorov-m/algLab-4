using System;

namespace algLab_4.Task3.Sorts
{
    public static class BurstSort
    {
        /// <summary> Лимит количества элементов для корзины, куда помещаются элементы </summary>
        private const int Threshold = 32;
        /// <summary> Размер поддерживаемого алфавита </summary>
        private const int Alphbet = 127;
        /// <summary> Начальный размер корзины </summary>
        private const int BucketStartSize = 4;
        /// <summary> Коэффициент роста размера корзины </summary>
        private const int BucketGrowthFactor = 4;

        private static Trie? _root;// Starting point of BurstTrie 

        /// <summary> Сортировать коллекцию строк алгоритмом "BurstSort" </summary>
        /// <param name="collection"> Коллекция для сортировки </param>
        public static void BurstSorting(this IList<string> collection)
        {
            collection.Insert();
            WritingToOriginalCollection(collection);
        }

        /// <summary>
        /// Фаза вставки:
        /// Вставка префикса строки в дерево и суффиксов в корзину
        /// </summary>
        /// <param name="s"> Коллекция строк </param>
        private static void Insert(this IList<string> s)
        {
            foreach (var i in s)
            {
                _root = Insert(i, _root);
            }

            // После фазы сортировки выполняется обход для сортировки данных
            Traverse();
        }

        /// <summary>
        /// Фаза вставки:
        /// Вставка префикса строки в дерево и суффиксов в корзину
        /// </summary>
        /// <param name="s"> Строка </param>
        /// <param name="node"> Дерево </param>
        private static Trie? Insert(string s, Trie? node)
        {
            if (node == null)
            {
                var n = new Trie();
                var c = CharAt(s);
                n.Insert(c, s);
                return n;
            }

            node.Insert(s[0], s);
            return node;
        }

        /// <summary>
        /// Фаза обхода:
        /// Перемещение слева на право BurstTrie
        /// Сортировка данных
        /// </summary>
        private static void Traverse() => _root?.Traverse(0);

        public static void WritingToOriginalCollection(IList<string> array)
        {
            var index = 0;
            _root?.WritingToOriginalCollection(0, array, ref index);
        }

        public static char CharAt(string s) => s[0];

        /// <summary> Trie — древовидная структура данных </summary>
        private class Trie
        {
            /// <summary> Корзина </summary>
            private readonly Buckets[] _buckets;
            private List<int> c;

            public Trie()
            {
                c = new List<int>();
                _buckets = new Buckets[Alphbet];
            }

            public void Insert(int x, string s)
            {
                if (!c.Contains(x))
                {
                    _buckets[x] = new Buckets();
                    _buckets[x].Insert(s.Substring(1));
                    c.Add(x);
                }

                else
                {
                    _buckets[x].Insert(s.Substring(1));
                }
            }


            public void Traverse(int ch)
            {
                // Сортировка префиксов
                for (var i = 0; i < c.Count; i++)
                {
                    for (var j = 0; j < c.Count; j++)
                    {
                        if (c[i] < c[j])
                        {
                            (c[i], c[j]) = (c[j], c[i]);
                        }
                    }
                }

                // Вызов корзины
                foreach (var item in c)
                {
                    _buckets[item].Traverse(ch, item);
                }
            }

            public void WritingToOriginalCollection(int ch, IList<string> array, ref int index)
            {
                foreach (var item in c)
                {
                    _buckets[item].WritingToOriginalCollection(ch, item, array, ref index);
                }
            }
        }

        /// <summary>
        /// Корзина для хранения суффиксов строки
        /// Представления ведра на основе массива
        /// </summary>
        private class Buckets
        {
            /// <summary> Коллекция корзины </summary>
            private IList<string> _buc;
            /// <summary> Указатель для вставки элементов в корзину </summary>
            private int _h;
            /// <summary> Значение для увеличения размера корзины </summary>
            private readonly int _growthFactor;
            /// <summary> Начальный размер корзины </summary>
            private int _initialSize;
            /// <summary> Лимит корзины </summary>
            private readonly int _finalLimit;
            /// <summary> Древовидная структура </summary>
            private readonly Trie? _root;

            public Buckets()
            {
                _root = null;
                _finalLimit = Threshold;
                _initialSize = BucketStartSize;
                _buc = new string[_initialSize];
                _h = 0;
                _growthFactor = BucketGrowthFactor;
            }

            /// <summary> Вставка элементов в ведро </summary>
            /// <param name="s"> Строка </param>
            public void Insert(string s)
            {
                if (_h < _initialSize && _h != _finalLimit) // Когда корзина неполная
                {
                    _buc[_h] = s;
                    _h++;
                }

                if (_h == _initialSize && _h != _finalLimit) // Когда корзина заполнена
                {
                    _buc = IncreaseSize(_buc); // Увеличение размера корзины
                }

                if (_h >= _finalLimit) // Корзина достигает порога
                {
                    // Новая фаза:
                    // 1. Создание нового узла древовидной структуры
                    // 2. Вставка элементов корзины в новый узел структуры
                    for (var i = 0; i < _h; i++)
                    {
                        Insert(_buc[i]);
                    }
                }
            }

            /// <summary> Увеличение размера ведра в соответствии с установленным коэффициентом </summary>
            /// <param name="b"> Коллекция </param>
            private IList<string> IncreaseSize(IList<string> b)
            {
                _initialSize *= _growthFactor;
                var a = new string[_initialSize];

                for (var i = 0; i < b.Count; i++)
                {
                    a[i] = b[i];
                }

                return a;
            }

            public void Traverse(int c, int ch)
            {
                if (_root == null)
                {
                    for (var i = 0; i < _h; i++)
                    {
                        if (_h > 1) // Размер ведра больше единицы
                        {
                            // Применение MultikeyQuickSort для сортировки ведра
                            var sort = new MultikeyQuickSort();
                            _buc = sort.Sort(_buc, 0, _h - 1, 0);
                        }
                    }
                }
                else
                {
                    _root.Traverse(ch);
                }
            }

            /// <summary> Запись отсортированных данных в исходную коллекцию </summary>
            /// <param name="array"> Исходная коллекция </param>
            /// <param name="index"> Индекс добавляемого элемента </param>
            public void WritingToOriginalCollection(int c, int ch, IList<string> array, ref int index)
            {
                if (_root == null)
                {
                    for (var i = 0; i < _h; i++)
                    {
                        array[index] = $"{Convert.ToChar(c)}{Convert.ToChar(ch)}{_buc[i]}";
                        index++;
                    }
                }
                else
                {
                    _root.WritingToOriginalCollection(ch, array, ref index);
                }
            }

        }

        /// <summary>
        /// Сортировка: MultikeyQuickSort
        /// Деление коллекции на три части:
        /// 1 - больше опорного элемента
        /// 2 - меньше опорного элемента
        /// 3 - равно опорному элементу
        /// </summary>
        private class MultikeyQuickSort
        {
            public IList<string> Sort(IList<string> a, int left, int right, int d)
            {
                if (right <= left) return a;
                int lt = left, gt = right;
                var s1 = a[left]; // Опорный элемент (самый левый)
                var v = -1;

                if (d < s1.Length)
                {
                    v = s1[d];
                }
                var i = left + 1;

                while (i <= gt)
                {
                    var s = a[i];
                    var t = -1;
                    if (d < s1.Length)
                    {
                        t = s[d];
                    }

                    if (t < v) InterChange(a, lt++, i++);    // Часть 1
                    else if (t > v) InterChange(a, i, gt--);   // Часть 2
                    else i++;                                    // Часть 3
                }

                Sort(a, left, lt - 1, d);
                if (v >= 0)
                    Sort(a, lt, gt, d + 1);
                Sort(a, gt + 1, right, d);

                return a;
            }

            /// <summary> Менять элементы коллекции местами </summary>
            /// <param name="a"> Коллекция </param>
            /// <param name="i"> Индекс первого элемента </param>
            /// <param name="j"> Индекс второго элемента </param>
            private void InterChange(IList<string> a, int i, int j) => (a[i], a[j]) = (a[j], a[i]);
        }
    }
}