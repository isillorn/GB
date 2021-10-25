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
        private PositionDatabase positiontDatabase = new PositionDatabase();
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
            PositionList = positiontDatabase.Positions;
            employeeDatabase = new EmployeeDatabase(DepartmentList, PositionList);

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

            EmployeeList[EmployeeList.IndexOf(SelectedEmployee)] = employeeControl.Employee;
            //employeeControl.UpdateContact();
            //UpdateBinding();

        }

        
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedEmployee == null) return;

            if (MessageBox.Show("Вы уверены?", "Удаление контакта", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes) {
                employeeDatabase.Employees.Remove(SelectedEmployee);
            }

            //UpdateBinding();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            EmployeeEditor employeeEditor = new EmployeeEditor();
            if (employeeEditor.ShowDialog() == true)
            {
                employeeDatabase.Employees.Add(employeeEditor.Employee);
                //UpdateBinding();
            }

        }


        //private void UpdateBinding()
        //{
        //    lvEmployee.ItemsSource = null;
        //    lvEmployee.ItemsSource = employeeDatabase.Employees;
        //}

        
    }
}
