using CoolWork.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace CoolWork.Controls
{
    
    public partial class EmployeeControl : UserControl, INotifyPropertyChanged
    {

        private Employee _employee;

        //public ObservableCollection<Position> PositionList { get; set; }//  = new ObservableCollection<Position>();
        //public ObservableCollection<Department> DepartmentList { get; set; }// = new ObservableCollection<Department>();

        public DepartmentDatabase DepartmentDB { get; set; }
        public PositionDatabase PositionDB { get; set; }


        public Employee Employee
        {
            get { return _employee; }
            set
            {
                _employee = value;
                NotifyPropertyChanged();

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string property = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(property));
            }
        }

        public EmployeeControl()
        {
            InitializeComponent();
            DataContext = this;

            //PositionList = PositionDatabase.Get();
            //DepartmentList = DepartmentDatabase.Get();
        }

        private void btnAddPos_Click(object sender, RoutedEventArgs e)
        {
            AddEntity addEntity = new AddEntity();
            if (addEntity.ShowDialog() == true)
            {
               PositionDB.Add(new Position(addEntity.Entity));
                
            }
        }

        private void btnAddDept_Click(object sender, RoutedEventArgs e)
        {
            AddEntity addEntity = new AddEntity();
            if (addEntity.ShowDialog() == true)
            {
                DepartmentDB.Add(new Department(addEntity.Entity));
            }
        }
    }
}
