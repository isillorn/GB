using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson4
{
    class Program
    {
        static Dictionary<int, int> Count(List<int> list)
        {
            Dictionary<int, int> d = new Dictionary<int, int>();
            foreach (var obj in list) {
                d[obj] = !d.ContainsKey(obj) ? 1 : d[obj] + 1;
            }
            return d;
        }
        static void Main(string[] args)
        {
            List<int> list = new List<int>(new int[] { 1, 3, 1, 3, 4, 5, 6, 6, 6, 7, 4, 1, 7, 0, 6 });
            ArrayList alist = new ArrayList(new Object[] { 18.0, "Peter", "Peter", false, true, 18.0, false });

            var res1 = list.CountElements();
            foreach (var k in res1.Keys)
            {
                Console.WriteLine($"{k}:\t{res1[k]} раз(а)");
            }
            Console.WriteLine();

            var res2 = alist.CountElements();
            foreach (var k in res2.Keys)
            {
                Console.WriteLine($"{k}:\t{res2[k]} раз(а)");
            }
            Console.WriteLine();


            Dictionary<string, int> dict = new Dictionary<string, int>()
            {
                {"four", 4 },
                {"two", 2 },
                {"one", 1 },
                {"three", 3 },
            };

            var d = dict.OrderBy(p => p.Value);
            foreach (var pair in d)
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }
            Console.ReadKey();


        }
    }
}
