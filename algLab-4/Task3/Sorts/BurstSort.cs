using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algLab_4.Task3.Sorts
{
    class BurstSortOutput
    {
        public static void Output(IList<string> array)
        {
            BurstSort b = new BurstSort();

            b.insert(array);
            b.print();
        }
    }


    class BurstSort
    {
        static int threshold = 1000;
        static int alphbet = 20000;
        static int bucket_start_size = 40;
        static int bucket_growth_factor = 40;


        Trie root;// Starting point of BurstTrie 

        public BurstSort()
        {
            root = null;
        }


        /*
         *                 "Insertion phase"
         * Inserting string prefix in trie and suffixes in buckets
         */
        public void insert(IList<string> s)
        {
            foreach (string i in s)
            {
                root = insert(i, root);
            }


            /* After insertion phase we traverse to sort the data */
            traverse();
        }

        private Trie insert(string s, Trie node)
        {
            if (node == null)
            {
                Trie n = new Trie();
                char c = charAt(s);
                n.insert(c, s);
                return n;
            }

            else
            {
                node.insert(s[0], s);
                return node;
            }

        }


        /*
         *              "Traversal phase"
         *    Travel BurstTrie from left to right 
         *    to sort data 
         */
        public void traverse()
        {
            root.traverse(0);
        }

        //to print we use print function
        public void print()
        {
            root.print(0);
        }

        // utility function return the First character of string
        public char charAt(string s)
        {
            return s[0];
        }

        //Trie node
        class Trie
        {
            Buckets[] buckets;
            List<int> c;

            public Trie()
            {
                c = new List<int>();
                buckets = new Buckets[alphbet];
            }

            public void insert(int x, string s)
            {
                if (!c.Contains(x))
                {
                    buckets[x] = new Buckets();
                    buckets[x].insert(s.Substring(1));
                    c.Add(x);
                }

                else
                {
                    buckets[x].insert(s.Substring(1));
                }

            }


            public void traverse(int ch)
            {
                //Sorting Prefixes
                for (int i = 0; i < c.Count; i++)
                {
                    for (int j = 0; j < c.Count; j++)
                    {
                        if (c[i] < c[j])
                        {
                            int temp = c[i];
                            c[i] = c[j];
                            c[j] = temp;
                        }

                    }

                }

                for (int i = 0; i < c.Count; i++)//Calling buckets
                {
                    buckets[c[i]].traverse(ch, c[i]);
                }

            }
            public void print(int ch)
            {
                for (int i = 0; i < c.Count; i++)//Calling buckets
                {
                    buckets[c[i]].print(ch, c[i]);
                }

            }

        }


        /*
         * Bucket to store the suffixes of string
         * Array based impelementation of buckets
         */
        class Buckets
        {
            IList<string> buc;      //Bucket
            int h;             //pointer help in inserting elements in bucket
            int growthFactor;  //by which we increase the size of bucket
            int initialSize;   //Starting size of bucket
            int finalLimit;    //Time to burst
            Trie root;

            public Buckets()
            {
                root = null;
                finalLimit = threshold;
                initialSize = bucket_start_size;
                buc = new string[initialSize];
                h = 0;
                growthFactor = bucket_growth_factor;
            }

            //Inerting elements in buckets
            public void insert(string s)
            {
                if (h < initialSize && h != finalLimit)//When bucket is not full
                {
                    buc[h] = s;
                    h++;
                }

                if (h == initialSize && h != finalLimit)//When bucket is full
                {
                    buc = increaseSize(buc);//Increasing the size of bucket
                }


                if (h >= finalLimit)//When bucket reaches threshold
                {
                    /*
                     *               "BURSTING PHASE"
                     * Create a new trie node 
                     * Inserting bucket elements in new trie node
                     */

                    for (int i = 0; i < h; i++)
                    {
                        insert(buc[i]);//Calling the same method which we use before
                    }

                }

            }

            /*
             * Increase size of bucket by bucket_growth_factor
             */
            public IList<string> increaseSize(IList<string> b)
            {
                initialSize *= growthFactor;
                string[] a = new string[initialSize];

                for (int i = 0; i < b.Count; i++)
                {
                    a[i] = b[i];
                }

                return a;
            }

            public void traverse(int c, int ch)
            {
                if (root == null)
                {
                    for (int i = 0; i < h; i++)
                    {
                        if (h > 1)//when the size of bucket is more than one
                        {

                            /** Applying Multikey_Quick_Sort to sort buckets */
                            MultikeyQuickSort sort = new MultikeyQuickSort();
                            buc = sort.sort(buc, 0, h - 1, 0);
                        }

                    }

                }

                else
                {
                    root.traverse(ch);
                }

            }

            //To print the sorted data
            public void print(int c, int ch)
            {
                if (root == null)
                {
                    for (int i = 0; i < h; i++)
                    {
                        Console.Write(Convert.ToChar(c));
                        Console.WriteLine(Convert.ToChar(ch) + buc[i]);
                    }

                }

                else
                {
                    root.print(ch);
                }

            }

        }

        class MultikeyQuickSort
        {
            /*
             * Multikey_Quick_Sort divide array into three parts
             * 
             * 1-greater than pivot
             * 2-less than pivot
             * 3-equal to pivot
             * 
             */

            public IList<string> sort(IList<string> a, int lo, int hi, int d)
            {
                if (hi <= lo) return a;
                int lt = lo, gt = hi;
                string s1 = a[lo];//pivot
                int v = -1;

                if (d < s1.Length)
                {
                    v = s1[d];
                }
                int i = lo + 1;

                while (i <= gt)
                {
                    string s = a[i];
                    int t = -1;
                    if (d < s1.Length)
                    {
                        t = s[d];
                    }

                    if (t < v) interChange(a, lt++, i++);    //part 1
                    else if (t > v) interChange(a, i, gt--); //part 2
                    else i++;                                //part 3
                }


                sort(a, lo, lt - 1, d);
                if (v >= 0)
                    sort(a, lt, gt, d + 1);
                sort(a, gt + 1, hi, d);

                return a;
            }

            private void interChange(IList<string> a, int i, int j)
            {
                string temp = a[i];
                a[i] = a[j];
                a[j] = temp;
            }

        }

    }
}

