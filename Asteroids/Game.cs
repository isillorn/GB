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
        static BaseObject[] _asteroids;
        static BaseObject[] _stars;
        static BaseObject[] _ufo;
        static Bullet _bullet;
        static Ship _ship;
        static Timer timer = new Timer();
        static Logger logger = new Logger();

        private static int _width;
        private static int _height;

        public static int Width {
            get
            {
                return _width;
            }
            set
            {
                if (value <= 1000 && value > 0)
                {
                    _width = value;
                } else { 
                    throw new ArgumentOutOfRangeException();
                }
            }
        }
        public static int Height {
            get
            {
                return _height;
            }
            set
            {
                if (value <= 1000 && value > 0)
                {
                    _height = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        public static void Init(Form form)
        {
            _ctx = BufferedGraphicsManager.Current;
            Graphics g = form.CreateGraphics();

            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;

            Buffer = _ctx.Allocate(g, new Rectangle(0, 0, Width, Height));

            Load();

            //Timer timer = new Timer();
            timer.Interval = 50;
            timer.Tick += Timer_Tick;
            timer.Start();

        }

        private static void Play()
        {
            System.IO.Stream stream = Resources.explosion;
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(stream);
            player.Play();
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Update();
            Draw();
        }

        public static void Load()
        {
            Random rnd = new Random();
            _asteroids = new BaseObject[10];
            _stars = new BaseObject[50];
            _ufo = new BaseObject[3];
            _bullet = new Bullet(new Point(0,rnd.Next(0,Height)), new Point(5, 0));

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

        private static void onScoreEvent(object sender, EventArgs e)
        {
            logger.Log("Ship hits asteroid");
        }

        private static void onEnergyChangeEvent(object sender, EnergyEventArgs e)
        {
            if (e.EnergyChange > 0) {
                logger.Log($"Ship recieved health +{e.EnergyChange}");
            }
            if (e.EnergyChange < 0)
            {
                logger.Log($"Ship got damage {e.EnergyChange}");
            }
        }

        private static void onDieEvent(object sender, EventArgs e)
        {
            timer.Stop();
            logger.Log("Ship destroyed");
            Buffer.Graphics.DrawString("Game Over", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Bold), Brushes.Red, Width/2 - 100, Height/2 - 50);
            Buffer.Render();
            
        }

        public static void Update()
        {
            //foreach (var asteroid in _asteroids)
            for (int i = 0; i < _asteroids.Length; i++)
            {
                asteroid.Update();
                if (asteroid.Collision(_bullet))
                {
                    Play();
                    _bullet.Regenerate();
                    asteroid.Regenerate();

                }

            }

            foreach (var star in _stars)
            {
                star.Update();
            }

            foreach (var ufo in _ufo)
            {
                ufo.Update();
            }

            _bullet.Update();
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
                if (asteroid != null) asteroid.Draw();
            }

            foreach (var ufo in _ufo)
            {
                ufo.Draw();
            }

            _bullet.Draw();

            Buffer.Render();
        }
    }
}
