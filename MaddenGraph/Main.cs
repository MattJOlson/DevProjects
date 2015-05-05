using System;
using Gtk;

namespace MaddenGraph
{
    class MaddenGraph
    {
        public static void Main (string[] args)
        {
            Application.Init ();
            MainWindow win = new MainWindow ();

            win.Show ();
            Application.Run ();
        }
    }
}
