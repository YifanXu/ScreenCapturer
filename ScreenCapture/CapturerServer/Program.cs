using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Owin.Hosting;

namespace CapturerServer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var options = new StartOptions();
            options.Urls.Add("http://127.0.0.1:80");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (WebApp.Start<StartUp>(options))
            {
                Application.Run(new MainForm());
                MainForm.staticInstance.abort = true;
                MainForm.staticInstance.captureThread.Join(5000);
            }
        }
    }
}
