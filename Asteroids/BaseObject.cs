using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    abstract class BaseObject: ICollision
    {
        protected Point pos;
        protected Point dir;
        protected Size size;

        public BaseObject(Point pos, Point dir, Size size)
        {
            this.pos = pos;
            this.dir = dir;
            this.size = size;
        }

        public Rectangle Rect
        {
            get
            {
                return new Rectangle(pos,size);
            }
        }

        public bool Collision(ICollision obj)
        {
            return obj.Rect.IntersectsWith(Rect);
        }

        public abstract void Draw();
        public abstract void Update();



    }
}
