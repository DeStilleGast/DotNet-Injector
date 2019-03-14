using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Threading;

namespace DotNet_Injector
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
            Application.Run(new frmMain());
        }

        [STAThread]
        public static void TakeTwo()
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                Thread thread = new Thread(delegate ()
                {
                    Application.Run(new frmLoader());
                });
                thread.Priority = ThreadPriority.AboveNormal;
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.IsBackground = true;
            }, null);
        }
    }
}
