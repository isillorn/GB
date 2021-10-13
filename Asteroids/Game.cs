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

        private static  Random rnd = new Random();

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

            form.KeyDown += Form_KeyDown;
            
        }

        
        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.Down) _ship.Down();
            if (e.KeyCode == Keys.Space && _bullet == null) _bullet = new Bullet(new Point(_ship.Rect.X + _ship.Rect.Width, _ship.Rect.Y + 20), new Point(25, 0));
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
            _asteroids = new BaseObject[10];
            _stars = new BaseObject[50];
            _ufo = new BaseObject[3];
            _ship = new Ship(new Point(0, 300), new Point(0,10), new Size(50,50));

            _ship.DieEvent += onDieEvent;
            _ship.EnergyChangeEvent += onEnergyChangeEvent;
            _ship.ScoreEvent += onScoreEvent;

            for (int i=0; i < _asteroids.Length; i++)
            {
                var size = rnd.Next(0, 5);
                var pos = new Point(rnd.Next(0, Width - (size + 1) *10), rnd.Next(0, Height - (size +1 )*10));
                var dir = new Point(rnd.Next(-10,10), rnd.Next(-10, 10));
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
                var dir = new Point(rnd.Next(-10, 10), rnd.Next(-10, 10));
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
                if (_asteroids[i] == null) continue;

                _asteroids[i].Update();

                if (_bullet != null && _asteroids[i].Collision(_bullet))
                {
                    Play();
                    _bullet = null;
                    _asteroids[i] = null;
                    _ship.ScoreChange(1);
                    continue;
                }

                if (_ship != null && _asteroids[i].Collision(_ship))
                {
                    _ship.EnergyChange(rnd.Next(-30, -10));
                    // Play sound
                    _asteroids[i] = null;
                    if (_ship.Energy <= 0)
                        _ship.Die();
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

            if (_bullet != null)
            {
                _bullet.Update();
                if (_bullet.Rect.X > Width) _bullet = null;
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
                if (asteroid != null) asteroid.Draw();
            }

            foreach (var ufo in _ufo)
            {
                ufo.Draw();
            }

            if (_bullet != null ) _bullet.Draw();
            if (_ship != null)
            {
                _ship.Draw();
                Buffer.Graphics.DrawString($"Energy: {_ship.Energy}", new Font(FontFamily.GenericSansSerif,16,FontStyle.Bold), Brushes.Cyan, Width - 300,0);
                Buffer.Graphics.DrawString($"Score: {_ship.Score}", new Font(FontFamily.GenericSansSerif, 16, FontStyle.Bold), Brushes.Yellow, Width - 150, 0);
            }

            Buffer.Render();
        }
    }
}
