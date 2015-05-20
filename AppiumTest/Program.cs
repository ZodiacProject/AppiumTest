using System;
using System.Collections.Generic;
using System.Linq;
using Selenium;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium.Android;

namespace AppiumTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driver;
            DesiredCapabilities capability = new DesiredCapabilities();
            driver = new RemoteWebDriver(new Uri("http://localhost:8080/wd/hub"), capability);
            driver.Navigate().GoToUrl("http://putlocker.is");
            Console.WriteLine(driver.Title);
            Thread.Sleep(2000);
            driver.SwitchTo().ActiveElement().Click();
            Thread.Sleep(4000);
            driver.Close();
        }
    }
}
