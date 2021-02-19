using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserManagementSystem.Forms;

namespace UserManagementSystem
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
<<<<<<< HEAD
            Application.Run(new ViewUsers());
=======
            Application.Run(new ViewRoles());
>>>>>>> 7437642c53b34f076c6c279e8f06b8e3988b428f
        }
    }
}
