using System;
using Newtonsoft.Json.Linq;
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
      private AndroidDriver driver;
      private List<PublisherTarget> _driverSettings; // for many publishers
      private bool _isLandChecked;
      private bool _isOnClick;
      private int _countWindowClick = 0;

      public List<AndroidDriver> Drivers { get; private set; }
     // public TestRail TestRun { get; set; }
    
//Constuctor
       public AppiumDriver()
        {
			Drivers = new List<AndroidDriver>();
           _driverSettings = new List<PublisherTarget>()
                {
                //new PublisherTarget() { Url = "http://putlocker.is", ZoneId = "10802", CountShowPopup = 3, IntervalPopup = 10000, StepCase = 0},
                //new PublisherTarget() { Url = "http://thevideos.tv/", ZoneId = "90446", CountShowPopup = 3, IntervalPopup = 45000, TargetClick = "morevids", StepCase = 1},              
                //new PublisherTarget() { Url = "http://www13.zippyshare.com/v/94311818/file.html/", ZoneId = "180376", CountShowPopup = 2, IntervalPopup = 45000, StepCase = 2},
                //new PublisherTarget() { Url = "http://um-fabolous.blogspot.ru/", ZoneId = "199287", CountShowPopup = 3, IntervalPopup = 45000, StepCase = 3},                
                new PublisherTarget() {Url = "http://www.flashx.tv/&?", ZoneId = "119133", CountShowPopup = 1, IntervalPopup = 20000, StepCase = 4},              
                };
        }
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

       public void StartOnclick()
       {
           ITouchAction tapScreen = new TouchAction(driver);
           TestRail TestRun = new TestRail();
           TestRun.StartTestRail();
           List<string> CaseToRun = new List<string>();

           foreach (string runCase in TestRun.GetRunCase(driver))
               CaseToRun.Add(runCase);

           //foreach (string c in CaseToRun)
           //    Console.WriteLine(c);
           //return;
           string successMessage = "";
           string errorMessage = "";
           string retestMessage = "";
           string commentMessage = "";
           foreach (PublisherTarget driverSet in _driverSettings)
           {
               driver.Navigate().GoToUrl(driverSet.Url);

               int failedLand = 0;
               // Проверка на наш Landing
               if (driver.Url != "http://thevideos.tv/")
               {
                   if (driver.PageSource.Contains(driverSet.ZoneId))
                       _isLandChecked = true;
                   else
                       _isLandChecked = false;
               }
               else
               {
                   driver.FindElement(By.ClassName(driverSet.TargetClick)).Click();
                   Thread.Sleep(3000);
                   if (driver.PageSource.Contains(driverSet.ZoneId))
                       _isLandChecked = true;
                   else
                       _isLandChecked = false;
               }

               string baseWindow = driver.CurrentWindowHandle;

               while (driverSet.CountShowPopup != 0)
               {
                   if (driver.Url != driverSet.Url)
                   {
                       driver.Navigate().GoToUrl(driverSet.Url);
                      // tapScreen.Tap(10, 10, null);
                   }
                   if (driver.Url != "http://thevideos.tv/")
                       Thread.Sleep(2000);
                   try
                   {
                       if (driver.SwitchTo().Frame(1).PageSource.Contains("Cancel"))
                       {
                           Thread.Sleep(1000);
                           driver.FindElementByXPath("//*[@id='B1']").Click();
                           Thread.Sleep(1000);
                       }
                   }
                   catch { Console.WriteLine("Pushup been on site"); }

                   driver.SwitchTo().Window(driver.WindowHandles.ElementAt(0)).SwitchTo().ActiveElement().Click();
                   Thread.Sleep(3000);
                   //*
                   try { driver.SwitchTo().Alert().Accept(); }
                   catch { }
                   //*
                   if (_isLandChecked)
                   {
                       OnclickProgress(driver, driverSet);
                       if (!_isOnClick)
                       {
                           errorMessage = "Во время клика не отработал показ. На сайте присутствует наш Network";
                           commentMessage = "OnClick не отработал";
                           Console.Error.WriteLine(driver.SwitchTo().Window(baseWindow).Url + " OnClick is " + _isOnClick);
                           TestRun.SetStatus(CaseToRun[driverSet.StepCase], 5, errorMessage, commentMessage);
                           break;
                       }
                   }
                   else
                   {
                       errorMessage = "FailedLand: " + failedLand + "\nLanding is " + _isLandChecked;
                       commentMessage = "Landing is " + _isLandChecked;
                       Console.Error.WriteLine(driver.SwitchTo().Window(baseWindow).Url + errorMessage);
                       TestRun.SetStatus(CaseToRun[driverSet.StepCase], 5, errorMessage, commentMessage);
                       break;
                   }

               }
               // Проверка на открытие после того, как все показы уже были
               try
               {
                   driver.SwitchTo().Window(driver.WindowHandles.ElementAt(0)).SwitchTo().ActiveElement().Click();
               }
               catch { }

               _countWindowClick = driver.WindowHandles.Count;
               if (_countWindowClick == 1 && driverSet.CountShowPopup == 0)
               {
                   successMessage = driver.Url + "\nLanding is - " + _isLandChecked;
                   Console.WriteLine(successMessage + " " + _isLandChecked + " " + _isOnClick);
                   TestRun.SetStatus(CaseToRun[driverSet.StepCase], 1, successMessage, null);
               }

               else if (_isLandChecked && _isOnClick)
               {
                   retestMessage = "Landing is " + _isLandChecked + " "
                       + driver.Url + " OnClick: popups is " + driverSet.CountShowPopup +
                       " & count of windows " + _countWindowClick + "\nIn the testing process is NOT open our Landing" +
                       "\nPlease, repeat this test";
                   Console.Error.WriteLine(errorMessage + " " + _isOnClick);

                   TestRun.SetStatus(CaseToRun[driverSet.StepCase], 4, retestMessage, null);
               }

           }//end foreach
       }
