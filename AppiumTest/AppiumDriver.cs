using System;
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
           //capabilites.SetCapability("browserName", "chrome");
           capabilites.SetCapability("appPackage", "com.ansroid.chrome");
           capabilites.SetCapability("appActivity", "com.ansroid.chrome");
           capabilites.SetCapability("deviceName", "HTC One mini 2");
           capabilites.SetCapability("platformName", "Android");
           capabilites.SetCapability("platformVersion", "4.2.2");

           driver = new AndroidDriver(new Uri("http://127.0.0.1:4723/wd/hub"), capabilites);
       }

      public void OpenHofHomePage()
       {
          driver.Navigate().GoToUrl("http://putlocker.is");
          Thread.Sleep(10000);
          try
          {
              if (driver.SwitchTo().Alert().Text == "http://putlocker.is")
                  driver.SwitchTo().Alert().Dismiss();
          }
          catch (Exception e)
          {
              Console.WriteLine(e);              
          }
          
       }
      public void CloseDriver()
      {
          driver.Quit();
      }
      
    }
}
