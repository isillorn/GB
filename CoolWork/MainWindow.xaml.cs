using CoolWork.Controls;
using CoolWork.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CoolWork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DepartmentDatabase departmentDatabase = new DepartmentDatabase();
        private PositionDatabase positionDatabase = new PositionDatabase();
        private EmployeeDatabase employeeDatabase; //= new EmployeeDatabase();

        public ObservableCollection<Employee> EmployeeList { get; set; }
        public ObservableCollection<Position> PositionList { get; set; }
        public ObservableCollection<Department> DepartmentList { get; set; }
        public Employee SelectedEmployee {get; set; }


        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            DepartmentList = departmentDatabase.Departments;
            PositionList = positionDatabase.Positions;
            employeeDatabase = new EmployeeDatabase(DepartmentList, PositionList);

            employeeControl.PositionDB = positionDatabase;
            employeeControl.DepartmentDB = departmentDatabase;

            EmployeeList = employeeDatabase.Employees;
        }

        private void lvEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                employeeControl.Employee = (Employee)SelectedEmployee.Clone();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedEmployee == null) return;

            if (employeeDatabase.Update(employeeControl.Employee) > 0)
            {
                MessageBox.Show("Данные работника изменены", "", MessageBoxButton.OK, MessageBoxImage.Information);
                EmployeeList[EmployeeList.IndexOf(SelectedEmployee)] = employeeControl.Employee;
            }
        }

        
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedEmployee == null) return;

            if (MessageBox.Show("Вы уверены?", "Удаление контакта", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes) {
                if (employeeDatabase.Remove(employeeControl.Employee) > 0)
                {
                    MessageBox.Show("Данные работника удалены", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    employeeDatabase.Employees.Remove(SelectedEmployee);
                    
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            EmployeeEditor employeeEditor = new EmployeeEditor(positionDatabase, departmentDatabase);
            //employeeEditor.DepartmentList = DepartmentList;
            //employeeEditor.PositionList = PositionList;
            if (employeeEditor.ShowDialog() == true)
            {
                var id = employeeDatabase.Add(employeeEditor.Employee);
                if (id > 0) 
                {
                    employeeEditor.Employee.Id = id;
                    MessageBox.Show("Работник добавлен", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    //employeeDatabase.Employees.Add(employeeEditor.Employee);
                }
            }
        }
    }
}
