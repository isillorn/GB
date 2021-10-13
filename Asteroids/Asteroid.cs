using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asteroids.Properties;

namespace Asteroids
{
    class Asteroid : BaseObject
    {
        protected Image[] images = new Image[] { Resources.asteroid1, Resources.asteroid2, Resources.asteroid3, Resources.asteroid4, Resources.asteroid5 };
        protected Image image;

        public Asteroid(Point pos, Point dir, int size) : base(pos, dir, new Size(size * 10, size * 10))
        {
            //this.pos = pos;
            //this.dir = dir;
            //this.size = size;
            image = images[size];
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, pos.X, pos.Y);
        }

        public override void Regenerate()
        {
            Random rnd = new Random();
            pos.X = rnd.Next(0, Game.Width - size.Width);
            pos.Y = rnd.Next(0, Game.Height - size.Height);
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