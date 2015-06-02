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
using OpenQA.Selenium.Interactions.Internal;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium.Android;

namespace AppiumTest
{

  public class AppiumDriver
    {
      public AndroidDriver driver;
      private bool _isLandChecked = true;      
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

      public void OpenHofHomePage(int WindowScore, int TimeShow)
       {        // 		Context	"WEBVIEW_1"	string
          int windowScore = WindowScore;
          int impressions = 0;
          string url1 = "http://putlocker.is/";//"http://49.ppsite.org/index.php";//"http://www13.zippyshare.com/v/94311818/file.html";
          string url2 = "http://thevideos.tv";
          ITouchAction tapScreen = new TouchAction(driver);
          string basicWindow = driver.CurrentWindowHandle;
          driver.Navigate().GoToUrl(url1);
        //  driver.FindElementByClassName("morevids").Click();
        //  url2 = driver.Url;       
         // IWebElement el = driver.FindElements(By.XPath("/html/body/iframe"));
          while (windowScore != 0)
           {
               Thread.Sleep(5000);
             //  Console.WriteLine(driver.SwitchTo().Frame(1).PageSource);
             //  return;
               if (driver.SwitchTo().Frame(1).PageSource.Contains("Cancel"))
               {
                   Thread.Sleep(1000);
                   driver.FindElementByXPath("//*[@id='B1']").Click();                  
                   Thread.Sleep(1000);
               }
               //if (driver.Url != url1)
               //{   
               //    driver.Navigate().GoToUrl(url1);
               //    tapScreen.Tap(10, 10, null);
               //}
               driver.SwitchTo().ActiveElement().Click();
               try
               {
                   driver.SwitchTo().Alert().Accept();
               }
               catch {}
               if ((driver.WindowHandles.Count) > 1 || (driver.Url != url1))
                   impressions++;

               IReadOnlyCollection<string> windows = driver.WindowHandles;
               foreach (string window in windows)
                   {
                       if (window != windows.ElementAt(0))
                       {
                           driver.SwitchTo().Window(window);
                           driver.Close();
                       }
                   }
               windowScore--;
               driver.SwitchTo().Window(windows.ElementAt(0));
               Thread.Sleep(TimeShow);
           }
         Console.WriteLine("<Impressions> " + impressions);
       }
       
    }
      
}

