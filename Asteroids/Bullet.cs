﻿using Asteroids.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Bullet : BaseObject
    {
        public Bullet(Point pos, Point dir) : base(pos, dir, new Size(10,10))
        {

        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Resources.bullet, pos.X, pos.Y);
        }

        public override void Update()
        {
            pos.X = pos.X + dir.X;
            if (pos.X > Game.Width)
            {
                Regenerate();
            }
        }

        public override void Regenerate()
        {
            Random rnd = new Random();
            pos.X = 0;
            pos.Y = rnd.Next(0, Game.Height);
            
        }
    }
}