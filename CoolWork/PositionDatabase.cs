using CoolWork.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolWork
{
    class PositionDatabase
    {


        public ObservableCollection<Position> Positions { get; set; } = new ObservableCollection<Position>();

        public PositionDatabase()
        {
            Positions.Add(new Position("Менеджер"));
            Positions.Add(new Position("Инженер"));
            Positions.Add(new Position("Специалист"));
            Positions.Add(new Position("Руководитель"));
            Positions.Add(new Position("Аналитик"));
        }


    }
}
