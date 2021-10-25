using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CoolWork.Data
{
    public class Department: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _deptname;


        public string DeptName
        {
            get { return _deptname; }
            set
            {
                _deptname = value;
                NotifyPropertyChanged();
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string property = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(property));
            }
        }

        public Department(string deptName)
        {
            _deptname = deptName;
        }


    }
}
