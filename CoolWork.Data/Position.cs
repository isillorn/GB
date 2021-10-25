using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CoolWork.Data
{
    public class Position : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _positionName;

        public string PositionName
        {
            get { return _positionName; }
            set
            {
                _positionName = value;
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

        public Position(string positionName)
        {
            _positionName = positionName;
        }


    }





}
