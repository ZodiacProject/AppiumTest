using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium.Android;

namespace AppiumTest
{

  public class AppiumDriver
    {
      public IWebDriver driver;
       public void Setup()
       {
           DesiredCapabilities capabilites = new DesiredCapabilities();
           capabilites.SetCapability("device", "Android");          
           capabilites.SetCapability("deviceName", "HTC One mini 2");
           driver = new AndroidDriver(new Uri("http://127.0.0.1:4723/wd/hub"), capabilites, TimeSpan.FromSeconds(380));
       }

      public void OpenHofHomePage()
       {
           IWebElement qStr = driver.FindElement(By.Id("org.mozilla.firefox_beta:id/address_bar_bg"));
           qStr.Click();
           qStr.Click();
           qStr.SendKeys("http://putlocker.is");
           IWebElement qButton = driver.FindElement(By.Id("org.mozilla.firefox_beta:id/awesomebar_button"));
           qButton.Click();
           //driver.SwitchTo().Alert().Dismiss();
           driver.SwitchTo().ActiveElement().Click();

          //driver.Navigate().GoToUrl("http://putlocker.is");
          //Console.WriteLine(" " + driver.Url.ToString());       
       }
      public void CloseDriver()
      {
          driver.Quit();
      }
      
    }
}
