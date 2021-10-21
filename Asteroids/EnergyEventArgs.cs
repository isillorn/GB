using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class EnergyEventArgs
    {
        public int EnergyChange { get; set; }

        public EnergyEventArgs (int energyChange)
        {
            EnergyChange = energyChange;
        }

    }
}
