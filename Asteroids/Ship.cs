using Asteroids.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Ship : BaseObject
    {

        protected int _energy = 100;
        private int _lastchange = 0;
        private int _score = 0;

        public event EventHandler DieEvent;
        public event EventHandler ScoreEvent;
        public event EventHandler<EnergyEventArgs> EnergyChangeEvent;

        public int Energy { 
            get {return _energy; } 
        }

        public int Score
        {
            get { return _score; }
        }
        
        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size) { }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Resources.ship150, pos.X, pos.Y);
        }

        public override void Regenerate()
        {
            
        }

        public void Up()
        {
            if (pos.Y - dir.Y > 0)  pos.Y = pos.Y - dir.Y; 
        }

        public void Down()
        {
            if (pos.Y + dir.Y < Game.Height - size.Height) pos.Y = pos.Y + dir.Y;
        }

        public override void Update()
        {
            
        }

        public void Die()
        {
            if (DieEvent != null)
            {
                DieEvent.Invoke(this, new EventArgs());
            }
        }

        internal void EnergyChange(int change)
        {
            _energy += change;
            _lastchange = change;
            EnergyChangeEvent.Invoke(this, new EnergyEventArgs(change));
        }

        internal void ScoreChange(int change)
        {
            _score += change;
            ScoreEvent.Invoke(this, new EventArgs());
        }



    }
}
