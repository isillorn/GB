using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Asteroids.Properties;

namespace Asteroids
{
    static class Game
    {
        private static BufferedGraphicsContext _ctx;
        public static BufferedGraphics Buffer;
        //static Image background;
        static Asteroid[] _asteroids;
        static Star[] _stars;
        static Ufo[] _ufo;

        public static int Width { get; set; }
        public static int Height { get; set; }

        public static void Init(Form form)
        {
            _ctx = BufferedGraphicsManager.Current;
            Graphics g = form.CreateGraphics();

            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;

            Buffer = _ctx.Allocate(g, new Rectangle(0, 0, Width, Height));

            Load();

            Timer timer = new Timer();
            timer.Interval = 50;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Update();
            Draw();
        }

        public static void Load()
        {
            Random rnd = new Random();
            _asteroids = new Asteroid[10];
            _stars = new Star[50];
            _ufo = new Ufo[3];

            for (int i=0; i < _asteroids.Length; i++)
            {
                var size = rnd.Next(0, 5);
                var pos = new Point(rnd.Next(0, Width - (size + 1) *10), rnd.Next(0, Height - (size +1 )*10));
                var dir = new Point(rnd.Next(0,10), rnd.Next(0, 10));
                _asteroids[i] = new Asteroid(pos, dir, size);
            }

            for (int i = 0; i < _stars.Length; i++)
            {
                var pos = new Point(rnd.Next(0, Width), rnd.Next(0, Height));
                _stars[i] = new Star(pos, rnd);
            }

            for (int i = 0; i < _ufo.Length; i++)
            {
                var pos = new Point(rnd.Next(0, Width-100), rnd.Next(0, Height-100));
                var dir = new Point(rnd.Next(0, 10), rnd.Next(0, 10));
                _ufo[i] = new Ufo(pos, dir, rnd);
            }
        }

        public static void Update()
        {
            foreach (var asteroid in _asteroids)
            {
                asteroid.Update();
            }

            foreach (var star in _stars)
            {
                star.Update();
            }

            foreach (var ufo in _ufo)
            {
                ufo.Update();
            }
        }


        public static void Draw()
        {
            //Buffer.Graphics.Clear(Color.Black);
            
            Buffer.Graphics.DrawImage(Resources.back, new Point(0, 0));

            foreach (var star in _stars)
            {
                star.Draw();
            }
            
            Buffer.Graphics.DrawImage(Resources.saturn, new Point(100,100));

            foreach (var asteroid in _asteroids)
            {
                asteroid.Draw();
            }

            foreach (var ufo in _ufo)
            {
                ufo.Draw();
            }

            Buffer.Render();
        }
    }
}
