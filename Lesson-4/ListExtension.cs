using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_4
{
    public static class ListExtension
    {
        public static Dictionary<int, int> CountElements(this List<int> list) {

            Dictionary<int, int> d = new Dictionary<int, int>();
            foreach (var i in list)
            {
                d[i] = !d.ContainsKey(i) ? 1 : d[i] + 1;
            }
            return d;
        }

        public static Dictionary<Object?, int> CountElements(this ArrayList list)
        {
   
            Dictionary<Object, int> d = new Dictionary<Object, int>();
            foreach (var o in list)
            {
                d[o] = !d.ContainsKey(o) ? 1 : d[o] + 1;
            }
            return d;
        }



    }
}
