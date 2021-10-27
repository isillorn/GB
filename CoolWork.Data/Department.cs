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
        private int _id;

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyPropertyChanged();
            }
        }


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

        internal Department() { }

        public Department(string deptName)
        {
            _deptname = deptName;
        }

        public Department(int id, string deptName)
        {
            _id = id;
            _deptname = deptName;
        }


    }
}
