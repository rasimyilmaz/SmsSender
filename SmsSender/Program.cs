using Microsoft.Owin.Hosting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            try
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                driver.Url = "http://m.home/index.html#sms";
                IWebElement newSms = WaitUntilElementClickable(By.Id("smslist-new-sms"), 30);
                newSms.Click();
                IWebElement phone = WaitUntilElementClickable(By.Id("chosen-search-field-input"));
                phone.Click();
                phone.SendKeys(request.phonenumber);
                phone.SendKeys(Keys.Return);
                IWebElement chat = WaitUntilElementClickable(By.Id("chat-input"));
                chat.SendKeys(request.message);
                IWebElement sendButton = WaitUntilElementClickable(By.Id("btn-send"));
                sendButton.Click();
                sendButton = WaitUntilElementClickable(By.Id("btn-send"),30);
                IWebElement deleteButton = WaitUntilElementClickable(By.ClassName("smslist-item-delete"));
                deleteButton.Click();
                IWebElement sureButton = WaitUntilElementClickable(By.Id("yesbtn"));
                sureButton.Click();
                sendButton = WaitUntilElementClickable(By.Id("btn-send"), 30);
                stopwatch.Stop();
                Console.WriteLine("Total processing time :" + stopwatch.ElapsedMilliseconds.ToString());
            }
            catch ( Exception Ex)
            {
                Console.WriteLine(Ex.Message);
            }
            
        }
        public static void DeleteAllMessages()
        {
            driver.Url = "";
            IWebElement checkAll = WaitUntilElementClickable(By.Id("checkbox-all"), 30);
            checkAll.Click();


        }
        public static IWebElement WaitUntilElementClickable(By elementLocator, int timeout = 10)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(ExpectedConditions.ElementToBeClickable(elementLocator));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with locator: '" + elementLocator + "' was not found in current context page.");
                throw;
            }
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
