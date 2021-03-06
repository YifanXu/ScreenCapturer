﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
            options.Urls.Add($"http://{Environment.MachineName}:80");
            try
            {
                string ip = GetLocalIPAddress();
                DataStatic.address = ip + ":80";
                options.Urls.Add(DataStatic.address);
            }
            catch(Exception)
            {
                
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (WebApp.Start<StartUp>(options))
            {
                Application.Run(new MainForm());
                MainForm.staticInstance.abort = true;
                MainForm.staticInstance.captureThread.Join(5000);
            }
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
