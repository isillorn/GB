using Asteroids.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Ufo : Asteroid
    {
        private Random rnd;

        public Ufo(Point pos, Point dir, Random rnd) : base(pos, dir, 0)
        {
            this.rnd = rnd;
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Resources.ufo, pos.X, pos.Y);
        }

        public override void Update()
        {
            //bool needTeleport = false;
            bool needTeleport = rnd.Next(0, 1000) > 995;
            bool needChangeDir = rnd.Next(0, 100) > 97;

            if (needTeleport)
            {
                pos.X = rnd.Next(0, Game.Width - 100);
                pos.Y = rnd.Next(0, Game.Height - 100);
                dir.X = rnd.Next(-5, 5);
                dir.Y = rnd.Next(-5, 5);
            }
            else if (needChangeDir)
            {
                dir.X = rnd.Next(-5, 5);
                dir.Y = rnd.Next(-5, 5);
            } 
            else { 
                pos.X = pos.X + dir.X;
                pos.Y = pos.Y + dir.Y;

                if (pos.X < 0 | pos.X > (Game.Width - 100))
                {
                    dir.X = -dir.X;
                }

                if (pos.Y < 0 | pos.Y > (Game.Height - 100))
                {
                    dir.Y = -dir.Y;
                }

            }
         
        }
    }
}
