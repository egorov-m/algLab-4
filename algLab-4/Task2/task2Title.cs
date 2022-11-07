using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace algLab_4.Task2.Sorts
{
    internal class task2Title
    {
        static void task2Mine()
        {
            var text = File.ReadAllText("..\\..\\..\\Table.csv");
            var lines = text.Split('\n');
            var title = lines[0].Split(';');
            var rows = lines.Skip(1).Select(x => x.Split(';')).ToList();
            int key = title.ToList().IndexOf(Console.ReadLine() ?? title[0]);

            switch (Console.ReadLine())
            {
                case "1": Straight(text, rows, title, key); break;
            }

            Console.ReadLine();

        }

        static void Straight(string text, List<string[]> rows, string[] title, int index)
        {
            Console.Clear();
            // массив значений
            var la = rows.Select(x => int.Parse(x[index])).ToList();
            var lb = la.Skip(la.Count / 2).ToList();
            var lc = la.Take(la.Count / 2).ToList();
            // лимиты
            var lim = lb.Count;
            var limMax = la.Count;
            // подсписки
            var slb = new List<int>();
            var slc = new List<int>();

            for (int c = 1; c <= lim; c *= 2)
            {
                // Очистить список
                la.Clear();

                for (int i = 0; i < lim / c; i++)
                {
                    // фрагмент списка
                    slb = lb.Skip(i * c).Take(c).ToList();
                    slc = lc.Skip(i * c).Take(c).ToList();
                    // вывод отобранных значений
                    Console.Write("b: "); foreach (var p in slb) Console.Write(p + " "); Console.WriteLine();
                    Console.Write("c: "); foreach (var p in slc) Console.Write(p + " "); Console.WriteLine();
                    // цикл сравнения фрагментов
                    while (slb.Count > 0 || slc.Count > 0)
                    {
                        if (slb.Count > 0 && slc.Count > 0)
                        {
                            int vb = slb[0], vc = slc[0];

                            if (vb < vc)
                            {
                                la.Add(vb);
                                slb.RemoveAt(0);
                            }
                            else if (vc < vb)
                            {
                                la.Add(vc);
                                slc.RemoveAt(0);
                            }
                        }
                        else if (slb.Count > 0 && slc.Count == 0)
                        {
                            la.AddRange(slb.ToArray());
                            slb.Clear();
                        }
                        else if (slc.Count > 0 && slb.Count == 0)
                        {
                            la.AddRange(slc.ToArray());
                            slc.Clear();
                        }
                    }

                }
                Console.WriteLine();
                // обновление подсписков
                lb = la.Skip(la.Count / 2).ToList();
                lc = la.Take(la.Count / 2).ToList();
                Console.Write("a: "); foreach (var p in la) Console.Write(p + " "); Console.WriteLine();
            }

        }

    }
}