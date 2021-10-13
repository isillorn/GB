using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Logger
    {
        protected string _logfile = $"{Directory.GetCurrentDirectory()}\\asteroids.log";
        protected StreamWriter _fs;

        public Logger()
        {
            //Console.Clear();
            _fs = File.AppendText(_logfile);
            _fs.AutoFlush = true;
            _fs.WriteLine("===== NEW GAME =====");
        }

        public void Log(string text)
        {
            Console.WriteLine(text);
            _fs.WriteLine(text);
        }
    }
}
