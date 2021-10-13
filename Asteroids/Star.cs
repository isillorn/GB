using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Star : BaseObject
    {
        protected int color;
        protected Pen[] penColors = new Pen[] { Pens.Transparent, Pens.Gray, Pens.LightGray, Pens.White, Pens.LightBlue };
        //protected Pen[] penColors = new Pen[] { Pens.Gray, Pens.LightBlue, Pens. };
        private Random rnd; // используем общий рандомайзер, иначе выдает одинаковые последовательности

        public Star(Point pos, Random rnd) : base(pos, new Point(0, 0), new Size(0, 0))
        {
            this.rnd = rnd;
            color = 0;
        }

        public override void Draw()
        {
            Point[] points = new Point[] { new Point(pos.X - 1, pos.Y), new Point(pos.X, pos.Y - 1), new Point(pos.X + 1, pos.Y), new Point(pos.X, pos.Y + 1) };
            Game.Buffer.Graphics.DrawPolygon(penColors[color], points);
        }

        public override void Regenerate()
        {
            //throw new NotImplementedException();
        }

        public override void Update()
        {
            int changeColor = rnd.Next(0, 100); // регулируем скорость мерцания
            if (changeColor < 33)
            {
                color = rnd.Next(0, penColors.Length);
            }
        }
    }
}