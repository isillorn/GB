using Asteroids.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    

    class Medikit: BaseObject
    {
        protected int _health;

        public int Health
        {
            get { return _health; }
        }


        public Medikit(Point pos, Point dir): base(pos, dir, new Size(50, 50))
        {
            //Random rnd = new Random();
            _health = Game.Rnd.Next(10, 30);
        }

        
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Resources.medikit, pos.X, pos.Y);
        }

        public override void Regenerate()
        {
            
        }

        public override void Update()
        {
            pos.X = pos.X + dir.X;
            pos.Y = pos.Y + dir.Y;

            if (pos.X < 0 | pos.X > Game.Width - size.Width)
            {
                dir.X = -dir.X;
            }

            if (pos.Y < 0 | pos.Y > Game.Height - size.Height)
            {
                dir.Y = -dir.Y;
            }
        }
    }
}
