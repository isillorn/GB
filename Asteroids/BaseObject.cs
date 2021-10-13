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
            if (size.Height > 100 | size.Height < 0 | size.Width >100 | size.Height < 0)
            {
                throw new GameObjectException($"Incorrect object size ({size.Width},{size.Height})");
            }

            if ( dir.X > 50 | dir.X < -50 | dir.Y > 50 | dir.Y < -50 ) {
                throw new GameObjectException($"Incorrect object speed ({dir.X},{dir.Y})");
            }

            if (pos.X < 0 | pos.X > Game.Width - size.Width | pos.Y < 0 | pos.Y > Game.Height - size.Height)
            {
                throw new GameObjectException($"Incorrect object position ({pos.X},{pos.Y})");
            }

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
        public abstract void Regenerate();


    }
}
