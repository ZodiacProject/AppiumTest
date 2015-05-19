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
           string app = "c:\\Users\\Userrr\\Downloads\\WhatsApp2.12.88.apk"; //WhatsApp2.12.88.apk
           DesiredCapabilities capabilites = new DesiredCapabilities();
           capabilites.SetCapability("device", "Android");          
           capabilites.SetCapability("deviceName", "HTC One mini 2");
           capabilites.SetCapability("platformName", "Android");
           capabilites.SetCapability("platformVersion", "4.2.2");
           capabilites.SetCapability("app", Path.GetFullPath(app));        

           driver = new RemoteWebDriver(new Uri("http://127.0.0.1:4723/wd/hub"), capabilites, TimeSpan.FromSeconds(180));
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
