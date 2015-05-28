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
         //  FileStream file = new FileStream ("C:\\pageSource.txt", FileMode.Open, FileAccess.ReadWrite);
         //  StreamWriter writer = new StreamWriter(file);
          int windowScore = 3;
          int Impressions = 0;
           driver.Navigate().GoToUrl("http://www13.zippyshare.com/v/94311818/file.html");
           string baseWindow = driver.CurrentWindowHandle;
         //Console.WriteLine(driver.WindowHandles.Count);
         //driver.SwitchTo().ActiveElement().Click();
          try
          {
           while (windowScore > 0)
           {
               driver.SwitchTo().ActiveElement().Click();
               Thread.Sleep(5000);
               if ((driver.WindowHandles.Count) > 1)
                   Impressions++;
                driver.SwitchTo().Alert().Accept(); 
               
                    
               IReadOnlyCollection<string> windows = driver.WindowHandles;
               foreach (string window in windows)
               {
                   if (baseWindow != window)
                       driver.Close();
               }
               Console.WriteLine(windowScore);
               windowScore--;
               driver.SwitchTo().Window(baseWindow);
               Thread.Sleep(45000);
           }
          }
          catch { }
        // driver.FindElementByXPath("/html/body/iframe");
         //Console.WriteLine(driver.FindElementByXPath("/html/body/iframe").Text);
         Console.WriteLine("Impressions " + Impressions);
         //Thread.Sleep(3000);
         // driver.SwitchTo().Alert().Accept();
      
       }
       
       }
      
}

