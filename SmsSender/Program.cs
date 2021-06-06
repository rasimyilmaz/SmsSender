using Microsoft.Owin.Hosting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
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
            using (WebApp.Start<Startup>("http://localhost:12345"))
            {
                Console.WriteLine("Web Server is running.");
                Console.WriteLine("Press any key to quit");
                Console.ReadLine();
            }
        }
        void SendSms(string phonenumber,string message)
        {
            IWebDriver driver;
            driver = new ChromeDriver();
            driver.Url = "https://www.google.com";
            IWebElement element = driver.FindElement(By.XPath("/html/body/div[1]/div[3]/form/div[1]/div[1]/div[1]/div/div[2]/input"));
            element.SendKeys(phonenumber);
            element.Clear();
            element.SendKeys(message);
            driver.Close();
            driver.Quit();
        }
    }
}
