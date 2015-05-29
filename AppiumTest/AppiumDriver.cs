using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Selenium.Internal.SeleniumEmulation;
using OpenQA.Selenium.Chrome;
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
      public AndroidDriver driver;
            
       public void Setup()
       {
//**
// Browsers 
// org.mozilla.firefox, appActivity App
// com.opera.browser, appActivity com.opera.Opera
// com.android.chrome, appActivity com.google.android.apps.chrome.Main
//**          
           DesiredCapabilities capabilites = new DesiredCapabilities();
           capabilites.SetCapability("device", "Android");          
           capabilites.SetCapability("deviceName", "Nexus 7");
           capabilites.SetCapability(CapabilityType.BrowserName, "chrome");
           capabilites.SetCapability("platformName", "Android");
           capabilites.SetCapability("platformVersion", "4.2.2");
           capabilites.SetCapability("appPackage", "com.android.chrome");
           capabilites.SetCapability("appActivity", "com.google.android.apps.chrome.Main");
           driver = new AndroidDriver(new Uri("http://127.0.0.1:4723/wd/hub"), capabilites, TimeSpan.FromSeconds(180));
       }

      public void OpenHofHomePage()
       {        
          int windowScore = 2;
          int Impressions = 0;
           driver.Navigate().GoToUrl("http://www13.zippyshare.com/v/94311818/file.html");
           while (windowScore != 0)
           {
               driver.SwitchTo().ActiveElement().Click();
               Thread.Sleep(5000);
               if ((driver.WindowHandles.Count) > 1)
                   Impressions++;
               try { driver.SwitchTo().Alert().Accept(); }
               catch { }
               IReadOnlyCollection<string> windows = driver.WindowHandles;
               foreach (string window in windows)
                   {
                       if (window == windows.ElementAt(0))
                           driver.Close();
                   }
               windowScore--;
               driver.SwitchTo().Window(windows.ElementAt(1));
               Thread.Sleep(45000);
           }
         Console.WriteLine("<Impressions> " + Impressions);
       }
       
    }
      
}

