using System;
using System.Collections.Generic;
using System.Text;

namespace Employee
{
    static class Program
    {
        
        static void Main()
        {
            var Employees = new BaseEmployee[]
            {
                new FixedWorker("Ivan",25,1000),
                new FixedWorker("ALex", 33, 2000),
                new FixedWorker("Peter", 30, 1800),
                new FixedWorker("Sam", 27, 1300),
                new HourlyWorker("Max", 19, 10.5),
                new HourlyWorker("Stas", 45, 9.1)
            };

            Array.Sort(Employees);

            foreach (var e in Employees) {
                Console.WriteLine(String.Format("{0} - {1}  - {2}",e.Name, e.Age, e.Calculate()));
            }
        }
    }
}
