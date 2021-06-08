using Microsoft.Owin.Hosting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Owin;

namespace SmsSender
{
    class Program
    {
        
        static void Main(string[] args)
        {

            File.AppendAllText("WriteLines.txt", DateTime.Now.ToString("HH:mm:ss.ffffff") + " - " + System.Threading.Thread.CurrentThread.ManagedThreadId.ToString() + " : Program Main called \n");
            Controller controller = new Controller();
                string input = "";
                while (input != "-1")
                {
                    Console.WriteLine("1 to Run , 0 to Stop, -1 to Exit");
                    input = Console.ReadLine();
                    if (input == "1")
                    {
                        if (controller.isRunning)
                        {
                            Console.WriteLine("Web Server is already running.");
                        } else
                        {
                            controller.Start();
                            Console.WriteLine("Web Server is running.");
                        }
                    } else if (input == "0")
                    {
                        if (!controller.isRunning)
                        {
                            Console.WriteLine("Web Server is not running.");
                        }
                        else
                        {
                            controller.Stop();
                            Console.WriteLine("Web Server is stopped.");
                        }
                    } else if (input == "-1")
                    {
                        if (controller.isRunning)
                        {
                            controller.Stop();
                            Console.WriteLine("Web Server is stopped.");
                        }
                    }
                }
            controller = null;
        }
    }
}
