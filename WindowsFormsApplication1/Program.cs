using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new Suche());
            }
            catch (Exception)
            {
                MessageBox.Show("Upps, das hätte nicht passieren dürfen\nBitte starte das Programm neu!!", "Suche",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            
        }
    }
}
