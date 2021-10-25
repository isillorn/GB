using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CoolWork.Data
{
    public class Employee: INotifyPropertyChanged, ICloneable
    {

        private string _firstName;
        private string _secondName;
        private string _lastName;
        private string _phone;
        private string _comment;
        private bool _fired = false;
        private Department _dept;
        private Position _position;

        private void NotifyPropertyChanged([CallerMemberName] string property = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(property));
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public string FirstName {
            get { return _firstName; }
            set { 
                _firstName = value;
                NotifyPropertyChanged();
                    
            }
        }
        public string SecondName
        {
            get { return _secondName; }
            set {
                _secondName = value;
                NotifyPropertyChanged();
            }
        }
        public string LastName
        {
            get { return _lastName; }
            set { 
                _lastName = value;
                NotifyPropertyChanged();
            }
        }
        public string Phone
        {
            get { return _phone; }
            set { 
                _phone = value;
                NotifyPropertyChanged();
            }
        }
        public string Comment
        {
            get { return _comment; }
            set { 
                _comment = value;
                NotifyPropertyChanged();
            }
        }
        public bool Fired
        {
            get { return _fired; }
            set {
                _fired = value;
                NotifyPropertyChanged();
            }
        }
        public Department Dept {
            get { return _dept; }
            set { 
                _dept = value;
                NotifyPropertyChanged();
            } 
        }
        public Position Position
        {
            get { return _position; }
            set { 
                _position = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

       


        public string FIO
        {
            get { return $"{LastName} {FirstName} {SecondName}"; }
        }


        public Employee() { }

        public Employee(string firstName, string secondName, string lastName, string phone, Department dept, Position position, string comment)
        {
            FirstName = firstName;
            SecondName = secondName;
            LastName = lastName;
            Phone = phone;
            Dept = dept;
            Position = position;
            Comment = comment;
        }

        
    }



}
