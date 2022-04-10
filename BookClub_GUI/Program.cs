using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data.MySqlClient;

namespace BookClub_GUI
{
    static class Program
    {
        
        public static Befizetés befizetes = null;
        [STAThread]

        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            befizetes = new Befizetés();
            
            Application.Run(befizetes);
        }
    }
}
