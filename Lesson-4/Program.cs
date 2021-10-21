using System;
using System.Collections;
using System.Collections.Generic;

namespace Lesson_4
{
    class Program
    {
        static Dictionary<int, int> Count(List<int> list)
        {
            Dictionary<int, int> d = new Dictionary<int, int>();
            foreach (var obj in list)
            {
                d[obj] = !d.ContainsKey(obj) ? 1 : d[obj] + 1;
            }
            return d;
        }
        static void Main(string[] args)
        {
            List<int> list = new List<int>(new int[] { 1, 3, 1, 3, 4, 5, 6, 6, 6, 7, 4, 1, 7, 0, 6 });
            ArrayList alist = new ArrayList(new Object[] { null, 1, "Peter" });

            var res1 = list.CountElements();
            foreach (var k in res1.Keys)
            {
                Console.Write($"{k}:{res1[k]}x\t");
            }

            var res2 = alist.CountElements();
            foreach (var k in res2.Keys)
            {
                Console.Write($"{k}:{res2[k]}x\t");
            }
            Console.ReadKey();


        }
    }
}
}
