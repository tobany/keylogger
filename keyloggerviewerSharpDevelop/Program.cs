using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace keyloggerviewer
{
    static class Program
    {
        public static DbLink connection = null;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread] 
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try {
            	Application.Run(new Form1());
            } catch (Exception e) {
            	//Console.WriteLine(e);
            }

        }
    }
}