private void OnclickProgress(AndroidDriver driver, PublisherTarget d_setting)
{
    if ((_countWindowClick = driver.WindowHandles.Count) > 1)
    {
        _isOnClick = true;
        Thread.Sleep(2000);
        try
        {
            foreach (string handle in driver.WindowHandles)
            {
                if (handle != driver.WindowHandles.ElementAt(0))//(driver.SwitchTo().Window(handle).Url != driver.SwitchTo().Window(baseWindow).Url)
                {
                    driver.SwitchTo().Window(handle).Close();
                    while ((_countWindowClick = driver.WindowHandles.Count) > 1)
                    {
                        driver.SwitchTo().Alert().Accept(); // если появился alert      
                    }
                }
            }
        }
        catch (Exception e) { Console.WriteLine(e); }
    }

    d_setting.CountShowPopup--;
    driver.SwitchTo().Window(driver.WindowHandles.ElementAt(0));
    // time Interval popup
    Thread.Sleep(d_setting.IntervalPopup);
}
      //public void StartOnclick(int WindowScore, int TimeShow)
      // {        // 		Context	"WEBVIEW_1"	string
      //    int windowScore = WindowScore;
      //    int impressions = 0;
      //    string url1 = "http://putlocker.is/";//"http://49.ppsite.org/index.php";//"http://www13.zippyshare.com/v/94311818/file.html";
      //    string url2 = "http://thevideos.tv";
      //    ITouchAction tapScreen = new TouchAction(driver);
      //    string basicWindow = driver.CurrentWindowHandle;
      //    driver.Navigate().GoToUrl(url1);
      //  //  driver.FindElementByClassName("morevids").Click();
      //  //  url2 = driver.Url;       
      //   // IWebElement el = driver.FindElements(By.XPath("/html/body/iframe"));
      //    while (windowScore != 0)
      //     {
      //         Thread.Sleep(5000);
      //       //  Console.WriteLine(driver.SwitchTo().Frame(1).PageSource);
      //       //  return;
      //         //try
      //         //{
      //         //    if (driver.SwitchTo().Frame(1).PageSource.Contains("Cancel"))
      //         //    {
      //         //        Thread.Sleep(1000);
      //         //        driver.FindElementByXPath("//*[@id='B1']").Click();
      //         //        Thread.Sleep(1000);
      //         //    }
      //         //}
      //         //catch { Console.WriteLine("No Such Pushup"); }
               
      //         //if (driver.Url != url1)
      //         //{   
      //         //    driver.Navigate().GoToUrl(url1);
      //         //    tapScreen.Tap(10, 10, null);
      //         //}
      //         driver.SwitchTo().ActiveElement().Click();
      //         try
      //         {
      //             driver.SwitchTo().Alert().Accept();
      //         }
      //         catch {}
      //         if ((driver.WindowHandles.Count) > 1 || (driver.Url != url1))
      //             impressions++;

      //         IReadOnlyCollection<string> windows = driver.WindowHandles;
      //         foreach (string window in windows)
      //             {
      //                 if (window != windows.ElementAt(0))
      //                 {
      //                     driver.SwitchTo().Window(window);
      //                     driver.Close();
      //                 }
      //             }
      //         windowScore--;
      //         driver.SwitchTo().Window(windows.ElementAt(0));
      //         Thread.Sleep(TimeShow);
      //     }
      //   Console.WriteLine("<Impressions> " + impressions);
      // }
       
    }
      
}

