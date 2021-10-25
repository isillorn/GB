using CoolWork.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolWork
{
    class EmployeeDatabase
    {
        private static string[] FNames = { "Иван", "Петр", "Илья", "Максим", "Николай", "Алексей", "Виталий", "Сергей", "Константин", "Василий" };
        private static string[] SNames = { "Иванович", "Петрович", "Ильич", "Максимович", "Николаевич", "Алексеевич", "Витальевич", "Сергеевич", "Константинович", "Васильевич" };
        private static string[] LNames = { "Иванов", "Петров", "Ильин", "Максимов", "Николаев", "Алексеев", "Витальев", "Сергеев", "Константинов", "Васильев" };

        private ObservableCollection<Department> _departments;
        private ObservableCollection<Position> _positions;

        private Random rnd = new Random();
        public ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();

        public EmployeeDatabase(ObservableCollection<Department> departments, ObservableCollection<Position> positions)
        {
            //Employees = new ObservableCollection<Employee>();
            _departments = departments;
            _positions = positions;
            GenerateEmployees(50);
        }

        private void GenerateEmployees(int amount)
        {
            for (var i = 0; i < amount; i++)
            {
                Employees.Add(new Employee(FNames[rnd.Next(9)], SNames[rnd.Next(9)], LNames[rnd.Next(9)], $"+7 499 {rnd.Next(100,999)}-{rnd.Next(1000,9999)}", _departments[rnd.Next(5)], _positions[rnd.Next(5)], ""));
            } 
        }
    }



}
