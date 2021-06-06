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
        static IWebDriver driver;
        public static void Send(SmsRequest request)
        {
            driver.Url = "https://www.google.com";
            IWebElement element = driver.FindElement(By.XPath("/html/body/div[1]/div[3]/form/div[1]/div[1]/div[1]/div/div[2]/input"));
            element.SendKeys(request.phonenumber);
            element.Clear();
            element.SendKeys(request.message);
        }
        static void Main(string[] args)
        {
            driver = new ChromeDriver();
            using (WebApp.Start<Startup>("http://localhost:12345"))
            {
                Console.WriteLine("Web Server is running.");
                Console.WriteLine("Press any key to quit");
                Console.ReadLine();
            }
            driver.Quit();
        }
        
    }
}
