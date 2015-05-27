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
           capabilites.SetCapability(CapabilityType.BrowserName, "firefox");
           capabilites.SetCapability("platformName", "Android");
           capabilites.SetCapability("platformVersion", "4.2.2");
           capabilites.SetCapability("appPackage", "org.mozilla.firefox");
           capabilites.SetCapability("appActivity", "App");
           driver = new AndroidDriver(new Uri("http://127.0.0.1:4723/wd/hub"), capabilites, TimeSpan.FromSeconds(180));
       }

      public void OpenHofHomePage()
       {
         //  FileStream file = new FileStream ("C:\\pageSource.txt", FileMode.Open, FileAccess.ReadWrite);
         //  StreamWriter writer = new StreamWriter(file);
       //  driver.Navigate().GoToUrl("http://putlocker.is");
       //   driver.ResetApp();
           int onclickScore = 3;
          IWebElement urlTitle = driver.FindElement(By.Id("org.mozilla.firefox:id/url_bar_entry"));
          while (onclickScore != 0)
          {
              urlTitle.Click();
              urlTitle.SendKeys("http://putlocker.is");
              driver.KeyEvent(AndroidKeyCode.Enter);
              Thread.Sleep(5000);
              driver.FindElementById("android:id/windowContentFrame").Click();
              driver.BackgroundApp(1);
              onclickScore--;
          }
          
          //js.ExecuteScript("$('#route').click()");
         // document.getElementById("route").click();
              //driver.FindElement(By.Id("org.mozilla.firefox:id/suggestions_prompt_yes")).Click();
        
          //IWebElement elPushup = driver.FindElement(By.XPath("//a"));
       
       }
      
    }
}
