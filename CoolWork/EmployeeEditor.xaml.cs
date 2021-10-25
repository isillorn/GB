using CoolWork.Data;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace CoolWork
{
    /// <summary>
    /// Interaction logic for EmployeeEditor.xaml
    /// </summary>
    public partial class EmployeeEditor : Window
    {
        public Employee Employee { get; set; } = new Employee();

        public EmployeeEditor()
        {
            InitializeComponent();
            employeeControl.Employee = Employee;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            //employeeControl.UpdateContact();
            DialogResult = true;
        }
    }
}
