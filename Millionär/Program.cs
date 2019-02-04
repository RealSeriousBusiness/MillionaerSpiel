using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Millionär
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if(args.Length > 0)
                if(args[0].Equals("1"))
                    bigscreen = true;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Intro());
        }

        public static bool bigscreen = false;
    }
}
