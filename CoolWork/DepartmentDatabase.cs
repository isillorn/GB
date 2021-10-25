using CoolWork.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolWork
{
    class DepartmentDatabase
    {
        public ObservableCollection<Department> Departments { get; set; } = new ObservableCollection<Department>();
        
        public DepartmentDatabase()
        {
            Departments.Add(new Department("Технический"));
            Departments.Add(new Department("ИТ"));
            Departments.Add(new Department("Юридический"));
            Departments.Add(new Department("Финансовый"));
            Departments.Add(new Department("Административный"));
        }



    }
}
