using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace ProcessorsSimulator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Manager manager = new Manager();
            Thread MySystem = new Thread(new
                ThreadStart(manager.manage));
            MySystem.Start();
            Application.Run(new Main());
            
            
        }
    }
}
