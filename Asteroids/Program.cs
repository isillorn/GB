using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asteroids
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Form form = new Form();
            form.MaximumSize = new System.Drawing.Size(800, 600);
            form.MinimumSize = new System.Drawing.Size(800, 600);
            form.MaximizeBox = false;
            form.MinimizeBox = false;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Text = "Asteroids 0.1b";

            Game.Init(form);
            Application.Run(form);
        }
    }
}
