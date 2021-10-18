using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Asteroids.Properties;
//using System.Windows.Media;

namespace Asteroids
{
    static class Game
    {
        private static BufferedGraphicsContext _ctx;
        public static BufferedGraphics Buffer;

        static List<Asteroid> _asteroids;
        static List<Bullet> _bullets;
        static List<Medikit> _medikits;
        static List<Star> _stars;
        static List<Ufo> _ufo;
        static Ship _ship;

        static Timer timer = new Timer();
        static Logger logger = new Logger();

        private static Random rnd = new Random();
        //private static System.Media.SoundPlayer player = new System.Media.SoundPlayer();
        //private static MediaPlayer player = new MediaPlayer();

        private static Dictionary<string, System.Media.SoundPlayer> GameSounds = new Dictionary<string, System.Media.SoundPlayer>
        {
            ["shot"] = new System.Media.SoundPlayer(Resources.shot),
            ["explosion"] = new System.Media.SoundPlayer(Resources.explosion),
            ["medikit"] = new System.Media.SoundPlayer(Resources.medi),
            ["damage"] = new System.Media.SoundPlayer(Resources.damage),
            ["level"] = new System.Media.SoundPlayer(Resources.level_theme)
        };

        //private static Dictionary<string, System.IO.Stream> GameSounds = new Dictionary<string, System.IO.Stream>
        //{
        //    ["shot"] = Resources.shot,
        //    ["explosion"] = Resources.explosion,
        //    ["medikit"] = Resources.medi,
        //    ["damage"] = Resources.damage,
        //    ["level"] = Resources.level_theme
        //};



        private static int _width;
        private static int _height;

        public static Random Rnd
        {
            get { return rnd; }
        }

        public static int Width
        {
            get
            {
                return _width;
            }
            set
            {
                if (value <= 1000 && value > 0)
                {
                    _width = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }
        public static int Height
        {
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

            //PlaySound("level", true);

        }


        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.Down) _ship.Down();
            if (e.KeyCode == Keys.Space && _bullets.Count < 5)
            {
                PlaySound("shot");
                _bullets.Add(new Bullet(new Point(_ship.Rect.X + _ship.Rect.Width, _ship.Rect.Y + 20), new Point(25, 0)));
            }
        }

        private static void PlaySound(string sound, bool loop = false)
        {
            //System.IO.Stream stream = GameSounds[sound];
            if (loop)
            {
                GameSounds[sound].PlayLooping();
            }
            else
            {
                GameSounds[sound].Play();
            }
           
        }


        private static void Timer_Tick(object sender, EventArgs e)
        {
            Update();
            Draw();
        }

        public static void Load()
        {
            _asteroids = new List<Asteroid>();
            _medikits = new List<Medikit>();
            _bullets = new List<Bullet>();
            _stars = new List<Star>();
            _ufo = new List<Ufo>();
            _ship = new Ship(new Point(0, 300), new Point(0, 10), new Size(50, 50));

            _ship.DieEvent += onDieEvent;
            _ship.EnergyChangeEvent += onEnergyChangeEvent;
            _ship.ScoreEvent += onScoreEvent;

            ReloadGameObjects();

            

            for (int i = 0; i <= 30; i++)
            {
                var pos = new Point(rnd.Next(0, Width), rnd.Next(0, Height));
                _stars.Add(new Star(pos, rnd));
            }

            for (int i = 0; i <= 2; i++)
            {
                var pos = new Point(rnd.Next(0, Width - 100), rnd.Next(0, Height - 100));
                var dir = new Point(rnd.Next(-10, 10), rnd.Next(-10, 10));
                _ufo.Add(new Ufo(pos, dir, rnd));
            }
        }

        private static void onScoreEvent(object sender, EventArgs e)
        {
            logger.Log("Ship hits asteroid");
        }

        private static void onEnergyChangeEvent(object sender, EnergyEventArgs e)
        {
            if (e.EnergyChange > 0)
            {
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
            Buffer.Graphics.DrawString("Game Over", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Bold), Brushes.Red, Width / 2 - 100, Height / 2 - 50);
            Buffer.Render();

        }


        public static void ReloadGameObjects()
        {

            _asteroids.Clear();
            _medikits.Clear();

            for (int i = 1; i <= 15; i++)
            {
                var size = rnd.Next(0, 5);
                var pos = new Point(rnd.Next(0, Width - (size + 1) * 10), rnd.Next(0, Height - (size + 1) * 10));
                var dir = new Point(rnd.Next(-10, 10), rnd.Next(-10, 10));
                _asteroids.Add(new Asteroid(pos, dir, size));
            }

            for (int i = 1; i <= 10; i++)
            {
                var pos = new Point(rnd.Next(0, Width - 25), rnd.Next(0, Height - 25));
                var dir = new Point(rnd.Next(-10, 10), rnd.Next(-10, 10));
                _medikits.Add(new Medikit(pos, dir));
            }
        }

        public static void Update()
        {

            if (_asteroids.Count <= 0) ReloadGameObjects();

            for (int i = _bullets.Count - 1; i >= 0; i--)
            {
                _bullets[i].Update();
                if (_bullets[i].Rect.X > Width) _bullets.Remove(_bullets[i]);
            }

            foreach (var star in _stars) star.Update();
            foreach (var ufo in _ufo) ufo.Update();

            for (int i = _asteroids.Count - 1; i >= 0; i--)
            {
                var isDestructed = false;
                _asteroids[i].Update();

                for (int j = _bullets.Count - 1; j >= 0; j--)
                {
                    if (_asteroids[i].Collision(_bullets[j]))
                    {
                        PlaySound("explosion");
                        _ship.ScoreChange(1);
                        _asteroids.Remove(_asteroids[i]);
                        _bullets.Remove(_bullets[j]);
                        isDestructed = true;
                        break;
                    }
                }

                if (!isDestructed && _asteroids[i].Collision(_ship))
                {
                    PlaySound("damage");
                    _ship.EnergyChange(rnd.Next(-30, -10));
                    _asteroids.Remove(_asteroids[i]);
                    if (_ship.Energy <= 0)
                        _ship.Die();
                }
            }

            for (int i = _medikits.Count - 1; i >= 0; i--)
            {
                _medikits[i].Update();

                for (int j = _bullets.Count - 1; j >= 0; j--)
                {
                    if (_medikits[i].Collision(_bullets[j]))
                    {
                        PlaySound("medikit");
                        _ship.EnergyChange(_medikits[i].Health);
                        _ship.ScoreChange(1);
                        _medikits.Remove(_medikits[i]);
                        _bullets.Remove(_bullets[j]);
                        break;
                    }

                }
            }
        }


        public static void Draw()
        {
            Buffer.Graphics.DrawImage(Resources.back, new Point(0, 0));

            foreach (var star in _stars) star.Draw();
            foreach (var ufo in _ufo) ufo.Draw();

            Buffer.Graphics.DrawImage(Resources.saturn, new Point(100, 100));


            foreach (var medikit in _medikits) medikit.Draw();
            foreach (var asteroid in _asteroids) asteroid.Draw();
            foreach (var bullet in _bullets) bullet.Draw();

            if (_ship != null)
            {
                _ship.Draw();
                Buffer.Graphics.DrawString($"Energy: {_ship.Energy}", new Font(FontFamily.GenericSansSerif, 16, FontStyle.Bold), Brushes.Cyan, Width - 300, 0);
                Buffer.Graphics.DrawString($"Score: {_ship.Score}", new Font(FontFamily.GenericSansSerif, 16, FontStyle.Bold), Brushes.Yellow, Width - 150, 0);
            }

            Buffer.Render();
        }
    }
}



