using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asteroids.Properties;

namespace Asteroids
{
    class Asteroid
    {
        protected Point pos;
        protected Point dir;
        protected int size;

        protected Image[] images = new Image[] { Resources.asteroid1, Resources.asteroid2, Resources.asteroid3, Resources.asteroid4, Resources.asteroid5 };

        public Asteroid(Point pos, Point dir, int size)
        {
            this.pos = pos;
            this.dir = dir;
            this.size = size;
        }

        public virtual void Draw()
        {
            Game.Buffer.Graphics.DrawImage(images[size], pos.X, pos.Y);
        }

        public virtual void Update()
        {
            pos.X = pos.X + dir.X;
            pos.Y = pos.Y + dir.Y;

            if (pos.X < 0 | pos.X > Game.Width - (size + 1)*10)
            {
                dir.X = -dir.X;
            }

            if (pos.Y < 0 | pos.Y > Game.Height - (size + 1)*10)
            {
                dir.Y = -dir.Y;
            }
        }
    }
}
